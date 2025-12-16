Imports System
Imports System.Windows.Controls
Imports System.Windows.Navigation

Imports SyncfusionWpfApp1.Contracts.Services
Imports SyncfusionWpfApp1.Contracts.ViewModels

Namespace SyncfusionWpfApp1.Services
    Public Class NavigationService
        Implements INavigationService
        Private ReadOnly _pageService As IPageService
        Private _frame As Frame
        Private _lastParameterUsed As Object

        Public Event Navigated As EventHandler(Of String) Implements INavigationService.Navigated

        Public ReadOnly Property CanGoBack As Boolean Implements INavigationService.CanGoBack
            Get
                Return _frame.CanGoBack
            End Get
        End Property

        Public Sub New(pageService As IPageService)
            _pageService = pageService
        End Sub

        Public Sub Initialize(shellFrame As Frame) Implements INavigationService.Initialize
            If _frame Is Nothing Then
                _frame = shellFrame
                AddHandler _frame.Navigated, AddressOf OnNavigated
            End If
        End Sub

        Public Sub UnsubscribeNavigation() Implements INavigationService.UnsubscribeNavigation
            RemoveHandler _frame.Navigated, AddressOf OnNavigated
            _frame = Nothing
        End Sub

        Public Sub GoBack() Implements INavigationService.GoBack
            _frame.GoBack()
        End Sub

        Public Function NavigateTo(pageKey As String, Optional parameter As Object = Nothing, Optional clearNavigation As Boolean = False) As Boolean Implements INavigationService.NavigateTo
            Dim pageType = _pageService.GetPageType(pageKey)
            Dim navigationAware As INavigationAware = Nothing

            If _frame.Content?.GetType() IsNot pageType OrElse parameter IsNot Nothing AndAlso Not parameter.Equals(_lastParameterUsed) Then
                _frame.Tag = clearNavigation
                Dim page = _pageService.GetPage(pageKey)
                Dim navigated = _frame.Navigate(page, parameter)
                If navigated Then
                    _lastParameterUsed = parameter
                    Dim dataContext = _frame.GetDataContext()

                    If CSharpImpl.__Assign(navigationAware, TryCast(dataContext, INavigationAware)) IsNot Nothing Then
                        navigationAware.OnNavigatedFrom()
                    End If
                End If

                Return navigated
            End If

            Return False
        End Function

        Public Sub CleanNavigation() Implements INavigationService.CleanNavigation
            _frame.CleanNavigation()
        End Sub

        Private Sub OnNavigated(sender As Object, e As NavigationEventArgs)
            Dim frame As Frame = Nothing, navigationAware As INavigationAware = Nothing

            If CSharpImpl.__Assign(frame, TryCast(sender, Frame)) IsNot Nothing Then
                Dim clearNavigation As Boolean = frame.Tag
                If clearNavigation Then
                    frame.CleanNavigation()
                End If

                Dim dataContext = frame.GetDataContext()

                If CSharpImpl.__Assign(navigationAware, TryCast(dataContext, INavigationAware)) IsNot Nothing Then
                    navigationAware.OnNavigatedTo(e.ExtraData)
                End If

                RaiseEvent Navigated(sender, dataContext.GetType().FullName)
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
