Imports System.Data.SqlClient
Imports System.Security.Cryptography
Public Class dbPersona

    Public ReadOnly ConectionString As String = ConfigurationManager.ConnectionStrings("II-46ConnectionString").ConnectionString
    Private ReadOnly dbHelper = New DbHelper() 'clase para manejar la conexion y consultas a la base de datos
    Public Function create(Persona As Persona) As String
        Try
            Dim sql As String = "INSERT INTO Persona (Nombre, Apellido1, Apellido2, Nacionalidad, FechaNacimiento, Telefono) 
             VALUES (@Nombre, @Apellido1, @Apellido2, @Nacionalidad, @FechaNacimiento, @Telefono)"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@Nombre", Persona.Nombre),
                New SqlParameter("@Apellido1", Persona.Apellido1),
                New SqlParameter("@Apellido2", Persona.Apellido2),
                New SqlParameter("@Nacionalidad", Persona.Nacionalidad),
                New SqlParameter("@FechaNacimiento", Persona.FechaNacimiento),
                New SqlParameter("@Telefono", Persona.Telefono)
            }
            dbHelper.ExecuteNonQuery(sql, parametros)

        Catch ex As Exception
            Return "Error al guardar la persona: " & ex.Message

        End Try
        Return "Persona Guardada"
    End Function

    Public Function delete(id As Integer) As String
        Try
            Dim sql As String = "DELETE FROM Persona WHERE idPersona = @id"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@id", id)
                }
            Using connection As New SqlConnection(ConectionString)
                Using command As New SqlCommand(sql, connection)
                    command.Parameters.AddRange(parametros.ToArray())
                    connection.Open()
                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Return "Error al eliminar la persona: " & ex.Message
        End Try
        Return "Persona Eliminada"
    End Function

    Public Function update(ByRef Persona As Persona) As String
        Try
            Dim sql As String = "UPDATE Persona SET Nombre = @Nombre, Apellido1 = @Apellido1, Apellido2 = @Apellido2, Nacionalidad = @Nacionalidad, FechaNacimiento = @FechaNacimiento, Telefono = @Telefono WHERE idPersona = @Id"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@Id", Persona.IdPersona),
                New SqlParameter("@Nombre", Persona.Nombre),
                New SqlParameter("@Apellido1", Persona.Apellido1),
                New SqlParameter("@Apellido2", Persona.Apellido2),
                New SqlParameter("@Nacionalidad", Persona.Nacionalidad),
                New SqlParameter("@FechaNacimiento", Persona.FechaNacimiento),
                New SqlParameter("@Telefono", Persona.Telefono)
                }
            Using connection As New SqlConnection(ConectionString)
                Using command As New SqlCommand(sql, connection)
                    command.Parameters.AddRange(parametros.ToArray())
                    connection.Open()
                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Return "Error al actualizar la persona: " & ex.Message
        End Try
        Return "Persona Actualizada"
    End Function

    Public Function Consulta() As DataTable
        Try
            Dim sql As String = "SELECT *,CONCAT(Nombre, ' ', Apellido1, ' ', Apellido2, ' ') As NombreCompleto FROM Persona"
            Return dbHelper.ExecuteQuery(sql, New List(Of SqlParameter)())
        Catch ex As Exception
            Return New DataTable()
        End Try

    End Function
End Class