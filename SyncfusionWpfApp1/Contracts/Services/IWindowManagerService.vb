Imports System.Windows

Namespace SyncfusionWpfApp1 .Contracts.Services
    Public Interface IWindowManagerService
        ReadOnly Property MainWindow As Window

        Sub OpenInNewWindow(pageKey As String, Optional parameter As Object = Nothing)

        Function OpenInDialog(pageKey As String, Optional parameter As Object = Nothing) As Boolean?

        Function GetWindow(pageKey As String) As Window
    End Interface
End Namespace
