Imports System.Data
Imports System.Data.SqlClient

Public MustInherit Class MasterRepository
    Inherits Repository REM Heredamos de la clase  base repositorio

    REM declaramos para los parametros de tipo lista generico sql
    Protected parameters As List(Of SqlParameter)

    REM este metodo se encargara de ejecutar comando SQL de consulta
    REM editar, insertar, eliminar datos
    Protected Function ExecuteNonQuery(trasactSql As String) As Integer
        Using connection = GetConnection()
            connection.Open()
            Using command = New SqlCommand()
                command.Connection = connection
                command.CommandText = trasactSql
                command.CommandType = CommandType.Text
                For Each item As SqlParameter In parameters
                    command.Parameters.Add(item)
                Next
                Dim result = command.ExecuteNonQuery()
                parameters.Clear()
                Return result
            End Using
        End Using
    End Function

    REM este metodo se encargara de ejecutar comando SQL para leer filas de 
    REM tablas SQL
    Protected Function ExecuteReader(trasactSql As String) As DataTable
        Using connection = GetConnection()
            connection.Open()
            Using command = New SqlCommand()
                command.Connection = connection
                command.CommandText = trasactSql
                command.CommandType = CommandType.Text
                Dim reader = command.ExecuteReader()
                Using table = New DataTable()
                    table.Load(reader)
                    reader.Dispose()
                    Return table
                End Using
            End Using
        End Using
    End Function
End Class
