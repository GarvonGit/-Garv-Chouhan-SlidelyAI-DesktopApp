Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Dim createForm As New CreateSubmissionForm()
        createForm.Show()
    End Sub

    Private Sub btnViewSubmissions_Click(sender As Object, e As EventArgs) Handles btnViewSubmissions.Click
        ViewSubmissions()
    End Sub

    Private Sub btnCreateSubmission_Click(sender As Object, e As EventArgs) Handles btnCreateSubmission.Click
        CreateNewSubmission()
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.V Then
            ' Call View Submissions Function
            ViewSubmissions()
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            ' Call Create New Submission Function
            CreateNewSubmission()
        End If
    End Sub

    Private Sub ViewSubmissions()
        Dim viewForm As New ViewSubmissionsForm()
        viewForm.Show()
    End Sub

    Private Sub CreateNewSubmission()
        Dim createForm As New CreateSubmissionForm()
        createForm.Show()
    End Sub
End Class
