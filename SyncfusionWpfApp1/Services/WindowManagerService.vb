Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Navigation

Imports Syncfusion.Windows.Shared

Imports SyncfusionWpfApp1 .Contracts.Services
Imports SyncfusionWpfApp1 .Contracts.ViewModels
Imports SyncfusionWpfApp1 .Contracts.Views

Namespace SyncfusionWpfApp1 .Services
    Public Class WindowManagerService
        Implements IWindowManagerService
        Private ReadOnly _serviceProvider As IServiceProvider
        Private ReadOnly _pageService As IPageService

        Public ReadOnly Property MainWindow As Window Implements IWindowManagerService.MainWindow
            Get
                Return Application.Current.MainWindow
            End Get
        End Property

        Public Sub New(serviceProvider As IServiceProvider, pageService As IPageService)
            _serviceProvider = serviceProvider
            _pageService = pageService
        End Sub

        Public Sub OpenInNewWindow(key As String, Optional parameter As Object = Nothing) Implements IWindowManagerService.OpenInNewWindow
            Dim window = GetWindow(key)
            If window IsNot Nothing Then
                window.Activate()
            Else
                window = New ChromelessWindow() With {
    .Title = "SyncfusionWpfApp1 ",
    .Style = TryCast(Application.Current.FindResource("CustomMetroWindow"), Style)
}
                Dim frame = New Frame() With {
    .Focusable = False,
    .NavigationUIVisibility = NavigationUIVisibility.Hidden
}

                window.Content = frame
                Dim page = _pageService.GetPage(key)
                AddHandler window.Closed, AddressOf OnWindowClosed
                window.Show()
                AddHandler frame.Navigated, AddressOf OnNavigated
                Dim navigated = frame.Navigate(page, parameter)
            End If
        End Sub

        Public Function OpenInDialog(key As String, Optional parameter As Object = Nothing) As Boolean? Implements IWindowManagerService.OpenInDialog
            Dim shellWindow = TryCast(_serviceProvider.GetService(GetType(IShellDialogWindow)), Window)
            Dim frame = CType(shellWindow, IShellDialogWindow).GetDialogFrame()
            AddHandler frame.Navigated, AddressOf OnNavigated
            AddHandler shellWindow.Closed, AddressOf OnWindowClosed
            Dim page = _pageService.GetPage(key)
            Dim navigated = frame.Navigate(page, parameter)
            Return shellWindow.ShowDialog()
        End Function

        Public Function GetWindow(key As String) As Window Implements IWindowManagerService.GetWindow
            For Each window As Window In Application.Current.Windows
                Dim dataContext = window.GetDataContext()
                If Equals(dataContext?.GetType().FullName, key) Then
                    Return window
                End If
            Next

            Return Nothing
        End Function

        Private Sub OnNavigated(sender As Object, e As NavigationEventArgs)
            Dim frame As Frame = Nothing, navigationAware As INavigationAware = Nothing

            If CSharpImpl.__Assign(frame, TryCast(sender, Frame)) IsNot Nothing Then
                Dim dataContext = frame.GetDataContext()

                If CSharpImpl.__Assign(navigationAware, TryCast(dataContext, INavigationAware)) IsNot Nothing Then
                    navigationAware.OnNavigatedTo(e.ExtraData)
                End If
            End If
        End Sub

        Private Sub OnWindowClosed(sender As Object, e As EventArgs)
            Dim window As Window = Nothing, frame As Frame = Nothing

            If CSharpImpl.__Assign(window, TryCast(sender, Window)) IsNot Nothing Then
                If CSharpImpl.__Assign(frame, TryCast(window.Content, Frame)) IsNot Nothing Then
                    RemoveHandler frame.Navigated, AddressOf OnNavigated
                End If

                RemoveHandler window.Closed, AddressOf OnWindowClosed
            End If
        End Sub

        Private Class CSharpImpl
            <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
    End Class
End Namespace
