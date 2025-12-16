Imports System
Imports System.Windows.Controls
Imports Syncfusion.SfSkinManager
Imports Syncfusion.Windows.Tools.Controls
Imports SyncfusionWpfApp1.ViewModels
Namespace SyncfusionWpfApp1.Views
    Partial Public Class NavigationDrawerPage
        Inherits Page
        Public themeName As String = If(Not Equals(Windows.Application.Current.Properties("Theme")?.ToString(), Nothing), Windows.Application.Current.Properties("Theme")?.ToString(), "Windows11Light")
        Public Sub New(viewModel  As NavigationDrawerViewModel)
            Me.InitializeComponent()
            DataContext = viewModel
            Call SfSkinManager.SetTheme(Me, New Theme(themeName))
        End Sub
    End Class
End Namespace
