Namespace SyncfusionWpfApp1.Contracts.Services
    Public Interface IFileService
        Function Read(Of T)(folderPath As String, fileName As String) As T

        Sub Save(Of T)(folderPath As String, fileName As String, content As T)

        Sub Delete(folderPath As String, fileName As String)
    End Interface
End Namespace
