Imports Domain

Public Class FormEmployee
    Dim employeemodel As New EmployeeModel()
    Private Sub FormEmployee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListEmployee()
    End Sub


    Private Sub ListEmployee()
        Try
            DataGridView1.DataSource = employeemodel.GetEmployees()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub
End Class