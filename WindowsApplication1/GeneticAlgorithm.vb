Public Class GeneticAlgorithm
    '// Class for structure and implementation of a Genetic Algorithm developed by Michael Flot Jr.
    ' //////////////////////////////////////////////////////////////////

    '//Declaration of the structure of the population of chromosomes

    Structure genotype
        Dim Fitness As Integer
        Dim X() As Integer
        Dim Y() As Integer
        Dim Orientation() As Integer
    End Structure

    Public population() As genotype         '// assume this is 1st population or, pop1 array of Genotypes
    Public newpopulation() As genotype      '// assume this is 2nd population or, pop2array of Genotypes
    Dim HPModel() As Integer
    Dim txtPopSize, txtProteinLength, txtTargetVal, txtMaxIterations As Integer
    Dim Iterations, Fullfitness As Integer
    Dim txtMutationRate, txtCrossoverRate, txtEliteRate As Double
    Dim ValidFolding, crossoverSuccess, MutationSuccess, endProcess As Boolean
    Dim PBProtien, lblFitness, TBFitness As Object



    '///////////////////////////////////////////////////////////////////////////////
    '/***********************************************************************/
    '* Fn: SetPopSize(void)   */
    '// Used to set desired population size from user input.
    Sub SetPopSize(Size As Integer)
        txtPopSize = Size
    End Sub
    '///////////////////////////////////////////////////////////////////////////////
    '/***********************************************************************/
    '* Fn: SetHPModel(void)   */
    '// Used to set desired HP structure from user input.
    Sub SetHPModel(ProteinStructure() As Integer)
        ReDim HPModel(0 To ProteinStructure.Length - 1)
        HPModel = ProteinStructure
    End Sub
    '///////////////////////////////////////////////////////////////////////////////
    '/***********************************************************************/
    '* Fn: SettxtMaxIterations(void)   */
    '// Used to set Maximum number of Iterations from user input.
    Sub SettxtMaxIterations(MaxIterations As Integer)
        txtMaxIterations = MaxIterations
    End Sub
    '///////////////////////////////////////////////////////////////////////////////
    '/***********************************************************************/
    '* Fn: SettxtMutationRate(void)   */  
    '// Used to set Mutation Rate from user input.
    Sub SettxtMutationRate(MutationRate As Double)
        txtMutationRate = MutationRate
    End Sub
    '///////////////////////////////////////////////////////////////////////////////
    '/***********************************************************************/
    '* Fn: SetCrossoverRate(void)   */
    '// Used to set Crossover Rate from user input.
    Sub SettxtCrossoverRate(CrossoverRate As Double)
        txtCrossoverRate = CrossoverRate
    End Sub
    '///////////////////////////////////////////////////////////////////////////////
    '/***********************************************************************/
    '* Fn: SettxtEliteRate(void)   */
    '// Used to set Elite Rate from user input.
    Sub SettxtEliteRate(EliteRate As Double)
        txtEliteRate = EliteRate
    End Sub
    '///////////////////////////////////////////////////////////////////////////////
    '/***********************************************************************/
    '* Fn: SettxtTargetVal(void)   */
    '// Used to set optimal Fitness  from user input.
    Sub SettxtTargetVal(TargetVal As Integer)
        txtTargetVal = TargetVal
    End Sub
    '///////////////////////////////////////////////////////////////////////////////
    '/***********************************************************************/
    '* Fn: SetProteinLength(void)   */
    '// Used to set desired Protein Length from user input.
    Sub SetProteinLength(Length As Integer)
        txtProteinLength = Length
    End Sub

    '///////////////////////////////////////////////////////////////////////////////
    '/***********************************************************************/
    '* Fn: GetPictureObject(void)   */
    '// Used to get the picture box object in order to draw protien structure.
    Sub GetPictureObject(Picture As Object)
        PBProtien = Picture
    End Sub
    '//////////////////////////////////////////////////////////////////////////////
    '/***********************************************************************/
    '* Fn: Initialization(void)   */
    '//Step 1 of Algorithm create random initial Population
    Sub Initialization()
        ReDim population(0 To txtPopSize - 1)
        ReDim newpopulation(0 To txtPopSize - 1)
        Dim i As Long
        '//Dim j As Long
        Fullfitness = 0
        Iterations = 0
        '//Fill population with random protien structures
        For i = 0 To txtPopSize - 1
            ReDim population(i).X(0 To txtProteinLength - 1)
            ReDim population(i).Y(0 To txtProteinLength - 1)
            ReDim population(i).Orientation(0 To txtProteinLength - 1)
            ReDim newpopulation(i).X(0 To txtProteinLength - 1)
            ReDim newpopulation(i).Y(0 To txtProteinLength - 1)
            ReDim newpopulation(i).Orientation(0 To txtProteinLength - 1)
            ValidFolding = False
            RandomOrientation(i)
            'Ensure that valid foldings are created
            While (ValidFolding = False)
                RandomOrientation(i)
            End While
            'compute the fitness and total fitness

            population(i).Fitness = ComputeFitness(i)
            Fullfitness = Fullfitness + population(i).Fitness
            ' DrawFolding(i)


        Next i
        ' sort poulation by fitness
        sortByFitness()
        DrawFolding(0)
        endProcess = checkTerminationCriteria()
        Iterations = 1
    End Sub



    '/////////////////////////////////////////////////////////////////////////////////////
    '***********************************************************************
    '   Draw best folding

    Sub DrawBest()
        DrawFolding(0)
    End Sub

    '/////////////////////////////////////////////////////////////////////////////////////
    '***********************************************************************
    '   Construct a random Orientation for a Protein String for mth population
    '   Note:It will try for valid (self-avoid walk) until success

    Sub Iterations1Ton()
        Dim eliteNum, mutationNum, crossoverNum, crossoverPoint, mutationPoint, oldFitness As Integer
        Dim z, w, v, j, randomNum, placeHolder, nextPlaceHolder As Integer
        'upper and lower bounds for random
        Dim upperBound, lowerBound As Integer
        ' Probability of each chromesome breeding based on fitness
        Dim breedProbability(0 To txtPopSize - 1)
        upperBound = 0
        lowerBound = 0
        'set probability of breeding for each chromesome
        For z = 0 To txtPopSize - 1
            breedProbability(z) = setBreedingProb(population(z).Fitness, Fullfitness)

            'set the maximum number to avoid rounding error
            upperBound = upperBound + breedProbability(z)
            '  Console.WriteLine("breedProbability {0} is {1} ,{2})", z, breedProbability(z), upperBound)

        Next z

        eliteNum = txtPopSize * txtEliteRate
        ' Console.WriteLine("eliteNum Is {0} of {1})", eliteNum, txtPopSize)
        mutationNum = txtPopSize * txtMutationRate
        ' Console.WriteLine("mutationNum Is {0} of {1})", mutationNum, txtPopSize)
        crossoverNum = txtPopSize * txtCrossoverRate
        ' Console.WriteLine("crossoverNum Is {0} of {1})", crossoverNum, txtPopSize)

        crossoverPoint = (txtProteinLength / 2)
        oldFitness = Fullfitness
        Fullfitness = 0
        '//Fill New Population
        While (Not endProcess)
            'put elites from old population into new population
            For i = 0 To eliteNum - 1
                newpopulation(i) = population(i)

                newpopulation(i).Fitness = ComputeFitness(i)
                'Console.WriteLine("elite {0} fitness Is  of {1})", i, newpopulation(i).Fitness)
                Fullfitness = Fullfitness + newpopulation(i).Fitness
                If (collisionHappened(newpopulation(i))) Then
                    ' Console.WriteLine("for protien {0} in elite", i)
                End If
                If (Structurejump(newpopulation(i))) Then
                    ' Console.WriteLine("for protien {0} in elite", i)
                End If
            Next i

            'preform crossover of chromesomes from old population
            'Console.WriteLine("Starting crossover ")
            ' Console.WriteLine("                      ")
            Dim endCrossover
            endCrossover = eliteNum + crossoverNum
            For i = eliteNum To endCrossover - 1

                ' Console.WriteLine("eliteNum - 1 Is {0})", eliteNum - 1)
                ' set w randomly
                nextPlaceHolder = 0
                placeHolder = 0
                randomNum = CInt(Math.Floor((upperBound - lowerBound) * Rnd())) + lowerBound
                'Console.WriteLine("randomNum Is {0})", randomNum)
                For z = 0 To txtPopSize - 1
                    nextPlaceHolder = breedProbability(z) + placeHolder
                    If (randomNum >= placeHolder And randomNum <= nextPlaceHolder - 1) Then

                        w = z
                        'Console.WriteLine(" w  Is {0})", w)
                        Exit For
                    Else
                        placeHolder = nextPlaceHolder
                    End If
                Next z
                ' set v randomly
                nextPlaceHolder = 0
                placeHolder = 0

                randomNum = CInt(Math.Floor((upperBound - lowerBound) * Rnd())) + lowerBound
                ' Console.WriteLine("randomNum Is {0})", randomNum)
                For z = 0 To txtPopSize - 1
                    nextPlaceHolder = breedProbability(z) + placeHolder
                    If (randomNum >= placeHolder And randomNum <= nextPlaceHolder - 1) Then
                        v = z
                        ' Console.WriteLine(" v  Is {0})", v)
                        Exit For
                    Else
                        placeHolder = nextPlaceHolder
                    End If
                Next z
                'begin 1st crossover

                CrossOver(w, v, i, crossoverPoint)
                ' Console.WriteLine(" crossoverSuccess 1st try Is {0})", crossoverSuccess)
                If (crossoverSuccess = False) Then
                    'start crossover from begining
                    i = i - 1
                    GoTo Crossoverout
                End If
                'double check for collision
                If (collisionHappened(newpopulation(i))) Then
                    'Console.WriteLine("for protien {0} in CO", i)
                    GoTo Crossoverout
                    i = i - 1
                End If
                'double check For correct protien structure
                If (Structurejump(newpopulation(i))) Then
                    'Console.WriteLine("for protien {0} in CO", i)
                    GoTo Crossoverout
                    i = i - 1
                End If
                newpopulation(i).Fitness = ComputeFitness(i)
                ' Console.WriteLine("crossover {0} fitness Is  of {1})", i, newpopulation(i).Fitness)
                Fullfitness = Fullfitness + newpopulation(i).Fitness
                'begin 2nd crossover
                ' crossoverSuccess = False
                i = i + 1

                CrossOver(v, w, i, crossoverPoint)
                ' Console.WriteLine(" crossoverSuccess 2 1st try Is {0})", crossoverSuccess)
                If (crossoverSuccess = False) Then
                    'start crossover from begining
                    i = i - 2
                    GoTo Crossoverout
                End If
                'double check for collision
                If (collisionHappened(newpopulation(i))) Then
                    ' Console.WriteLine("for protien {0} in CO", i)
                    GoTo Crossoverout
                    i = i - 2
                End If
                'double check For correct protien structure
                If (Structurejump(newpopulation(i))) Then
                    'Console.WriteLine("for protien {0} in CO", i)
                    GoTo Crossoverout
                    i = i - 2
                End If
                newpopulation(i).Fitness = ComputeFitness(i)
                'Console.WriteLine("crossover {0} fitness Is  of {1})", i, newpopulation(i).Fitness)
                Fullfitness = Fullfitness + newpopulation(i).Fitness

