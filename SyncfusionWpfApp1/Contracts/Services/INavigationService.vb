Imports System
Imports System.Windows.Controls

Namespace SyncfusionWpfApp1.Contracts.Services
    Public Interface INavigationService
        Event Navigated As EventHandler(Of String)

        ReadOnly Property CanGoBack As Boolean

        Sub Initialize(shellFrame As Frame)

        Function NavigateTo(pageKey As String, Optional parameter As Object = Nothing, Optional clearNavigation As Boolean = False) As Boolean

        Sub GoBack()

        Sub UnsubscribeNavigation()

        Sub CleanNavigation()
    End Interface
End Namespace
