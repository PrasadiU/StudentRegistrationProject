Imports System.Data.OleDb
Public Class frmStRegisration
    Dim adSt As New OleDbDataAdapter
    Dim ds As New DataSet
    Dim n As Integer
    Dim chrDBCommand As Char

    Private Sub frmStRegisration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        con.Open()
        Dim cmSt As New OleDbCommand
        cmSt.Connection = con
        cmSt.CommandText = "SELECT * FROM tblStudent"
        adSt.SelectCommand = cmSt
        adSt.Fill(ds, "Student")
        n = ds.Tables("Student").Rows.Count - 1
        con.Close()
        showRecords()
    End Sub
    Sub showRecords()
        Dim drSt As DataRow
        If n >= 0 Then
            drSt = ds.Tables("Student").Rows(n)
            With drSt
                txtIndex.Text = .Item("IndexNo")
                txtName.Text = .Item("STName")
                txtAddressLine1.Text = .Item("AddressLine1")
                txtAddressLine2.Text = .Item("AddressLine2")
                txtAddressLine3.Text = .Item("AddressLine3")
                txtDOB.Text = .Item("DOB")
                txtGrade.Text = .Item("Grade")
                txtTP.Text = .Item("TelNo")
                radMale.Checked = (.Item("Gender") = "M")
                radFemale.Checked = (.Item("Gender") = "F")

            End With
        End If

    End Sub

    Private Sub btnFirst_Click(sender As Object, e As EventArgs) Handles btnFirst.Click
        n = 0
        showRecords()
    End Sub

   
    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If n > 0 Then
            n = n - 1
            showRecords()
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If n < ds.Tables("Student").Rows.Count - 1 Then
            n = n + 1
            showRecords()
        End If
    End Sub

    Private Sub btnLast_Click(sender As Object, e As EventArgs) Handles btnLast.Click
        n = ds.Tables("Student").Rows.Count - 1
        showRecords()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        chrDBCommand = "A"
        txtIndex.Focus()
        ClearControls()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If n >= 0 Then
            chrDBCommand = "E"
            txtIndex.Focus()
        End If
        txtMsg.Clear()

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If n >= 0 Then
            chrDBCommand = "D"
        End If
        txtMsg.Clear()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim chrGender As String
        If radMale.Checked = True Then
            chrGender = "M"
        Else
            chrGender = "F"
        End If
        Dim cmBuilder As New OleDbCommandBuilder
        cmBuilder.DataAdapter = adSt
        If chrDBCommand = "A" Then
            If txtIndex.Text = "" Then
                MessageBox.Show("Please enter data before saving..")
            Else
                Dim drSt As DataRow
                drSt = ds.Tables("Student").NewRow
                With drSt
                    .Item("IndexNo") = txtIndex.Text
                    .Item("STName") = txtName.Text
                    .Item("DOB") = txtDOB.Text
                    .Item("Grade") = txtGrade.Text
                    .Item("TelNo") = txtTP.Text
                    .Item("Gender") = chrGender
                    .Item("AddressLine1") = txtAddressLine1.Text
                    .Item("AddressLine2") = txtAddressLine2.Text
                    .Item("AddressLine3") = txtAddressLine3.Text
                End With
                ds.Tables("Student").Rows.Add(drSt)
                adSt.InsertCommand = cmBuilder.GetInsertCommand
                n = n + 1
                txtMsg.Text = "Data Save Successfully.."
                txtMsg.ForeColor = Color.Red
            End If
        ElseIf chrDBCommand = "E" Then
            Dim tbSt As DataTable
            Dim dcPrimaryKey(0) As DataColumn
            tbSt = ds.Tables("Student")
            dcPrimaryKey(0) = tbSt.Columns("IndexNo")
            tbSt.PrimaryKey = dcPrimaryKey
            Dim drSt As DataRow = tbSt.Rows.Find(txtIndex.Text)
            With drSt
                '.Item("IndexNo") = txtIndex.Text
                .Item("STName") = txtName.Text
                .Item("DOB") = txtDOB.Text
                .Item("Grade") = txtGrade.Text
                .Item("TelNo") = txtTP.Text
                .Item("Gender") = chrGender
                .Item("AddressLine1") = txtAddressLine1.Text
                .Item("AddressLine2") = txtAddressLine2.Text
                .Item("AddressLine3") = txtAddressLine3.Text
            End With
            adSt.UpdateCommand = cmBuilder.GetUpdateCommand
            txtMsg.Text = "Data update Successfully.."
            txtMsg.ForeColor = Color.Purple
        ElseIf chrDBCommand = "D" Then
            ds.Tables("Student").Rows(n).Delete()
            adSt.DeleteCommand = cmBuilder.GetDeleteCommand
            n = n - 1
            txtMsg.Text = "Data Delete Successfully.."
            txtMsg.ForeColor = Color.Plum
        End If
        con.Open()
        Try
            adSt.Update(ds, "Student")
            ClearControls()
            showRecords()
        Catch ex As Exception
            MessageBox.Show("You are trying to save data incorrectly...")
        End Try
        con.Close()
    End Sub

   
    Sub ClearControls()
        txtIndex.Clear()
        txtName.Clear()
        txtAddressLine1.Clear()
        txtAddressLine2.Clear()
        txtAddressLine3.Clear()
        txtDOB.Clear()
        txtGrade.Clear()
        txtTP.Clear()
        radMale.Checked = True

    End Sub

    

    Private Sub btnExite_Click(sender As Object, e As EventArgs) Handles btnExite.Click
        Me.Close()

    End Sub

    
    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim tbStudent As DataTable
        Dim dcPrimarykey(0) As DataColumn
        tbStudent = ds.Tables("Student")
        dcPrimarykey(0) = tbStudent.Columns("IndexNo")
        tbStudent.PrimaryKey = dcPrimarykey

        Dim frmFind As New Search
        Dim strSNo As String

        frmFind.ShowDialog()

        strSNo = frmFind.strStNo

        If Not strSNo Is Nothing Then
            Dim drStudent As DataRow = tbStudent.Rows.Find(strSNo)
            If Not drStudent Is Nothing Then
                With drStudent
                    txtIndex.Text = .Item("IndexNo")
                    txtName.Text = .Item("STName")
                    txtAddressLine1.Text = .Item("AddressLine1")
                    txtAddressLine2.Text = .Item("AddressLine2")
                    txtAddressLine3.Text = .Item("AddressLine3")
                    txtDOB.Text = .Item("DOB")
                    txtGrade.Text = .Item("Grade")
                    txtTP.Text = .Item("TelNo")
                    radMale.Checked = (.Item("Gender") = "M")
                    radFemale.Checked = (.Item("Gender") = "F")
                End With
            Else
                MessageBox.Show("Student not found....", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

   
    Private Sub btnCancle_Click(sender As Object, e As EventArgs) Handles btnCancle.Click
        showRecords()
    End Sub
End Class
