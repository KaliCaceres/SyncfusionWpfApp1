Imports System.Runtime.CompilerServices

Namespace System.Windows.Controls
    Public Module FrameExtensions
        <Extension()>
        Public Function GetDataContext(ByVal frame As Frame) As Object
            Dim element As FrameworkElement = Nothing

            If CSharpImpl.__Assign(element, TryCast(frame.Content, FrameworkElement)) IsNot Nothing Then
                Return element.DataContext
            End If

            Return Nothing
        End Function

        <Extension()>
        Public Sub CleanNavigation(ByVal frame As Frame)
            While frame.CanGoBack
                frame.RemoveBackEntry()
            End While
        End Sub

        Private Class CSharpImpl
            <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
    End Module
End Namespace
