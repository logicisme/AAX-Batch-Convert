

Public Class HelpForm

    Private Sub HelpForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RichTextBox1.Rtf = My.Resources.Helpfile

        Me.Text += "                 Program Build# " & Me.GetType.Assembly.GetName.Version.ToString

    End Sub

    Private Sub HelpForm_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles Me.MouseDoubleClick
        Clipboard.SetText(" Build " & Me.GetType.Assembly.GetName.Version.ToString)
        Media.SystemSounds.Hand.Play()

    End Sub




    Private Sub HelpForm_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        RichTextBox1.Width = Me.Width - 20
        RichTextBox1.Height = Me.Height - 40
    End Sub
End Class