Imports System
Imports System.Windows.Input

Namespace SyncfusionWpfApp1.Helpers
    Public Class RelayCommand
        Implements ICommand
        Private ReadOnly _execute As Action

        Private ReadOnly _canExecute As Func(Of Boolean)

        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        Public Sub New(execute As Action)
            Me.New(execute, Nothing)
        End Sub

        Public Sub New(execute As Action, canExecute As Func(Of Boolean))
            _execute = If(execute, CSharpImpl.__Throw(Of Action)(New ArgumentNullException(NameOf(execute))))
            _canExecute = canExecute
        End Sub

        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            Return _canExecute Is Nothing OrElse _canExecute()
        End Function

        Public Sub Execute(parameter As Object) Implements ICommand.Execute
            _execute()
        End Sub

        Public Sub OnCanExecuteChanged()
            RaiseEvent CanExecuteChanged(Me, EventArgs.Empty)
        End Sub

        Private Class CSharpImpl
            <Obsolete("Please refactor calling code to use normal throw statements")>
            Shared Function __Throw(Of T)(ByVal e As Exception) As T
                Throw e
            End Function
        End Class
    End Class

    Public Class RelayCommandType(Of T)
        Implements ICommand
        Private ReadOnly _execute As Action(Of T)

        Private ReadOnly _canExecute As Func(Of T, Boolean)

        Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged

        Public Sub New(execute As Action(Of T))
            Me.New(execute, Nothing)
        End Sub

        Public Sub New(execute As Action(Of T), canExecute As Func(Of T, Boolean))
            _execute = If(execute, CSharpImpl.__Throw(Of Action(Of T))(New ArgumentNullException(NameOf(execute))))
            _canExecute = canExecute
        End Sub

        Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
            Return _canExecute Is Nothing OrElse _canExecute(parameter)
        End Function

        Public Sub Execute(parameter As Object) Implements ICommand.Execute
            _execute(parameter)
        End Sub

        Public Sub OnCanExecuteChanged()
            RaiseEvent CanExecuteChanged(Me, EventArgs.Empty)
        End Sub

        Private Class CSharpImpl
            <Obsolete("Please refactor calling code to use normal throw statements")>
            Shared Function __Throw(Of T)(ByVal e As Exception) As T
                Throw e
            End Function
        End Class

    End Class
End Namespace
