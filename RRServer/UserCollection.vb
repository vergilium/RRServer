Public Class UserCollection
    Inherits Hashtable

    Public Event _Remove(ByVal Item As Object)
    Private _isWebUser As Boolean = False

    Public Property isWebUser()
        Get
            Return _isWebUser
        End Get
        Set(ByVal value)
            _isWebUser = value
        End Set
    End Property

    Public Shadows Sub Add(ByVal gid As Guid)
        Dim x As New User(gid)
        x.UserName = GenerateName()
        MyBase.Add(gid, x)
    End Sub

    Public Shadows Sub Clear()
        RaiseEvent _Remove(MyBase.Count)
        MyBase.Clear()
    End Sub

    Public Shadows Sub Remove(ByVal ip As String)
        For Each usr As User In Me.Values
            If usr.UserName = ip Then
                MyBase.Remove(usr.wskGUID)
                RaiseEvent _Remove(MyBase.Item(Me.FindUsr(ip)))
            End If
        Next
    End Sub

    Public Shadows Sub Remove(ByVal gid As Guid)

        RaiseEvent _Remove(MyBase.Item(gid))
        MyBase.Remove(gid)
    End Sub

    Private Function GenerateName() As String
        Dim base As String = "Guest"
        For i As Integer = 1 To Me.Count
            If Not HasName(base & i) Then Return base & i
        Next
        Return base
    End Function
    Public Function GetUsrByName(ByVal name As String) As User
        For Each usr As User In Me.Values
            If usr.UserName = name Then Return usr
        Next
        Return Nothing
    End Function
    Public Function FindUsr(ByVal name As String, Optional ByVal case_sensitive As Boolean = False) As Guid
        For Each usr As User In Me.Values
            If case_sensitive Then
                If usr.UserName = name Then Return usr.wskGUID
            Else
                If usr.UserName.ToLower = name.ToLower Then Return usr.wskGUID
            End If
        Next
        Return Guid.Empty
    End Function

    Public Function HasName(ByVal name As String) As Boolean
        For Each usr As User In Me.Values
            If usr.UserName = name Then Return True
        Next
        Return False
    End Function

End Class


Public Class User
    Public Enum UserTypes
        UnknownClient = 0
        HookConService = 100
        ClientPlayer = 1
        WebServer = 500
    End Enum

    Public Sub New(ByVal wsk As Guid)
        _wsk = wsk
        _loggedIn = False
    End Sub

#Region " HookCon Propertyes "
    Private _errN As Integer = 0
    Private _errStr As String = ""
    Public Property errNum() As Integer
        Get
            Return _errN
        End Get
        Set(ByVal value As Integer)
            _errN = value
        End Set
    End Property
    Public Property errString() As String
        Get
            Return _errStr
        End Get
        Set(ByVal value As String)
            _errStr = value
        End Set
    End Property
#End Region

    Private _uType As UserTypes
    Public Property uType()
        Get
            Return _uType
        End Get
        Set(ByVal value)
            _uType = value
        End Set
    End Property

    Private _name As String
    Public Property UserName() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Private _wsk As Guid
    Public ReadOnly Property wskGUID() As Guid
        Get
            Return _wsk
        End Get
    End Property

    Private _loggedIn As Boolean
    Public ReadOnly Property IsLoggedIn() As Boolean
        Get
            Return _loggedIn
        End Get
    End Property

    Private _IP As String
    Private _TRM As String
    Private _port As Integer
    Public Property IP() As String
        Get
            Return _IP
        End Get
        Set(ByVal value As String)
            _IP = value
        End Set
    End Property
    Public Property TRM() As String
        Get
            Return _TRM
        End Get
        Set(ByVal value As String)
            _TRM = value
        End Set
    End Property
    Public Property Port() As Integer
        Get
            Return _port
        End Get
        Set(ByVal value As Integer)
            _port = value
        End Set
    End Property

    Public Sub Login(ByVal user As String, ByVal password As String)
        _loggedIn = True
    End Sub

#Region "Player Propertyes"
    Private _bets As String
    Public Property Bets() As String
        Get
            Return _bets
        End Get
        Set(ByVal value As String)
            _bets = value
        End Set
    End Property

    Private _winValue As Integer
    Public Property WinValue() As Integer
        Get
            Return _winValue
        End Get
        Set(ByVal value As Integer)
            _winValue = value
        End Set
    End Property

    Private _credit As Integer
    Public Property Credit() As Integer
        Get
            Return _credit
        End Get
        Set(ByVal value As Integer)
            _credit = value
        End Set
    End Property
#End Region
End Class