Imports System.Data.SqlClient
Imports System.Configuration

' Managing the connection with the SQL Server database instance for this project
Public Class Connection
    Public Shared Function GetDBConnection() As SqlConnection
        ' Reading the database connection String from the file 'App.config' and getting a SqlConnection object
        ' to generate SQL queries
        Return New SqlConnection(ConfigurationManager.ConnectionStrings("DatabaseConnection").ConnectionString)
    End Function
End Class

