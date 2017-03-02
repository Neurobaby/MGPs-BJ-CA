<Serializable()> Public Class BJCAEORsClass
    Public ForcedNetGameEV As Double
    Public ForcedNetGameEVEOR As Double
    Public ForcedStratTD(21, 1) As BJCATDStratClass

    Public CDNetGameEV As Double
    Public CDNetGameEVEOR As Double

    Public TDNetGameEV As Double
    Public TDNetGameEVEOR As Double
    Public TDStratTD(21, 1) As BJCATDStratClass

    Public EVs(3084) As BJCAEOREVsClass
End Class

<Serializable()> Public Class BJCAEOREVsClass
    Public StandEV(10) As Double                'Upcard     Stand EV's relative to each dealer upcard
    Public StandPushEV(10) As Double            'Upcard     EV of pushing; can be used for special rules and variance
    Public DEV(10) As Double                    'Upcard     Double EV's relative to each dealer upcard
    Public DPushEV(10) As Double                'Upcard     EV of pushing; can be used for special rules and variance
    Public SurrEV(10) As Double                 'Upcard     Surrender EV's relative to each dealer upcard

    Public ForcedStrat(10) As Integer           'Hit EV for given strategy
    Public ForcedHitEV(10) As Double            'Hit EV for given strategy
    Public ForcedHitPushEV(10) As Double        'Hit Push EV for given strategy
    Public ForcedSplitEV(10) As Double          'EV based on the number of splits allowed

    Public CDStrat(10) As Integer               'Hit EV for given strategy
    Public CDHitEV(10) As Double                'Hit EV for given strategy
    Public CDHitPushEV(10) As Double            'Hit Push EV for given strategy
    Public CDSplitEV(10) As Double              'EV based on the number of splits allowed

    Public TDStrat(10) As Integer               'Hit EV for given strategy
    Public TDHitEV(10) As Double                'Hit EV for given strategy
    Public TDHitPushEV(10) As Double            'Hit Push EV for given strategy
    Public TDSplitEV(10) As Double              'EV based on the number of splits allowed
End Class
