Imports System.Data.OleDb
Module Module1
    Public con As New OleDbConnection
    Sub main()
        con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\MyProject\Student.mdb"
        Dim login As New Login
        login.ShowDialog()
    End Sub
End Module