Crossoverout:
            Next i
            '  Console.WriteLine("Starting mutation ")
            ' Console.WriteLine("                      ")
            Dim endMutation
            endMutation = endCrossover + mutationNum
            'Mutate random proteins
            For i = endCrossover To endMutation - 1
                ' Console.WriteLine("crossoverNum - 1 Is {0})", crossoverNum - 1)
                'chose index for protien from population at random
                j = CInt(Math.Floor((txtPopSize - 2 - 1) * Rnd()))
                ' Console.WriteLine("j Is {0})", j)
                '' chose point where mutation occurs at random b/w 1 and txtPopSize - 1
                mutationPoint = CInt(Math.Floor((txtProteinLength - 1) * Rnd())) + 1
                ' Console.WriteLine("mutationPoint Is {0})", mutationPoint)
                Mutation(j, i, mutationPoint)
                ' Console.WriteLine(" MutationSuccess 1st try Is {0})", MutationSuccess)
                If (MutationSuccess = False) Then
                    'start mutation from begining
                    i = i - 1
                    GoTo Mutationout
                End If
                'double check for collision 
                If (collisionHappened(newpopulation(i))) Then
                    ' Console.WriteLine("for protien {0} in mut", i)
                    GoTo Mutationout
                    i = i - 1
                End If
                'double check For correct protien structure
                If (Structurejump(newpopulation(i))) Then
                    'Console.WriteLine("for protien {0} in mut", i)
                    GoTo Mutationout
                    i = i - 1
                End If
                newpopulation(i).Fitness = ComputeFitness(i)
                Fullfitness = Fullfitness + newpopulation(i).Fitness
