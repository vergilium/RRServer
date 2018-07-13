Imports System.Net
Imports System.Net.Sockets
Imports RRServer.My.Resources
Imports RRServer.MSSQL

Public Class FormServer

#Region " Need Values "
    Public silentMode As Boolean = False
    Public firstRun As Boolean = True

    Private WithEvents _wsks As New Winsock_Orcas.WinsockCollection(True, True)
    Private _users As New UserCollection
    Private WithEvents _wusers As New UserCollection

    Private sqlBase As New MSSQL
    Private allNums As New LastNumbers

    Public Enum LastGameResultEnum
        GameStart
        GameEnd
        GameWin
    End Enum

    Public Enum ConDiscon
        conNull = 0
        conConnect = 1
        conDisconnect = -1
    End Enum
#End Region

#Region " Form Routines "

    Private Sub DisconnectClientToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisconnectClientToolStripMenuItem.Click
        Dim r As String = InputBox("Disconnect reason (0 - no reason)", , "0")
        If Not r.Length = 0 Then DisconnectFromServer(r)
    End Sub

    Private Sub EditClientOptionsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditClientOptionsToolStripMenuItem.Click

    End Sub

    Private Sub NotifyIcon_MouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon.MouseDoubleClick
        ShowWindowToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub FormServer_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If wskListener.State = Winsock_Orcas.WinsockStates.Listening Then
            Me.Hide()
            e.Cancel = True
        End If
    End Sub

    Private Sub FormServer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        NotifyIcon.Text = "RRServer " & GetVersion()
        StartServer()
    End Sub

    Private Sub StopServerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopServerToolStripMenuItem.Click
        If MsgBox("Really stop the server?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub
        StartServer(ConDiscon.conDisconnect)
    End Sub

    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        If wskListener.State = Winsock_Orcas.WinsockStates.Listening Then
            MsgBox("Server already running", MsgBoxStyle.Information)
            Exit Sub
        End If
        StartServer(ConDiscon.conConnect)
    End Sub

    Private Sub FormServer_MinimumSizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MinimumSizeChanged
        Me.Hide()
    End Sub

    Private Sub ShowWindowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowWindowToolStripMenuItem.Click
        Me.Show()
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        If wskListener.State = Winsock_Orcas.WinsockStates.Listening Then MsgBox("First stops the Server!", MsgBoxStyle.Exclamation)
        Me.Close()
    End Sub

    Private Sub ShowDetailInfoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowDetailInfoToolStripMenuItem.Click
        showUInfo()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ListLog.Items.Clear()
    End Sub
#End Region

#Region " Helper Routines "

    Private lastGame As Long, lastGameResult As LastGameResultEnum

    Public Sub toolMsg(ByVal Text As String, Optional ByVal Icon As System.Windows.Forms.ToolTipIcon = ToolTipIcon.Info, Optional ByVal ShowTime As Integer = 5000)
        If silentMode = True Then Exit Sub
        NotifyIcon.BalloonTipText = Text
        NotifyIcon.BalloonTipIcon = Icon
        Select Case Icon
            Case ToolTipIcon.Info, ToolTipIcon.None
                NotifyIcon.BalloonTipTitle = "Remote Roulet Server"
            Case ToolTipIcon.Error
                NotifyIcon.BalloonTipTitle = "Remote Roulet Server - Error"
            Case ToolTipIcon.Warning
                NotifyIcon.BalloonTipTitle = "Remote Roulet Server - Warning"
        End Select

        NotifyIcon.ShowBalloonTip(ShowTime)
    End Sub

    Public Function GetVersion() As String
        Return "v: " & Application.ProductVersion
    End Function

    Private Function toDate(ByVal String2Convert As String, ByRef ReturnDate As Date, Optional ByVal dataformat As String = "MM/dd/yyyy") As Boolean

        If (String2Convert.Trim = "") Then
            Return False
        End If
        Dim myDate As Date

        Try
            myDate = Date.ParseExact(String2Convert, dataformat, System.Globalization.DateTimeFormatInfo.InvariantInfo)
            ReturnDate = myDate
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Private Sub StartServer(Optional ByVal Disconnect As ConDiscon = ConDiscon.conNull)
        If Disconnect = ConDiscon.conNull Then
            Select Case wskListener.State
                Case Winsock_Orcas.WinsockStates.Closed
                    wskListener.Listen(16666)

                    lblPort.Text = String.Format("Port: {0}", 16666)
                    ConnSQL()
                    Log("Server is running. Waiting for incoming connections.")
                Case Winsock_Orcas.WinsockStates.Listening
                    wskListener.Close()
                    Log("Server stopped! All clients disconnected.")
            End Select

        ElseIf Disconnect = ConDiscon.conDisconnect Then
            wskListener.Close()
            Dim i As Integer
            For i = 0 To _wsks.Count - 1
                _wsks(i).Close()
            Next

            Log("Server stopped! All clients disconnected.")
        ElseIf Disconnect = ConDiscon.conConnect Then
            wskListener.Listen(16666)
            ConnSQL()
            lblPort.Text = String.Format("Port: {0}", 16666)
            Log("Server is running. Waiting for incoming connections.")
        End If
    End Sub

    Private Sub ConnSQL()
        Dim f As Boolean
        If sqlBase.ConState <> True Then
            f = sqlBase.Connect()
        Else
            f = True
        End If
        If f = True Then
            ' GOOD@)
        Else
            'MsgBox("Не удается подключиться к БД")
            Log("Can't connect to MSSQL Database")
        End If
    End Sub

    Private Sub UpdateUserList()
        lstUsers.Items.Clear()
        For Each usr As User In _users.Values
            Dim lvi As ListViewItem = lstUsers.Items.Add(usr.UserName)

            lvi.Tag = usr.wskGUID
            Dim lvsi1 As New ListViewItem.ListViewSubItem, lvsi2 As New ListViewItem.ListViewSubItem
            If usr.errNum = 0 Then
                lvsi1.Text = "0"
                lvsi2.Text = "Online"
                lvi.ImageKey = "green"
            Else
                lvi.ImageKey = "red"
                lvsi1.Text = usr.errNum.ToString
                lvsi2.Text = usr.errString
                lvsi2.BackColor = Color.Red
            End If
            lvi.SubItems.Add(lvsi1)
            lvi.SubItems.Add(lvsi2)
        Next

        For Each usr As User In _wusers.Values
            Dim lvi As ListViewItem = lstUsers.Items.Add(usr.UserName)

            lvi.Tag = usr.wskGUID
            Dim lvsi1 As New ListViewItem.ListViewSubItem, lvsi2 As New ListViewItem.ListViewSubItem
            If usr.uType = User.UserTypes.ClientPlayer Then
                lvsi1.Text = "0"
                lvsi2.Text = "Online"
                lvi.ImageKey = "clients_"
            End If
            lvi.SubItems.Add(lvsi1)
            lvi.SubItems.Add(lvsi2)
        Next

        'Dim i As Integer
        'For i = 0 To lstUsers.Items.Count-1

        'Next
    End Sub

    Public Sub showUInfo()
        If lstUsers.SelectedIndices.Count > 0 Then
            Dim usr As User = _users.Values(lstUsers.SelectedIndices(0))
            If IsNothing(usr) = True Then
                usr = _wusers.GetUsrByName(lstUsers.Items(lstUsers.SelectedIndices(0)).Text)
            End If
            Dim msg = "Client name: " & usr.UserName & vbNewLine
            msg += "ID: " & usr.wskGUID.ToString & vbNewLine
            msg += "------ ------ ------ ------" & vbNewLine
            msg += "IP: " & usr.IP & vbNewLine
            msg += "port: " & usr.Port & vbNewLine
            msg += "Client type: " & usr.uType.ToString & vbNewLine
            If usr.uType = User.UserTypes.ClientPlayer Then
                msg += "TRM:" & usr.TRM & vbNewLine
                msg += "Credit:" & usr.Credit & vbNewLine
                msg += "LoggedIn:" & usr.IsLoggedIn.ToString
            End If
            MsgBox(msg, MsgBoxStyle.Information)
        End If
    End Sub

    Sub DisconnectFromServer(ByVal r As String)
        Dim usr As User = _users.Values(lstUsers.SelectedIndices(0))

        If usr Is Nothing Then
            For Each ausr As User In _users.Values
                If ausr.uType = User.UserTypes.WebServer Then
                    Dim lvi As ListViewItem = lstUsers.Items(lstUsers.SelectedIndices(0))
                    SendMsg(ausr.wskGUID, "/DISCONCLI " & _wusers.GetUsrByName(lvi.Text).IP & " " & IIf(r = "0", "no reason", r))
                    Exit Sub
                End If
            Next
        End If

        If usr.uType = User.UserTypes.WebServer Then
            _wsks(usr.wskGUID).Send("/DISCONNECT " & IIf(r = "0", "no reason", r))
        End If

        '_wsks(usr.wskGUID).Close()
    End Sub

    Public Sub Log(ByVal msg As String)
        msg = DateTime.Now.ToString("dd.MM.yy hh:mm:ss") & " >> " & msg
        If CheckBoxAdd.Checked = True Then
            ListLog.Items.Add(msg)
            ListLog.SelectedIndex = ListLog.Items.Count - 1
        End If
        IO.File.AppendAllText(Application.StartupPath & "\RRServer_" & DateTime.Now.ToString("dd_MM_yy") & ".log", msg & vbNewLine)
    End Sub

    Private Sub ChangeName(ByVal ID As Object, ByVal new_name As String)
        Dim i As Integer
        For i = 0 To lstUsers.Items.Count - 1
            Dim lvi As ListViewItem = lstUsers.Items(i)
            If lvi.Tag = ID Then
                lvi.Text = new_name
                Exit Sub
            End If
        Next
    End Sub

    Private Sub ChangeLight(ByVal ID As String, ByVal light As Color)
        Dim i As Integer, col As String = ""
        For i = 0 To lstUsers.Items.Count - 1
            Dim lvi As ListViewItem = lstUsers.Items(i)
            If lvi.Tag = ID Then
                Select Case light
                    Case Color.Red
                        col = "red"
                    Case Color.Green
                        col = "green"
                    Case Color.Gray
                        col = "grey"
                End Select
                lvi.ImageKey = col
            End If
        Next i
    End Sub

    Private Sub ChangeErr(ByVal ID As Object, ByVal Errors As String, Optional ByVal Closed As Boolean = False)
        Dim i As Integer
        For i = 0 To lstUsers.Items.Count - 1
            Dim lvi As ListViewItem = lstUsers.Items(i)
            If lvi.Tag = ID Then
                Dim lvsi1 As ListViewItem.ListViewSubItem = lvi.SubItems(1)
                Dim lvsi2 As ListViewItem.ListViewSubItem = lvi.SubItems(2)

                If Closed = False And Errors.Length > 0 And Errors <> "!client" Then
                    lvsi1.Text = UBound(Split(Errors, ","))
                    lvsi2.Text = Errors
                    lvsi2.BackColor = Color.Red
                    lvi.ImageKey = "red"
                ElseIf Closed = True And Errors <> "!client" Then
                    lvsi1.Text = "1"
                    lvsi2.Text = "Roulette server is down!"
                    lvsi2.BackColor = Color.Red
                    lvi.ImageKey = "grey"
                ElseIf Errors.Length = 0 Then
                    lvsi1.Text = "0"
                    lvsi2.Text = "None"
                    lvsi2.BackColor = Color.White
                    lvi.ImageKey = "green"
                End If
            End If
        Next
    End Sub

    Private Sub ChangeOnline(ByVal ID As Object, ByVal online As Boolean)
        Dim i As Integer
        For i = 0 To lstUsers.Items.Count - 1
            Dim lvi As ListViewItem = lstUsers.Items(i)
            If lvi.Tag.ToString() = ID.ToString() Then
                Dim lvsi1 As ListViewItem.ListViewSubItem = lvi.SubItems(1)
                Dim lvsi2 As ListViewItem.ListViewSubItem = lvi.SubItems(2)

                lvsi2.Text = IIf(online = True, "Online", "Offline")
                lvsi2.BackColor = Color.White
                lvi.ImageKey = IIf(online = True, "clients_", "clients_off")
            End If
        Next
    End Sub

    Private Function IsLoggedOn(ByVal ID As Object) As Boolean
        For Each usr As User In _wusers.Values
            If usr.IP = ID Then
                Return True
            End If
        Next
        Return False
    End Function
#End Region

#Region " Send Methods "

    Private Sub SendToAll(ByVal msg As String)
        For Each usr As User In _users.Values
            If usr.IsLoggedIn AndAlso _wsks.Item(usr.wskGUID).State = Winsock_Orcas.WinsockStates.Connected Then _wsks.Item(usr.wskGUID).Send(msg)
        Next
    End Sub

    Private Sub SendToAllBut(ByVal gid As Guid, ByVal msg As String)
        For Each usr As User In _users.Values
            If usr.wskGUID <> gid AndAlso usr.IsLoggedIn AndAlso _wsks.Item(usr.wskGUID).State = Winsock_Orcas.WinsockStates.Connected Then _wsks.Item(usr.wskGUID).Send(msg)
        Next
    End Sub

    Private Sub SendMsg(ByVal gid As Guid, ByVal msg As String)
        _wsks.Item(gid).Send(msg)
    End Sub

#End Region

#Region " Listener "

    Private Sub wskListener_Connected(ByVal sender As Object, ByVal e As Winsock_Orcas.WinsockConnectedEventArgs) Handles wskListener.Connected
        toolMsg("Client successfully connected from " & e.SourceIP)
        Log("Client successfully connected from " & e.SourceIP)
    End Sub

    Private Sub wskListener_ConnectionRequest(ByVal sender As Object, ByVal e As Winsock_Orcas.WinsockConnectionRequestEventArgs) Handles wskListener.ConnectionRequest
        Dim gid As Guid = _wsks.Accept(e.Client)
        _users.Add(gid)
        UpdateUserList()
    End Sub

    Private Sub wskListener_ErrorReceived(ByVal sender As System.Object, ByVal e As Winsock_Orcas.WinsockErrorReceivedEventArgs) Handles wskListener.ErrorReceived
        Dim f As New frmError()
        f.ShowDialog(e.Message, "Remote Roulet Server", e.Function & vbCrLf & e.Details, Me)
    End Sub

    Private Sub wskListener_StateChanged(ByVal sender As Object, ByVal e As Winsock_Orcas.WinsockStateChangedEventArgs) Handles wskListener.StateChanged
        lblStatus.Text = String.Format("Status: {0}", e.New_State.ToString)
    End Sub

#End Region

#Region " Winsock Collection Handlers "
    Private _lastUser As User = Nothing

    Private Sub _wsks_ErrorReceived(ByVal sender As System.Object, ByVal e As Winsock_Orcas.WinsockErrorReceivedEventArgs) Handles _wsks.ErrorReceived

        Select Case e.ErrorCode
            Case SocketError.ConnectionReset
                Log("[" & _lastUser.UserName & "] : Disconnected. Reason: " & e.Message.ToString)
                _lastUser = Nothing
                Exit Sub
        End Select

        Dim f As New frmError()
        f.ShowDialog(e.Message, "Remote Roulet Server", e.Function & vbCrLf & e.Details & vbCrLf & e.ToString, Me)
    End Sub

    Private Sub wskCountChanged(ByVal sender As Object, ByVal e As Winsock_Orcas.WinsockCollectionCountChangedEventArgs) Handles _wsks.CountChanged
        lblClients.Text = String.Format("Clients: {0}    ", e.NewCount)
    End Sub

    Private Sub _wsks_Connected(ByVal sender As Object, ByVal e As Winsock_Orcas.WinsockConnectedEventArgs) Handles _wsks.Connected
        Log("Client was connected: " & e.SourceIP)
    End Sub

    Private Sub _wsks_Disconnected(ByVal sender As Object, ByVal e As System.EventArgs) Handles _wsks.Disconnected
        Dim gid As Guid = _wsks.findGID(CType(sender, Winsock_Orcas.Winsock))
        Dim usr As User = CType(_users(gid), User)
        _lastUser = usr
        Dim nm2Rem As String = usr.UserName
        If usr.uType = User.UserTypes.WebServer Then
            Try
                _wusers.Clear()
            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Exclamation)
            End Try
        End If
        Try
            _users.Remove(gid)
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Exclamation)
        End Try
        Log("[" & nm2Rem & "] : Disconnected. Reason: " & e.ToString & ". All Web-clients disconnected.")
        UpdateUserList()
        'SendToAll(String.Format("/r_lst {0}", nm2Rem))
    End Sub

    Private Sub _wsks_DataArrival(ByVal sender As Object, ByVal e As Winsock_Orcas.WinsockDataArrivalEventArgs) Handles _wsks.DataArrival
        Dim gid As Guid = _wsks.findGID(CType(sender, Winsock_Orcas.Winsock))
        Dim usr As User = CType(_users(gid), User)

        Dim obj As Object = _wsks.Item(gid).Get()
        'Dim s As String = CStr(obj)
        Dim s As String = CStr(System.Text.Encoding.UTF8.GetString(obj))
        Dim dp As New DataPacket(s)

        If wskListener.State = Winsock_Orcas.WinsockStates.Closed Then Exit Sub

        Select Case dp.Command
            Case "SQL"
                Dim args() As String = dp.Arguments(2)
                Select Case args(0)
                    Case "ADD"
                        args = args(1).Split(";")
                        Dim q1 As String = "SELECT term_credit FROM terminals WHERE term_number='" & args(0) & "'"
                        Dim r() As Object
                        sqlBase.ExecQuery(q1, r)
                        Dim oldcr As Integer = Val(r(0))
                        ' =====================================================================================
                        Dim q As String = _
