Imports System.Windows.Controls

Imports Syncfusion.Windows.Shared

Imports SyncfusionWpfApp1 .Contracts.Views
Imports SyncfusionWpfApp1 .ViewModels

Namespace SyncfusionWpfApp1 .Views
    Public Partial Class ShellDialogWindow
        Inherits ChromelessWindow
        Implements IShellDialogWindow
        Public Sub New(viewModel As ShellDialogViewModel)
            Me.InitializeComponent()
            viewModel.SetResult = AddressOf OnSetResult
            DataContext = viewModel
        End Sub

        Public Function GetDialogFrame() As Frame Implements IShellDialogWindow.GetDialogFrame
            Return Me.dialogFrame
        End Function

        Private Sub OnSetResult(result As Boolean?)
            DialogResult = result
            Close()
        End Sub
    End Class
End Namespace
