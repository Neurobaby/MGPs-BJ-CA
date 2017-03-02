Imports BJ_CA.BJCAShared

Public Class TestClass
    Public emptytesthand As New BJCAHandClass
    Public testhand(1000000) As BJCAHandClass

    Public Sub New()
        Dim i As Long

        For i = 1 To 100000
            testhand(i) = New BJCAHandClass
            testhand(i).Cards(1) = 5
        Next
    End Sub

    Public Sub BruteForceEmpty()
        Dim i As Long

        For i = 1 To 100000
            testhand(i).Empty()
        Next
    End Sub

    Public Sub CloneEmpty()
        Dim i As Long

        For i = 1 To 100000
            testhand(i) = CloneObject(emptytesthand)
        Next
    End Sub

End Class
