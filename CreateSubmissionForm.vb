Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class CreateSubmissionForm

    Private stopwatch As New Stopwatch()

    Private Sub CreateSubmissionForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
    End Sub

    Private Sub CreateSubmissionForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.T Then
            ' Call Toggle Stopwatch
            ToggleStopwatch()
        ElseIf e.Control AndAlso e.KeyCode = Keys.S Then
            ' Call Submit Function
            SubmitForm()
        End If
    End Sub

    Private Sub btnStopwatch_Click(sender As Object, e As EventArgs) Handles btnStopwatch.Click
        ToggleStopwatch()
    End Sub

    Private Sub ToggleStopwatch()
        If stopwatch.IsRunning Then
            stopwatch.Stop()
            timerCounter.Stop()
        Else
            stopwatch.Start()
            timerCounter.Start()
        End If
    End Sub

    Private Sub timerCounter_Tick(sender As Object, e As EventArgs) Handles timerCounter.Tick
        lblTimer.Text = stopwatch.Elapsed.ToString("hh\:mm\:ss")
    End Sub

    Private Sub UpdateTimerLabel()
        ' Display the elapsed time formatted as hh:mm:ss
        lblTimer.Text = stopwatch.Elapsed.ToString("hh\:mm\:ss")
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        SubmitForm()
    End Sub

    Private Sub SubmitForm()
        ' Collect form data
        Dim name As String = txtName.Text
        Dim email As String = txtEmail.Text
        Dim phone As String = txtPhone.Text
        Dim github As String = txtGithub.Text
        Dim stopwatchTime As String = stopwatch.Elapsed.ToString("hh\:mm\:ss")

        ' Create a Submission object
        Dim newSubmission As New Submission With {
            .Name = name,
            .Email = email,
            .Phone = phone,
            .Github = github,
            .Stopwatch_time = stopwatchTime
        }

        ' Path to your JSON file
        Dim jsonFilePath As String = "C:\Users\MYPC\Desktop\project1\SlidelyFormApp\backend\db.json"

        Try
            ' Read existing JSON data from the file
            Dim json As String = File.ReadAllText(jsonFilePath)

            ' Deserialize JSON to a List(Of Submission)
            Dim jsonData As JObject = JObject.Parse(json)
            Dim submissions As List(Of Submission) = JsonConvert.DeserializeObject(Of List(Of Submission))(jsonData("submissions").ToString())

            ' If submissions is null, create a new list
            If submissions Is Nothing Then
                submissions = New List(Of Submission)()
            End If

            ' Add the new submission to the list
            submissions.Add(newSubmission)

            ' Serialize the updated list back to JSON
            Dim updatedJson As String = JsonConvert.SerializeObject(New With {Key .submissions = submissions}, Formatting.Indented)

            ' Write the updated JSON data back to the file, overwriting existing content
            File.WriteAllText(jsonFilePath, updatedJson)

            MessageBox.Show("Submission saved successfully!")

            ' Reset the form and stopwatch for a new entry
            txtName.Clear()
            txtEmail.Clear()
            txtPhone.Clear()
            txtGithub.Clear()
            stopwatch.Reset()
            UpdateTimerLabel() ' Update the label to show 00:00:00

        Catch ex As Exception
            MessageBox.Show("Error saving submission: " & ex.Message)
        End Try
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs)
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles lblTimer.Click
    End Sub

    Private Sub txtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
    End Sub

    Private Sub Label4_Click_1(sender As Object, e As EventArgs) Handles Label4.Click
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
    End Sub

    Private Sub txtEmail_TextChanged(sender As Object, e As EventArgs) Handles txtEmail.TextChanged
    End Sub

    Private Sub txtPhone_TextChanged(sender As Object, e As EventArgs) Handles txtPhone.TextChanged
    End Sub

    Private Sub txtGithub_TextChanged(sender As Object, e As EventArgs) Handles txtGithub.TextChanged
    End Sub
End Class
