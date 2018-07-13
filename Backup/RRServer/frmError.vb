Public Class frmError

    Private _text As String = Nothing
    Private _moreHeight As Integer = 0
    Private _lessHeight As Integer = 0
    Private _textHeight As Integer = 0

    Public Overloads Sub ShowDialog(ByVal message As String, Optional ByVal title As String = Nothing, Optional ByVal details As String = Nothing, Optional ByVal owner As Windows.Forms.IWin32Window = Nothing)
        If title IsNot Nothing OrElse title.Trim() <> "" Then
            Me.Text = title
        Else
            Me.Text = My.Application.Info.Title
        End If
        If details IsNot Nothing OrElse details.Trim() <> "" Then
            txtDetails.Text = details
            txtDetails.Visible = False
            cmdMoreLess.Text = "More"
            cmdMoreLess.Visible = True
        Else
            txtDetails.Visible = False
            cmdMoreLess.Visible = False
        End If
        _text = message
        DetermineSize()
        Me.Height = _lessHeight
        If owner Is Nothing Then
            MyBase.ShowDialog()
        Else
            MyBase.ShowDialog(owner)
        End If
    End Sub

    Private Sub DetermineSize()
        Dim rect As New Rectangle(0, 0, Me.Width, Me.Height)
        Dim rct2 As Rectangle = Me.ClientRectangle
        Dim bWidth As Integer = rect.Width - rct2.Width
        Dim bHeight As Integer = rect.Height - rct2.Height
        Dim g As Graphics = Me.CreateGraphics()
        Dim sf As New StringFormat()
        sf.Alignment = StringAlignment.Near
        sf.LineAlignment = StringAlignment.Center
        Dim sz As SizeF = g.MeasureString(_text, lblFont.Font, Me.Width - 24 - bWidth)
        g.Dispose()
        Dim pWidth As Integer = CInt(sz.Width) + 24 + bWidth
        Dim mWidth As Integer = 24 + bWidth + cmdOK.Width
        mWidth += CInt(IIf(cmdMoreLess.Visible, 12 + cmdMoreLess.Width, 0))
        If pWidth < Me.Width - 24 - bWidth AndAlso pWidth > mWidth Then
            Me.Width = CInt(sz.Width) + 24 + bWidth
        ElseIf pWidth < mWidth Then
            Me.Width = mWidth
        End If
        _textHeight = CInt(sz.Height)
        _moreHeight = 48 + txtDetails.Height + cmdMoreLess.Height + CInt(sz.Height) + bHeight
        _lessHeight = 36 + cmdMoreLess.Height + CInt(sz.Height) + bHeight
    End Sub

    Private Sub cmdMoreLess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMoreLess.Click
        Select Case cmdMoreLess.Text
            Case "More"
                Me.Height = _moreHeight
                txtDetails.Visible = True
                cmdMoreLess.Text = "Less"
            Case "Less"
                txtDetails.Visible = False
                Me.Height = _lessHeight
                cmdMoreLess.Text = "More"
        End Select
    End Sub

    Private Sub frmError_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim ly As New RectangleF(12, 12, Me.Width - 24, _textHeight)
        Dim sf As New StringFormat()
        sf.Alignment = StringAlignment.Near
        sf.LineAlignment = StringAlignment.Center
        e.Graphics.DrawString(_text, lblFont.Font, New SolidBrush(Color.Black), ly, sf)
    End Sub

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

End Class