Imports System
Imports System.Collections

<Serializable()> Public Class BJCADealerProbsClass
    Public p(10, 4) As Double           'Upcard, Dealer Total
    Public bjadj(10) As Double          'Prob of dealer BJ given shoe

    Public Sub New()
    End Sub
End Class

<Serializable()> Public Class BJCADealerProbsDictionary
    Inherits System.Collections.DictionaryBase

    Public Sub New()
        Dim dprobs As New BJCADealerProbsClass

        MyBase.Dictionary.Add("None-S17-0-0-0-0-0-0-0-0-0-0", dprobs)
        MyBase.Dictionary.Add("None-H17-0-0-0-0-0-0-0-0-0-0", dprobs)

    End Sub

    Default Public Property Item(ByVal key As [String]) As [BJCADealerProbsClass]
        Get
            Return CType(Dictionary(key), [BJCADealerProbsClass])
        End Get
        Set(ByVal dprobs As [BJCADealerProbsClass])
            Dictionary(key) = dprobs
        End Set
    End Property

    Public Sub Add(ByVal key As [String], ByVal dprobs As [BJCADealerProbsClass])
        Dictionary.Add(key, dprobs)
    End Sub 'Add

    Public Function Contains(ByVal key As [String]) As Boolean
        Return Dictionary.Contains(key)
    End Function 'Contains

    Public Sub Remove(ByVal key As [String])
        Dictionary.Remove(key)
    End Sub 'Remove

    Public Sub Replace(ByVal key As [String], ByVal dprobs As [BJCADealerProbsClass])
        Dictionary.Remove(key)
        Dictionary.Add(key, dprobs)
    End Sub 'Replace

    Public Function GetHashKey(ByVal standonsoft As Integer, ByVal ndecks As Integer, ByVal hand As BJCAHandClass, Optional ByVal countName As String = "None") As String
        Dim keystr As String

        If hand.Cards(1) > ndecks * 4 Or _
           hand.Cards(2) > ndecks * 4 Or _
           hand.Cards(3) > ndecks * 4 Or _
           hand.Cards(4) > ndecks * 4 Or _
           hand.Cards(5) > ndecks * 4 Or _
           hand.Cards(6) > ndecks * 4 Or _
           hand.Cards(7) > ndecks * 4 Or _
           hand.Cards(8) > ndecks * 4 Or _
           hand.Cards(9) > ndecks * 4 Or _
           hand.Cards(10) > ndecks * 16 Then

            keystr = "None-S17-0-0-0-0-0-0-0-0-0-0"
        End If

        keystr = countName + "-" + "S" + CStr(standonsoft)
        keystr += CStr(ndecks * 4 - hand.Cards(1)) + "-"
        keystr += CStr(ndecks * 4 - hand.Cards(2)) + "-"
        keystr += CStr(ndecks * 4 - hand.Cards(3)) + "-"
        keystr += CStr(ndecks * 4 - hand.Cards(4)) + "-"
        keystr += CStr(ndecks * 4 - hand.Cards(5)) + "-"
        keystr += CStr(ndecks * 4 - hand.Cards(6)) + "-"
        keystr += CStr(ndecks * 4 - hand.Cards(7)) + "-"
        keystr += CStr(ndecks * 4 - hand.Cards(8)) + "-"
        keystr += CStr(ndecks * 4 - hand.Cards(9)) + "-"
        keystr += CStr(ndecks * 16 - hand.Cards(10))


        Return keystr

    End Function

    Public Function GetHashKey(ByVal standonsoft As Integer, ByVal shoe As BJCAShoeClass, ByVal hand As BJCAHandClass, Optional ByVal countName As String = "None") As String
        Dim keystr As String

        If hand.Cards(1) > shoe.Cards(1) Or _
           hand.Cards(2) > shoe.Cards(2) Or _
           hand.Cards(3) > shoe.Cards(3) Or _
           hand.Cards(4) > shoe.Cards(4) Or _
           hand.Cards(5) > shoe.Cards(5) Or _
           hand.Cards(6) > shoe.Cards(6) Or _
           hand.Cards(7) > shoe.Cards(7) Or _
           hand.Cards(8) > shoe.Cards(8) Or _
           hand.Cards(9) > shoe.Cards(9) Or _
           hand.Cards(10) > shoe.Cards(10) Then

            keystr = "None-S17-0-0-0-0-0-0-0-0-0-0"
        End If

        keystr = countName + "-" + "S" + CStr(standonsoft)
        keystr += CStr(shoe.Cards(1) - hand.Cards(1)) + "-"
        keystr += CStr(shoe.Cards(2) - hand.Cards(2)) + "-"
        keystr += CStr(shoe.Cards(3) - hand.Cards(3)) + "-"
        keystr += CStr(shoe.Cards(4) - hand.Cards(4)) + "-"
        keystr += CStr(shoe.Cards(5) - hand.Cards(5)) + "-"
        keystr += CStr(shoe.Cards(6) - hand.Cards(6)) + "-"
        keystr += CStr(shoe.Cards(7) - hand.Cards(7)) + "-"
        keystr += CStr(shoe.Cards(8) - hand.Cards(8)) + "-"
        keystr += CStr(shoe.Cards(9) - hand.Cards(9)) + "-"
        keystr += CStr(shoe.Cards(10) - hand.Cards(10))

        Return keystr

    End Function

    Public Function GetHashKey(ByVal standonsoft As Integer, ByVal shoe As BJCAShoeClass, Optional ByVal countName As String = "None") As String
        Dim keystr As String

        keystr = countName + "-" + "S" + CStr(standonsoft)
        keystr += CStr(shoe.Cards(1)) + "-"
        keystr += CStr(shoe.Cards(2)) + "-"
        keystr += CStr(shoe.Cards(3)) + "-"
        keystr += CStr(shoe.Cards(4)) + "-"
        keystr += CStr(shoe.Cards(5)) + "-"
        keystr += CStr(shoe.Cards(6)) + "-"
        keystr += CStr(shoe.Cards(7)) + "-"
        keystr += CStr(shoe.Cards(8)) + "-"
        keystr += CStr(shoe.Cards(9)) + "-"
        keystr += CStr(shoe.Cards(10))

        Return keystr

    End Function

End Class

