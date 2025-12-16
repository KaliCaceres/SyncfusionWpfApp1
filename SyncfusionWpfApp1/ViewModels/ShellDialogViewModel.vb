Imports System
Imports System.Windows.Input

Imports SyncfusionWpfApp1.Helpers

Namespace SyncfusionWpfApp1.ViewModels
    Public Class ShellDialogViewModel
        Inherits Observable
        Private _closeCommand As ICommand

        Public ReadOnly Property CloseCommand As ICommand
            Get
                Return If(_closeCommand, Function()
                                             _closeCommand = New RelayCommand(AddressOf OnClose)
                                             Return _closeCommand
                                         End Function())
            End Get
        End Property

        Public Sub New()
        End Sub

        Public Property SetResult As Action(Of Boolean?)

        Private Sub OnClose()
            Dim result As Boolean = True
            If SetResult IsNot Nothing Then
                SetResult.Invoke(result)
            End If
        End Sub


        Private Class CSharpImpl
            <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class


    End Class
End Namespace