Mutationout:
            Next i

            population = newpopulation
            Iterations = Iterations + 1
            'Console.WriteLine("Iterations {0} of {1})", Iterations, txtMaxIterations)
            'Console.WriteLine("                                                 ")
            'Console.WriteLine("*************************************************")
            '  Console.WriteLine("*************************************************")
            sortByFitness()
            endProcess = checkTerminationCriteria()
            DrawBest()
        End While
        newpopulation = Nothing
        'ReDim population(i).X(0 To txtProteinLength - 1)
        ' ReDim population(i).Y(0 To txtProteinLength - 1)
        ' ReDim population(i).Orientation(0 To txtProteinLength - 1)
        'ReDim newpopulation(i).X(0 To txtProteinLength - 1)
        'ReDim newpopulation(i).Y(0 To txtProteinLength - 1)
        'ValidFolding = False
        'RandomOrientation(i)

        'While (ValidFolding = False)
        'RandomOrientation(i)
        ' End While
        ' DrawFolding(i)






    End Sub

    '/////////////////////////////////////////////////////////////////////////////////////
    'set probabilites for breeding of a chromesome based on fitness
    '***********************************************************************
    Function setBreedingProb(fit As Integer, totalFit As Integer) As Integer
        Dim percent As Double
        Dim weight As Integer
        percent = fit / totalFit
        weight = txtPopSize * percent
        Return weight
    End Function
    '/////////////////////////////////////////////////////////////////////////////////////
    'Bubble sort population by fitness
    '***********************************************************************
    Sub sortByFitness()
        Dim i As Integer
        Dim tempPopulation(0 To txtPopSize - 1) As genotype
        Dim tempGenotype As genotype
        Dim doneSorting As Boolean
        ' //tempPopulation(0) = population(0)
        doneSorting = False

        While (Not doneSorting)

            doneSorting = True

            For i = 0 To txtPopSize - 2
                'Console.WriteLine("Fitness {0} Is {1})", i, population(i).Fitness)
                'For k = 0 To txtPopSize - 2
                'Console.WriteLine("Fitness {0} Is {1})", i, population(i).Fitness)
                'Console.WriteLine("Fitness {0} Is {1})", i + 1, population(i + 1).Fitness)
                If (population(i).Fitness > population(i + 1).Fitness) Then
                    doneSorting = False
                    tempGenotype = population(i)
                    population(i) = population(i + 1)
                    population(i + 1) = tempGenotype
                    'Console.WriteLine("Fitness {0} Is {1})", i, population(i).Fitness)
                    ' Console.WriteLine("Fitness {0} Is {1})", i + 1, population(i + 1).Fitness)

                End If


                'Next k


            Next i
            'Console.WriteLine("doneSorting = {0}", doneSorting)
        End While
        For i = 0 To txtPopSize - 1
            '  Console.WriteLine("Fitness {0} Is {1})", i, population(i).Fitness)



            'Console.WriteLine("tempFitness {0} Is {1})", i, tempPopulation(i).Fitness)
        Next i


    End Sub
    '/////////////////////////////////////////////////////////////////////////////////////
    '***********************************************************************
    'Check to see if termination criteria have been met. If so,draw the best folding
    Function checkTerminationCriteria() As Boolean
        Dim i As Integer
        Dim Terminate As Boolean
        Terminate = False
        If Iterations = txtMaxIterations Then
            DrawFolding(0)
            Terminate = True
        End If
        For i = 0 To txtPopSize - 1
            If (population(i).Fitness = txtTargetVal) Then
                DrawFolding(i)
                Terminate = True
            End If


        Next i
        Return Terminate
    End Function
    '/////////////////////////////////////////////////////////////////////////////////////////////////

    '************************************************************/
    '* **********************************************************
    'CrossOver i,j = ith and jth polulation, n = cross point
    'First part of i and 2nd part of j.
    'Return: 1 => Success, 0=> Failure.
    'n is already n-1

    'Call Style:
    '1st:         CurrentPosNewPopulation = CurrentPosNewPopulation + CrossOver(i, j, n)
    '2nd:         CurrentPosNewPopulation = CurrentPosNewPopulation + CrossOver(j, i, n)


    Sub CrossOver(i As Long, j As Long, CurrentPosNewPopulation As Integer, n As Integer)

        Dim PrevDirection, k, z, p As Long
        Dim temp1, temp2, temp3, Collision, dx, dy, Step2 As Long
        Dim id As Long


        Dim CrossoverInternalFailCount As Integer

        Dim a(0 To 3) As Long
        Dim Ax(0 To 3) As Long
        Dim Ay(0 To 3) As Long
        crossoverSuccess = False
        id = CurrentPosNewPopulation

        '/* Detect Previous Direction */
        If (population(i).X(n - 1) = population(i).X(n - 2)) Then
            p = population(i).Y(n - 2) - population(i).Y(n - 1)
            If (p = 1) Then
                PrevDirection = 3
            Else
                PrevDirection = 4
            End If

        Else
            p = population(i).X(n - 2) - population(i).X(n - 1)
            If (p = 1) Then
                PrevDirection = 1
            Else
                PrevDirection = 2
            End If
        End If


        Select Case PrevDirection
            Case 1
                Ax(1) = -1
                Ay(1) = 0
                Ax(2) = 0
                Ay(2) = 1
                Ax(3) = 0
                Ay(3) = -1
            Case 2
                Ax(1) = 1
                Ay(1) = 0
                Ax(2) = 0
                Ay(2) = 1
                Ax(3) = 0
                Ay(3) = -1
            Case 3
                Ax(1) = 1
                Ay(1) = 0
                Ax(2) = -1
                Ay(2) = 0
                Ax(3) = 0
                Ay(3) = -1

            Case 4
                Ax(1) = 1
                Ay(1) = 0
                Ax(2) = -1
                Ay(2) = 0
                Ax(3) = 0
                Ay(3) = 1
        End Select

        temp1 = Int(Rnd() * 3 + 1)
        'Console.WriteLine("temp1 = ({0})", temp1)
        'determine location of next particle
        newpopulation(id).X(n) = population(i).X(n - 1) + Ax(temp1)
        newpopulation(id).Y(n) = population(i).Y(n - 1) + Ay(temp1)
        ' Console.WriteLine("t1 particle {0}'s coordinates are ({1},{2})", n - 1, population(i).X(n - 1), population(i).Y(n - 1))
        'Console.WriteLine("t1 particle {0}'s coordinates are ({1},{2})", n, newpopulation(id).X(n), newpopulation(id).Y(n))
        Collision = 0

        dx = newpopulation(id).X(n) - population(j).X(n)
        dy = newpopulation(id).Y(n) - population(j).Y(n)

        'Console.WriteLine("t2 particle {0}'s coordinates are ({1},{2})", n, population(j).X(n), population(j).Y(n))
        'Console.WriteLine("t2 dx  dy  are ({1},{2})", n, dx, dy)
        For k = n To txtProteinLength - 1
            newpopulation(id).X(k) = population(j).X(k) + dx

            newpopulation(id).Y(k) = population(j).Y(k) + dy
            'collision check
            For z = 0 To n - 1

                If ((newpopulation(id).X(k) = population(i).X(z)) And (newpopulation(id).Y(k) = population(i).Y(z))) Then
                    Collision = 1
                    ' Console.WriteLine("Collision error 1 ")


                    GoTo MyOut1

                End If
                ' Console.WriteLine("t3 particle {0}'s coordinates are ({1},{2})", k - 1, population(j).X(k - 1), population(j).Y(k - 1))
                'Console.WriteLine("t3 particle {0}'s coordinates are ({1},{2})", k, newpopulation(id).X(k), newpopulation(id).Y(k))
            Next z
        Next k

