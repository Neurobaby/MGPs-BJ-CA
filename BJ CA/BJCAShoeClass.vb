<Serializable()> Public Class BJCAShoeClass

    Public Cards(10) As Integer
    Public Suits(10, 3) As Integer

    Public CardsLeft As Integer
    Public Hand As New BJCAHandClass

    Public Sub Reset(ByVal originalShoe As BJCAShoeClass)
        Dim card As Integer

        For card = 1 To 10
            Cards(card) = originalShoe.Cards(card)
            Suits(card, 0) = originalShoe.Suits(card, 0)
            Suits(card, 1) = originalShoe.Suits(card, 1)
            Suits(card, 2) = originalShoe.Suits(card, 2)
            Suits(card, 3) = originalShoe.Suits(card, 3)
        Next card

        CardsLeft = originalShoe.CardsLeft
        Hand.Empty()
    End Sub

    Public Sub Reset(ByVal nDecks As Integer)
        Dim card As Integer

        For card = 1 To 9
            Cards(card) = 4 * nDecks
            Suits(card, 0) = nDecks
            Suits(card, 1) = nDecks
            Suits(card, 2) = nDecks
            Suits(card, 3) = nDecks
        Next card
        Cards(10) = 16 * nDecks
        Suits(10, 0) = 4 * nDecks
        Suits(10, 1) = 4 * nDecks
        Suits(10, 2) = 4 * nDecks
        Suits(10, 3) = 4 * nDecks

        CardsLeft = nDecks * 52
        Hand.Empty()
    End Sub

    Public Sub Deal(ByVal card As Integer, Optional ByVal nCards As Integer = 1)
        Cards(card) -= nCards
        CardsLeft -= nCards

        Hand.Cards(card) += nCards
        Hand.NumCards += nCards
    End Sub

    Public Sub Deal(ByVal dealtHand As BJCAHandClass)
        Cards(1) -= dealtHand.Cards(1)
        Cards(2) -= dealtHand.Cards(2)
        Cards(3) -= dealtHand.Cards(3)
        Cards(4) -= dealtHand.Cards(4)
        Cards(5) -= dealtHand.Cards(5)
        Cards(6) -= dealtHand.Cards(6)
        Cards(7) -= dealtHand.Cards(7)
        Cards(8) -= dealtHand.Cards(8)
        Cards(9) -= dealtHand.Cards(9)
        Cards(10) -= dealtHand.Cards(10)
        CardsLeft -= dealtHand.NumCards

        Hand.Cards(1) += dealtHand.Cards(1)
        Hand.Cards(2) += dealtHand.Cards(2)
        Hand.Cards(3) += dealtHand.Cards(3)
        Hand.Cards(4) += dealtHand.Cards(4)
        Hand.Cards(5) += dealtHand.Cards(5)
        Hand.Cards(6) += dealtHand.Cards(6)
        Hand.Cards(7) += dealtHand.Cards(7)
        Hand.Cards(8) += dealtHand.Cards(8)
        Hand.Cards(9) += dealtHand.Cards(9)
        Hand.Cards(10) += dealtHand.Cards(10)
        Hand.NumCards += dealtHand.NumCards
    End Sub

    Public Sub Undeal(ByVal card As Integer, Optional ByVal nCards As Integer = 1)
        Cards(card) += nCards
        CardsLeft += nCards

        Hand.Cards(card) -= nCards
        Hand.NumCards -= nCards
    End Sub

    Public Sub Undeal(ByVal dealthand As BJCAHandClass)
        Cards(1) += dealthand.Cards(1)
        Cards(2) += dealthand.Cards(2)
        Cards(3) += dealthand.Cards(3)
        Cards(4) += dealthand.Cards(4)
        Cards(5) += dealthand.Cards(5)
        Cards(6) += dealthand.Cards(6)
        Cards(7) += dealthand.Cards(7)
        Cards(8) += dealthand.Cards(8)
        Cards(9) += dealthand.Cards(9)
        Cards(10) += dealthand.Cards(10)
        CardsLeft += dealthand.NumCards

        Hand.Cards(1) -= dealthand.Cards(1)
        Hand.Cards(2) -= dealthand.Cards(2)
        Hand.Cards(3) -= dealthand.Cards(3)
        Hand.Cards(4) -= dealthand.Cards(4)
        Hand.Cards(5) -= dealthand.Cards(5)
        Hand.Cards(6) -= dealthand.Cards(6)
        Hand.Cards(7) -= dealthand.Cards(7)
        Hand.Cards(8) -= dealthand.Cards(8)
        Hand.Cards(9) -= dealthand.Cards(9)
        Hand.Cards(10) -= dealthand.Cards(10)
        Hand.NumCards -= dealthand.NumCards
    End Sub

    Public Sub DealSuited(ByVal card As Integer, ByVal suit As Integer, Optional ByVal nCards As Integer = 1)
        Cards(card) -= nCards
        Suits(card, suit) -= nCards
        CardsLeft -= nCards

        Hand.Cards(card) += nCards
        Hand.NumCards += nCards
    End Sub

    Public Sub DealSuited(ByVal dealtHand As BJCAHandClass, ByVal suit As Integer)
        Cards(1) -= dealtHand.Cards(1)
        Cards(2) -= dealtHand.Cards(2)
        Cards(3) -= dealtHand.Cards(3)
        Cards(4) -= dealtHand.Cards(4)
        Cards(5) -= dealtHand.Cards(5)
        Cards(6) -= dealtHand.Cards(6)
        Cards(7) -= dealtHand.Cards(7)
        Cards(8) -= dealtHand.Cards(8)
        Cards(9) -= dealtHand.Cards(9)
        Cards(10) -= dealtHand.Cards(10)
        CardsLeft -= dealtHand.NumCards

        Suits(1, suit) -= dealtHand.Cards(1)
        Suits(2, suit) -= dealtHand.Cards(2)
        Suits(3, suit) -= dealtHand.Cards(3)
        Suits(4, suit) -= dealtHand.Cards(4)
        Suits(5, suit) -= dealtHand.Cards(5)
        Suits(6, suit) -= dealtHand.Cards(6)
        Suits(7, suit) -= dealtHand.Cards(7)
        Suits(8, suit) -= dealtHand.Cards(8)
        Suits(9, suit) -= dealtHand.Cards(9)
        Suits(10, suit) -= dealtHand.Cards(10)

        Hand.Cards(1) += dealtHand.Cards(1)
        Hand.Cards(2) += dealtHand.Cards(2)
        Hand.Cards(3) += dealtHand.Cards(3)
        Hand.Cards(4) += dealtHand.Cards(4)
        Hand.Cards(5) += dealtHand.Cards(5)
        Hand.Cards(6) += dealtHand.Cards(6)
        Hand.Cards(7) += dealtHand.Cards(7)
        Hand.Cards(8) += dealtHand.Cards(8)
        Hand.Cards(9) += dealtHand.Cards(9)
        Hand.Cards(10) += dealtHand.Cards(10)
        Hand.NumCards += dealtHand.NumCards
    End Sub

    Public Sub UndealSuited(ByVal card As Integer, ByVal suit As Integer, Optional ByVal nCards As Integer = 1)
        Cards(card) += nCards
        Suits(card, suit) += nCards
        CardsLeft += nCards

        Hand.Cards(card) -= nCards
        Hand.NumCards -= nCards
    End Sub

    Public Sub UndealSuited(ByVal dealthand As BJCAHandClass, ByVal suit As Integer)
        Cards(1) += dealthand.Cards(1)
        Cards(2) += dealthand.Cards(2)
        Cards(3) += dealthand.Cards(3)
        Cards(4) += dealthand.Cards(4)
        Cards(5) += dealthand.Cards(5)
        Cards(6) += dealthand.Cards(6)
        Cards(7) += dealthand.Cards(7)
        Cards(8) += dealthand.Cards(8)
        Cards(9) += dealthand.Cards(9)
        Cards(10) += dealthand.Cards(10)
        CardsLeft += dealthand.NumCards

        Suits(1, suit) += dealthand.Cards(1)
        Suits(2, suit) += dealthand.Cards(2)
        Suits(3, suit) += dealthand.Cards(3)
        Suits(4, suit) += dealthand.Cards(4)
        Suits(5, suit) += dealthand.Cards(5)
        Suits(6, suit) += dealthand.Cards(6)
        Suits(7, suit) += dealthand.Cards(7)
        Suits(8, suit) += dealthand.Cards(8)
        Suits(9, suit) += dealthand.Cards(9)
        Suits(10, suit) += dealthand.Cards(10)

        Hand.Cards(1) -= dealthand.Cards(1)
        Hand.Cards(2) -= dealthand.Cards(2)
        Hand.Cards(3) -= dealthand.Cards(3)
        Hand.Cards(4) -= dealthand.Cards(4)
        Hand.Cards(5) -= dealthand.Cards(5)
        Hand.Cards(6) -= dealthand.Cards(6)
        Hand.Cards(7) -= dealthand.Cards(7)
        Hand.Cards(8) -= dealthand.Cards(8)
        Hand.Cards(9) -= dealthand.Cards(9)
        Hand.Cards(10) -= dealthand.Cards(10)
        Hand.NumCards -= dealthand.NumCards
    End Sub

    Public Function HandPossible(ByVal hand As BJCAHandClass) As Boolean
        HandPossible = Cards(1) >= hand.Cards(1) And _
                    Cards(2) >= hand.Cards(2) And _
                    Cards(3) >= hand.Cards(3) And _
                    Cards(4) >= hand.Cards(4) And _
                    Cards(5) >= hand.Cards(5) And _
                    Cards(6) >= hand.Cards(6) And _
                    Cards(7) >= hand.Cards(7) And _
                    Cards(8) >= hand.Cards(8) And _
                    Cards(9) >= hand.Cards(9) And _
                    Cards(10) >= hand.Cards(0)
    End Function

End Class