"UPDATE terminals" & vbNewLine & _
     "SET term_credit = term_credit + " & args(1) & ", term_in = term_in +" & args(1) & vbNewLine & _
      "WHERE term_number = '" & args(0) & "';" & vbNewLine & _
     "INSERT INTO oper_term (date, geme_number, terminal_number, event, value, id_cashier)" & vbNewLine & _
      "VALUES (GETDATE(), 0, '" & args(0) & "', 'add_credit', '" & args(1) & "', '" & args(2) & "')"
                        ' =====================================================================================
                        Dim ret(0) As Object, i As Integer
                        i = sqlBase.ExecQuery(q, ret)

                        SendMsg(usr.wskGUID, "/SET_CR " & args(0) & " " & Val(args(1)) + oldcr)


                    Case "TAKE"
                        args = args(1).Split(";")
                        ' =====================================================================================
                        Dim q As String = _
"DECLARE @crd int" & vbNewLine & _
     "SET @crd = (SELECT term_credit FROM terminals WHERE term_number = '" & args(0) & "');" & vbNewLine & _
     "IF @crd > 0 BEGIN" & vbNewLine & _
      "UPDATE terminals" & vbNewLine & _
       "SET term_credit = 0, term_out = term_out + @crd" & vbNewLine & _
        "WHERE term_number = '" & args(0) & "';" & vbNewLine & _
     "INSERT INTO oper_term (date, geme_number, terminal_number, event, value, id_cashier)" & vbNewLine & _
      "VALUES (GETDATE(), 0, '" & args(0) & "', 'clear_credit', @crd, '" & args(1) & "')" & vbNewLine & _
     "END" & vbNewLine & _
     "ELSE" & vbNewLine & _
      "PRINT 'zero'"
                        ' =====================================================================================
                        Dim ret(0) As Object, i As Integer
                        i = sqlBase.ExecQuery(q, ret)
                        SendMsg(usr.wskGUID, "/SET_CR " & args(0) & " 0")
                    Case "CHANGE_CR"
                        args = args(1).Split(";")

                        If UBound(args) <> 1 Then Return
                        If args(0).Length = 0 Or args(1) = 0 Then Return

                        Dim q As String = _
                        "UPDATE terminals" & vbNewLine & _
