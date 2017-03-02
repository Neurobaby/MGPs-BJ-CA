Imports BJ_CA.BJCAShared

<Serializable()> Public Class BJCAExceptionClass
    Public Index As Integer
    Public Upcard As Integer
    Public Paircard As Integer
    Public ExceptionType As Integer
    Public SplitHand As Integer
    Public ShoeState As String
    Public ExceptionName As String

    Public Sub New()
        Index = 0
        Upcard = 0
        Paircard = 0
        ExceptionType = BJCAGlobalsClass.ExType.None
        SplitHand = 0
        ShoeState = "Pre-Split"
        ExceptionName = ""
    End Sub

    Public Sub Empty()
        Index = 0
        Upcard = 0
        Paircard = 0
        ExceptionType = BJCAGlobalsClass.ExType.None
        SplitHand = 0
        ShoeState = "Pre-Split"
        ExceptionName = ""
    End Sub

End Class

<Serializable()> Public Class BJCAExceptionListClass
    Public NumExceptions As Integer
    Public L() As BJCAExceptionClass

    Public Sub AddException(ByVal newException As BJCAExceptionClass)
        NumExceptions += 1
        ReDim Preserve L(NumExceptions - 1)
        L(NumExceptions - 1) = New BJCAExceptionClass
        L(NumExceptions - 1) = CType(CloneObject(newException), BJCAExceptionClass)
    End Sub

End Class

<Serializable()> Public Class BJCANCardExceptionClass
    Public NCards As Integer
    Public Total As Integer
    Public Soft As Boolean
    Public Upcard As Integer
    Public Strat As Integer
    Public SEV As Double
    Public HEV As Double
    Public DEV As Double
    Public SurrEV As Double
    Public ExceptionType As Integer
    Public ExceptionName As String

    Public Sub New()
        NCards = 0
        Total = 0
        Soft = False
        Upcard = 0
        Strat = 0
        SEV = 0
        HEV = 0
        DEV = 0
        SurrEV = 0
        ExceptionType = BJCAGlobalsClass.ExType.None
        ExceptionName = ""
    End Sub

    Public Sub Empty()
        NCards = 0
        Total = 0
        Soft = False
        Upcard = 0
        Strat = 0
        SEV = 0
        HEV = 0
        DEV = 0
        SurrEV = 0
        ExceptionType = BJCAGlobalsClass.ExType.None
        ExceptionName = ""
    End Sub

End Class

<Serializable()> Public Class BJCANCardExceptionListClass
    Public NumExceptions As Integer
    Public L() As BJCANCardExceptionClass

    Public Sub AddException(ByVal newException As BJCANCardExceptionClass)
        NumExceptions += 1
        ReDim Preserve L(NumExceptions - 1)
        L(NumExceptions - 1) = New BJCANCardExceptionClass
        L(NumExceptions - 1) = CType(CloneObject(newException), BJCANCardExceptionClass)
    End Sub

End Class
