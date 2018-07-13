''' <summary>
''' This class does not need to be used - it was created only for this demo as an easy way to parse chat commands.
''' </summary>
Public Class DataPacket

    Private _command As String
    Public ReadOnly Property Command() As String
        Get
            Return _command
        End Get
    End Property

    Private _other As String
    Public ReadOnly Property Arguments(ByVal count As Integer) As String()
        Get
            Dim sep() As Char = {CType(" ", Char)}
            Dim tmp() As String = _other.Split(sep, count)
            Return tmp
        End Get
    End Property

    Public Sub New(ByVal data As String)
        If data Is Nothing Then
            Throw New ArgumentNullException("data")
        End If
        data = data.Trim()
        If data = "" Then
            Throw New ArgumentNullException("data")
        End If
        If data.StartsWith("/") Then
            Dim iSp As Integer = data.IndexOf(" ")
            If iSp = -1 Then
                _command = data.Substring(1)
                _other = ""
            Else
                _command = data.Substring(1, iSp - 1)
                _other = data.Substring(iSp + 1)
            End If
        Else
            _command = "chat"
            _other = data
        End If
    End Sub
End Class
