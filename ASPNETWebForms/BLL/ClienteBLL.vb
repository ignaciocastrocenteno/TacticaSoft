' All defined methods could return 3 possible different integer values to show the operation status:
' * If returns 1 or more, it refers to the amount of affected registers onto the database (so the operation was successful)
' * If returns 0, no changes were performed onto the database with respect to the last consistent state.
' * If any exception happens to be thrown, it's going to be manage with the Try-Catch statement.
Imports ASPNETWebForms.Controllers
Imports ASPNETWebForms.DAL
Imports ASPNETWebForms.Models

Namespace BLL
    Public Class ClienteBLL
        Private ReadOnly _clienteDAL As New ClienteDAL()

        ' Method to get all clients, against the DAL (Data-Access-Layer)
        Public Shared Function ObtenerClientes() As List(Of Cliente)
            Return ClienteController.ListarClientes()
        End Function

        ' Method to get a specific client based on its ID, against the DAL (Data-Access-Layer)
        Public Shared Function ObtenerClientePorId(id As Integer) As Cliente
            If id <= 0 Then Return Nothing

            Return ClienteController.ObtenerClientePorID(id)
        End Function

        ' Method to add a new client, against the DAL (Data-Access-Layer)
        Public Shared Function AgregarCliente(nombre As String, telefono As String, correo As String) As Boolean
            ' Basic data validations before the insertion
            If String.IsNullOrWhiteSpace(nombre) OrElse String.IsNullOrWhiteSpace(telefono) OrElse String.IsNullOrWhiteSpace(correo) Then
                Return False
            End If

            ' Creating a new client object, based on the data provided by the user
            Dim nuevoCliente As New Cliente With {
                .Nombre = nombre,
                .Telefono = telefono,
                .Correo = correo
            }

            Return ClienteController.AgregarCliente(nuevoCliente) > 0 ' Return 'true' if was inserted correctly
        End Function

        ' Method to update the client information, against the DAL (Data-Access-Layer)
        Public Shared Function ActualizarCliente(id As Integer, nombre As String, telefono As String, correo As String) As Boolean
            ' Basic data validations before the performing changes over existing data
            If id <= 0 OrElse String.IsNullOrWhiteSpace(nombre) OrElse String.IsNullOrWhiteSpace(telefono) OrElse String.IsNullOrWhiteSpace(correo) Then
                Return False
            End If

            Dim cliente As New Cliente With {
                .ID = id,
                .Nombre = nombre,
                .Telefono = telefono,
                .Correo = correo
            }

            Return ClienteController.ActualizarCliente(cliente) > 0
        End Function

        ' Method to remove a client from the database, against the DAL (Data-Access-Layer)
        Public Shared Function EliminarCliente(id As Integer) As Boolean
            If id <= 0 Then Return False

            Return ClienteController.EliminarCliente(id) > 0
        End Function
    End Class
End Namespace

