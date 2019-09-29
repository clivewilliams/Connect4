Public Class consts
    Public Const cols = 7
    Public Const rows = 6
End Class

Public Class board

    Public cells(,) As Integer = Nothing
    Public status As String = "" ' when non-empty, the game is finished. 
    Public boardscore As Integer = 0
    Public notes As String = ""

    Public Sub New()
        ReDim cells(consts.cols - 1, consts.rows - 1)
        ' dimension 0 = columns, 0 to 6, left to right
        ' dimension 1 = rows, 0 to 5, top to bottom
        ' cells(i, j) = column i, row j. 
        For i As Integer = cells.GetLowerBound(0) To cells.GetUpperBound(0)

            For j As Integer = cells.GetLowerBound(1) To cells.GetUpperBound(1)
                cells(i, j) = 0
            Next

        Next
    End Sub

    Public Function Clone() As board
        Dim b2 As board = New board()

        For i As Integer = cells.GetLowerBound(0) To cells.GetUpperBound(0)
            For j As Integer = cells.GetLowerBound(1) To cells.GetUpperBound(1)
                b2.cells(i, j) = cells(i, j)
            Next
        Next
        Return b2
    End Function

End Class
