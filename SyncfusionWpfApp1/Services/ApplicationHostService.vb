Imports System
Imports System.Linq
Imports System.Threading
Imports System.Threading.Tasks

Imports Microsoft.Extensions.Hosting

Imports SyncfusionWpfApp1.Contracts.Services
Imports SyncfusionWpfApp1.Contracts.Views
Imports SyncfusionWpfApp1.Models
Imports SyncfusionWpfApp1.ViewModels

Namespace SyncfusionWpfApp1.Services
    Public Class ApplicationHostService
        Implements IHostedService
        Private ReadOnly _serviceProvider As IServiceProvider
        Private ReadOnly _navigationService As INavigationService
        Private ReadOnly _themeSelectorService As IThemeSelectorService

        Private ReadOnly _persistAndRestoreService As IPersistAndRestoreService
        Private _shellWindow As IShellWindow

        Public Sub New(serviceProvider As IServiceProvider, navigationService As INavigationService, persistAndRestoreService As IPersistAndRestoreService, themeSelectorService As IThemeSelectorService)
            _serviceProvider = serviceProvider
            _navigationService = navigationService
            _persistAndRestoreService = persistAndRestoreService
            _themeSelectorService = themeSelectorService
        End Sub

        Public Async Function StartAsync(cancellationToken As CancellationToken) As Task Implements IHostedService.StartAsync
            ' Initialize services that you need before app activation
            Await InitializeAsync()

            Await HandleActivationAsync()

            ' Tasks after activation
            Await StartupAsync()
        End Function

        Public Async Function StopAsync(cancellationToken As CancellationToken) As Task Implements IHostedService.StopAsync
            _persistAndRestoreService.PersistData()
            Await Task.CompletedTask
        End Function

        Private Async Function InitializeAsync() As Task
            _persistAndRestoreService.RestoreData()
            Dim currentTheme = _themeSelectorService.GetCurrentTheme()
            Dim theme As AppTheme = If(currentTheme.Equals(New AppTheme()), AppTheme.Office2019White, currentTheme)
            _themeSelectorService.SetTheme(theme)
            Await Task.CompletedTask
        End Function

        Private Async Function StartupAsync() As Task
            Await Task.CompletedTask
        End Function

        Private Async Function HandleActivationAsync() As Task
            If Windows.Application.Current.Windows.OfType(Of IShellWindow)().Count() = 0 Then
                ' Default activation that navigates to the apps default page
                _shellWindow = TryCast(_serviceProvider.GetService(GetType(IShellWindow)), IShellWindow)
                _navigationService.Initialize(_shellWindow.GetNavigationFrame())
                _shellWindow.ShowWindow()
                _navigationService.NavigateTo(GetType(NavigationDrawerViewModel).FullName)
                Await Task.CompletedTask
            End If
        End Function
    End Class
End Namespace
