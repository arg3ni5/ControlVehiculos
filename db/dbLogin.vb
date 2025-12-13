Imports System.Data.SqlClient
Public Class dbLogin
    Private ReadOnly dbHelper = New DbHelper() ' Clase para manejar conexiones y consultas

    Public Function ValidateLogin(ByRef usuario As String, ByRef password As String) As Boolean
        Try
            Dim sql As String = "SELECT COUNT(*) FROM Usuarios WHERE NombreUsuario = @Usuario AND Contrasena = @Password"
            Dim Parametros As New List(Of SqlParameter) From {
                New SqlParameter("@Usuario", usuario),
                New SqlParameter("@Password", password)
            }
            Dim dt As DataTable = dbHelper.ExecuteQuery(sql, Parametros)
            If dt.Rows.Count > 0 AndAlso Convert.ToInt32(dt.Rows(0)(0)) > 0 Then
                Return True
            End If
        Catch ex As Exception
            ' Manejo de errores si es necesario
            Return "Creedenciales incorrectas."
        End Try
        Return False
    End Function

    Public Function RegisterUser(ByRef usuario As String, ByRef password As String, ByRef email As String) As String
        Try
            Dim sql As String = "INSERT INTO Usuarios ([NombreUsuario]
           ,[Contrasena]
           ,[Email]) VALUES (@Usuario, @Password, @email)"
            Dim Parametros As New List(Of SqlParameter) From {
                New SqlParameter("@Usuario", usuario),
                New SqlParameter("@Password", password),
                New SqlParameter("@email", password)
            }
            dbHelper.ExecuteNonQuery(sql, Parametros)
        Catch ex As Exception
            Return "Error al registrar el usuario: " & ex.Message
        End Try
        Return "Usuario registrado"
    End Function
End Class
