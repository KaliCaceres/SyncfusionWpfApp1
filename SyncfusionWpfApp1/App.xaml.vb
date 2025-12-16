Imports System.IO
Imports System.Reflection
Imports System.Windows
Imports System.Windows.Threading

Imports Microsoft.Extensions.Configuration
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Hosting

Imports SyncfusionWpfApp1.Contracts.Services
Imports SyncfusionWpfApp1.Contracts.Views
Imports SyncfusionWpfApp1.Models
Imports SyncfusionWpfApp1.Services
Imports SyncfusionWpfApp1.ViewModels
Imports SyncfusionWpfApp1.Views

Namespace SyncfusionWpfApp1
   ' For more inforation about application lifecyle events see https://docs.microsoft.com/dotnet/framework/wpf/app-development/application-management-overview

    ' WPF UI elements use language en-US by default.
    ' If you need to support other cultures make sure you add converters and review dates and numbers in your UI to ensure everything adapts correctly.
    ' Tracking issue for improving this is https://github.com/dotnet/wpf/issues/1946
    Public Partial Class App
        Inherits Application

            Private _host As IHost

            Public Function GetService(Of T As Class)() As T
                Return TryCast(_host.Services.GetService(GetType(T)), T)
            End Function


        Public Sub New()
            ' Add your Syncfusion license key for WPF platform with corresponding Syncfusion NuGet version referred in project. For more information about license key see https://help.syncfusion.com/common/essential-studio/licensing/license-key.
            ' Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Add your license key here")
        End Sub

        Private Async Sub OnStartup(ByVal sender As Object, ByVal e As StartupEventArgs)
            Dim appLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)

            ' For more information about .NET generic host see https//docs.microsoft.com/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.0
            _host = Host.CreateDefaultBuilder(e.Args) _
            .ConfigureAppConfiguration(Sub(c) c.SetBasePath(appLocation)) _
            .ConfigureServices(AddressOf ConfigureServices) _
            .Build()

            Await _host.StartAsync()
        End Sub

		Private Sub ConfigureServices(ByVal context As HostBuilderContext, ByVal services As IServiceCollection)
                ' TODO WTS: Register your services, viewmodels, And pages here

                ' App Host
                services.AddHostedService(Of ApplicationHostService)()

                ' Core Services
			services.AddSingleton(Of IFileService, FileService)()



                ' Services
                services.AddSingleton(Of IThemeSelectorService, ThemeSelectorService)()
            services.AddSingleton(Of IPersistAndRestoreService, PersistAndRestoreService)()
                services.AddSingleton(Of IWindowManagerService, WindowManagerService)()

                services.AddSingleton(Of IPageService, PageService)()
                services.AddSingleton(Of INavigationService, NavigationService)()

                ' Views And ViewModels
                services.AddTransient(Of IShellWindow, ShellWindow)()
                services.AddTransient(Of ShellViewModel)()

            services.AddTransient(Of NavigationDrawerViewModel)()
            services.AddTransient(Of NavigationDrawerPage)()

                services.AddTransient(Of IShellDialogWindow, ShellDialogWindow)()
                services.AddTransient(Of ShellDialogViewModel)()

                ' Configuration
                services.Configure(Of AppConfig)(context.Configuration.GetSection(NameOf(AppConfig)))
        End Sub


        Private Async Sub OnExit(ByVal sender As Object, ByVal e As ExitEventArgs)
                Await _host.StopAsync()
                _host.Dispose()
                _host = Nothing

        End Sub

        Private Sub OnDispatcherUnhandledException(ByVal sender As Object, ByVal e As DispatcherUnhandledExceptionEventArgs)
        End Sub
    End Class
End Namespace
