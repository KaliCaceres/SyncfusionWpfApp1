Imports System.Collections.Generic
Imports System.Windows.Media

Namespace SyncfusionWpfApp1.Models
    Public Class ThemeList
        Public Shared Function GetThemeList() As List(Of String)
            Dim themeList As List(Of String) = New List(Of String)() From {
    "Windows11 Light",
    "Windows11 Dark",
    "Material Light",
    "Material Dark",
    "Material Light Blue",
    "Material Dark Blue",
    "Office 2019 Colorful",
    "Office 2019 Black",
    "Office 2019 White",
    "Office 2019 Dark Gray",
    "Office 2019 High Contrast"
}
            Return themeList
        End Function
    End Class

    Public Class Palette
        Private nameField As String
        ''' <summary>
        ''' Denotes the palette name
        ''' </summary>
        Public Property Name As String
            Get
                Return nameField
            End Get
            Set(value As String)
                nameField = value
            End Set
        End Property

        Private themeField As String
        ''' <summary>
        ''' Denotes the Theme Name
        ''' </summary>
        Public Property Theme As String
            Get
                Return themeField
            End Get
            Set(value As String)
                themeField = value
            End Set
        End Property

        Private primaryBackgroundField As Brush
        ''' <summary>
        ''' Denotes the palette primary background brush
        ''' </summary>
        Public Property PrimaryBackground As Brush
            Get
                Return primaryBackgroundField
            End Get
            Set(value As Brush)
                primaryBackgroundField = value
            End Set
        End Property

        Private primaryForegroundField As Brush
        ''' <summary>
        ''' Denotes the palette primary foreground brush
        ''' </summary>
        Public Property PrimaryForeground As Brush
            Get
                Return primaryForegroundField
            End Get
            Set(value As Brush)
                primaryForegroundField = value
            End Set
        End Property

        Private primaryBackgroundAltField As Brush
        ''' <summary>
        ''' Denotes the palette primay alternate background brush
        ''' </summary>
        Public Property PrimaryBackgroundAlt As Brush
            Get
                Return primaryBackgroundAltField
            End Get
            Set(value As Brush)
                primaryBackgroundAltField = value
            End Set
        End Property

        Private displayNameField As String
        ''' <summary>
        ''' denotes the name to be displayed in the UI
        ''' </summary>
        Public Property DisplayName As String
            Get
                Return displayNameField
            End Get
            Set(value As String)
                displayNameField = value
            End Set
        End Property

        Private primaryBorderColorField As Brush
        ''' <summary>
        ''' denotes the name to be displayed in the UI
        ''' </summary>
        Public Property PrimaryBorderColor As Brush
            Get
                Return primaryBorderColorField
            End Get
            Set(value As Brush)
                primaryBorderColorField = value
            End Set
        End Property
    End Class
End Namespace
