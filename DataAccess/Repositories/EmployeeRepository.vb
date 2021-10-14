
Imports System.Data
Imports System.Data.SqlClient

REM clase concreta para el repositorio empleado
Public Class EmployeeRepository
    Inherits MasterRepository
    Implements IEmployeeRepository

    REM Declaramos los campos de forma privada para las cuatro operaciones basicas.
    Private selectall As String
    Private insert As String
    Private update As String
    Private delete As String

    REM creamos el constructor e inicializamos los campos
    Public Sub New()
        selectall = "select * from Employee"
        insert = " insert into Employee values(@IdNumber,@name,@mail,@birthday)"
        update = "update Employee set Idnumber=@Idnumber,name=@name,mail=@mail,birthday=@birthday where IdPK=@IdPK"
        delete = "delete from Employee where IdPK=@IdPK"
    End Sub


    Public Function GetAll() As IEnumerable(Of Employees) Implements IGenericRepository(Of Employees).GetAll
        Dim resultable = ExecuteReader(selectall)
        Dim listEmployee = New List(Of Employees)
        For Each item As DataRow In resultable.Rows
            listEmployee.Add(New Employees With {
             .IdPK = item(0),
             .IdNumber = item(1),
             .Name = item(2),
             .Mail = item(3),
             .Birthday = item(4)
            })
        Next
        Return listEmployee
    End Function

    Public Function Add(entity As Employees) As Integer Implements IGenericRepository(Of Employees).Add
        parameters = New List(Of SqlParameter)
        parameters.Add(New SqlParameter("@IdNumber", entity.IdNumber))
        parameters.Add(New SqlParameter("@name", entity.Name))
        parameters.Add(New SqlParameter("@mail", entity.Mail))
        parameters.Add(New SqlParameter("@birthday", entity.Birthday))
        Return ExecuteNonQuery(insert)
    End Function

    Public Function Edit(entity As Employees) As Integer Implements IGenericRepository(Of Employees).Edit
        parameters = New List(Of SqlParameter)
        parameters.Add(New SqlParameter("@IdPK", entity.IdPK))
        parameters.Add(New SqlParameter("@IdNumber", entity.IdNumber))
        parameters.Add(New SqlParameter("@name", entity.Name))
        parameters.Add(New SqlParameter("@mail", entity.Mail))
        parameters.Add(New SqlParameter("@birthday", entity.Birthday))
        Return ExecuteNonQuery(update)
    End Function

    Public Function Remove(id As Integer) As Integer Implements IGenericRepository(Of Employees).Remove
        parameters = New List(Of SqlParameter)
        parameters.Add(New SqlParameter("@IdPK", id))
        Return ExecuteNonQuery(delete)
    End Function
End Class
