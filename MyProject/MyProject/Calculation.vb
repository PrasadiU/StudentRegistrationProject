Imports System.Data.OleDb

Public Class Calculation
    Dim con As New OleDbConnection
    Dim command As OleDbCommand
    Dim ra As Integer

    Private Sub Calculation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\MyProject\Student.mdb"
    End Sub

    Private Sub btnCount_Click(sender As Object, e As EventArgs) Handles btnCount.Click
        Try
            con.Open()
            command = New OleDbCommand("Select Count(IndexNo)from tblStudent", con)
            ra = command.ExecuteScalar
            txtCount.Text = ra
            con.Close()

        Catch ex As Exception
            MsgBox("Exception")
        End Try
    End Sub
End Class