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
        Dim topscore As Integer = -99999
        b.notes = ""

        For i As Integer = 0 To consts.cols - 1
            Dim thisscore As Integer = ScoreMove(b, i, player, 1)
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
            x = InputBox("Make a move (1 to " & consts.cols.ToString & ") or type QUIT", "Player " & player.ToString)
            If x.ToLower = "quit" Then
                b.status = "Player " & player.ToString & " quit!"
                Exit Sub
            End If
            If Len(x) = 1 And IsNumeric(x) Then
                If CInt(x) <= consts.cols And CInt(x) > 0 Then
                    b.notes = ""
                    Dim thisscore As Integer = ScoreMove(b, x - 1, player, 0)
                    ok = PlaceCounter(b, x - 1, player)
                    DisplayBoard(b) ' so we see the updated notes
                    If thisscore >= 400 Then
                        b.status = "Player " & player.ToString & " wins!"
                        Exit Sub
                    End If
                End If
            End If

        End While

    End Sub

    Private Function ValidColumn(ByRef b As board, col As Integer) As Boolean
        Return b.cells(col, 0) = 0
    End Function

    Private Function PlaceCounter(ByRef b As board, col As Integer, player As Integer) As Boolean
        ' col is 0 to 6, not 1 to 7S
        For i As Integer = b.cells.GetLowerBound(1) To b.cells.GetUpperBound(1)
            If b.cells(col, i) <> 0 Then
                ' We've found a cell. Let's try and place a counter in the cell above. 
                If i = b.cells.GetLowerBound(1) Then
                    ' There is no space, error
                    Return False
                Else
                    b.cells(col, i - 1) = player
                    Return True ' done
                End If
            End If
        Next
        ' Otherwise stick it on the bottom
        b.cells(col, b.cells.GetUpperBound(1)) = player
        Return True
    End Function

    Private Sub DisplayBoard(ByRef b As board)

        Dim s As String = ""
        For j As Integer = b.cells.GetLowerBound(1) To b.cells.GetUpperBound(1)
            For i As Integer = b.cells.GetLowerBound(0) To b.cells.GetUpperBound(0)
                s = s & b.cells(i, j).ToString() & " "
            Next
            s = s & vbCrLf
        Next
        s = s & b.status & vbCrLf
        tbNotes.Text = b.notes
        tbNotes.Refresh()

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
        Return b.cells(col, row)
    End Function

    Private Function ScoreMove(ByRef b As board, col As Integer, player As Integer, ply As Integer) As Integer

        Dim dcol() As Integer = {0, 1, 1, 1, 0, -1, -1, -1}
        Dim drow() As Integer = {1, 1, 0, -1, -1, -1, 0, 1}
        Dim dir() As String = {"S", "SE", "E", "NE", "N", "NW", "W", "SW"}
        Dim scores() As Integer = {0, 0, 0, 0, 0, 0, 0, 0}
        Dim howmanys() As Integer = {0, 0, 0, 0, 0, 0, 0, 0}
        Dim highscore As Integer = 0
        Dim highcount As Integer = 0

        Dim row As Integer = GetCellPos(b, col)
        If row < 0 Then
            Return -9999
        End If
        Dim exp As String = ""
        Dim exp2 As String = ""
        Dim bestotherscore As Integer = -9999

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

            Dim usefulmove As Boolean = True ' assume the best
            If GetCell(b, scol, srow) <> 0 And howmany <= 3 Then
                usefulmove = False ' the square at the end of the line is not blank, so trying for a line here makes no sense. 
            End If
            If GetCell(b, scol + dcol(d), srow + drow(d)) <> 0 And howmany <= 2 Then
                usefulmove = False ' the square next to that is not blank, so trying for a line here makes no sense. 
            End If
            If GetCell(b, scol + dcol(d) + dcol(d), srow + drow(d) + drow(d)) <> 0 And howmany <= 1 Then
                usefulmove = False ' the logical conclusion, but really not a lot of use to know. 
            End If

            ' Now scol and srow are pointing at the space at the end of the line, and howmany is the number of counters in this sequence. 

            If (usefulmove) Then
                ' It's a move with a chance of completing 4 in a row

                scores(d) = howmany * 100
                howmanys(d) = howmany
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
                exp = exp & " / "
            End If
            exp = exp & dir(d) & ":" & scores(d).ToString
        Next

        Dim bestothermove As String = ""
        If ply >= 1 And ply <= 1 And highscore < 400 Then ' Recursively figure out the best move for the other player
            Dim b2 As board = b.Clone()
            Dim otherplayer As Integer = 3 - player
            If (PlaceCounter(b2, col, player)) Then
                For j As Integer = 0 To consts.cols - 1
                    Dim thisotherscore As Integer = ScoreMove(b2, j, otherplayer, ply + 1)
                    If thisotherscore > -9999 Then
                        If exp2 <> "" Then
                            exp2 = exp2 & " / "
                        End If
                        exp2 = exp2 & (j + 1).ToString() & ":" & thisotherscore.ToString

                        If thisotherscore > bestotherscore Then
                            bestotherscore = thisotherscore
                            bestothermove = (j + 1).ToString & ":" & bestotherscore
                        End If
                        If (bestotherscore >= 400) Then
                            GoTo LeaveLoop
                        End If
                    End If
                Next
LeaveLoop:
                highscore = highscore - bestotherscore
            End If
        End If

        If ply = 1 Then
            b.notes = b.notes & vbCrLf & player.ToString() & ": " & ply.ToString & ": " & (col + 1).ToString() & " [" & exp & "] {" & exp2 & "} " & highscore & ": " & bestothermove.ToString
        End If
        highscore = highscore + highcount ' being able to do something twice beats being able to do it once

        Return highscore

    End Function

    Private Function GetCellPos(ByRef b As board, col As Integer) As Integer
        ' col is 0 to 6, not 1 to 7
        If col < 0 Or col >= consts.cols Then
            Return -1
        End If
        For i As Integer = b.cells.GetLowerBound(1) To b.cells.GetUpperBound(1)
            If b.cells(col, i) <> 0 Then
                ' We've found a cell. Let's try and place a counter in the cell above. 
                If i = b.cells.GetLowerBound(1) Then
                    ' There is no space, error
                    Return -1
                Else
                    Return i - 1 ' done
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
