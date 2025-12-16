Imports System
Imports System.Collections
Imports System.IO

Imports Microsoft.Extensions.Options

Imports SyncfusionWpfApp1.Contracts.Services
Imports SyncfusionWpfApp1.Models

Namespace SyncfusionWpfApp1.Services
    Public Class PersistAndRestoreService
        Implements IPersistAndRestoreService
        Private ReadOnly _fileService As IFileService
        Private ReadOnly _appConfig As AppConfig
        Private ReadOnly _localAppData As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)

        Public Sub New(fileService As IFileService, appConfig As IOptions(Of AppConfig))
            _fileService = fileService
            _appConfig = appConfig.Value
        End Sub

        Public Sub PersistData() Implements IPersistAndRestoreService.PersistData
            If Windows.Application.Current.Properties IsNot Nothing Then
                Dim folderPath = Path.Combine(_localAppData, _appConfig.ConfigurationsFolder)
                Dim fileName = _appConfig.AppPropertiesFileName
                _fileService.Save(folderPath, fileName, Windows.Application.Current.Properties)
            End If
        End Sub

        Public Sub RestoreData() Implements IPersistAndRestoreService.RestoreData
            Dim folderPath = Path.Combine(_localAppData, _appConfig.ConfigurationsFolder)
            Dim fileName = _appConfig.AppPropertiesFileName
            Dim properties = _fileService.Read(Of IDictionary)(folderPath, fileName)
            If properties IsNot Nothing Then
                For Each [property] As DictionaryEntry In properties
                    Windows.Application.Current.Properties.Add([property].Key, [property].Value)
                Next
            End If
        End Sub
    End Class
End Namespace
