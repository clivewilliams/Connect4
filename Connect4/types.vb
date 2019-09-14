Public Class consts
    Public Const cols = 7
    Public Const rows = 6
End Class

Public Class board

    Public cells(,) As cell = Nothing
    Public status As String = "" ' when non-empty, the game is finished. 

    Public Sub New()
        ReDim cells(consts.cols - 1, consts.rows - 1)
        ' dimension 0 = columns, 0 to 6, left to right
        ' dimension 1 = rows, 0 to 5, top to bottom
        ' cells(i, j) = column i, row j. 
        For i As Integer = cells.GetLowerBound(0) To cells.GetUpperBound(0)

            For j As Integer = cells.GetLowerBound(1) To cells.GetUpperBound(1)
                cells(i, j) = New cell
                cells(i, j).value = 0
            Next

        Next
    End Sub

End Class

Public Class col

    Public celllist() As cell = Nothing

End Class

Public Class cell

    Public value As Integer = 0

End Class