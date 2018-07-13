Imports System.Data
Imports System.Data.SqlClient

Public Class MSSQL
    Private con As New SqlConnection
    Private connected As Boolean = False

    Public Function Connect() As ConnectionState
        'tcp:servername, portnumber
        '(local)

        If con.State <> ConnectionState.Open Then con.ConnectionString = "server=" + My.Settings.mdbAddr + ";Initial Catalog=RBASE;Persist Security Info=True;User ID=" + My.Settings.mdbUser + ";Password='" + My.Settings.mdbUserPass + "'"
        Try
            con.Open()
        Catch ex As Exception
            FormServer.Log("Error: " & ex.Message.ToString() & " in MSSQL.vb. More Info:" & ex.ToString())
        End Try

        connected = con.State
        Return con.State
    End Function

    Public Function ExecQuery(ByVal query As String, ByRef values() As Object) As Integer
        If connected Then
            Dim command As SqlCommand, dataReader As SqlDataReader
            Try
                command = New SqlCommand(query, con)
                dataReader = command.ExecuteReader
            Catch ex As Exception
                FormServer.Log("Error: " & ex.Message.ToString() & " in MSSQL.vb. More Info:" & ex.ToString())
                Return False
            End Try

            If dataReader.Read = True Then
                ReDim values(dataReader.FieldCount - 1)
                Dim cnt As Integer = dataReader.GetValues(values)
                dataReader.Close()
                Return cnt
            Else
                dataReader.Close()
                Return -1
            End If
        Else
            Err.Raise(24563, Me, "Data Base is not opened")
            Return -1
        End If
    End Function

    Public Sub Disconnect()
        con.Close()
        connected = False
    End Sub

    Public ReadOnly Property ConState() As System.Data.ConnectionState
        Get
            Return con.State
        End Get
    End Property
End Class
