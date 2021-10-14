Imports System.Configuration
Imports System.Data.SqlClient

Public MustInherit Class Repository
    Public ReadOnly cadenadeconexion As String

    REM declaramos el constructor
    Public Sub New()
        cadenadeconexion = ConfigurationManager.ConnectionStrings("ConnMycompany").ToString()
    End Sub

    REM funcion para obtener la conecion protegidad
    Protected Function GetConnection() As SqlConnection
        Return New SqlConnection(cadenadeconexion)
    End Function

End Class
