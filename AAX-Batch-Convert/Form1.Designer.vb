<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.lblMB = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblBitrate = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblTime = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblFileName = New System.Windows.Forms.ToolStripStatusLabel()
        Me.cboFileList = New System.Windows.Forms.ListBox()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddFilesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InputFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OutputFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FFMPEGErrorLogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConvertedFileLogToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FileFormatToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        '
        'Timer1
        '
        Me.Timer1.Interval = 10000
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblMB, Me.lblBitrate, Me.lblTime, Me.lblFileName})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 141)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(397, 22)
        Me.StatusStrip1.TabIndex = 13
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'lblMB
        '
        Me.lblMB.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.lblMB.Name = "lblMB"
        Me.lblMB.Size = New System.Drawing.Size(0, 17)
        '
        'lblBitrate
        '
        Me.lblBitrate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.lblBitrate.Name = "lblBitrate"
        Me.lblBitrate.Size = New System.Drawing.Size(0, 17)
        '
        'lblTime
        '
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(0, 17)
        '
        'lblFileName
        '
        Me.lblFileName.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.lblFileName.Name = "lblFileName"
        Me.lblFileName.Size = New System.Drawing.Size(538, 15)
        Me.lblFileName.Text = "Drag files over this window to add to queue. Highlighted items exist in output. D" & _
    "ouble click to delete."
        '
        'cboFileList
        '
        Me.cboFileList.FormattingEnabled = True
        Me.cboFileList.Location = New System.Drawing.Point(12, 27)
        Me.cboFileList.Name = "cboFileList"
        Me.cboFileList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.cboFileList.Size = New System.Drawing.Size(373, 108)
        Me.cboFileList.TabIndex = 4
        '
        'Timer2
        '
        Me.Timer2.Interval = 1000
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.RefreshToolStripMenuItem, Me.StopToolStripMenuItem, Me.StartToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(397, 24)
        Me.MenuStrip1.TabIndex = 15
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddFilesToolStripMenuItem, Me.InputFolderToolStripMenuItem, Me.OutputFolderToolStripMenuItem, Me.OptionsToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'AddFilesToolStripMenuItem
        '
        Me.AddFilesToolStripMenuItem.Name = "AddFilesToolStripMenuItem"
        Me.AddFilesToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.AddFilesToolStripMenuItem.Text = "Add files..."
        '
        'InputFolderToolStripMenuItem
        '
        Me.InputFolderToolStripMenuItem.Name = "InputFolderToolStripMenuItem"
        Me.InputFolderToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.InputFolderToolStripMenuItem.Text = "Input folder..."
        '
        'OutputFolderToolStripMenuItem
        '
        Me.OutputFolderToolStripMenuItem.Name = "OutputFolderToolStripMenuItem"
        Me.OutputFolderToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.OutputFolderToolStripMenuItem.Text = "Output folder..."
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(58, 20)
        Me.RefreshToolStripMenuItem.Text = "Refresh"
        '
        'StopToolStripMenuItem
        '
        Me.StopToolStripMenuItem.Name = "StopToolStripMenuItem"
        Me.StopToolStripMenuItem.Size = New System.Drawing.Size(43, 20)
        Me.StopToolStripMenuItem.Text = "Stop"
        '
        'StartToolStripMenuItem
        '
        Me.StartToolStripMenuItem.Name = "StartToolStripMenuItem"
        Me.StartToolStripMenuItem.Size = New System.Drawing.Size(43, 20)
        Me.StartToolStripMenuItem.Text = "Start"
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConvertedFileLogToolStripMenuItem, Me.FFMPEGErrorLogToolStripMenuItem, Me.FileFormatToolStripMenuItem})
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.OptionsToolStripMenuItem.Text = "Options..."
        '
        'FFMPEGErrorLogToolStripMenuItem
        '
        Me.FFMPEGErrorLogToolStripMenuItem.Name = "FFMPEGErrorLogToolStripMenuItem"
        Me.FFMPEGErrorLogToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.FFMPEGErrorLogToolStripMenuItem.Text = "FFMPEG output log"
        '
        'ConvertedFileLogToolStripMenuItem
        '
        Me.ConvertedFileLogToolStripMenuItem.Name = "ConvertedFileLogToolStripMenuItem"
        Me.ConvertedFileLogToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.ConvertedFileLogToolStripMenuItem.Text = "Converted file log"
        '
        'FileFormatToolStripMenuItem
        '
        Me.FileFormatToolStripMenuItem.Name = "FileFormatToolStripMenuItem"
        Me.FileFormatToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.FileFormatToolStripMenuItem.Text = "Output file format"
        '
        'Form1
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(397, 163)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.cboFileList)
        Me.HelpButton = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(413, 600)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(413, 202)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AAX Batch Convert MP3"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents lblMB As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblFileName As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblBitrate As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblTime As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents cboFileList As System.Windows.Forms.ListBox
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddFilesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InputFolderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OutputFolderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StartToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StopToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConvertedFileLogToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FFMPEGErrorLogToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileFormatToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