MyOut1:
        If (Collision = 1) Then         '/* ======> Second try ==== */
            Collision = 0
            Step2 = 6 - temp1
            'Console.WriteLine("Step2 {0} ", Step2)
            Select Case Step2
                Case 3
                    If Int(Rnd() * 2 + 1) = 1 Then
                        temp2 = 1
                    Else
                        temp2 = 2
                    End If

                Case 4
                    If Int(Rnd() * 2 + 1) = 1 Then
                        temp2 = 1
                    Else
                        temp2 = 3
                    End If

                Case 5
                    If Int(Rnd() * 2 + 1) = 1 Then
                        temp2 = 2
                    Else
                        temp2 = 3
                    End If
            End Select

            temp3 = 6 - (temp1 + temp2)
            newpopulation(id).X(n) = population(i).X(n - 1) + Ax(temp2)
            newpopulation(id).Y(n) = population(i).Y(n - 1) + Ay(temp2)
            ' Console.WriteLine("t4 particle {0}'s coordinates are ({1},{2})", n - 1, population(i).X(n - 1), population(i).Y(n - 1))
            'Console.WriteLine("t4 particle {0}'s coordinates are ({1},{2})", n, newpopulation(id).X(n), newpopulation(id).Y(n))
            dx = newpopulation(id).X(n) - population(j).X(n)
            dy = newpopulation(id).Y(n) - population(j).Y(n)
            ' Console.WriteLine("t5 dx  dy {0}'s coordinates are ({1},{2})", n, dx, dx)
            'Console.WriteLine("t5 dy {0}'s coordinates are ({1},{2})", n, population(j).X(n), population(j).Y(n))
            For k = n To txtProteinLength - 1

                newpopulation(id).X(k) = population(j).X(k) + dx
                newpopulation(id).Y(k) = population(j).Y(k) + dy

                For z = 0 To n - 1
                    If ((newpopulation(id).X(k) = population(i).X(z)) And (newpopulation(id).Y(k) = population(i).Y(z))) Then
                        Collision = 1
                        'Console.WriteLine("Collision error 2 ")
                        'CrossoverCollisionCount = CrossoverCollisionCount + 1
                        CrossoverInternalFailCount = CrossoverInternalFailCount + 1
                        GoTo MyOut2
                    End If
                Next z
            Next k

