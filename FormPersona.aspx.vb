Imports System.Web.Services.Description
Imports ControlVehiculos.Utils

Public Class FormPersona
    Inherits System.Web.UI.Page
    Public persona As New Persona()
    Protected dbHelper As New dbPersona()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btn_guardar(sender As Object, e As EventArgs)
        Try

            persona.Nombre = txtNombre.Text
            persona.Apellido1 = txtApellido1.Text
            persona.Apellido2 = txtApellido2.Text
            persona.Nacionalidad = txtNacionalidad.Text
            persona.FechaNacimiento = txtfechaNacimiento.Text
            persona.Telefono = txtTelefono.Text

            Dim mensaje = dbHelper.create(persona)
            If mensaje.Contains("Error") Then
                SwalUtils.ShowSwalError(Me, "Error", mensaje)
            Else
                SwalUtils.ShowSwal(Me, mensaje)
            End If

            txtNombre.Text = ""
            txtApellido1.Text = ""
            txtApellido2.Text = ""
            txtNacionalidad.Text = ""
            txtfechaNacimiento.Text = ""
            txtTelefono.Text = ""

            gvPersonas.DataBind()
        Catch ex As Exception
            lblMensaje.Text = "Error al guardar la persona: " & ex.Message
            SwalUtils.ShowSwalError(Me, "Error al guardar la persona", ex.Message)
        End Try

    End Sub

    Protected Sub gvPersonas_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)

        Try
            Dim id As Integer = Convert.ToInt32(gvPersonas.DataKeys(e.RowIndex).Value)

            Dim mensaje = dbHelper.delete(id)
            If mensaje.Contains("Error") Then
                SwalUtils.ShowSwalError(Me, "Error", mensaje)
            Else
                SwalUtils.ShowSwal(Me, mensaje)
            End If
            e.Cancel = True
            gvPersonas.DataBind()
        Catch ex As Exception
            lblMensaje.Text = "Error al eliminar la persona: " & ex.Message
            SwalUtils.ShowSwalError(Me, "Error al eliminar la persona", ex.Message)
        End Try

    End Sub

    Protected Sub gvPersonas_RowEditing(sender As Object, e As GridViewEditEventArgs)



    End Sub

    Protected Sub gvPersonas_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs)

        gvPersonas.EditIndex = -1
        gvPersonas.DataBind()


    End Sub

    Protected Sub gvPersonas_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)
        Try
            Dim id As Integer = Convert.ToInt32(gvPersonas.DataKeys(e.RowIndex).Value)
            Dim persona = New Persona With {
                .Nombre = e.NewValues("Nombre"),
                .Apellido1 = e.NewValues("Apellido1"),
                .Apellido2 = e.NewValues("Apellido2"),
                .FechaNacimiento = e.NewValues("FechaNacimiento"),
                .Telefono = e.NewValues("Telefono"),
                .Nacionalidad = e.NewValues("Nacionalidad"),
                .IdPersona = id
            }

            Dim mensaje = dbHelper.update(persona)
            If mensaje.Contains("Error") Then
                SwalUtils.ShowSwalError(Me, "Error", mensaje)
            Else
                SwalUtils.ShowSwal(Me, mensaje)
            End If
            gvPersonas.DataBind()
            e.Cancel = True
            gvPersonas.EditIndex = -1
        Catch ex As Exception
            SwalUtils.ShowSwalError(Me, "Error al actulizar la persona", ex.Message)
        End Try

    End Sub

    Protected Sub gvPersonas_SelectedIndexChanged(sender As Object, e As EventArgs)

        Dim row As GridViewRow = gvPersonas.SelectedRow()
        'Dim id As Integer = Convert.ToInt32(row.Cells(2).Text)
        Dim persona As Persona = New Persona()

        txtNombre.Text = row.Cells(3).Text
        txtApellido1.Text = row.Cells(4).Text
        txtApellido2.Text = row.Cells(5).Text

        txtNacionalidad.Text = row.Cells(6).Text
        txtfechaNacimiento.Text = row.Cells(7).Text
        txtTelefono.Text = row.Cells(8).Text

        'editando.Value = id

    End Sub

    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs)


        Dim persona As Persona = New Persona With {
            .Nombre = txtNombre.Text(),
            .Apellido1 = txtApellido1.Text(),
            .Apellido2 = txtApellido2.Text(),
            .FechaNacimiento = txtfechaNacimiento.Text(),
            .IdPersona = editando.Value()
        }
        dbHelper.update(persona)
        gvPersonas.DataBind()
        gvPersonas.EditIndex = -1

    End Sub
End Class