Public Class Form1
    Private Sub QuitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QuitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub PlayerVCPUToolStripMenuItem_Click(sender As Object, e As EventArgs)
        PlayGame(False, True)
    End Sub

    Private Sub CPUVPlayerToolStripMenuItem_Click(sender As Object, e As EventArgs)
        PlayGame(True, False)
    End Sub

    Private Sub PlayerVPlayerToolStripMenuItem_Click(sender As Object, e As EventArgs)
        PlayGame(False, False)
    End Sub

    Private Sub PlayGame(p1cpu As Boolean, p2cpu As Boolean)

        PlayerVCPUToolStripMenuItem1.Enabled = False
        CPUVPlayerToolStripMenuItem1.Enabled = False
        PlayerVPlayerToolStripMenuItem1.Enabled = False

        Dim b As board = New board()
        DisplayBoard(b)

        While b.status = ""

            For player = 1 To 2

                If ((player = 1) And p1cpu) Or ((player = 2) And p2cpu) Then
                    MakeCPUMove(b, player)
                Else
                    MakePlayerMove(b, player)
                End If
                If b.status <> "" Then
                    Exit For
                End If

                DisplayBoard(b)
            Next

        End While
        DisplayBoard(b)

        PlayerVCPUToolStripMenuItem1.Enabled = True
        CPUVPlayerToolStripMenuItem1.Enabled = True
        PlayerVPlayerToolStripMenuItem1.Enabled = True

    End Sub

    Public Sub MakeCPUMove(ByRef b As board, player As Integer)

        Dim col As Integer = 1
        Dim topscore As Integer = -1
        b.notes = ""

        For i As Integer = 0 To consts.cols - 1
            Dim thisscore As Integer = ScoreMove(b, i, player)
            b.notes = b.notes & thisscore & " "
            If thisscore > topscore OrElse ((thisscore = topscore) And (New Random().Next(2) = 1)) Then
                col = i
                topscore = thisscore
            End If
            If thisscore >= 400 Then
                b.status = "Player " & player.ToString & " wins!"
            End If
        Next

        Dim ok As Boolean = False

        If ValidColumn(b, col) Then
            ok = PlaceCounter(b, col, player)
        End If
        DisplayBoard(b) ' so we see the updated notes
        Dim x As MsgBoxResult = MsgBox("Player " & player.ToString & " moves column " & (col + 1).ToString, vbOKCancel, "Player " & player.ToString)
        If x = vbCancel Then
            b.status = "Player " & player.ToString & " quit!"
        End If

    End Sub

    Public Sub MakePlayerMove(ByRef b As board, player As Integer)

        Dim x As String = ""
        Dim ok As Boolean = False

        While Not ok
            x = InputBox("Make a move (1 to " & consts.cols.ToString & ") or type QUIT", "")
            If x.ToLower = "quit" Then
                b.status = "Player " & player.ToString & " quit!"
                Exit Sub
            End If
            If Len(x) = 1 And IsNumeric(x) Then
                If CInt(x) <= consts.cols And CInt(x) > 0 Then
                    Dim thisscore As Integer = ScoreMove(b, x - 1, player)
                    ok = PlaceCounter(b, x - 1, player)
                    If thisscore >= 400 Then
                        b.status = "Player " & player.ToString & " wins!"
                        Exit Sub
                    End If
                End If
            End If
        End While

    End Sub

    Private Function ValidColumn(ByRef b As board, col As Integer) As Boolean
        Return b.cells(col, 0).value = 0
    End Function

    Private Function PlaceCounter(ByRef b As board, col As Integer, player As Integer) As Boolean
        ' col is 0 to 6, not 1 to 7S
        For i As Integer = b.cells.GetLowerBound(1) To b.cells.GetUpperBound(1)
            If b.cells(col, i).value <> 0 Then
                ' We've found a cell. Let's try and place a counter in the cell above. 
                If i = b.cells.GetLowerBound(1) Then
                    ' There is no space, error
                    Return False
                Else
                    b.cells(col, i - 1).value = player
                    Return True ' done
                End If
            End If
        Next
        ' Otherwise stick it on the bottom
        b.cells(col, b.cells.GetUpperBound(1)).value = player
        Return True
    End Function

    Private Sub DisplayBoard(ByRef b As board)

        Dim s As String = ""
        For j As Integer = b.cells.GetLowerBound(1) To b.cells.GetUpperBound(1)
            For i As Integer = b.cells.GetLowerBound(0) To b.cells.GetUpperBound(0)
                s = s & b.cells(i, j).value.ToString() & " "
            Next
            s = s & vbCrLf
        Next
        s = s & b.status & vbCrLf
        s = s & b.notes & vbCrLf

        tbDisplay.Text = s
        tbDisplay.Refresh()
        Application.DoEvents()

    End Sub

    Private Function GetCell(ByRef b As board, col As Integer, row As Integer) As Integer
        Dim i As Integer = 0
        If (col < 0 Or col >= consts.cols) Then ' off end of board
            Return -1
        End If
        If (row < 0 Or row >= consts.rows) Then
            Return -1
        End If
        Return b.cells(col, row).value
    End Function

    Private Function ScoreMove(ByRef b As board, col As Integer, player As Integer) As Integer

        Dim dcol() As Integer = {0, 1, 1, 1, 0, -1, -1, -1}
        Dim drow() As Integer = {1, 1, 0, -1, -1, -1, 0, 1}
        Dim scores() As Integer = {0, 0, 0, 0, 0, 0, 0, 0}
        Dim highscore As Integer = 0
        Dim highcount As Integer = 0

        Dim row As Integer = GetCellPos(b, col)
        Dim exp As String = ""

        For d As Integer = 0 To 7
            Dim scol As Integer = col
            Dim srow As Integer = row

            While (GetCell(b, scol, srow) = player) Or ((scol = col) And (srow = row)) ' go back, until we've found one end of the line
                scol = scol - dcol(d)
                srow = srow - drow(d)
            End While
            scol = scol + dcol(d) ' we went back one place too far; undo that
            srow = srow + drow(d)

            Dim howmany As Integer = 0
            While (GetCell(b, scol, srow) = player) Or ((scol = col) And (srow = row))  ' go forward, until we've found one end of the line
                scol = scol + dcol(d)
                srow = srow + drow(d)
                howmany = howmany + 1
            End While

            ' Now scol and srow are pointing at the space at the end of the line, and howmany is the number of counters in this sequence. 

            If (GetCell(b, scol, srow) = 0) Then
                ' It's a valid move

                scores(d) = howmany * 100
                If (GetCellPos(b, scol) = srow) Then
                    ' we could conceivably play there next time, so score this better.
                    scores(d) = scores(d) + 50
                End If

                If (scores(d) = highscore) Then
                    highcount = highcount + 1
                End If
                If (scores(d) > highscore) Then
                    highcount = 0
                    highscore = scores(d)
                End If
            Else
                scores(d) = -1
            End If
            If exp <> "" Then
                exp = exp & "/"
            End If
            exp = exp & scores(d).ToString

        Next
        b.notes = b.notes & vbCrLf & "[" & exp & "] "
        highscore = highscore + highcount - 1 ' being able to do something twice beats being able to do it once
        Return highscore

    End Function

    Private Function GetCellPos(ByRef b As board, col As Integer) As Integer
        ' col is 0 to 6, not 1 to 7
        For i As Integer = b.cells.GetLowerBound(1) To b.cells.GetUpperBound(1)
            If b.cells(col, i).value <> 0 Then
                ' We've found a cell. Let's try and place a counter in the cell above. 
                If i = b.cells.GetLowerBound(1) Then
                    ' There is no space, error
                    Return -1
                Else
                    Return i ' done
                End If
            End If
        Next
        ' Otherwise stick it on the bottom
        Return b.cells.GetUpperBound(1)
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub PlayerVPlayerToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PlayerVPlayerToolStripMenuItem1.Click
        PlayGame(False, False)

    End Sub

    Private Sub CPUVPlayerToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CPUVPlayerToolStripMenuItem1.Click
        PlayGame(True, False)

    End Sub

    Private Sub PlayerVCPUToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PlayerVCPUToolStripMenuItem1.Click
        PlayGame(False, True)

    End Sub
End Class
