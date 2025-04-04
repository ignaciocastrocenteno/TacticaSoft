Imports System.Data.SqlClient
Imports ASPNETWebForms.Models

Namespace DAL

    Public Class ClienteDAL
        Private Const ERROR_SQL_OPERATION = "Error al ejecutar la consulta SQL"

        Public Shared Function ObtenerClientes() As List(Of Cliente)
            Dim clientes As New List(Of Cliente)()

            Try
                ' Establishing connection with the SQL Server database
                Using conn As SqlConnection = Connection.GetDBConnection()
                    ' SQL query to read all the existing client registers
                    Dim query As String = "SELECT * FROM clientes"
                    Dim cmd As New SqlCommand(query, conn)
                    conn.Open()
                    Dim reader As SqlDataReader = cmd.ExecuteReader()

                    ' Looking up the information from the database and saving each 'Cliente' object to the list
                    While reader.Read()
                        clientes.Add(New Cliente() With {
                        .ID = If(Not IsDBNull(reader("ID")), Convert.ToInt32(reader("ID")), 0), ' Si es NULL, asigna 0
                        .Nombre = If(Not IsDBNull(reader("Cliente")), reader("Cliente").ToString(), String.Empty),
                        .Telefono = If(Not IsDBNull(reader("Telefono")), reader("Telefono").ToString(), String.Empty),
                        .Correo = If(Not IsDBNull(reader("Correo")), reader("Correo").ToString(), String.Empty)
                    })
                    End While
                End Using
            Catch ex As Exception
                Throw New Exception("Error al ejecutar la consulta SQL: " & ex.Message & " - InnerException: " & ex.InnerException?.Message, ex)
            End Try

            ' Returning the clients' list to be used in a View
            Return clientes
        End Function

        Public Shared Function InsertarCliente(cliente As Cliente) As Integer
            Dim rowsAffected As Integer = 0

            Try
                ' Establishing connection with the SQL Server database
                Using conn As SqlConnection = Connection.GetDBConnection()
                    ' SQL query to insert a new client within the database
                    Dim query As String = "INSERT INTO clientes (Cliente, Telefono, Correo) VALUES (@Nombre, @Telefono, @Correo)"
                    Dim cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre)
                    cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono)
                    cmd.Parameters.AddWithValue("@Correo", cliente.Correo)
                    conn.Open()
                    ' cmd.ExecuteNonQuery()

                    ' Saving up the amount of affected rows in the database to be returned as a value
                    rowsAffected = cmd.ExecuteNonQuery()
                End Using
            Catch ex As Exception
                Throw New Exception(ERROR_SQL_OPERATION, ex)
            End Try

            Return rowsAffected
        End Function
        Public Shared Function ModificarCliente(cliente As Cliente) As Integer
            Dim rowsAffected As Integer = 0
            Try
                Using conn As SqlConnection = Connection.GetDBConnection()
                    conn.Open()

                    ' Iniciar una transacción manualmente
                    Using transaction As SqlTransaction = conn.BeginTransaction()
                        Try
                            ' Verificar si el cliente existe antes de actualizarlo
                            Dim checkQuery As String = "SELECT COUNT(*) FROM clientes WHERE ID = @ID"
                            Using checkCmd As New SqlCommand(checkQuery, conn, transaction)
                                checkCmd.Parameters.AddWithValue("@ID", cliente.ID)
                                Dim exists As Integer = Convert.ToInt32(checkCmd.ExecuteScalar())
                                If exists = 0 Then
                                    Throw New Exception("El cliente con ID " & cliente.ID & " no existe en la base de datos.")
                                End If
                            End Using

                            ' Query para actualizar cliente
                            Dim query As String = "UPDATE clientes SET Cliente=@Nombre, Telefono=@Telefono, Correo=@Correo WHERE ID=@ID"
                            Using cmd As New SqlCommand(query, conn, transaction)
                                cmd.Parameters.AddWithValue("@ID", cliente.ID)
                                cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre)
                                cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono)
                                cmd.Parameters.AddWithValue("@Correo", cliente.Correo)

                                rowsAffected = cmd.ExecuteNonQuery()
                            End Using

                            ' Confirmar los cambios en la base de datos
                            transaction.Commit()

                            ' 🔍 Verificar si los cambios realmente se aplicaron en la base de datos
                            Dim testQuery As String = "SELECT Cliente, Telefono, Correo FROM clientes WHERE ID = @ID"
                            Using testCmd As New SqlCommand(testQuery, conn, transaction)
                                testCmd.Parameters.AddWithValue("@ID", cliente.ID)
                                Using reader As SqlDataReader = testCmd.ExecuteReader()
                                    If reader.Read() Then
                                        Debug.WriteLine("📌 Después del UPDATE en DB: " & reader("Cliente") & ", " & reader("Telefono") & ", " & reader("Correo"))
                                    Else
                                        Debug.WriteLine("❌ El SELECT no encontró el cliente, algo salió mal.")
                                    End If
                                End Using
                            End Using

                        Catch ex As Exception
                            ' Si algo falla, deshacer la transacción
                            transaction.Rollback()
                            Throw New Exception("Error en ModificarCliente: " & ex.Message, ex)
                        End Try
                    End Using
                End Using

            Catch ex As Exception
                Throw New Exception("Error en ModificarCliente: " & ex.Message, ex)
            End Try

            Return rowsAffected
        End Function


        Public Shared Function BorrarCliente(id As Integer) As Integer
            Dim rowsAffected As Integer = 0

            Try
                ' Establishing connection with the SQL Server database
                Using conn As SqlConnection = Connection.GetDBConnection()
                    ' SQL query to delete a client from the database
                    Dim query As String = "DELETE FROM clientes WHERE ID=@ID"
                    Dim cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ID", id)
                    conn.Open()
                    ' cmd.ExecuteNonQuery()

                    ' Saving up the amount of affected rows in the database to be returned as a value
                    rowsAffected = cmd.ExecuteNonQuery()

                    Debug.WriteLine("Cantidad de registros actualizados, luego del DELETE: " & rowsAffected)
                End Using
            Catch ex As Exception
                Throw New Exception(ERROR_SQL_OPERATION, ex)
            End Try

            Return rowsAffected
        End Function
        Public Shared Function ObtenerClientePorId(id As Integer) As Cliente
            Dim client As Cliente = Nothing
            Dim query As String = "SELECT ID, Cliente, Telefono, Correo FROM Clientes WHERE ID=@ID"

            Using conn As SqlConnection = Connection.GetDBConnection()
                Using cmd As New SqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@ID", id)

                    conn.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            client = New Cliente() With {
                        .ID = Convert.ToInt32(reader("ID")),
                        .Nombre = reader("Cliente").ToString(),
                        .Telefono = reader("Telefono").ToString(),
                        .Correo = reader("Correo").ToString()
                    }
                        End If
                    End Using
                End Using
            End Using

            Return client
        End Function

    End Class
End Namespace
