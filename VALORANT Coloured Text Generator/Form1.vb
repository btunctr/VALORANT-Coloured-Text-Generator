Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Xml
Imports Newtonsoft.Json.Linq

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
    Enum RainbowType
        Letter
        Word
    End Enum

    Private isConfigLoaded As Boolean = False
    Private ConfigPath As String
    Public LocLang As LanguageType

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
                Button1.Text = "Config Yükle"
                Button2.Text = "Görüntüle"
                Label3.Text = IIf(isConfigLoaded = False, "Yüklü Değil", "Yüklü")
                GroupBox7.Text = "Gökkuşağı Yazı (.)"
                letter.Text = "Harf"
                word.Text = "Sözcük"
                Button4.Text = "Uygula"
                GroupBox3.Text = "Renkler"
                Button3.Text = "Configi Kaldır"
                LocLang = LanguageType.Tr
            Case LanguageType.Eng
                clsBrackets.Text = "Close"
                GroupBox2.Text = "Language"
                GroupBox4.Text = "Text Area"
                Me.Text = "VALORANT Coloured Text Generator"
                Button1.Text = "Load Config"
                Button2.Text = "View Config"
                GroupBox7.Text = "Rainbow Text (.)"
                letter.Text = "Letter"
                word.Text = "Word"
                Label3.Text = IIf(isConfigLoaded = False, "Not Loaded", "Loaded")
                Button4.Text = "Apply"
                GroupBox3.Text = "Colors"
                Button3.Text = "Unload Config"
                LocLang = LanguageType.Eng
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

        LoadColorList()

        With ColorListPerma
            .Add(TextColours.Red)
            .Add(TextColours.Blue)
            .Add(TextColours.Yellow)
            .Add(TextColours.Green)
            .Add(TextColours.Pink)
        End With
    End Sub

    Private Sub LoadColorList()
        For i As Integer = 0 To ColorListPerma.Count - 1
            ColorList.Add(ColorListPerma.Item(i))
        Next
    End Sub

    Public Function GetColorStrings(Optional ByVal color As TextColours = Nothing) As String() ' No input = All Colors At Computer Language; Color input = inputed color at computer language
        Dim sb As New StringBuilder
        If color = Nothing Then
            sb.AppendLine(ChangeColorStringLang("Red", LocLang))
            sb.AppendLine(ChangeColorStringLang("Blue", LocLang))
            sb.AppendLine(ChangeColorStringLang("Yellow", LocLang))
            sb.AppendLine(ChangeColorStringLang("Green", LocLang))
            sb.AppendLine(ChangeColorStringLang("Pink", LocLang))
        Else
            Select Case color
                Case TextColours.Red
                    sb.AppendLine(ChangeColorStringLang("Red", LocLang))
                Case TextColours.Blue
                    sb.AppendLine(ChangeColorStringLang("Blue", LocLang))
                Case TextColours.Yellow
                    sb.AppendLine(ChangeColorStringLang("Yellow", LocLang))
                Case TextColours.Green
                    sb.AppendLine(ChangeColorStringLang("Green", LocLang))
                Case TextColours.Pink
                    sb.AppendLine(ChangeColorStringLang("Pink", LocLang))
            End Select
        End If
        Return sb.ToString.Split(vbNewLine)


    End Function

    Private Function ChangeColorStringLang(ByVal ColorStr As String, ByVal lang As LanguageType) As String
        Dim trArray As String() = {"Kırmızı", "Mavi", "Sarı", "Yeşil", "Pembe"}
        Dim enArray As String() = {"Red", "Blue", "Yellow", "Green", "Pink"}
        Dim baseLang As LanguageType = IIf(trArray.Contains(ColorStr), LanguageType.Tr, LanguageType.Eng)
        If baseLang = LanguageType.Tr And lang = LanguageType.Eng Then
            Dim index As Integer
            For i As Integer = 0 To trArray.Count - 1
                If trArray(i) = ColorStr Then
                    index = i
                    Exit For
                End If
            Next
            Return enArray(index)
        ElseIf baseLang = LanguageType.Eng And lang = LanguageType.Tr Then
            Dim index As Integer
            For i As Integer = 0 To enArray.Count - 1
                If enArray(i) = ColorStr Then
                    index = i
                    Exit For
                End If
            Next
            Return trArray(index)
        ElseIf baseLang = LanguageType.Eng And lang = LanguageType.Eng Or baseLang = LanguageType.Tr And lang = LanguageType.Tr Then
            Return ColorStr
        End If
    End Function


    Public Function ColorStrToTextColor(colorStr As String) As TextColours
        Select Case ChangeColorStringLang(colorStr, LanguageType.Eng)
            Case "Red"
                Return TextColours.Red
            Case "Blue"
                Return TextColours.Blue
            Case "Yellow"
                Return TextColours.Yellow
            Case "Green"
                Return TextColours.Green
            Case "Pink"
                Return TextColours.Pink
        End Select
    End Function


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
    Public ColorList As New List(Of TextColours)
    Public ColorListPerma As New List(Of TextColours)
    Private Function MakeTextRainbow(ByVal text As String, ByVal RType As RainbowType) As String
        Dim rnd As New Random(System.DateTime.Now.Millisecond)
        Dim rainbowTEXT As New StringBuilder

        LoadColorList()

        If RType = RainbowType.Letter Then
            For Each ch As Char In input.Text
                If ColorList.Count = 0 Then
                    LoadColorList()
                End If
                If ch <> " " Then
                    Dim res As TextColours = ColorList.Item(rnd.Next(0, ColorList.Count - 1))
                    rainbowTEXT.Append("<" & GetHTMLColorCodeFromType(res) & ">" & ch & "<" & GetHTMLColorCodeFromType(TextColours.EndStatement) & ">")
                    ColorList.Remove(res)
                Else
                    rainbowTEXT.Append(" ")
                End If
            Next
        ElseIf RType = RainbowType.Word Then
            For i As Integer = 0 To input.Text.Split(" ").Count - 1

                If ColorList.Count = 0 Then
                    LoadColorList()
                End If
                Dim res As TextColours = ColorList.Item(rnd.Next(0, ColorList.Count - 1))
                rainbowTEXT.Append("<" & GetHTMLColorCodeFromType(res) & ">" & input.Text.Split(" ")(i) & "<" & GetHTMLColorCodeFromType(TextColours.EndStatement) & ">")
                ColorList.Remove(res)
                rainbowTEXT.Append((" "))

            Next
        End If
        Return rainbowTEXT.ToString
    End Function


