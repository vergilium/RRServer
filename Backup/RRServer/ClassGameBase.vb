Public Class ClassGameBase
    Dim _gamesBase() As GameStat
    Dim _gbCount As Long

    Public Structure GameStat
        Public gStat As GameStatus
        Public gNumber As Long
        Public gWin As Long
    End Structure
    Enum GameStatus
        GameStart
        GameEnd
        GameProcess
    End Enum

    Public Sub AddGame(ByVal GameNumber As Long, Optional ByVal GameStat As GameStatus = GameStatus.GameStart, Optional ByVal GameValue As Long = 0)
        ReDim Preserve _gamesBase(_gbCount)
        _gamesBase(_gbCount).gNumber = GameNumber
        _gamesBase(_gbCount).gStat = GameStat
        _gamesBase(_gbCount).gWin = GameValue
    End Sub
    Public Property GetStatus(ByVal idx As Long) As GameStatus
        Get
            Return _gamesBase(idx).gStat
        End Get
        Set(ByVal value As GameStatus)
            _gamesBase(idx).gStat = value
        End Set
    End Property
    Public Property GetGameValue(ByVal idx As Long)
        Get
            Return _gamesBase(idx).gWin
        End Get
        Set(ByVal value)
            _gamesBase(idx).gWin = value
        End Set
    End Property
    Public ReadOnly Property GetGameNumber(ByVal idx As Long)
        Get
            Return _gamesBase(idx).gNumber
        End Get
    End Property
End Class
