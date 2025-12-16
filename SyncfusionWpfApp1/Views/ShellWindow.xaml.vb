Imports System.Collections.ObjectModel
Imports System.Windows.Controls
Imports System.Windows.Media

Imports Syncfusion.SfSkinManager
Imports Syncfusion.UI.Xaml.NavigationDrawer
Imports Syncfusion.Windows.Shared

Imports SyncfusionWpfApp1.Contracts.Views
Imports SyncfusionWpfApp1.ViewModels

Namespace SyncfusionWpfApp1.Views
    Public Partial Class ShellWindow
        Inherits ChromelessWindow
        Implements IShellWindow
        Public themeName As String = System.Windows.Application.Current.Properties("Theme")?.ToString()
        Public _ShellViewModel As ShellViewModel

        Public Sub New( ByVal viewModel As ShellViewModel )
            InitializeComponent()
            If navigationDrawer.SelectedItem IsNot Nothing Then
				navigationDrawer.SelectedItem = viewModel.MenuItems(0)
			End If
           DataContext = viewModel
            _ShellViewModel = viewModel

            themeName = If(Equals(themeName, Nothing), "Windows11Light", themeName)
            Call SfSkinManager.SetTheme(Me, New Syncfusion.SfSkinManager.Theme(themeName))
            If TypeOf Me Is ShellWindow Then
                If TryCast(Me, ShellWindow) IsNot Nothing AndAlso (TypeOf TryCast(Me, ShellWindow).Content Is SfNavigationDrawer) AndAlso TryCast(TryCast(Me, ShellWindow).Content, SfNavigationDrawer) IsNot Nothing AndAlso TryCast((TryCast(TryCast(Me, ShellWindow).Content, SfNavigationDrawer).ContentView), Frame) IsNot Nothing Then

                    Call SfSkinManager.SetTheme(TryCast((TryCast(TryCast(Me, ShellWindow).Content, SfNavigationDrawer).ContentView), Frame), New Syncfusion.SfSkinManager.Theme(themeName))
                    Call SfSkinManager.SetTheme(TryCast(TryCast(Me, ShellWindow).Content, SfNavigationDrawer), New Syncfusion.SfSkinManager.Theme(themeName))
                End If
            End If
			If themeName = "MaterialDark" OrElse themeName = "Office2019HighContrast" OrElse themeName = "MaterialDarkBlue" OrElse themeName = "Office2019Black" OrElse themeName = "Windows11Dark" Then
                _ShellViewModel.UpdateFillColor(New SolidColorBrush(Colors.White))
            Else
                _ShellViewModel.UpdateFillColor(New SolidColorBrush(Colors.Black))
            End If
        End Sub

        Public Function GetNavigationFrame() As Frame Implements IShellWindow.GetNavigationFrame
            Return Me.shellFrame
        End Function

        Public Sub ShowWindow() Implements IShellWindow.ShowWindow
            Show()
        End Sub

        Public Sub CloseWindow() Implements IShellWindow.CloseWindow
            Close()
        End Sub
    End Class
    Public Class MyObservableCollection
        Inherits ObservableCollection(Of Object)
    End Class
End Namespace
