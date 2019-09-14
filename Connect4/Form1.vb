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
        Dim x As MsgBoxResult = MsgBox("Player " & player.ToString & " moves column " & col.ToString, vbOKCancel, "Player " & player.ToString)
        If x = vbCancel Then
            b.status = "Player " & player.ToString & " quit!"
        End If
        Dim ok As Boolean = False

        If ValidColumn(b, col) Then
            ok = PlaceCounter(b, col - 1, player)
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
                    ok = PlaceCounter(b, x - 1, player)
                End If
            End If
        End While

    End Sub

    Public Function ValidColumn(ByRef b As board, col As Integer) As Boolean
        Return b.cells(col, 0).value = 0
    End Function

    Public Function PlaceCounter(ByRef b As board, col As Integer, player As Integer) As Boolean
        ' col is 0 to 6, not 1 to 7
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

    Public Sub DisplayBoard(ByRef b As board)

        Dim s As String
        For j As Integer = b.cells.GetLowerBound(1) To b.cells.GetUpperBound(1)
            For i As Integer = b.cells.GetLowerBound(0) To b.cells.GetUpperBound(0)
                s = s & b.cells(i, j).value.ToString() & " "
            Next
        s = s & vbCrLf
        Next
        s = s & b.status & vbCrLf

        tbDisplay.Text = s
        tbDisplay.Refresh()
        Application.DoEvents()

    End Sub

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