#Region "Color Radio Btns"


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

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        System.Diagnostics.Process.Start("https://discord.gg/Ud7ZCuNYqD")
    End Sub

#End Region

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If letter.Checked = True And word.Checked = False Then
            input.Text = MakeTextRainbow(input.Text, RainbowType.Letter)
        ElseIf letter.Checked = False And word.Checked = True Then
            input.Text = MakeTextRainbow(input.Text, RainbowType.Word)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If OpenFileDialog1.ShowDialog = DialogResult.OK Then
            isConfigLoaded = True
            ChangeConfigStatus()
            ConfigPath = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If isConfigLoaded = True Then
            isConfigLoaded = False
            ChangeConfigStatus()
        End If
    End Sub
    Private Sub ChangeConfigStatus()
        If isConfigLoaded = True Then
            Button1.Enabled = False
            Label3.Text = IIf(LocLang = LanguageType.Eng, "Loaded", "Yüklü")
            Label3.ForeColor = Color.Green
            Button2.Enabled = True
            Button3.Enabled = True
        Else
            Button1.Enabled = True
            Label3.Text = IIf(LocLang = LanguageType.Eng, "Not Loaded", "Yüklü Değil")
            Label3.ForeColor = Color.FromArgb(192, 0, 0)
            Button2.Enabled = False
            Button3.Enabled = False
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ViewConfig.Show()
        ViewConfig.ListView1.Items.Clear()
        ViewConfig.ListView1.Columns(0).Text = IIf(LocLang = LanguageType.Eng, "Name", "Ad")

        Dim jsonString As String = My.Computer.FileSystem.ReadAllText(ConfigPath)
        Dim json As JArray = TryCast(JArray.Parse(jsonString), JArray)

        For Each Row In json.ToList()
            ViewConfig.ListView1.Items.Add(DirectCast(Row("Name"), JValue).Value)
            ViewConfig.list.Add(DirectCast(Row("ColorCode"), JValue).Value & ":" & DirectCast(Row("Name"), JValue).Value)
        Next

    End Sub

    Private Sub GroupBox7_DoubleClick(sender As Object, e As EventArgs) Handles GroupBox7.DoubleClick
        EditRainbowColors.Show()
    End Sub

    Public Function GetColorStringsWithNolangChange(Optional ByVal color As TextColours = Nothing) As String()
        Dim sb As New StringBuilder
        If color = Nothing Then
            sb.AppendLine(("Red"))
            sb.AppendLine(("Blue"))
            sb.AppendLine(("Yellow"))
            sb.AppendLine(("Green"))
            sb.AppendLine(("Pink"))
        Else
            Select Case color
                Case TextColours.Red
                    sb.AppendLine(("Red"))
                Case TextColours.Blue
                    sb.AppendLine(("Blue"))
                Case TextColours.Yellow
                    sb.AppendLine(("Yellow"))
                Case TextColours.Green
                    sb.AppendLine(("Green"))
                Case TextColours.Pink
                    sb.AppendLine(("Pink"))
            End Select
        End If
        Return sb.ToString.Split(vbNewLine)


    End Function
End Class