MyOut2:
            If (Collision = 1) Then
                Collision = 0
                newpopulation(id).X(n) = population(i).X(n - 1) + Ax(temp3)
                newpopulation(id).Y(n) = population(i).Y(n - 1) + Ay(temp3)
                dx = newpopulation(id).X(n) - population(j).X(n)
                dy = newpopulation(id).Y(n) - population(j).Y(n)
                For k = n To txtProteinLength - 1
                    newpopulation(id).X(k) = population(j).X(k) + dx
                    newpopulation(id).Y(k) = population(j).Y(k) + dy
                    For z = 0 To n - 1
                        If ((newpopulation(id).X(k) = population(i).X(z)) And (newpopulation(id).Y(k) = population(i).Y(z))) Then
                            Collision = 1
                            ' Console.WriteLine("Collision error 3 ")
                            CrossoverInternalFailCount = CrossoverInternalFailCount + 1
                            'CrossoverCollisionCount = CrossoverCollisionCount + 1
                            GoTo MyOut3
                        End If
                        ' Console.WriteLine("t6 particle {0}'s coordinates are ({1},{2})", k - 1, population(j).X(k - 1), population(j).Y(k - 1))
                        ' Console.WriteLine("t6 particle {0}'s coordinates are ({1},{2})", k, newpopulation(id).X(k), newpopulation(id).Y(k))
                    Next z
                Next k
            End If '/* 3rd try if ends */
        End If '/* 2nd try if ends */

MyOut3:
        If Collision = 0 Then
            '   CrossoverSuccessCount = CrossoverSuccessCount + 1
            For k = 0 To n - 1
                newpopulation(id).X(k) = population(i).X(k)
                newpopulation(id).Y(k) = population(i).Y(k)
            Next k

            crossoverSuccess = True

            newpopulation(id).Orientation(0) = 0
            newpopulation(id).Orientation(1) = 1
            For z = 2 To txtProteinLength - 1
                If (newpopulation(id).X(z) - newpopulation(id).X(z - 1) = 1) Then
                    'moved right
                    newpopulation(id).Orientation(z) = 1
                ElseIf (newpopulation(id).X(z) - newpopulation(id).X(z - 1) = -1) Then
                    'moved left
                    newpopulation(id).Orientation(z) = 2
                ElseIf (newpopulation(id).Y(z) - newpopulation(id).Y(z - 1) = -1) Then
                    'moved up
                    newpopulation(id).Orientation(z) = 3
                ElseIf (newpopulation(id).Y(z) - newpopulation(id).Y(z - 1) = 1) Then
                    'moved down
                    newpopulation(id).Orientation(z) = 4

                End If



                ' Console.WriteLine("crossover at {0})", n)
                ' Console.WriteLine("present direction me {0} Is {1})", z, population(id).Orientation(z))
                ' Console.WriteLine("previous direction me {0} Is {1})", z, population(id).Orientation(z - 1))
                ' Console.WriteLine("particle {0}'s coordinates are ({1},{2})", z - 1, population(id).X(z - 1), population(id).Y(z - 1))
                'Console.WriteLine("particle {0}'s coordinates are ({1},{2})", z, population(id).X(z), population(id).Y(z))
                'Console.WriteLine("****************************************************************")
                ' Console.WriteLine("                                                                ")
            Next z
            'CrossOver = 1

        Else
            '  CrossOver = 0
            crossoverSuccess = False


        End If
        ' CrossoverFailCount = CrossoverFailCount + 1

        'CrossOver = CalculateThePaths(i, n, id) ' if successful should return 1 else 0
        ' If CrossOver = 0 Then
        'CrossoverFailCountafterDFS = CrossoverFailCountafterDFS + 1
        'End If


    End Sub
    '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    Sub Mutation(i As Long, CurrentPosNewPopulation As Integer, n As Integer)
        Dim id As Long
        Dim a As Long
        Dim b As Long
        Dim A_Limit As Long
        Dim choice As Long
        Dim Collision As Long
        Dim k As Long
        Dim z As Long
        Dim p As Long


        Dim Ary(0 To 2) As Long

        id = CurrentPosNewPopulation

        ' possible rotations 90ß,180ß,270ß
        '           index       1   2    3
        '


        Ary(0) = 1
        Ary(1) = 2
        Ary(2) = 3
        A_Limit = 2

        a = population(i).X(n)          '/* (a, b) rotating point */
        b = population(i).Y(n)

        Do
            Collision = 0
            If (A_Limit > 0) Then
                choice = Int(A_Limit * Rnd())
            Else
                choice = A_Limit
            End If


            p = Ary(choice)
            For k = choice To A_Limit - 1
                Ary(k) = Ary(k + 1)
            Next k

            A_Limit = A_Limit - 1

            For k = n To txtProteinLength - 1
                Select Case p

                    Case 1
                        newpopulation(id).X(k) = a + b - population(i).Y(k)       '/* X' = (a+b)-Y  */
                        newpopulation(id).Y(k) = population(i).X(k) + b - a       '/* Y' = (X+b)-a  */
                    Case 2
                        newpopulation(id).X(k) = 2 * a - population(i).X(k)       '/* X' = (2a - X) */
                        newpopulation(id).Y(k) = 2 * b - population(i).Y(k)       '/* Y' = (2b-Y)   */
                    Case 3
                        newpopulation(id).X(k) = population(i).Y(k) + a - b       '/* X' =  Y+a-b   */
                        newpopulation(id).Y(k) = a + b - population(i).X(k)       '/* Y' =  (a+b)-X */
                End Select

                For z = 0 To n - 1

                    If ((newpopulation(id).X(k) = population(i).X(z)) And (newpopulation(id).Y(k) = population(i).Y(z))) Then
                        Collision = 1
                        ' MutationInternalFailCount = MutationInternalFailCount + 1
                        'MutationCollisionCount = MutationCollisionCount + 1
                        GoTo MyJump
                    End If
                Next z
            Next k

            If (Collision = 0) Then
                A_Limit = 0
            End If
