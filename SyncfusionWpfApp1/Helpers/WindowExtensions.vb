Imports System.Runtime.CompilerServices
Imports System.Windows.Controls

Namespace System.Windows
    Public Module WindowExtensions
        <Extension()>
        Public Function GetDataContext(ByVal window As Window) As Object
            Dim frame As Frame = Nothing

            If CSharpImpl.__Assign(frame, TryCast(window.Content, Frame)) IsNot Nothing Then
                Return frame.GetDataContext()
            End If

            Return Nothing
        End Function

        Private Class CSharpImpl
            <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
    End Module
End Namespace

