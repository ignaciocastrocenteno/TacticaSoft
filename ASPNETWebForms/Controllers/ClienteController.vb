Imports ASPNETWebForms.DAL
Imports ASPNETWebForms.Models

Namespace Controllers
    ' CRUD (Create-Read-Update and Delete) for the Client entity
    ' Each method calls the 'ClienteDAL' layer to execute the database related logic
    Public Class ClienteController
        Public Shared Function ListarClientes() As List(Of Cliente)
            ' Calling the method 'GetClients', to obtain all the available client registers from the database
            Return ClienteDAL.ObtenerClientes()
        End Function
        Public Shared Function ObtenerClientePorID(id As Integer) As Cliente
            Return ClienteDAL.ObtenerClientePorId(id)
        End Function

        Public Shared Function AgregarCliente(cliente As Cliente) As Integer
            Return ClienteDAL.InsertarCliente(cliente)
        End Function

        Public Shared Function ActualizarCliente(cliente As Cliente) As Integer
            Return ClienteDAL.ModificarCliente(cliente)
        End Function

        Public Shared Function EliminarCliente(id As Integer) As Integer
            Return ClienteDAL.BorrarCliente(id)
        End Function

    End Class
End Namespace

