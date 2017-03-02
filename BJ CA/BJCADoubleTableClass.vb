<Serializable()> Public Class BJCADoubleTableClass
    Public T(17, 3) As Boolean

    Public Sub New()
        Dim row As Integer
        Dim column As Integer

        For row = 0 To 17
            For column = 0 To 3
                T(row, column) = False
            Next column
        Next row
    End Sub

    Public Sub Empty()
        Dim row As Integer
        Dim column As Integer

        For row = 0 To 17
            For column = 0 To 3
                T(row, column) = False
            Next column
        Next row
    End Sub

End Class
