Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Controls

Imports SyncfusionWpfApp1.Contracts.Services
Imports SyncfusionWpfApp1.Helpers
Imports SyncfusionWpfApp1.ViewModels
Imports SyncfusionWpfApp1.Views

Namespace SyncfusionWpfApp1.Services
    Public Class PageService
        Implements IPageService
        Private ReadOnly _pages As Dictionary(Of String, Type) = New Dictionary(Of String, Type)()
        Private ReadOnly _serviceProvider As IServiceProvider

        Public Sub New(serviceProvider As IServiceProvider)
            _serviceProvider = serviceProvider
    Call Configure(Of NavigationDrawerViewModel, NavigationDrawerPage)()
        End Sub

        Public Function GetPageType(key As String) As Type Implements IPageService.GetPageType
            Dim pageType As Type
            SyncLock _pages
                If Not _pages.TryGetValue(key, pageType) Then
                    Throw New ArgumentException($"Page not found: {key}. Did you forget to call PageService.Configure?")
                End If
            End SyncLock

            Return pageType
        End Function

        Public Function GetPage(key As String) As Page Implements IPageService.GetPage
            Dim pageType = GetPageType(key)
            Return TryCast(_serviceProvider.GetService(pageType), Page)
        End Function

        Private Sub Configure(Of VM As Observable, V As Page)()
            SyncLock _pages
                Dim key = GetType(VM).FullName
                If _pages.ContainsKey(key) Then
                    Throw New ArgumentException($"The key {key} is already configured in PageService")
                End If

                Dim type = GetType(V)
                If _pages.Any(Function(p) p.Value Is type) Then
                    Throw New ArgumentException($"This type is already configured with key {Enumerable.First(Of KeyValuePair(Of Global.System.[String], Global.System.Type))(_pages, CType(Function(p) CBool(p.Value Is type), Func(Of KeyValuePair(Of String, Type), Boolean))).Key}")
                End If

                _pages.Add(key, type)
            End SyncLock
        End Sub
    End Class
End Namespace