MyJump:
        Loop Until A_Limit = 0

        If (Collision = 0) Then

            For k = 0 To n - 1
                newpopulation(id).X(k) = population(i).X(k)
                newpopulation(id).Y(k) = population(i).Y(k)
            Next k


            MutationSuccess = True

            newpopulation(id).Orientation(0) = 0
            newpopulation(id).Orientation(1) = 1
            For z = 2 To txtProteinLength - 1
                If (newpopulation(id).X(z) - newpopulation(id).X(z - 1) = 1) Then
                    'moved right
                    newpopulation(id).Orientation(z) = 1
                ElseIf (newpopulation(id).X(z) - newpopulation(id).X(z - 1) = -1) Then
                    'moved left
                    newpopulation(id).Orientation(z) = 2
                ElseIf (newpopulation(id).Y(z) - newpopulation(id).Y(z - 1) = -1) Then
                    'moved up
                    newpopulation(id).Orientation(z) = 3
                ElseIf (newpopulation(id).Y(z) - newpopulation(id).Y(z - 1) = 1) Then
                    'moved down
                    newpopulation(id).Orientation(z) = 4

                End If



                ' Console.WriteLine("crossover at {0})", n)
                ' Console.WriteLine("present direction me {0} Is {1})", z, population(id).Orientation(z))
                ' Console.WriteLine("previous direction me {0} Is {1})", z, population(id).Orientation(z - 1))
                ' Console.WriteLine("particle {0}'s coordinates are ({1},{2})", z - 1, population(id).X(z - 1), population(id).Y(z - 1))
                'Console.WriteLine("particle {0}'s coordinates are ({1},{2})", z, population(id).X(z), population(id).Y(z))
                'Console.WriteLine("****************************************************************")
                ' Console.WriteLine("                                                                ")
            Next z
        Else
            '  MutationFailCount = MutationFailCount + 1
            MutationSuccess = False
        End If

    End Sub
    ' *****************************************************************
    '//////////////////////////////////////////////////////////////////////////////////////////////////////////

    '/////////////////////////////////////////////////////////////////////////////////////
    '***********************************************************************
    '   Construct a random Orientation for a Protein String for mth population
    '   Note:It will try for valid (self-avoid walk) until success

    Sub RandomOrientation(m As Long)

        Dim PreviousDirection, PresentDirection As Long
        Dim i, temp1, temp2, temp3, X, Y, j, Flag, Step2 As Long
        Dim a(0 To 3) As Long
        Dim Ax(0 To 3) As Long
        Dim Ay(0 To 3) As Long

        '                                        3
        '             Select Direction as:     2 X 1
        '                                        4
        '

        ValidFolding = True
        population(m).X(0) = 0
        population(m).Y(0) = 0
        population(m).Orientation(0) = 0
        ' Console.WriteLine("present direction 0 Is 0)")
        population(m).X(1) = 1
        population(m).Y(1) = 0
        'Console.WriteLine("present direction 1 Is 1)")
        population(m).Orientation(1) = 1
        PreviousDirection = 1


        For i = 2 To txtProteinLength - 1

            Select Case PreviousDirection
                Case 1
                    Ax(1) = -1
                    Ay(1) = 0
                    Ax(2) = 0
                    Ay(2) = 1
                    Ax(3) = 0
                    Ay(3) = -1
                Case 2
                    Ax(1) = 1
                    Ay(1) = 0
                    Ax(2) = 0
                    Ay(2) = 1
                    Ax(3) = 0
                    Ay(3) = -1
                Case 3
                    Ax(1) = 1
                    Ay(1) = 0
                    Ax(2) = -1
                    Ay(2) = 0
                    Ax(3) = 0
                    Ay(3) = -1

                Case 4
                    Ax(1) = 1
                    Ay(1) = 0
                    Ax(2) = -1
                    Ay(2) = 0
                    Ax(3) = 0
                    Ay(3) = 1
            End Select

            temp1 = Int(3 * Rnd() + 1)
            PresentDirection = temp1
            temp2 = 0
            temp3 = 0
            X = population(m).X(i - 1) + Ax(temp1)
            Y = population(m).Y(i - 1) + Ay(temp1)
            Flag = 0

            For j = 0 To i - 1
                If (X = population(m).X(j) And Y = population(m).Y(j)) Then
                    Flag = 1
                    GoTo MyJump1
                End If
            Next j

MyJump1:
            If (Flag = 1) Then
                Flag = 0
                Step2 = 6 - temp1
                Select Case Step2
                    Case 3
                        If Int(Rnd() * 2 + 1) = 1 Then
                            temp2 = 1
                        Else
                            temp2 = 2
                        End If
                    Case 4
                        If Int(Rnd() * 2 + 1) = 1 Then
                            temp2 = 1
                        Else
                            temp2 = 3
                        End If
                    Case 5
                        If Int(Rnd() * 2 + 1) = 1 Then
                            temp2 = 2
                        Else
                            temp2 = 3
                        End If
                End Select

                PresentDirection = temp2
                temp3 = 6 - (temp1 + temp2)
                X = population(m).X(i - 1) + Ax(temp2)
                Y = population(m).Y(i - 1) + Ay(temp2)

                For j = 0 To i - 1
                    If (X = population(m).X(j) And Y = population(m).Y(j)) Then
                        Flag = 1
                        GoTo MyJump2
                    End If
                Next j
