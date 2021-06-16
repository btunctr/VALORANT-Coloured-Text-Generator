Public Class EditRainbowColors



    Private Sub RefreshList()
        ListBox1.Items.Clear()
        ComboBox1.Items.Clear()
        For i As Integer = 0 To Form1.ColorListPerma.Count - 1
            With ListBox1
                .Items.Add(Form1.ColorListPerma.Item(i).ToString)
            End With
        Next
        Dim csList As String() = Form1.GetColorStringsWithNolangChange()
        For x As Integer = 0 To 4
            With ComboBox1
                .Items.Add(csList(x))
            End With
        Next
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form1.ColorListPerma.Remove(DirectCast([Enum].Parse(GetType(Form1.TextColours), ComboBox1.SelectedItem.ToString), Form1.TextColours))
        ListBox1.Items.Remove(DirectCast([Enum].Parse(GetType(Form1.TextColours), ComboBox1.SelectedItem.ToString), Form1.TextColours))
        RefreshList()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Form1.ColorListPerma.Add(DirectCast([Enum].Parse(GetType(Form1.TextColours), ComboBox1.SelectedItem.ToString), Form1.TextColours))
        ListBox1.Items.Add(DirectCast([Enum].Parse(GetType(Form1.TextColours), ComboBox1.SelectedItem.ToString), Form1.TextColours))
        RefreshList()
    End Sub

    Private Sub EditRainbowColors_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshList()
    End Sub
End Class