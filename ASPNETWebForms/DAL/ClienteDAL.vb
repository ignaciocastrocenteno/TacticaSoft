Imports System.Data.SqlClient
Imports ASPNETWebForms.Models

Namespace DAL
    Public Class ClienteDAL
        Private Const ERROR_SQL_OPERATION As String = "Error al ejecutar la consulta SQL"

        Public Shared Function ObtenerClientes() As List(Of Cliente)
            Dim clientes As New List(Of Cliente)()
            Dim query As String = "SELECT ID, Cliente, Telefono, Correo FROM clientes"

            Try
                Using conn As SqlConnection = Connection.GetDBConnection(),
                      cmd As New SqlCommand(query, conn)
                    conn.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            clientes.Add(New Cliente() With {
                                .ID = reader.GetInt32(0),
                                .Nombre = reader.GetString(1),
                                .Telefono = reader.GetString(2),
                                .Correo = reader.GetString(3)
                            })
                        End While
                    End Using
                End Using
            Catch ex As Exception
                Throw New Exception(ERROR_SQL_OPERATION, ex)
            End Try

            Return clientes
        End Function

        Public Shared Function InsertarCliente(cliente As Cliente) As Integer
            Dim query As String = "INSERT INTO clientes (Cliente, Telefono, Correo) VALUES (@Nombre, @Telefono, @Correo)"
            Return EjecutarQuery(query, cliente)
        End Function

        Public Shared Function ModificarCliente(cliente As Cliente) As Integer
            Dim query As String = "UPDATE clientes SET Cliente=@Nombre, Telefono=@Telefono, Correo=@Correo WHERE ID=@ID"
            Return EjecutarQuery(query, cliente, True)
        End Function

        Public Shared Function BorrarCliente(id As Integer) As Integer
            Dim rowsAffected As Integer = 0
            Dim query As String = "DELETE FROM clientes WHERE ID=@ID"

            Try
                Using conn As SqlConnection = Connection.GetDBConnection()
                    conn.Open()
                    Using transaction As SqlTransaction = conn.BeginTransaction(),
                          cmd As New SqlCommand(query, conn, transaction)

                        cmd.Parameters.AddWithValue("@ID", id)
                        rowsAffected = cmd.ExecuteNonQuery()
                        transaction.Commit()
                    End Using
                End Using
            Catch ex As Exception
                Throw New Exception(ERROR_SQL_OPERATION, ex)
            End Try

            Return rowsAffected
        End Function

        Public Shared Function ObtenerClientePorId(id As Integer) As Cliente
            Dim client As Cliente = Nothing
            Dim query As String = "SELECT ID, Cliente, Telefono, Correo FROM clientes WHERE ID=@ID"

            Try
                Using conn As SqlConnection = Connection.GetDBConnection(),
                      cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ID", id)
                    conn.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            client = New Cliente() With {
                                .ID = reader.GetInt32(0),
                                .Nombre = reader.GetString(1),
                                .Telefono = reader.GetString(2),
                                .Correo = reader.GetString(3)
                            }
                        End If
                    End Using
                End Using
            Catch ex As Exception
                Throw New Exception(ERROR_SQL_OPERATION, ex)
            End Try

            Return client
        End Function

        Private Shared Function EjecutarQuery(query As String, cliente As Cliente, Optional includeID As Boolean = False) As Integer
            Dim rowsAffected As Integer = 0

            Try
                Using conn As SqlConnection = Connection.GetDBConnection()
                    conn.Open()
                    Using transaction As SqlTransaction = conn.BeginTransaction(),
                          cmd As New SqlCommand(query, conn, transaction)

                        If includeID Then cmd.Parameters.AddWithValue("@ID", cliente.ID)
                        cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre)
                        cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono)
                        cmd.Parameters.AddWithValue("@Correo", cliente.Correo)

                        rowsAffected = cmd.ExecuteNonQuery()
                        transaction.Commit()
                    End Using
                End Using
            Catch ex As Exception
                Throw New Exception(ERROR_SQL_OPERATION, ex)
            End Try

            Return rowsAffected
        End Function
    End Class
End Namespace
