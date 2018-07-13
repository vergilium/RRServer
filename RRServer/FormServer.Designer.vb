<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormServer
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormServer))
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup3 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left)
        Me.ListLog = New System.Windows.Forms.ListBox
        Me.NotifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.TrayContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.StopServerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator
        Me.ShowWindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.chScroll = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lstUsers = New System.Windows.Forms.ListView
        Me.ch1 = New System.Windows.Forms.ColumnHeader("clients_")
        Me.ch2 = New System.Windows.Forms.ColumnHeader("error_")
        Me.ch3 = New System.Windows.Forms.ColumnHeader("caution_")
        Me.UsersMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ShowDetailInfoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator
        Me.EditClientOptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DisconnectClientToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CliImages = New System.Windows.Forms.ImageList(Me.components)
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.lblStatus = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblClients = New System.Windows.Forms.ToolStripStatusLabel
        Me.lblPort = New System.Windows.Forms.ToolStripStatusLabel
        Me.Label2 = New System.Windows.Forms.Label
        Me.wskListener = New Winsock_Orcas.Winsock
        Me.CheckBoxAdd = New System.Windows.Forms.CheckBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.TrayContextMenu.SuspendLayout()
        Me.UsersMenuStrip.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListLog
        '
        Me.ListLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListLog.FormattingEnabled = True
        Me.ListLog.Location = New System.Drawing.Point(15, 183)
        Me.ListLog.Name = "ListLog"
        Me.ListLog.Size = New System.Drawing.Size(533, 290)
        Me.ListLog.TabIndex = 0
        '
        'NotifyIcon
        '
        Me.NotifyIcon.ContextMenuStrip = Me.TrayContextMenu
        Me.NotifyIcon.Icon = CType(resources.GetObject("NotifyIcon.Icon"), System.Drawing.Icon)
        Me.NotifyIcon.Visible = True
        '
        'TrayContextMenu
        '
        Me.TrayContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.StopServerToolStripMenuItem, Me.ToolStripMenuItem2, Me.ShowWindowToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.TrayContextMenu.Name = "TrayContextMenu"
        Me.TrayContextMenu.Size = New System.Drawing.Size(151, 98)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Image = Global.RRServer.My.Resources.Resources.bright_next_16
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(150, 22)
        Me.ToolStripMenuItem1.Text = "Start Server"
        '
        'StopServerToolStripMenuItem
        '
        Me.StopServerToolStripMenuItem.Image = Global.RRServer.My.Resources.Resources.bright_info_16
        Me.StopServerToolStripMenuItem.Name = "StopServerToolStripMenuItem"
        Me.StopServerToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.StopServerToolStripMenuItem.Text = "Stop Server"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(147, 6)
        '
        'ShowWindowToolStripMenuItem
        '
        Me.ShowWindowToolStripMenuItem.Image = Global.RRServer.My.Resources.Resources.chat_16
        Me.ShowWindowToolStripMenuItem.Name = "ShowWindowToolStripMenuItem"
        Me.ShowWindowToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.ShowWindowToolStripMenuItem.Text = "Show Window"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Image = Global.RRServer.My.Resources.Resources.whois_close_16
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'chScroll
        '
        Me.chScroll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chScroll.AutoSize = True
        Me.chScroll.Checked = True
        Me.chScroll.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chScroll.Location = New System.Drawing.Point(478, 167)
        Me.chScroll.Name = "chScroll"
        Me.chScroll.Size = New System.Drawing.Size(70, 17)
        Me.chScroll.TabIndex = 3
        Me.chScroll.Text = "ScrollLog"
        Me.chScroll.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Users List:"
        '
        'lstUsers
        '
        Me.lstUsers.AllowColumnReorder = True
        Me.lstUsers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstUsers.AutoArrange = False
        Me.lstUsers.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ch1, Me.ch2, Me.ch3})
        Me.lstUsers.ContextMenuStrip = Me.UsersMenuStrip
        Me.lstUsers.GridLines = True
        ListViewGroup1.Header = "ListViewGroup"
        ListViewGroup1.Name = "ListViewGroup1"
        ListViewGroup2.Header = "ListViewGroup"
        ListViewGroup2.Name = "ListViewGroup2"
        ListViewGroup3.Header = "ListViewGroup"
        ListViewGroup3.Name = "ListViewGroup3"
        Me.lstUsers.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2, ListViewGroup3})
        Me.lstUsers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lstUsers.Location = New System.Drawing.Point(15, 25)
        Me.lstUsers.MultiSelect = False
        Me.lstUsers.Name = "lstUsers"
        Me.lstUsers.ShowGroups = False
        Me.lstUsers.ShowItemToolTips = True
        Me.lstUsers.Size = New System.Drawing.Size(533, 135)
        Me.lstUsers.SmallImageList = Me.CliImages
        Me.lstUsers.TabIndex = 6
        Me.lstUsers.UseCompatibleStateImageBehavior = False
        Me.lstUsers.View = System.Windows.Forms.View.Details
        '
        'ch1
        '
        Me.ch1.Text = "Client:"
        Me.ch1.Width = 313
        '
        'ch2
        '
        Me.ch2.Text = "Errors Count:"
        Me.ch2.Width = 104
        '
        'ch3
        '
        Me.ch3.Text = "Errors List:"
        Me.ch3.Width = 106
        '
        'UsersMenuStrip
        '
        Me.UsersMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowDetailInfoToolStripMenuItem, Me.ToolStripMenuItem3, Me.EditClientOptionsToolStripMenuItem, Me.DisconnectClientToolStripMenuItem})
        Me.UsersMenuStrip.Name = "UsersMenuStrip"
        Me.UsersMenuStrip.Size = New System.Drawing.Size(174, 76)
        '
        'ShowDetailInfoToolStripMenuItem
        '
        Me.ShowDetailInfoToolStripMenuItem.Image = Global.RRServer.My.Resources.Resources.user_wake_zoom_16
        Me.ShowDetailInfoToolStripMenuItem.Name = "ShowDetailInfoToolStripMenuItem"
        Me.ShowDetailInfoToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.ShowDetailInfoToolStripMenuItem.Text = "Show Detail Info"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(170, 6)
        '
        'EditClientOptionsToolStripMenuItem
        '
        Me.EditClientOptionsToolStripMenuItem.Image = Global.RRServer.My.Resources.Resources.executive_config_16
        Me.EditClientOptionsToolStripMenuItem.Name = "EditClientOptionsToolStripMenuItem"
        Me.EditClientOptionsToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.EditClientOptionsToolStripMenuItem.Text = "Edit Client Options"
        '
        'DisconnectClientToolStripMenuItem
        '
        Me.DisconnectClientToolStripMenuItem.Image = Global.RRServer.My.Resources.Resources.executive_info_16
        Me.DisconnectClientToolStripMenuItem.Name = "DisconnectClientToolStripMenuItem"
        Me.DisconnectClientToolStripMenuItem.Size = New System.Drawing.Size(173, 22)
        Me.DisconnectClientToolStripMenuItem.Text = "Disconnect Client"
        '
        'CliImages
        '
        Me.CliImages.ImageStream = CType(resources.GetObject("CliImages.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.CliImages.TransparentColor = System.Drawing.Color.Transparent
        Me.CliImages.Images.SetKeyName(0, "green")
        Me.CliImages.Images.SetKeyName(1, "red")
        Me.CliImages.Images.SetKeyName(2, "grey")
        Me.CliImages.Images.SetKeyName(3, "clients_")
        Me.CliImages.Images.SetKeyName(4, "error_")
        Me.CliImages.Images.SetKeyName(5, "caution_")
        Me.CliImages.Images.SetKeyName(6, "clients_off")
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblStatus, Me.lblClients, Me.lblPort})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 484)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(560, 22)
        Me.StatusStrip1.TabIndex = 8
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblStatus
        '
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(81, 17)
        Me.lblStatus.Text = "Status: Closed"
        '
        'lblClients
        '
        Me.lblClients.Name = "lblClients"
        Me.lblClients.Size = New System.Drawing.Size(405, 17)
        Me.lblClients.Spring = True
        Me.lblClients.Text = "Clients: 0    "
        Me.lblClients.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPort
        '
        Me.lblPort.Name = "lblPort"
        Me.lblPort.Size = New System.Drawing.Size(59, 17)
        Me.lblPort.Text = "Port: 8080"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 167)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Server Log:"
        '
        'wskListener
        '
        Me.wskListener.BufferSize = 8192
        Me.wskListener.LegacySupport = True
        Me.wskListener.LocalPort = 8080
        Me.wskListener.MaxPendingConnections = 1
        Me.wskListener.Protocol = Winsock_Orcas.WinsockProtocol.Tcp
        Me.wskListener.RemoteHost = "localhost"
        Me.wskListener.RemotePort = 8080
        '
        'CheckBoxAdd
        '
        Me.CheckBoxAdd.AutoSize = True
        Me.CheckBoxAdd.Checked = True
        Me.CheckBoxAdd.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxAdd.Location = New System.Drawing.Point(394, 167)
        Me.CheckBoxAdd.Name = "CheckBoxAdd"
        Me.CheckBoxAdd.Size = New System.Drawing.Size(82, 17)
        Me.CheckBoxAdd.TabIndex = 10
        Me.CheckBoxAdd.Text = "Add To Log"
        Me.CheckBoxAdd.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(325, 164)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(60, 20)
        Me.Button1.TabIndex = 11
        Me.Button1.Text = "Clear Log"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'FormServer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(560, 506)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lstUsers)
        Me.Controls.Add(Me.ListLog)
        Me.Controls.Add(Me.chScroll)
        Me.Controls.Add(Me.CheckBoxAdd)
        Me.Controls.Add(Me.Button1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FormServer"
        Me.Text = "Remote Roulet Server"
        Me.TrayContextMenu.ResumeLayout(False)
        Me.UsersMenuStrip.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListLog As System.Windows.Forms.ListBox
    Friend WithEvents NotifyIcon As System.Windows.Forms.NotifyIcon
    Friend WithEvents TrayContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents StopServerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ShowWindowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents chScroll As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lstUsers As System.Windows.Forms.ListView
    Friend WithEvents ch1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ch2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ch3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblStatus As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblClients As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblPort As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents wskListener As Winsock_Orcas.Winsock
    Friend WithEvents CliImages As System.Windows.Forms.ImageList
    Friend WithEvents UsersMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ShowDetailInfoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CheckBoxAdd As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EditClientOptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DisconnectClientToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
