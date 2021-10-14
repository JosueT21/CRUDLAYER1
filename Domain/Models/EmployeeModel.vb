Imports DataAccess
Imports System.ComponentModel.DataAnnotations
Public Class EmployeeModel
    'Campos
    Private _IdPK As Integer
    Private _IdNumber As String
    Private _Name As String
    Private _Mail As String
    Private _Birthday As Date
    Private _age As Integer
    Private _State As EntityState
    Private Repository As IEmployeeRepository

    'Propiedades
#Region "Propiedades/modelosdevistas/ validaciondedatos"
    Public Property IdPK As Integer
        Get
            Return _IdPK
        End Get
        Set(value As Integer)
            _IdPK = value
        End Set
    End Property

    <Required(ErrorMessage:="El campo de Identificacion es requerido")>
    <RegularExpression("([0-9])+", ErrorMessage:="El campo de Identificacion solo requiere numeros")>
    <StringLength(10, MinimumLength:=10, ErrorMessage:="El campo de Identificacion requiere 10 numeros")>
    Public Property IdNumber As String
        Get
            Return _IdNumber
        End Get
        Set(value As String)
            _IdNumber = value
        End Set
    End Property

    <Required>
    <RegularExpression("^[a-zA-Z]+$", ErrorMessage:="Este campo solo requiere letras")>
    <StringLength(100, MinimumLength:=3)>
    Public Property Name As String
        Get
            Return _Name
        End Get
        Set(value As String)
            _Name = value
        End Set
    End Property
    <Required>
    <EmailAddress>
    Public Property Mail As String
        Get
            Return _Mail
        End Get
        Set(value As String)
            _Mail = value
        End Set
    End Property


    Public Property Birthday As Date
        Get
            Return _Birthday
        End Get
        Set(value As Date)
            _Birthday = value
        End Set
    End Property

    Public Property Age As Integer
        Get
            Return _age
        End Get
        Private Set(value As Integer)
            _age = value
        End Set
    End Property

    Public Property State As EntityState
        Private Get
            Return _State
        End Get
        Set(value As EntityState)
            _State = value
        End Set
    End Property
#End Region

    'Constructor
    Public Sub New()
        Repository = New EmployeeRepository()
    End Sub

    'Metodos
    Public Function SaveChanges() As String
        Dim message As String = Nothing
        Try
            Dim employeeDataModel As New Employees()
            employeeDataModel.IdPK = IdPK
            employeeDataModel.IdNumber = IdNumber
            employeeDataModel.Name = Name
            employeeDataModel.Mail = Mail
            employeeDataModel.Birthday = Birthday

            Select Case _State
                Case EntityState.Added
                    Repository.Add(employeeDataModel)
                    message = "Registro guardado correctamente"

                Case EntityState.Modified
                    Repository.Edit(employeeDataModel)
                    message = "Registro Editado correctamente"

                Case EntityState.Deleted
                    Repository.Remove(IdPK)
                    message = "Registro Elimando Correctamente"
            End Select

        Catch ex As Exception
            Dim sqlEx As System.Data.SqlClient.SqlException = TryCast(ex, System.Data.SqlClient.SqlException)
            If sqlEx IsNot Nothing AndAlso sqlEx.Number = 2627 Then
                message = "Registro Duplicado"

            Else
                message = ex.ToString()
            End If

        End Try
        Return message
    End Function

    REM funcion para obtener la lista de los empleados
    Public Function GetEmployees() As List(Of EmployeeModel)
        Dim ListEmployeeDataModel = Repository.GetAll()
        Dim ListEmployeeViewModel = New List(Of EmployeeModel)

        For Each item As Employees In ListEmployeeDataModel
            Dim birthDay = item.Birthday
            ListEmployeeViewModel.Add(New EmployeeModel With {
            .IdPK = item.IdPK,
            .IdNumber = item.IdNumber,
            .Name = item.Name,
            .Mail = item.Mail,
            .Birthday = item.Birthday,
            .Age = CalculateAge(birthDay)
            })
        Next
        Return ListEmployeeViewModel
    End Function

    REM funcion para calcular la edad
    Private Function CalculateAge(Birthdate As Date) As Integer
        Dim datenow = Date.Now

        Return datenow.Year - Birthdate.Year
    End Function
End Class
