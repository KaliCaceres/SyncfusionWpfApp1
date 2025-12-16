Imports System
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Shapes

Imports Syncfusion.Windows.Shared

Imports SyncfusionWpfApp1.Contracts.Services
Imports SyncfusionWpfApp1.Helpers
Imports SyncfusionWpfApp1.Properties

Namespace SyncfusionWpfApp1.ViewModels
    Public Class ShellViewModel
        Inherits Observable
        Private ReadOnly _navigationService As INavigationService
        Private _selectedMenuItem As Object
        Private _goBackCommand As RelayCommand
        Private _menuItemInvokedCommand As ICommand
        Private _loadedCommand As ICommand
        Private _unloadedCommand As ICommand

        Public Property SelectedMenuItem As Object
            Get
                Return _selectedMenuItem
            End Get
            Set(value As Object)
                If TryCast(value, NavigationPaneItem) Is Nothing Then
                    [Set](_selectedMenuItem, CType(value, FrameworkElement).DataContext, "SelectedMenuItem")
                Else
                    [Set](_selectedMenuItem, value, "SelectedMenuItem")
                End If
                'NavigateTo((_selectedMenuItem as NavigationPaneItem).TargetType);
                Dim navigationPaneItem As NavigationPaneItem = Nothing

                If CSharpImpl.__Assign(navigationPaneItem, TryCast(_selectedMenuItem, NavigationPaneItem)) IsNot Nothing AndAlso navigationPaneItem.TargetType IsNot Nothing Then
                    NavigateTo(navigationPaneItem.TargetType)
                End If
            End Set
        End Property

        Public Sub UpdateFillColor(FillColor As SolidColorBrush)
            For Each item In MenuItems
                TryCast(item, NavigationPaneItem).Path.Fill = FillColor
            Next
            SetttingsIconColor = FillColor
        End Sub

        Private setttingsIconColorField As SolidColorBrush

        Public Property SetttingsIconColor As SolidColorBrush
            Get
                Return setttingsIconColorField
            End Get
            Set(value As SolidColorBrush)
                setttingsIconColorField = value
                OnPropertyChanged(NameOf(ShellViewModel.SetttingsIconColor))
            End Set
        End Property

        ' TODO WTS: Change the icons and titles for all HamburgerMenuItems here.
        Public Property MenuItems As ObservableCollection(Of NavigationPaneItem) = New ObservableCollection(Of NavigationPaneItem)() From {
    New NavigationPaneItem() With {
                .Label = Resources.ShellNavigationDrawerPage,
                                        .Path = New Path() With {
                    .Width = 15,
                    .Height = 15,
                    .HorizontalAlignment = HorizontalAlignment.Center,
                    .VerticalAlignment = VerticalAlignment.Center,
                    .Data = Geometry.Parse("M6 2C3.79086 2 2 3.79086 2 6V34C2 36.2091 3.79086 38 6 38H22V2H6ZM24 2V38H42C44.2091 38 46 36.2091 46 34V6C46 3.79086 44.2091 2 42 2H24ZM0 6C0 2.68629 2.68629 0 6 0H23H42C45.3137 0 48 2.68629 48 6V34C48 37.3137 45.3137 40 42 40H23H6C2.68629 40 0 37.3137 0 34V6ZM6 8C6 7.44772 6.44772 7 7 7H18C18.5523 7 19 7.44772 19 8C19 8.55229 18.5523 9 18 9H7C6.44772 9 6 8.55229 6 8ZM7 15C6.44772 15 6 15.4477 6 16C6 16.5523 6.44772 17 7 17H18C18.5523 17 19 16.5523 19 16C19 15.4477 18.5523 15 18 15H7ZM6 24C6 23.4477 6.44772 23 7 23H18C18.5523 23 19 23.4477 19 24C19 24.5523 18.5523 25 18 25H7C6.44772 25 6 24.5523 6 24ZM7 31C6.44772 31 6 31.4477 6 32C6 32.5523 6.44772 33 7 33H18C18.5523 33 19 32.5523 19 32C19 31.4477 18.5523 31 18 31H7Z"),
                    .Fill = New SolidColorBrush(Colors.Black),
                    .Stretch = Stretch.Fill
                },
                .TargetType = GetType(NavigationDrawerViewModel)
    }
}

        Public ReadOnly Property GoBackCommand As RelayCommand
            Get
                Return If(_goBackCommand, Function()
                                              _goBackCommand = New RelayCommand(AddressOf OnGoBack, AddressOf CanGoBack)
                                              Return _goBackCommand
                                          End Function())
            End Get
        End Property

        Public ReadOnly Property LoadedCommand As ICommand
            Get
                Return If(_loadedCommand, Function()
                                              _loadedCommand = New RelayCommand(AddressOf OnLoaded)
                                              Return _loadedCommand
                                          End Function())
            End Get
        End Property

        Public ReadOnly Property UnloadedCommand As ICommand
            Get
                Return If(_unloadedCommand, Function()
                                                _unloadedCommand = New RelayCommand(AddressOf OnUnloaded)
                                                Return _unloadedCommand
                                            End Function())
            End Get
        End Property

        Public Sub New(navigationService As INavigationService)
            _navigationService = navigationService
            SetttingsIconColor = New SolidColorBrush(Colors.Black)
        End Sub

        Private Sub OnLoaded()
            AddHandler _navigationService.Navigated, AddressOf OnNavigated
        End Sub

        Private Sub OnUnloaded()
            RemoveHandler _navigationService.Navigated, AddressOf OnNavigated
        End Sub

        Private Function CanGoBack() As Boolean
            Return _navigationService.CanGoBack
        End Function

        Private Sub OnGoBack()
            _navigationService.GoBack()
        End Sub

        Private Sub NavigateTo(targetViewModel As Type)
            If targetViewModel IsNot Nothing Then
                _navigationService.NavigateTo(targetViewModel.FullName)
            End If
        End Sub

        Private Sub OnNavigated(sender As Object, viewModelName As String)
            Dim item = MenuItems.OfType(Of NavigationPaneItem)().FirstOrDefault(Function(i) Equals(viewModelName, i.TargetType?.FullName))
            If item IsNot Nothing Then
                SelectedMenuItem = item
            End If

            GoBackCommand.OnCanExecuteChanged()
        End Sub

        Private Class CSharpImpl
            <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
    End Class

    Public Class NavigationPaneItem
        Public Property Label As String
        Public Property Path As Path
        Public Property TargetType As Type

    End Class
End Namespace
