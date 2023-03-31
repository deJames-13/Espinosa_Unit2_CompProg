Imports System.Windows

Module ComputationModule

    Public arrQz As New ArrayList()
    Public arrUt As New ArrayList()
    Public arrMp As New ArrayList()
    Public termTest As Integer
    Public AveQz As Integer
    Public AveUnit As Integer
    Public AveMp As Integer
    Public wgTerm As Integer
    Public wtGrade As Integer
    Public fGrade As Double
    Public countMiss As Integer
    Public strRemark As String

    Public studentData As String

    Public arrUniqueID As New ArrayList()
    Public arrStudentData As New ArrayList()


    Public Function getRadioText(radios As RadioButton()) As String
        Dim s As String = ""
        For Each radio In radios
            If radio.Checked Then
                s = radio.Text
                Return s
            End If
        Next
        Return s
    End Function

    Public Function GetInputs(inputs As String()) As ArrayList
        Dim list As New ArrayList()
        For Each s In inputs
            If CInt(s) = 0 Then
                countMiss += 1
            End If
            list.Add(CInt(s))
        Next
        Return list
    End Function

    Public Function weightedGrade(qz As ArrayList, ut As ArrayList, mp As ArrayList) As Integer
        AveQz = ((qz(0) + qz(1) + qz(2)) / 300) * 100 * 0.2
        AveUnit = ((ut(0) + ut(1) + ut(2)) / 300) * 100 * 0.35
        AveMp = ((mp(0) + mp(1) + mp(2) + mp(3)) / 400) * 100 * 0.25
        wgTerm = (termTest / 100) * 100 * 0.2
        Dim wt As Integer = AveQz + AveUnit + AveMp + wgTerm
        Return wt
    End Function

    Public Function finalGrade(grade As Integer) As Double
        Dim final As Double

        Select Case grade
            Case 98 To 100
                final = 1.0
            Case 91 To 97
                final = 1.25
            Case 85 To 90
                final = 1.5
            Case 79 To 84
                final = 1.75
            Case 73 To 78
                final = 2.0
            Case 67 To 72
                final = 2.25
            Case 61 To 66
                final = 2.5
            Case 55 To 60
                final = 2.75
            Case 50 To 54
                final = 3.0
            Case Else
                final = 5.0
        End Select
        Return final
    End Function

    Public Function getRemark(grade As Integer) As String
        Dim rmrk As String = "passed"
        If countMiss = 3 Then
            rmrk = "INC"
        ElseIf countMiss > 3 Then
            rmrk = "DRP"
        ElseIf grade < 50 Then
            rmrk = "failed"
        End If
        Return rmrk
    End Function

    Public Sub PrintResults()
        Dim current As MainWindowForm = MainWindowForm

        current.txtAveQz.Text = (AveQz / 0.2).ToString("f2")
        current.txtAveUT.Text = (AveUnit / 0.35).ToString("f2")
        current.txtAveMP.Text = (AveMp / 0.25).ToString("f2")
        current.txtAveTerm.Text = termTest
        current.txtWeighted.Text = wtGrade
        current.txtFinal.Text = fGrade
        current.txtRemark.Text = strRemark

    End Sub


    Public Function ConcatString(data As Array) As String
        ' Comma Separated Values
        Dim str As String = ""
        For Each s In data
            str = str & s & ","
        Next
        Return str
    End Function

    Public Function splitArray(arrDatas As ArrayList, n As Integer, d As Char) As Array
        Return Split(arrDatas(n), d)
    End Function

    Public Sub addToGrid(ByRef db As DataGridView, arrDatas As Array, Optional isNewRow As Boolean = True)
        Dim newRow As Integer = db.Rows.Add
        If Not isNewRow Then
            newRow = 0
        End If
        For col As Integer = 0 To db.ColumnCount - 1
            db.Item(col, newRow).Value = arrDatas(col)
        Next
    End Sub

    Public Function searchGrid(query As String) As String
        Dim pos As Integer = arrUniqueID.IndexOf(query)
        Dim student As String = ""
        If pos Then
            student = arrStudentData(pos)
        End If
        Return student
    End Function


    ' INPUT VALIDATORS
    Public Function isEmptyInput(ByRef input As TextBox, Optional message As String = "") As Boolean
        If String.IsNullOrEmpty(input.Text) Then
            input.Focus()
            message = "Input required for " & input.PlaceholderText
            MsgBox(message, MsgBoxStyle.Exclamation, "Empty Input!")
            Return True
        End If
        Return False
    End Function
    Public Function validNumber(ByRef str As TextBox) As Boolean
        Dim res As Boolean = False
        If Not IsNumeric(str.Text) Then
            res = False
        ElseIf (CInt(str.Text) >= 20 AndAlso CInt(str.Text) <= 100) OrElse CInt(str.Text) = 0 Then
            res = True
        End If
        Return res
    End Function
    Public Function validNumbers(str As TextBox()) As Boolean
        For Each s In str
            If Not validNumber(s) Then
                s.Clear()
                s.Focus()
                MsgBox("Examination scores must be a valid number and is not lower than 20 pts!", MsgBoxStyle.Exclamation, "Empty Input!")
                Return False
            End If
        Next
        Return True
    End Function
    Public Function validInputs(str As TextBox()) As Boolean
        For Each s In str
            If isEmptyInput(s) Then
                Return False
            End If
        Next
        Return True
    End Function



End Module
