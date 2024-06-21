Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class ViewSubmissionsForm

    Private submissions As List(Of Submission) ' List to hold your submission objects
    Private currentSubmissionIndex As Integer = 0 ' Track the index of the current submission

    Private Sub ViewSubmissionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        ' Load data from JSON file
        LoadSubmissionsFromJson()

        ' Display initial submission data
        UpdateSubmissionData()
    End Sub

    Private Sub ViewSubmissionsForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            ' Call Previous Function
            PreviousSubmission()
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            ' Call Next Function
            NextSubmission()
        ElseIf e.Control AndAlso e.KeyCode = Keys.D Then
            ' Call Delete Function
            DeleteSubmission()
        ElseIf e.Control AndAlso e.KeyCode = Keys.E Then
            ' Call Edit Function
            EditSubmission()
        End If
    End Sub

    Private Sub LoadSubmissionsFromJson()
        Dim jsonFilePath As String = "C:\Users\MYPC\Desktop\project1\SlidelyFormApp\backend\db.json"

        Try
            If File.Exists(jsonFilePath) Then
                Dim json As String = File.ReadAllText(jsonFilePath)

                ' Deserialize JSON to a JObject (JSON object)
                Dim jsonObject As JObject = JObject.Parse(json)

                ' Extract the 'submissions' array from the JSON object
                Dim submissionsArray As JArray = jsonObject("submissions")

                ' Deserialize the submissions array into List(Of Submission)
                submissions = submissionsArray.ToObject(Of List(Of Submission))()

                ' Check if submissions is null or empty
                If submissions Is Nothing OrElse submissions.Count = 0 Then
                    MessageBox.Show("No submissions found in JSON file.")
                End If
            Else
                MessageBox.Show("JSON file not found!")
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading JSON file: " & ex.Message)
        End Try
    End Sub

    Public Sub UpdateSubmissionData()
        ' Update form controls with data from current submission
        If submissions IsNot Nothing AndAlso submissions.Count > 0 AndAlso currentSubmissionIndex >= 0 AndAlso currentSubmissionIndex < submissions.Count Then
            Dim currentSubmission As Submission = submissions(currentSubmissionIndex)

            ' Display submission data in text boxes or labels
            txtName.Text = currentSubmission.Name
            txtEmail.Text = currentSubmission.Email
            txtPhone.Text = currentSubmission.Phone
            txtGithub.Text = currentSubmission.Github
            txtTimer.Text = currentSubmission.Stopwatch_time
        End If
    End Sub

    Public Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        PreviousSubmission()
    End Sub

    Public Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        NextSubmission()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteSubmission()
    End Sub

    Private Sub PreviousSubmission()
        If currentSubmissionIndex > 0 Then
            currentSubmissionIndex -= 1
            UpdateSubmissionData()
        End If
    End Sub

    Private Sub NextSubmission()
        If currentSubmissionIndex < submissions.Count - 1 Then
            currentSubmissionIndex += 1
            UpdateSubmissionData()
        End If
    End Sub

    Private Sub DeleteSubmission()
        If submissions IsNot Nothing AndAlso currentSubmissionIndex >= 0 AndAlso currentSubmissionIndex < submissions.Count Then
            submissions.RemoveAt(currentSubmissionIndex)

            ' Serialize the updated submissions list back to JSON
            Dim jsonFilePath As String = "C:\Users\MYPC\Desktop\project1\SlidelyFormApp\backend\db.json"
            Try
                Dim updatedJson As String = JsonConvert.SerializeObject(New With {Key .submissions = submissions}, Formatting.Indented)
                File.WriteAllText(jsonFilePath, updatedJson)
                MessageBox.Show("Submission deleted successfully!")
                currentSubmissionIndex = Math.Min(currentSubmissionIndex, submissions.Count - 1) ' Ensure index stays within bounds
                UpdateSubmissionData() ' Update UI with the next submission or clear if list is empty
            Catch ex As Exception
                MessageBox.Show("Error deleting submission: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub EditSubmission()
        ' Implement your edit submission functionality here
        MessageBox.Show("Edit Submission functionality to be implemented.")
    End Sub

    Private Sub lblName_Click(sender As Object, e As EventArgs) Handles label2.Click
    End Sub

    Private Sub txtName_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
    End Sub

    Private Sub lblEmail_Click(sender As Object, e As EventArgs) Handles lblEmail.Click
    End Sub
End Class

' Define a class to represent your JSON structure
Public Class Submission
    Public Property Name As String
    Public Property Email As String
    Public Property Phone As String
    Public Property Github As String
    Public Property Stopwatch_time As String ' Stopwatch time formatted as hh:mm:ss
End Class
