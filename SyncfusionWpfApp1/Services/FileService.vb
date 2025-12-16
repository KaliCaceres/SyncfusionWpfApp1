Imports System.IO
Imports System.Text

Imports Newtonsoft.Json

Imports SyncfusionWpfApp1.Contracts.Services

Namespace SyncfusionWpfApp1.Services
    Public Class FileService
        Implements IFileService
        Public Function Read(Of T)(folderPath As String, fileName As String) As T Implements IFileService.Read
            Dim path = System.IO.Path.Combine(folderPath, fileName)
            If File.Exists(path) Then
                Dim json = File.ReadAllText(path)
                Return JsonConvert.DeserializeObject(Of T)(json)
            End If

            Return Nothing
        End Function

        Public Sub Save(Of T)(folderPath As String, fileName As String, content As T) Implements IFileService.Save
            If Not Directory.Exists(folderPath) Then
                Directory.CreateDirectory(folderPath)
            End If

            Dim fileContent = JsonConvert.SerializeObject(content)
            File.WriteAllText(Path.Combine(folderPath, fileName), fileContent, Encoding.UTF8)
        End Sub

        Public Sub Delete(folderPath As String, fileName As String) Implements IFileService.Delete
            If Not Equals(fileName, Nothing) AndAlso File.Exists(Path.Combine(folderPath, fileName)) Then
                File.Delete(Path.Combine(folderPath, fileName))
            End If
        End Sub
    End Class
End Namespace
