Imports System
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls

Imports Microsoft.Win32

Imports Syncfusion.SfSkinManager

Imports SyncfusionWpfApp1.Contracts.Services
Imports SyncfusionWpfApp1.Models
Imports SyncfusionWpfApp1.Views

Namespace SyncfusionWpfApp1.Services
    Public Class ThemeSelectorService
        Implements IThemeSelectorService
        Private ReadOnly Property IsHighContrastActive As Boolean
            Get
                Return SystemParameters.HighContrast
            End Get
        End Property

        Public Sub New()
            AddHandler SystemEvents.UserPreferenceChanging, AddressOf OnUserPreferenceChanging
        End Sub

        Public Function SetTheme(Optional theme As AppTheme? = Nothing) As Boolean Implements IThemeSelectorService.SetTheme
            ' TODO WTS: Set high contrast theme
            ' You can add custom themes following the docs on https://mahapps.com/docs/themes/thememanager
            If IsHighContrastActive Then
            ElseIf theme Is Nothing Then
                If Application.Current.Properties.Contains("Theme") Then
                    ' Read saved theme from properties
                    Dim themeName = Application.Current.Properties("Theme").ToString()
                    theme = CType([Enum].Parse(GetType(AppTheme), themeName), AppTheme)
                Else
                    ' Set default theme
                    theme = AppTheme.Windows11Light
                End If
            End If

            Dim themeNames As String = theme.Value.ToString()
            Dim productDemosWindow = Application.Current.Windows.OfType(Of ShellWindow)()
            For Each window In productDemosWindow
                Call SfSkinManager.SetTheme(window, New Theme(theme.ToString()))
            Next
            Application.Current.Properties("Theme") = theme.ToString()

            Return True
        End Function

        Public Function GetCurrentTheme() As AppTheme Implements IThemeSelectorService.GetCurrentTheme
            Dim themeName = Application.Current.Properties("Theme")?.ToString()
            If Equals(themeName, Nothing) Then
                themeName = "Windows11Light"
            End If
            Dim theme As AppTheme = Nothing
            [Enum].TryParse(themeName, theme)
            Return theme
        End Function

        Private Sub OnUserPreferenceChanging(sender As Object, e As UserPreferenceChangingEventArgs)
            If e.Category = UserPreferenceCategory.Color OrElse e.Category = UserPreferenceCategory.VisualStyle Then
                SetTheme()
            End If
        End Sub
    End Class
End Namespace
