Imports System.Data.SqlClient

Public Class dbPersona
    Public ReadOnly ConectionString As String = ConfigurationManager.ConnectionStrings("II46ConnectionString").ConnectionString

    Public Function create(Persona As Persona) As String
        Try
            Dim sql As String = "INSERT INTO Personas (Nombre, Apellido1, Apellido2, Nacionalidad, fechaNacimiento, Telefono) 
            VALUES (@Nombre, @Apellido1, @Apellido2, @Nacionalidad, @fechaNacimiento, @Telefono)"
            Dim Parametros As New List(Of SqlParameter) From {
            New SqlParameter("@Nombre", Persona.Nombre),
            New SqlParameter("@Apellido1", Persona.Apellido1),
            New SqlParameter("@Apellido2", Persona.Apellido2),
            New SqlParameter("@Nacionalidad", Persona.Nacionalidad),
            New SqlParameter("@fechaNacimiento", Persona.FechaNacimiento),
            New SqlParameter("@Telefono", Persona.Telefono)
            }

            Using connetion As New SqlConnection(ConectionString)
                Using command As New SqlCommand(sql, connetion)
                    command.Parameters.AddRange(Parametros.ToArray())
                    connetion.Open()
                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Return "Error al guardar la persona: " & ex.Message
        End Try
        Return "Persona Guardada"
    End Function

    Public Function delete(ByRef id As Integer) As String
        Try
            Dim sql As String = "DELETE FROM Personas WHERE idPersona = @idPersona"
            Dim Parametros As New List(Of SqlParameter) From {
                New SqlParameter("@idPersona", id)
            }
            Using connetion As New SqlConnection(ConectionString)
                Using command As New SqlCommand(sql, connetion)
                    command.Parameters.AddRange(Parametros.ToArray())
                    connetion.Open()
                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Return "Error al eliminar la persona: " & ex.Message
        End Try
        Return "Persona eliminada"
    End Function

    Public Function update(ByRef Persona As Persona) As String
        Try
            Dim sql As String = "UPDATE Personas SET Nombre = @Nombre, Apellido = @Apellido, Edad = @Edad WHERE ID = @Id"
            Dim Parametros As New List(Of SqlParameter) From {
                New SqlParameter("@Id", Persona.IdPersona),
                New SqlParameter("@Nombre", Persona.Nombre),
                New SqlParameter("@Apellido", Persona.Apellido1),
                New SqlParameter("@Edad", Persona.FechaNacimiento)
            }
            Using connetion As New SqlConnection(ConectionString)
                Using command As New SqlCommand(sql, connetion)
                    command.Parameters.AddRange(Parametros.ToArray())
                    connetion.Open()
                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Return "Error al actualizar la persona: " & ex.Message
        End Try
        Return "Persona actualizada"
    End Function
End Class