"SET term_credit = " & args(1).ToString() & vbNewLine & _
"WHERE term_number= '" & args(0) & "'"
                        Dim ret(0) As Object, i As Integer
                        i = sqlBase.ExecQuery(q, ret)

                End Select
                ' запрос количества кредитов на терминале
            Case "GET_CR"
                Dim args() As String = dp.Arguments(1)
                Dim ret As Object, q As String = "SELECT term_credit FROM terminals WHERE term_number = '" & args(0) & "'"
                sqlBase.ExecQuery(q, ret)
                If (UBound(args) = 0 And IsArray(ret) = True) Then
                    SendMsg(usr.wskGUID, "/SET_CR " & args(0) & " " & ret(0).ToString)
                Else
                    SendMsg(usr.wskGUID, "err no_user")
                    Log("Can't find credit for trm:[" & args(0) & "]")
                End If

                ' подключение веб-клиента
            Case "WCCON"
                Dim args() As String = dp.Arguments(2)
                Dim nGUID As System.Guid = System.Guid.NewGuid()

                If IsLoggedOn(args(0)) = True Then
                    ChangeOnline(args(0), True)
                Else
                    _wusers.Add(nGUID)
                    'Dim wusr As User = _wusers(nGUID)
                    Dim wusr As User = New User(nGUID)

                    wusr.UserName = args(0)
                    wusr.Login("", "")
                    wusr.uType = User.UserTypes.ClientPlayer
                    wusr.IP = args(0)

                    If args.Length = 2 Then
                        wusr.TRM = args(1)
                    Else
                        wusr.TRM = "of0001"
                    End If

                    ChangeName(wusr.wskGUID, "  Terminal " & args(0))
                    SendMsg(usr.wskGUID, "/LOK True")
                    Log(wusr.UserName & " [logon]")
                    _wusers(nGUID) = wusr
                    UpdateUserList()
                End If

                ' отключение веб-клиента
            Case "WCDISCON"
                ''  WCDISCON <IP> <LOGOFF/OFFLINE>
                Dim args() As String = dp.Arguments(2)
                For Each usr In _wusers.Values
                    If usr.IP = args(0) Then
                        If args(1) = "LOGOFF" Then
                            _wusers.Remove(usr.wskGUID)
                            UpdateUserList()
                            Exit For
                            'ChangeOnline(usr.wskGUID, False)
                        ElseIf args(1) = "OFFLINE" Then
                            ChangeOnline(usr.wskGUID, False)
                            Exit For
                        End If
                    End If
                Next
                Log(args(0) & " [logoff]")

                ' сообщение веб-клиента
            Case "WCMSG"

                ' 
            Case "INITWS"
                Dim args() As String = dp.Arguments(4)

                usr.UserName = "Web Server"
                usr.Login("", "")
                usr.uType = User.UserTypes.WebServer

                usr.IP = args(0)
                usr.Port = CType(args(1), Integer)

                ChangeName(usr.wskGUID, "Web Server")
                SendMsg(usr.wskGUID, "/LOK True")
                'SendMsg(_users.FindUsr("web server"), "/LAST_GAMES " & allNums.GetStr())
                SendMsg(usr.wskGUID, "/INITDATA " & allNums.GetStr())

            Case "LAST_GAMES"
                Dim args() As String = dp.Arguments(1)
                Dim nums() As String, i As Integer
                Log("HookCon LastNums:" & args(0))
                nums = args(0).Split(",")
                If UBound(nums) >= 0 Then
                    For i = 0 To UBound(nums) - 1
                        allNums.Add(CInt(nums(i)))
                    Next
                    Log("Server LastNums:" & allNums.GetStr())
                End If

            Case "INIT"
                Dim args() As String = dp.Arguments(4)

                usr.UserName = args(0)
                usr.Login("", "")
                usr.uType = CType(args(1), Integer)

                usr.IP = args(2)
                usr.Port = CType(args(3), Integer)

                ChangeName(usr.wskGUID, args(0))
                SendMsg(usr.wskGUID, "/LOK True")

                If usr.uType = User.UserTypes.HookConService Then
                    SendMsg(usr.wskGUID, "/NEED_GAMES")
                End If

            Case "ERR_PROGRAM"
                Dim args() As String = dp.Arguments(1)
                usr.errNum = UBound(Split(args(0), ","))
                usr.errString = args(0)
                ChangeErr(usr.wskGUID, args(0))

            Case "ERR_CLOSED"
                If usr.errNum = -1 Then Exit Select
                usr.errNum = -1
                usr.errString = "The Roulet Server Is OFF!"
                ChangeErr(usr.wskGUID, "", True)
                Log("[" & usr.UserName & "] : Roulette server is down!")

            Case "ERR_INSIDE"
                Dim args() As String = dp.Arguments(1)
                Log("[" & usr.UserName & "] : Inside error : " & args(0))

            Case "PROGRAM_LOADED"
                If usr.errNum = 0 Then Exit Select
                usr.errNum = 0
                usr.errString = "Включен"
                ChangeErr(usr.wskGUID, "")
                Log("[" & usr.UserName & "] : Roulette server is running!")

            Case "WIN_EVENT"
                Dim args() As String = dp.Arguments(2)
                Log("[" & usr.UserName & "] : Game#" & args(1) & " Win Number: [" & args(0) & "]")
                lastGame = CType(args(1), Long)
                lastGameResult = LastGameResultEnum.GameWin
                SendMsg(usr.wskGUID, "/GWIN True")
                SendToAllBut(usr.wskGUID, "/GWIN True " & args(0))
                allNums.Add(CInt(args(0)))

            Case "STOP_EVENT"
                Dim args() As String = dp.Arguments(1)
                Log("[" & usr.UserName & "] : Game#" & args(0) & " End")

                If lastGameResult <> LastGameResultEnum.GameWin And firstRun = False Then
                    Log("Last Game Win Result has been lost. Sending repeated request")
                    SendMsg(usr.wskGUID, "/NEED_LAST_RESULT " & lastGame.ToString)
                    Exit Select
                End If

                lastGame = CType(args(0), Long)
                lastGameResult = LastGameResultEnum.GameEnd
                SendMsg(usr.wskGUID, "/WSTOP True")
                SendToAllBut(usr.wskGUID, "/WSTOP")

            Case "LAST_RESULT"
                Dim args() As String = dp.Arguments(2)
                Dim retVal As Long = CType(args(0), Long)
                If retVal = 0 Then Exit Select
                If retVal = "-1" Then
                    Log("Game Timeout")
                    Exit Select
                End If
                Log("[" & usr.UserName & "] : Game#" & args(1) & " Win Number: [" & args(0) & "]")
                lastGame = CType(args(1), Long)
                lastGameResult = LastGameResultEnum.GameWin
                SendMsg(usr.wskGUID, "/GWIN True " & args(0))

            Case "START_EVENT"
                Dim args() As String = dp.Arguments(1)
                Log("[" & usr.UserName & "] : Game#" & args(0) & " Start")
                lastGame = CType(args(0), Long)
                lastGameResult = LastGameResultEnum.GameStart
                SendMsg(usr.wskGUID, "/WRUN True")
                SendToAllBut(usr.wskGUID, "/WRUN")
                firstRun = False

            Case "STOP_SERVER"
                _wusers.Clear()
                UpdateUserList()
                Log("[" & usr.UserName & "] : WebSocket Service was Stopped. All Web-clients disconnected.")

            Case "BETS"
                Dim args() As String = dp.Arguments(2)
                If args(1).Length > 0 Then
                    usr.Bets = args(1)
                    Log("WebClient [" + args(0) + "] made a next bet(s)" & args(1))
                End If
            Case "DEBUG"
                Dim args() As String = dp.Arguments(1)
                Log("[" & usr.UserName & "] : Debug information: " & args(0))

        End Select
    End Sub

#End Region

    'Private Sub wskWebListener_ConnectionRequest(ByVal sender As Object, ByVal e As Winsock_Orcas.WinsockConnectionRequestEventArgs)
    '    Dim gid As Guid = _wsks.Accept(e.Client)
    '    _users.Add(gid)
    '    UpdateUserList()
    'End Sub

    'Private Sub wskWebListener_ErrorReceived(ByVal sender As System.Object, ByVal e As Winsock_Orcas.WinsockErrorReceivedEventArgs)

    'End Sub

    Private Sub _wusers__Remove(ByVal Item As Object) Handles _wusers._Remove
        Try
            Dim usr As User = CType(_wusers(Item), User)
            Log("[" & usr.UserName & "] : Disconnected.")
        Catch ex As Exception

        End Try
    End Sub
End Class
