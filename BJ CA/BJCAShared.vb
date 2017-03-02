Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO

Public Class BJCAShared

    Public Class IndexedTextBox
        Inherits System.Windows.Forms.TextBox
        Public Index As Double
        Public Index2 As Double
        Public Hand As New BJCAHandClass
    End Class

    Public Class IndexedCheckBox
        Inherits System.Windows.Forms.CheckBox
        Public Index As Double
        Public Index2 As Double
    End Class

    Public Class IndexedComboBox
        Inherits System.Windows.Forms.ComboBox
        Public Index As Double
        Public Index2 As Double
    End Class

    Public Class IndexedLabel
        Inherits System.Windows.Forms.Label
        Public Index As Double
        Public Index2 As Double
    End Class

    Public Class IndexedButton
        Inherits System.Windows.Forms.Button
        Public Index As Double
        Public Index2 As Double
    End Class

    Public Shared Function GetBackColor(ByVal num As Double, Optional ByVal usegray As Boolean = False) As Color
        If usegray Then
            GetBackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(192, Byte), CType(192, Byte))
        ElseIf num < 0 Then
            GetBackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(192, Byte), CType(192, Byte))
        ElseIf num > 0 Then
            GetBackColor = System.Drawing.Color.FromArgb(CType(192, Byte), CType(255, Byte), CType(192, Byte))
        Else
            GetBackColor = Nothing
        End If
    End Function

    Public Shared Sub FillStratTextBox(ByRef box As TextBox, ByVal strat As Integer, ByVal fulltext As Boolean, ByVal colortable As BJCAColorTableClass)
        Dim constants As New BJCAGlobalsClass

        If Not box Is Nothing Then
            If fulltext Then
                box.Text = constants.StratFullText(strat)
            Else
                box.Text = constants.StratShortText(strat)
            End If
            box.BackColor = colortable.C(strat)
        End If
    End Sub

    Public Shared Sub FillNumberTextBox(ByRef box As TextBox, ByVal val As Double, ByVal nfixed As Integer, ByVal percent As Boolean, Optional ByVal useval As Boolean = True, Optional ByVal usegray As Boolean = False)
        If Not box Is Nothing Then
            If val <> 0 And useval Then
                If percent Then
                    box.Text = CStr(Math.Round(val * 100, nfixed)) + "%"
                Else
                    box.Text = CStr(Math.Round(val, nfixed))
                End If
                box.BackColor = GetBackColor(val, usegray)
            Else
                box.Text = ""
                box.BackColor = Nothing
            End If
        End If
    End Sub

    Public Shared Function GetHandString(ByVal hand As BJCAHandClass) As String
        Dim card As Integer
        Dim i As Integer
        Dim handStr As String

        handStr = ""
        For card = 1 To 10
            For i = 1 To hand.Cards(card)
                If card = 1 Then
                    handStr = handStr & "A"
                ElseIf card = 10 Then
                    handStr = handStr & "T"
                Else
                    handStr = handStr & Format(card)
                End If
            Next i
        Next card

        GetHandString = handStr
    End Function

    Public Shared Function GetCardString(ByVal card As Integer) As String
        Dim i As Integer
        Dim cardStr As String

        If card = 1 Then
            cardStr = "A"
        ElseIf card = 10 Then
            cardStr = "T"
        Else
            cardStr = CStr(card)
        End If

        GetCardString = cardStr
    End Function

    Public Shared Function GetStringHand(ByVal handstr As String) As BJCAHandClass
        Dim i As Integer
        Dim card As String
        Dim hand As New BJCAHandClass

        For i = 0 To handstr.Length - 1
            card = handstr.Chars(i)
            Select Case card
                Case "A", "a", "1"
                    hand.Deal(1)
                Case "T", "t", "0"
                    hand.Deal(10)
                Case "2", "3", "4", "5", "6", "7", "8", "9"
                    hand.Deal(CInt(card))
                Case Else
            End Select
        Next i

        GetStringHand = hand
    End Function

    Public Shared Sub PopulateUpcardLabels(ByVal sender As System.Object, ByVal coord1 As Integer, ByVal coord2 As Integer, ByVal spacing As Integer)
        Dim i As Integer

        For i = 0 To 9
            Dim box As New Label

            box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            box.Size = New System.Drawing.Size(24, 16)
            box.Location = New System.Drawing.Point(coord1 + i * spacing, coord2)
            If i = 8 Then
                box.Text = "T"
            ElseIf i = 9 Then
                box.Text = "A"
            Else
                box.Text = i + 2
            End If
            sender.Controls.Add(box)
        Next
    End Sub

    Public Shared Function GetUpcardLabelString(ByVal nSpaces As Integer, ByVal aceFirst As Boolean) As String
        Dim i As Integer
        Dim textString As String

        For i = 1 To 10
            If aceFirst Then
                If i = 10 Then
                    textString += SetStringSize("T", nSpaces, nSpaces, StringAlignment.Center)
                ElseIf i = 1 Then
                    textString += SetStringSize("A", nSpaces, nSpaces, StringAlignment.Center)
                Else
                    textString += SetStringSize(CStr(i), nSpaces, nSpaces, StringAlignment.Center)
                End If
            Else
                If i = 9 Then
                    textString += SetStringSize("T", nSpaces, nSpaces, StringAlignment.Center)
                ElseIf i = 10 Then
                    textString += SetStringSize("A", nSpaces, nSpaces, StringAlignment.Center)
                Else
                    textString += SetStringSize(CStr(i + 1), nSpaces, nSpaces, StringAlignment.Center)
                End If
            End If
        Next

        Return textString
    End Function

    Public Shared Sub CopyIndexedTextBoxContents(ByRef originalBox As IndexedTextBox, ByVal destinationBox As IndexedTextBox)
        destinationBox.Index = originalBox.Index
        destinationBox.Index2 = originalBox.Index2
        destinationBox.Text = originalBox.Text
        destinationBox.BackColor = originalBox.BackColor
        destinationBox.Hand.Copy(originalBox.Hand)
    End Sub

    Public Shared Function CheckValidInteger(ByVal entry As String, ByVal minnum As Integer, ByVal maxnum As Integer, ByVal showMessage As Boolean) As Boolean
        Dim valid As Boolean

        valid = True
        If IsNumeric(entry) Then
            If Not (entry >= minnum And entry <= maxnum) Then
                valid = False
            End If
            If entry <> Int(entry) Then
                valid = False
            End If
        Else
            valid = False
        End If
        If Not valid And showMessage Then
            MsgBox("Please enter an integer between " + CStr(minnum) + " and " + CStr(maxnum) + ".", MsgBoxStyle.OKOnly)
        End If
        CheckValidInteger = valid
    End Function

    Public Shared Function CheckValidDecimal(ByVal entry As String, ByVal minnum As Double, ByVal maxnum As Double, ByVal showMessage As Boolean) As Boolean
        Dim valid As Boolean

        valid = True
        If IsNumeric(entry) Then
            If Not (entry >= minnum And entry <= maxnum) Then
                valid = False
            End If
        Else
            valid = False
        End If
        If Not valid And showMessage Then
            MsgBox("Please enter an value between " + CStr(minnum) + " and " + CStr(maxnum) + ".", MsgBoxStyle.OKOnly)
        End If
        CheckValidDecimal = valid
    End Function

    Public Shared Function ValidCardString(ByVal cardString As String) As Boolean
        Dim handValid As Boolean
        Dim i As Integer
        Dim card As String

        handValid = True
        For i = 0 To cardString.Length - 1
            card = cardString.Chars(i)
            Select Case card
                Case "A", "a", "1", "2", "3", "4", "5", "6", "7", "8", "9", "T", "t", "0"
                Case Else
                    handValid = False
            End Select
        Next i

        Return handValid

    End Function

    Public Shared Function CloneObject(ByVal originalObject As Object) As Object
        Dim BinFormatter As New BinaryFormatter
        Dim memStream As New MemoryStream
        BinFormatter.Serialize(memStream, originalObject)
        memStream.Position = 0
        CloneObject = BinFormatter.Deserialize(memStream)
        memStream.Close()
    End Function

    Public Shared Sub SaveObjectFile(ByVal fileName As String, ByVal saveObject As Object)
        Dim fileStream As New FileStream(fileName, FileMode.Create)
        Dim fileFormatter As New BinaryFormatter
        Try
            fileFormatter.Serialize(fileStream, saveObject)
        Finally
            fileStream.Close()
        End Try
    End Sub

    Public Shared Function LoadObjectFile(ByVal fileName As String) As Object
        Dim fileStream As New FileStream(fileName, FileMode.Open)
        Dim fileFormatter As New BinaryFormatter
        Try
            LoadObjectFile = CType(fileFormatter.Deserialize(fileStream), Object)
        Finally
            fileStream.Close()
        End Try
    End Function

    Public Shared Function GetPath(ByVal Full As String) As String
        If Full <> "" Then
            For i As Integer = Full.Length - 1 To 0 Step -1
                If Full.Substring(i, 1) = "\" OrElse Full.Substring(i, 1) = "/" Then 'Find the rightmost \ or /, which should be cut off the file part
                    Return Full.Substring(0, i) & "\"
                End If
            Next
        Else
            Return ""
        End If
    End Function

    Public Shared Function GetFileName(ByVal Full As String) As String
        If GetPath(Full) = Nothing Then
            Return Full
        Else
            Return Full.Substring(GetPath(Full).Length) 'Cut off everything up to the path
        End If
    End Function

    Public Shared Sub SortListBox(ByRef listBox As Windows.Forms.ListBox, Optional ByVal ignoreFirstChar As String = "")
        Dim i As Integer
        Dim j As Integer
        Dim maxString As String
        Dim nextString As String
        Dim tempString As String

        'Now sort the codes by value which should keep values sorted throughout
        If ignoreFirstChar <> "" Then ignoreFirstChar = ignoreFirstChar.Substring(0, 1)
        i = 0
        Do While i < listBox.Items.Count
            If ignoreFirstChar = "" Then
                maxString = listBox.Items.Item(i)
            Else
                tempString = listBox.Items.Item(i)
                If tempString.Substring(0, 1) = ignoreFirstChar Then
                    maxString = tempString.Substring(1)
                Else
                    maxString = listBox.Items.Item(i)
                End If
            End If
            For j = i + 1 To listBox.Items.Count - 1
                If ignoreFirstChar = "" Then
                    nextString = listBox.Items.Item(j)
                Else
                    tempString = listBox.Items.Item(j)
                    If tempString.Substring(0, 1) = ignoreFirstChar Then
                        nextString = tempString.Substring(1)
                    Else
                        nextString = listBox.Items.Item(j)
                    End If
                End If
                If nextString.CompareTo(maxString) < 0 Then
                    maxString = nextString
                    tempString = listBox.Items.Item(j)
                    listBox.Items.Item(j) = listBox.Items.Item(i)
                    listBox.Items.Item(i) = tempString
                End If
            Next j
            i += 1
        Loop
    End Sub

    Public Shared Function SetStringSize(ByVal oldString As String, Optional ByVal minLength As Integer = 0, Optional ByVal maxLength As Integer = 0, Optional ByVal alignType As System.Drawing.StringAlignment = StringAlignment.Far) As String
        Dim i As Integer
        Dim halfLength As Integer
        Dim newString As String

        If oldString = "" Then
            newString = ""
            For i = 1 To minLength
                newString += " "
            Next i
        Else
            If oldString.Length > maxLength And maxLength <> 0 Then
                newString = oldString.Substring(0, maxLength)
            ElseIf oldString.Length < minLength Then
                newString = oldString
                If alignType = StringAlignment.Center Then
                    halfLength = Int((minLength - oldString.Length) / 2)
                    For i = 1 To halfLength
                        newString = " " + newString
                    Next i
                    For i = 1 To minLength - oldString.Length - halfLength
                        newString += " "
                    Next i
                Else
                    For i = 1 To minLength - oldString.Length
                        If alignType = StringAlignment.Far Then
                            newString = " " + newString
                        Else
                            'alignType=StringAlignment.Near 
                            newString += " "
                        End If
                    Next i
                End If
            Else
                newString = oldString
            End If
        End If

        Return newString
    End Function

End Class
