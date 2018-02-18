Public Class ProtienSynth
    Dim HPModel() As Integer
    Private Sub BtnStart_Click(sender As Object, e As EventArgs) Handles BtnStart.Click


        If (TBPopSize.Text.Equals("")) Then
            MsgBox("Please enter all data")
            Exit Sub
        ElseIf (TBElite.Text.Equals("")) Then
            MsgBox("Please enter all data")
            Exit Sub
        ElseIf (TBCrossover.Text.Equals("")) Then
            MsgBox("Please enter all data")
            Exit Sub
        ElseIf (TBMut.Text.Equals("")) Then
            MsgBox("Please enter all data")
            Exit Sub
        ElseIf (TBProtLength.Text.Equals("")) Then
            MsgBox("Please enter all data")
            Exit Sub
        ElseIf (TBTarget.Text.Equals("")) Then
            MsgBox("Please enter all data")
            Exit Sub
        ElseIf (TBMax.Text.Equals("")) Then
            MsgBox("Please enter all data")
            Exit Sub
        End If

        If (Double.Parse(TBTarget.Text) > 0) Then
            MsgBox("Target Value must be a Negative number")
            Exit Sub
        End If
        If (Double.Parse(TBElite.Text) + Double.Parse(TBCrossover.Text) + Double.Parse(TBMut.Text) <> 1.0) Then
            MsgBox("The Sum of Elite, Mutation, and crossover rates must be a 1.0")
            Exit Sub
        End If
        If (Double.Parse(TBElite.Text) = 0 Or Double.Parse(TBCrossover.Text) = 0 Or Double.Parse(TBMut.Text) = 0) Then
            MsgBox(" Elite, Mutation, and crossover rates must be greater than 0")
            Exit Sub
        End If

        Dim GA = New GeneticAlgorithm
        GA.SetPopSize(Integer.Parse(TBPopSize.Text))
        GA.SetProteinLength(Integer.Parse(TBProtLength.Text))
        GA.SettxtTargetVal(Integer.Parse(TBTarget.Text))
        GA.SettxtMaxIterations(Integer.Parse(TBMax.Text))
        GA.SettxtEliteRate(Double.Parse(TBElite.Text))
        GA.SettxtMutationRate(Double.Parse(TBMut.Text))
        GA.SettxtCrossoverRate(Double.Parse(TBCrossover.Text))

        GA.SetHPModel(HPModel)
        GA.GetPictureObject(PBProtien)
        GA.Initialization()
        GA.Iterations1Ton()


    End Sub
    Private Sub TBProtStruct_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBProtStruct.KeyPress
        If (e.KeyChar = Convert.ToChar(13)) Then
            Dim i As Integer
            Dim ProtStruct(0 To TBProtStruct.Text.Length - 1) As String
            Dim HPModelchar(0 To TBProtStruct.Text.Length - 1) As Char
            ReDim HPModel(0 To TBProtStruct.Text.Length - 1)
            For i = 0 To TBProtStruct.Text.Length - 1
                If (TBProtStruct.Text(i) = "h" Or TBProtStruct.Text(i) = "H") Then
                    HPModel(i) = 1
                    HPModelchar(i) = "1"
                ElseIf (TBProtStruct.Text(i) = "p" Or TBProtStruct.Text(i) = "P") Then
                    HPModel(i) = 0
                    HPModelchar(i) = "0"
                Else
                    MsgBox("Please enter only h or p as valid protien components ")
                    Exit Sub
                End If

            Next i

            TBProtArray.Text = HPModelchar
            TBProtLength.Text = TBProtStruct.Text.Length
        End If

    End Sub

    Private Sub BtnDrawBest_Click(sender As Object, e As EventArgs)
        Dim GA = New GeneticAlgorithm

        GA.SetPopSize(Integer.Parse(TBPopSize.Text))
        GA.SetProteinLength(Integer.Parse(TBProtLength.Text))
        GA.SettxtTargetVal(Integer.Parse(TBTarget.Text))
        GA.SettxtMaxIterations(Integer.Parse(TBMax.Text))
        GA.SettxtEliteRate(Double.Parse(TBElite.Text))
        GA.SettxtMutationRate(Double.Parse(TBMut.Text))
        GA.SettxtCrossoverRate(Double.Parse(TBCrossover.Text))

        GA.SetHPModel(HPModel)
        GA.GetPictureObject(PBProtien)
        GA.DrawBest()
    End Sub


End Class
