Imports ASPNETWebForms.BLL
Imports ASPNETWebForms.Models
Imports System.Drawing

Public Class Clientes
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarClientes()
        End If
    End Sub

    ' Private Sub CargarClientes()
    ' RepeaterClientes.DataSource = ClienteBLL.ObtenerClientes()
    ' RepeaterClientes.DataBind()
    ' End Sub
    Public Sub CargarClientes()
        Try
            Dim clientes = ClienteBLL.ObtenerClientes()

            If clientes IsNot Nothing AndAlso clientes.Count > 0 Then
                RepeaterClientes.DataSource = clientes
                RepeaterClientes.DataBind()
            Else
                Response.Write("No hay clientes en la base de datos.")
            End If
        Catch ex As Exception
            Response.Write("Error de SQL: " & ex.Message & "<br/>StackTrace: " & ex.StackTrace)
        End Try
    End Sub

    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As EventArgs)
        If ClienteBLL.AgregarCliente(txtNombre.Text, txtTelefono.Text, txtCorreo.Text) Then
            agregarClienteResultado.Text = "Cliente agregado correctamente a la base de datos."
            agregarClienteResultado.ForeColor = Color.Green
            CargarClientes() ' Recargar la lista de clientes en el Repeater
        Else
            agregarClienteResultado.Text = "Error al agregar al cliente a la base de datos."
            agregarClienteResultado.ForeColor = Color.Red
        End If
    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim idCliente As Integer
        If Integer.TryParse(txtBuscarId.Text, idCliente) Then
            Dim clienteEncontrado As Cliente = ClienteBLL.ObtenerClientePorId(idCliente)

            If clienteEncontrado IsNot Nothing Then
                txtNombre.Text = clienteEncontrado.Nombre
                txtTelefono.Text = clienteEncontrado.Telefono
                txtCorreo.Text = clienteEncontrado.Correo
                buscarClienteResultado.Text = "Cliente encontrado."
                buscarClienteResultado.ForeColor = Color.Green
            Else
                buscarClienteResultado.Text = "No se encontró ningún cliente con ese ID."
                buscarClienteResultado.ForeColor = Color.Red
            End If
        Else
            buscarClienteResultado.Text = "Por favor, ingrese un ID de cliente válido para buscar al cliente."
            buscarClienteResultado.ForeColor = Color.Red
        End If
    End Sub
    Protected Sub btnActualizar_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim idCliente As Integer

        ' Verificar que el campo no esté vacío
        If String.IsNullOrWhiteSpace(txtActualizarId.Text) Then
            actualizarInformacionResultado.Text = "El ID del cliente no puede estar vacío."
            actualizarInformacionResultado.ForeColor = Color.Red
            Exit Sub
        End If

        ' Intentar obtener el ID del campo de actualización
        If Not Integer.TryParse(txtActualizarId.Text.Trim(), idCliente) Then
            actualizarInformacionResultado.Text = "Por favor, ingrese un ID de cliente válido (número entero)."
            actualizarInformacionResultado.ForeColor = Color.Red
            Exit Sub
        End If

        ' Llamar al método de actualización con el ID válido
        Dim resultado As Boolean = ClienteBLL.ActualizarCliente(idCliente, txtActualizarNombre.Text,
                                                                txtActualizarTelefono.Text, txtActualizarCorreo.Text)

        If resultado Then
            actualizarInformacionResultado.Text = "Cliente actualizado correctamente."
            actualizarInformacionResultado.ForeColor = Color.Green
            CargarClientes()
        Else
            actualizarInformacionResultado.Text = "No se pudo actualizar el cliente. Verifique que el ID exista."
            actualizarInformacionResultado.ForeColor = Color.Red
        End If
    End Sub


    Protected Sub btnEliminar_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim idCliente As Integer
        If Integer.TryParse(txtBuscarId.Text, idCliente) Then
            If ClienteBLL.EliminarCliente(idCliente) Then
                eliminarClienteResultado.Text = "Cliente eliminado correctamente."
                eliminarClienteResultado.ForeColor = Color.Green
                CargarClientes()
            Else
                eliminarClienteResultado.Text = "Error al eliminar el cliente de la base de datos."
                eliminarClienteResultado.ForeColor = Color.Red
            End If
        Else
            eliminarClienteResultado.Text = "Por favor, ingrese un ID de cliente válido para eliminar un registro."
            eliminarClienteResultado.ForeColor = Color.Red
        End If
    End Sub


End Class