


Public Class Search
    Public strStNo As String
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        strStNo = txtSearch.Text
        frmStRegisration.Show()

        Me.Close()


    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()

    End Sub

    Private Sub Search_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtSearch.Focus()
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged

    End Sub
End Class