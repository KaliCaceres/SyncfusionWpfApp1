Imports System

Imports SyncfusionWpfApp1.Models

Namespace SyncfusionWpfApp1.Contracts.Services
    Public Interface IThemeSelectorService
        Function SetTheme(Optional theme As AppTheme? = Nothing) As Boolean

        Function GetCurrentTheme() As AppTheme
    End Interface
End Namespace

