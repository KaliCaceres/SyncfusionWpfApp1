Imports System
Imports System.Windows.Controls

Namespace SyncfusionWpfApp1.Contracts.Services
    Public Interface IPageService
        Function GetPageType(key As String) As Type

        Function GetPage(key As String) As Page
    End Interface
End Namespace
