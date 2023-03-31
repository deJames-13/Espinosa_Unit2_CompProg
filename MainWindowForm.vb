Public Class MainWindowForm
    Private Sub btnCompute_Click(sender As Object, e As EventArgs) Handles btnCompute.Click

        'Input Validation
        If Not validInputs({txtName, txtID}) Then
            Return
        End If
        If Not validNumbers({txtQz1, txtQz2, txtQz3}) Then
            Return
        End If
        If Not validNumbers({txtUt1, txtUt2, txtUt3}) Then
            Return
        End If
        If Not validNumbers({txtMp1, txtMp2, txtMp3, txtMp4, txtTerm}) Then
            Return
        End If

        ' Get all valid inputs 
        arrQz = GetInputs({txtQz1.Text, txtQz2.Text, txtQz3.Text})
        arrUt = GetInputs({txtUt1.Text, txtUt2.Text, txtUt3.Text})
        arrMp = GetInputs({txtMp1.Text, txtMp2.Text, txtMp3.Text, txtMp4.Text})
        termTest = CInt(txtTerm.Text)

        ' Compute the necessary values
        wtGrade = weightedGrade(arrQz, arrUt, arrMp)
        fGrade = finalGrade(wtGrade)
        strRemark = getRemark(wtGrade)

        ' Print the output to the form
        PrintResults()

        ' Save Input
        Dim savedInfo As String = ConcatString({txtID.Text, txtName.Text, txtSec.Text, txtYear.Text, txtSub.Text, txtCourse.Text})

        Dim savedExams As String = $"{ConcatString(arrQz.ToArray())}{ConcatString(arrUt.ToArray())}{ConcatString(arrMp.ToArray())}{termTest}"

        Dim savedResults As String = ConcatString({(AveQz / 0.2).ToString("f2"), (AveUnit / 0.35).ToString("f2"), (AveMp / 0.25).ToString("f2"), wgTerm, wtGrade, fGrade, strRemark})

        Dim studentData As String = $"{savedInfo}/{savedExams}/{savedResults}"

        If arrUniqueID.Count > 0 And arrUniqueID.IndexOf(txtID.Text) > -1 Then
            Return
        End If
        arrUniqueID.Add(txtID.Text)
        arrStudentData.Add(studentData)
    End Sub


    Private Sub btnShowAll_Click(sender As Object, e As EventArgs) Handles btnShowAll.Click
        For idx As Integer = InfoDataGridPage.gridStdInfo.RowCount To arrStudentData.Count - 1
            Dim arr As Array = splitArray(arrStudentData, idx, "/")
            addToGrid(InfoDataGridPage.gridStdInfo, Split(arr(0), ","))
            addToGrid(InfoDataGridPage.gridExamInfo, Split(arr(1), ","))
            addToGrid(InfoDataGridPage.gridFinalInfo, Split(arr(2), ","))
        Next

        InfoDataGridPage.Show()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim s As String = searchGrid(txtID.Text)
        If s = "" Then
            Return
        End If
        Dim arr As Array = Split(s, "/")
        addToGrid(SearchedGrid.gridStdInfo, Split(arr(0), ","), False)
        addToGrid(SearchedGrid.gridExamInfo, Split(arr(1), ","), False)
        addToGrid(SearchedGrid.gridFinalInfo, Split(arr(2), ","), False)
        SearchedGrid.Show()
    End Sub

    Private Sub handle_SubjectSelection(sender As Object, e As EventArgs) Handles rdbSub1.CheckedChanged, rdbSub2.CheckedChanged, rdbSub3.CheckedChanged, rdbSub4.CheckedChanged, rdbSub5.CheckedChanged
        txtSub.Text = getRadioText({rdbSub1, rdbSub2, rdbSub3, rdbSub4, rdbSub5})
    End Sub

    Private Sub handle_CourseSelection(sender As Object, e As EventArgs) Handles rdbCourse1.CheckedChanged, rdbCourse2.CheckedChanged, rdbCourse3.CheckedChanged, rdbCourse4.CheckedChanged, rdbCourse5.CheckedChanged
        txtCourse.Text = getRadioText({rdbCourse1, rdbCourse2, rdbCourse3, rdbCourse4, rdbCourse5})
    End Sub

    Private Sub handle_YearSelection(sender As Object, e As EventArgs) Handles rdbYear1.CheckedChanged, rdbYear2.CheckedChanged, rdbYear3.CheckedChanged, rdbYear1.CheckedChanged
        txtYear.Text = getRadioText({rdbYear1, rdbYear2, rdbYear3, rdbYear4})
    End Sub

    Private Sub handle_SectionSelection(sender As Object, e As EventArgs) Handles rdbSecA.CheckedChanged, rdbSecB.CheckedChanged, rdbSecC.CheckedChanged
        txtSec.Text = getRadioText({rdbSecA, rdbSecB, rdbSecC})
    End Sub


End Class
