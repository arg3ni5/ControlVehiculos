Imports ControlVehiculos.Utils

Public Class Registro
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnRegistrar_Click(sender As Object, e As EventArgs)
        Dim Password = txtPassword.Text
        Dim PasswordC = txtConfirmarPassword.Text

        If Password <> PasswordC Then
            SwalUtils.ShowSwalError(Me, "No coincide la contraseña")

        End If

        Dim usuario As Usuario = New Usuario(txtUsuario.Text, txtPassword.Text)
        usuario.Email = txtEmail.Text
    End Sub
End Class