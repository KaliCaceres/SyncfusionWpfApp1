Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Imports Syncfusion.Windows.Shared

Namespace SyncfusionWpfApp1.Helpers
    Public Class Observable
        Inherits NotificationObject
        Implements INotifyPropertyChanged
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Protected Sub [Set] (Of T)(ByRef storage As T, value As T,
<CallerMemberName> Optional propertyName As String = Nothing)
            If Equals(storage, value) Then
                Return
            End If

            storage = value
            OnPropertyChanged(propertyName)
        End Sub

        Protected Sub OnPropertyChanged(propertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
    End Class
End Namespace