MyJump2:
                If (Flag = 1) Then
                    Flag = 0
                    PresentDirection = temp3
                    X = population(m).X(i - 1) + Ax(temp3)
                    Y = population(m).Y(i - 1) + Ay(temp3)
                    For j = 0 To i - 1
                        If (X = population(m).X(j) And Y = population(m).Y(j)) Then
                            Flag = 1
                            ValidFolding = False

                            'GoTo MyJump3

                        End If
                    Next j
                End If
            End If
            PreviousDirection = a(PresentDirection)
            population(m).X(i) = population(m).X(i - 1) + Ax(PresentDirection)
            population(m).Y(i) = population(m).Y(i - 1) + Ay(PresentDirection)
            'set orientation of structure

            If (population(m).X(i) - population(m).X(i - 1) = 1) Then
                'moved right
                population(m).Orientation(i) = 1
            ElseIf (population(m).X(i) - population(m).X(i - 1) = -1) Then
                'moved left
                population(m).Orientation(i) = 2
            ElseIf (population(m).Y(i) - population(m).Y(i - 1) = -1) Then
                'moved up
                population(m).Orientation(i) = 3
            ElseIf (population(m).Y(i) - population(m).Y(i - 1) = 1) Then
                'moved down
                population(m).Orientation(i) = 4

            End If
            '//population(m).Orientation(i) = PreviousDirection
            '    Console.WriteLine("present direction {0} Is {1})", i, PresentDirection)
            '    Console.WriteLine("present direction me {0} Is {1})", i, population(m).Orientation(i))

            'Console.WriteLine("previous direction {0} Is {1})", i, PreviousDirection)
            ' Console.WriteLine("previous direction me {0} Is {1})", i, population(m).Orientation(i - 1))
            'Console.WriteLine("particle {0}'s coordinates are ({1},{2})", i - 1, population(m).X(i - 1), population(m).Y(i - 1))
            'Console.WriteLine("particle {0}'s coordinates are ({1},{2})", i, population(m).X(i), population(m).Y(i))


        Next i
