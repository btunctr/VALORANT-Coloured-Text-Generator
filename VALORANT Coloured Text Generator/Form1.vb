Imports System.Globalization

Public Class Form1
    Enum TextColours
        Red
        Blue
        Yellow
        Green
        Pink
        EndStatement
    End Enum
    Enum LanguageType
        Tr
        Eng
    End Enum

    Private Function GetHTMLColorCodeFromType(ByVal colorType As TextColours) As String
        Select Case colorType
            Case TextColours.Red
                Return "enemy"
            Case TextColours.Blue
                Return "team"
            Case TextColours.Yellow
                Return "system"
            Case TextColours.Green
                Return "notification"
            Case TextColours.Pink
                Return "warning"
            Case TextColours.EndStatement
                Return "/"
        End Select
    End Function
    Private Sub ChangeLang(ByVal lang As LanguageType)
        Select Case lang
            Case LanguageType.Tr
                clsBrackets.Text = "Kapa"
                GroupBox2.Text = "Dil"
                GroupBox4.Text = "Yazı Alanı"
                Me.Text = "VALORANT Renkli Yazı Oluşturucu"
            Case LanguageType.Eng
                clsBrackets.Text = "Close"
                GroupBox2.Text = "Language"
                GroupBox4.Text = "Text Area"
                Me.Text = "VALORANT Coloured Text Generator"
        End Select
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex = 0 Then
            ChangeLang(LanguageType.Tr)
        ElseIf ComboBox1.SelectedIndex = 1 Then
            ChangeLang(LanguageType.Eng)
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If CultureInfo.CurrentCulture.EnglishName.StartsWith("Turkish") Then
            ComboBox1.SelectedIndex = 0
        Else
            ComboBox1.SelectedIndex = 1
        End If
    End Sub

    Private Sub AddColor(ByVal radioButton As RadioButton, ByVal ClrType As TextColours)
        Dim rb As RadioButton = radioButton
        Dim clr As TextColours = ClrType
        Dim SL As Integer = input.SelectionLength
            Dim SS As Integer = input.SelectionStart
            If SL > 0 Then
                input.Text = input.Text.Insert(SS + SL, "<" & GetHTMLColorCodeFromType(TextColours.EndStatement) & ">")
                input.Text = input.Text.Insert(SS, "<" & GetHTMLColorCodeFromType(clr) & ">")
            Else
                input.Text = input.Text.Insert(SS, "<" & GetHTMLColorCodeFromType(ClrType) & ">")
            End If
        rb.Checked = False
    End Sub


    Private Sub red_CheckedChanged(sender As Object, e As EventArgs) Handles red.CheckedChanged
        If red.Checked = True Then AddColor(red, TextColours.Red)
        red.Checked = False
    End Sub

    Private Sub blue_CheckedChanged(sender As Object, e As EventArgs) Handles blue.CheckedChanged
        If blue.Checked = True Then AddColor(blue, TextColours.Blue)
        blue.Checked = False
    End Sub

    Private Sub yellow_CheckedChanged(sender As Object, e As EventArgs) Handles yellow.CheckedChanged
        If yellow.Checked = True Then AddColor(yellow, TextColours.Yellow)
        yellow.Checked = False
    End Sub

    Private Sub green_CheckedChanged(sender As Object, e As EventArgs) Handles green.CheckedChanged
        If green.Checked = True Then AddColor(green, TextColours.Green)
        green.Checked = False
    End Sub

    Private Sub pink_CheckedChanged(sender As Object, e As EventArgs) Handles pink.CheckedChanged
        If pink.Checked = True Then AddColor(pink, TextColours.Pink)
        pink.Checked = False
    End Sub

    Private Sub clsBrackets_CheckedChanged(sender As Object, e As EventArgs) Handles clsBrackets.CheckedChanged
        If clsBrackets.Checked = True Then AddColor(clsBrackets, TextColours.EndStatement)
        clsBrackets.Checked = False
    End Sub
End Class