MyJump3:

    End Sub

    '* ***************************************************
    'Full Fitness Computation for nth newpopulation

    Function ComputeFitness(n As Long) As Integer

        Dim F, i, j, k As Long
        F = 0
        For i = 0 To txtProteinLength - 2
            For j = i + 1 To txtProteinLength - 1
                '

                'check if two components of the protein are both Hydrophobic
                If (HPModel(i) = 1 And HPModel(j) = 1) Then


                    ' Console.WriteLine("particle {0}'s coordinates are ({1},{2})", i, population(n).X(i), population(n).Y(i))
                    ' Console.WriteLine("particle {0}'s coordinates are ({1},{2})", j, population(n).X(j), population(n).Y(j))

                    If (population(n).X(i) = population(n).X(j)) Then
                        If (population(n).Y(i) - population(n).Y(j) = 1 Or population(n).Y(i) - population(n).Y(j) = -1) Then

                            If (j > i + 1) Then
                                For k = i + 1 To j

                                    ' If it went up or down at some point meaning it can't be directly connected since this is based on x coordinates
                                    If (population(n).Orientation(k) = 3 Or population(n).Orientation(k) = 4) Then
                                        'Console.WriteLine(":)")
                                        F = F - 1
                                        ' Console.WriteLine(F)
                                        Exit For
                                    End If
                                Next k
                            End If
                        End If

                    ElseIf (population(n).Y(i) = population(n).Y(j)) Then
                        If (population(n).X(i) - population(n).X(j) = 1 Or population(n).X(i) - population(n).X(j) = -1) Then
                            If (j > i + 1) Then

                                For k = i + 1 To j
                                    ' If it went left or right at some point meaning it can't be directly connected since this is based on y coordinates
                                    If (population(n).Orientation(k) = 1 Or population(n).Orientation(k) = 2) Then
                                        ' Console.WriteLine(":)")
                                        F = F - 1
                                        ' Console.WriteLine(F)
                                        Exit For
                                    End If
                                Next k

                            End If

                        End If
                    End If


                End If
            Next j
        Next i

        Return F
        'ComputeFullFitnessNewPop = F

    End Function

    '************************************************************/
    '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Function collisionHappened(protein As genotype) As Boolean
        Dim i, k As Integer
        Dim Fail As Boolean
        For i = 0 To protein.X.Length - 1
            For k = 0 To protein.X.Length - 1
                If (protein.X(i) = protein.X(k) And protein.Y(i) = protein.Y(k)) Then
                    If (i <> k) Then
                        ' Console.WriteLine("there was a collision between Particle {0} and Particle {1} at ({2},{3})", i, k, protein.X(i), protein.Y(i))
                        Fail = True
                        Return Fail
                    End If

                End If
            Next k
        Next i
        Fail = False
        Return Fail
    End Function
    '/////////////////////////////////////////////////////////////////////////////////////////////////
    '************************************************************/
    'checks to make sure protien structure stays intact
    '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Function Structurejump(protein As genotype) As Boolean
        Dim i As Integer
        Dim Fail As Boolean
        For i = 0 To protein.X.Length - 2
            Dim Dx, Dy As Integer
            Dx = protein.X(i) - protein.X(i + 1)
            Dy = protein.Y(i) - protein.Y(i + 1)
            If (Dx > 1 Or Dx < -1) Then
                '  Console.WriteLine("there was a Structure fail between Particle {0} and Particle {1} it jumped from ({2},{3}) to ({4},{5})", i, i + 1, protein.X(i), protein.Y(i), protein.X(i + 1), protein.Y(i + 1))
                Fail = True
                Return Fail
            ElseIf (Dy > 1 Or Dy < -1) Then
                ' Console.WriteLine("there was a Structure fail between Particle {0} and Particle {1} it jumped from ({2},{3}) to ({4},{5})", i, i + 1, protein.X(i), protein.Y(i), protein.X(i + 1), protein.Y(i + 1))
                Fail = True
                Return Fail
            End If

        Next i
        Fail = False
        Return Fail
    End Function
    '/////////////////////////////////////////////////////////////////////////////////////////////////
    '************************************************************/
    '////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    Sub DrawFolding(i As Long)
        Dim dx As Long
        Dim dy As Long
        Dim dx1 As Long
        Dim dy1 As Long
        Dim dx2 As Long
        Dim dy2 As Long


        Dim massx As Double
        Dim massy As Double




        Dim GraphUnit As Long
        Dim SmallLength As Long
        Dim k As Long

        Dim g As Graphics
        g = PBProtien.CreateGraphics
        ' Clear picture box

        g.Clear(Color.White)

        dx = PBProtien.Width / 2
        dy = PBProtien.Height / 2

        SmallLength = PBProtien.Width

        If SmallLength > PBProtien.Height Then
            SmallLength = PBProtien.Height
        End If

        SmallLength = (SmallLength / 2) '/ 2
        If (txtProteinLength < 10) Then
            GraphUnit = SmallLength / (txtProteinLength * 2)
        ElseIf (txtProteinLength < 20 And txtProteinLength >= 10) Then
            GraphUnit = SmallLength / (txtProteinLength)
        ElseIf (txtProteinLength < 38 And txtProteinLength >= 20) Then
            GraphUnit = SmallLength / (txtProteinLength / 2)
        ElseIf (txtProteinLength < 44 And txtProteinLength >= 38) Then
            GraphUnit = SmallLength / (txtProteinLength / 2.5)
        ElseIf (txtProteinLength < 50 And txtProteinLength >= 44) Then
            GraphUnit = SmallLength / (txtProteinLength / 2.75)
        Else
            GraphUnit = SmallLength / (txtProteinLength / 4)
        End If

        ' Create string to draw.

        Dim drawString As [String] = "Fitness = " + Str(population(i).Fitness) '+ "/" + Str(txtTargetValue)

        ' Create font and brush.
        Dim drawFont As New Font("Arial", 16)
        Dim drawBrush As New SolidBrush(Color.Black)

        ' Create rectangle for drawing.

        Dim x As Single = PBProtien.Width - 200
        Dim y As Single = 20
        Dim width As Single = 150
        Dim height As Single = 20
        Dim drawRect As New RectangleF(x, y, width, height)

        ' Draw rectangle to screen.
        Dim blackPen As New Pen(Color.Black)
        'e.Graphics.DrawRectangle(blackPen, x, y, width, height)

        ' Set format of string.
        Dim drawFormat As New StringFormat
        drawFormat.Alignment = StringAlignment.Center



        ' Draw string to screen.
        g.DrawString(drawString, drawFont, drawBrush, drawRect, drawFormat)
        Dim redPen As New Pen(Color.Red, 4)
        x = 30
        y = 20
        width = 20
        height = 20
        Dim HRect As New RectangleF(x, y, width, height)
        drawRect.Contains(x, y)
        drawRect.Height = height
        drawRect.Width = width
        drawString = "H"

        Dim greenPen As New Pen(Color.Green, 4)
        g.DrawEllipse(redPen, 10, 30 - 4, 8, 8)
        g.DrawString(drawString, drawFont, drawBrush, HRect, drawFormat)
        g.DrawEllipse(greenPen, 60, 30 - 4, 8, 8)
        x = 80
        y = 20
        width = 20
        height = 20
        Dim PRect As New RectangleF(x, y, width, height)
        drawString = "P"
        g.DrawString(drawString, drawFont, drawBrush, PRect, drawFormat)

        If (HPModel(0) <> 0) Then
            g.DrawEllipse(redPen, dx - 4, dy - 4, 8, 8)
            ' PBProtien.Circle(dx, dy), 70, vbRed
        Else
            g.DrawEllipse(greenPen, dx - 4, dy - 4, 8, 8)
            'PBProtien.Circle(dx, dy), 70, vbGreen
        End If

        dx1 = dx
        dy1 = dy

        For k = 1 To txtProteinLength - 1
            dx2 = population(i).X(k) * GraphUnit + dx
            dy2 = population(i).Y(k) * GraphUnit + dy
            If (HPModel(k) <> 0) Then

                g.DrawEllipse(redPen, dx2 - 4, dy2 - 4, 8, 8)
                ' PBProtien.Circle(dx, dy), 70, vbRed
            Else
                g.DrawEllipse(greenPen, dx2 - 4, dy2 - 4, 8, 8)
                'PBProtien.Circle(dx, dy), 70, vbGreen
            End If
            g.DrawLine(Pens.Black, dx1, dy1, dx2, dy2)
            'PBProtien.Line(dx1, dy1)-(dx2, dy2), vbBlack

            dx1 = dx2
            dy1 = dy2

        Next k

        ' Draw the Centre of mass
        massx = 0
        massy = 0
        For k = 0 To txtProteinLength - 1

            massx = massx + population(i).X(HPModel(k))
            massy = massy + population(i).Y(HPModel(k))

        Next k
        massx = massx / (txtProteinLength)
        massy = massy / (txtProteinLength)
        Dim COMx, COMy As Integer
        COMx = massx * GraphUnit + dx
        COMy = massy * GraphUnit + dy
        ' g.DrawEllipse(Pens.Red, dx, dy, 70, 70)
        g.DrawEllipse(Pens.Blue, (COMx), (COMy), 6, 6)
        'PBProtien.Circle(massx * GraphUnit + dx, massy * GraphUnit + dy), 10, vbBlue
        redPen.Dispose()
        greenPen.Dispose()
        g.Dispose()
        drawFont.Dispose()
        drawBrush.Dispose()
        blackPen.Dispose()





    End Sub

End Class
