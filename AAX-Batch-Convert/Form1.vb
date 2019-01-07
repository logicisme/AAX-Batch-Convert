'I modeled creating the worker for the conversion and learning read/write streams from AAXMan's code. Converted to VB for my use.
Imports System.IO
Imports System.ComponentModel
Imports System.Threading


Public Class Form1
    Public pathFFMpeg As String = Application.StartupPath & "\ffmpeg.exe"
    Public workingDir As String = Application.StartupPath ' set starting value of current dir, changes if user specify switch on cmd line
    Public pathAAXToMP3EXE As String = Application.StartupPath & "\aaxtomp3.exe"
    Public pathAudibleDLL As String = Application.StartupPath & "\AAXSDKWIN.dll"
    Public pathAudibleFolderDLL As String = "C:\Program Files (x86)\Audible\Bin\AAXSDKWIN.dll"
    Public folderInput As String 'working input directory 
    Public folderOutput As String ' working output directory
    Public opF As String = "mp3" 'mp3 or m4a or whatever ff can do no period. 
    Public URLFFMPEG As String = "https://ffmpeg.org/download.html#build-windows"
    Public URLAAX2MP3 As String = "https://sourceforge.net/projects/aaxtomp3/files/"
    Public URLAudibleDL As String = "https://d26m6e6wixvnt0.cloudfront.net/AM50/ActiveSetupN.exe"
    Public folderAudibleDownload As String = folderInput
    Dim convertProcess As New Process 'process that runs aax2mp3.exe
    Dim ffmpegProcess As New Process ' process that runs ffmpeg.exe 
    Dim outputText As String ' holds text to display in status bar
    Dim stopWork As Boolean = False ' on true worker stops conversion
    Dim workingFile As String ' holds text to display in status bar of current file in conversion

    ' " & opf & "
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        stopWork = True ' causes worker to finish in convert function
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'look for files
        If Command$() <> "" Then 'use commandline parameter for working dir  and removing trailing slash
            If My.Computer.FileSystem.DirectoryExists(Command$.Trim.Replace("""", "")) Then
                Dim cc As String = Command$.Trim.Replace("""", "")
                If Mid(cc, cc.Length) = "\" Then 'kill trailing slash
                    workingDir = Mid(cc, 1, cc.Length - 1)
                Else
                    workingDir = cc
                End If
                Debug.Print("Working Dir" & workingDir)
            End If
        End If
        'set in out dirs to include working directory, uses preset path in variable def if not specify by cmdline
        folderInput = workingDir & "\Input"
        folderOutput = workingDir & "\Output"

        'Look for required files then prompt user to download
        Dim ans As Integer 'holds user response from msgbox
        If My.Computer.FileSystem.FileExists(pathFFMpeg) = False Then
            ans = MsgBox("FFMPEG is missing. Please download ffmpeg.exe from " & URLFFMPEG & ". Click OK to open browser to that location", vbOKCancel, "FFMPEG is missing")
            If ans = vbOK Then
                Process.Start(URLFFMPEG) 'opens url to download page
            End If
        End If
        If My.Computer.FileSystem.FileExists(pathAAXToMP3EXE) = False Then
            ans = MsgBox("FFMPEG is missing. Please download aax2mp3.exe from " & URLAAX2MP3 & ". Click OK to open browser to that location", vbOKCancel, "AAX2MP3 is missing")
            If ans = vbOK Then
                Process.Start(URLAAX2MP3) 'opens url to download page
            End If
        End If
        If My.Computer.FileSystem.FileExists(pathAudibleDLL) = False Then
            Try 'copy audible dll file into the app path
                If My.Computer.FileSystem.FileExists(pathAudibleFolderDLL) Then My.Computer.FileSystem.CopyFile(pathAudibleFolderDLL, Application.StartupPath & "\" & Path.GetFileName(pathAudibleFolderDLL))
            Catch ex As Exception
                MsgBox("Error copying audibleDLL into program path; " & ex.Message)
            End Try
            If My.Computer.FileSystem.FileExists(pathAudibleDLL) = False Then
                ans = MsgBox("Audible download manager is missing. Please download it from Audible's website from " & URLAudibleDL & ". Click OK to open browser to that location", vbOKCancel, "AAX2MP3 is missing")
                If ans = vbOK Then
                    Process.Start(URLAudibleDL) 'opens url to download page
                End If
            End If
        End If


        If My.Computer.FileSystem.DirectoryExists(Environ("userprofile") & "\Audible\Audible\Downloads") Then folderAudibleDownload = Environ("userprofile") & "\Audible\Audible\Downloads"
        Try
            If My.Computer.FileSystem.DirectoryExists(folderInput) = False Then My.Computer.FileSystem.CreateDirectory(folderInput)
            If My.Computer.FileSystem.DirectoryExists(folderOutput) = False Then My.Computer.FileSystem.CreateDirectory(folderOutput)
        Catch ex As Exception
            MsgBox("Error creating directories" & vbCrLf & ex.Message)
        End Try
        'If My.Computer.FileSystem.DirectoryExists(folderInput) Then Timer2.Enabled = True 'timer2 is one time run delay to check/alert users of files existing in input folder and that have been converted
        parseInputDir(False)
        Debug.Print("AudibleDIR: " & folderAudibleDownload)
        Debug.Print("Output: " & folderOutput)
        Debug.Print("Input: " & folderInput)
        Timer1.Enabled = True ' input folder polling 
        'set output format
        If GetSetting(Application.ProductName, "Output", "Format", "mp3") <> "" Then opF = GetSetting(Application.ProductName, "Output", "Format", "mp3")
        Me.Text = Application.ProductName.Replace("MP3", opF.ToUpper)
    End Sub

    Private Sub parseInputDir(alertUser As Boolean) 'polling funtion for input folder monitors for aax files

        cboFileList.Items.Clear()
        For Each item In My.Computer.FileSystem.GetFiles(folderInput)
            If Path.GetExtension(item).ToUpper = ".AAX" Or Path.GetExtension(item).ToUpper = ".AA" Then
                'get rid of spaces
                If Path.GetFileName(item).Contains(" ") Then My.Computer.FileSystem.RenameFile(item, Path.GetFileName(item).Replace(" ", "_"))
            End If
            'add match to cbobox
            If Path.GetExtension(item).ToUpper = ".AAX" Then cboFileList.Items.Add(Path.GetFileName(item))
            If Path.GetExtension(item).ToUpper = ".AA" Then cboFileList.Items.Add(Path.GetFileName(item))
            Debug.Print("file ext: " & Path.GetExtension(item))
        Next

        For Each item In My.Computer.FileSystem.GetFiles(folderInput) 'search for mp3 of same name in output folder and alert user / highlight
            If My.Computer.FileSystem.FileExists(folderOutput & "\" & Path.GetFileNameWithoutExtension(item) & "." & opF & "") Then
                For i As Integer = 0 To cboFileList.Items.Count - 1

                    If cboFileList.Items.Item(i).ToString.ToUpper = Path.GetFileName(item.ToUpper) Then
                        cboFileList.SetSelected(i, True)
                    End If

                Next
            End If
        Next
        'alert user if boolean true  and more than one item is in list
        If cboFileList.Items.Count > 0 And alertUser Then MsgBox("Highlighted files exist in input aa/aax format and output directory as an " & opF & ". You must delete or move either the input file or the output file before starting.")
        If cboFileList.Items.Count = 0 Then
            cboFileList.Items.Add("-Drag AA / AAX files here to add to the queue")
            cboFileList.Items.Add("-")
            cboFileList.Items.Add("-Highlighted items indicate that an output file exists for the input file")
            cboFileList.Items.Add("-Double click to delete a file from the input folder")
            cboFileList.Items.Add("-")
            cboFileList.Items.Add("-When adding bigger files to list, program may appear to lock up")
            cboFileList.Items.Add("- while copying files. Be patient ;-)")
        End If


    End Sub


    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        For Each item As String In OpenFileDialog1.FileNames 'go through multiple files selected
            Try 'look for file name markers and shorten file names to just title  to get rid of garbage 
                If item.Contains("_ep") And Path.GetExtension(item).ToUpper = ".AAX" Then
                    My.Computer.FileSystem.CopyFile(item, folderInput & "\" & Mid(Path.GetFileName(item), 1, InStr(Path.GetFileName(item), "_") - 1) & ".aax")
                    cboFileList.Items.Add(Mid(Path.GetFileName(item), 1, InStr(Path.GetFileName(item), "_") - 1) & ".aax")
                Else ' otherwise just add file as is
                    My.Computer.FileSystem.CopyFile(item, folderInput & "\" & Path.GetFileName(item))
                    cboFileList.Items.Add(Path.GetFileName(item))
                End If

            Catch ex As Exception
                MsgBox("Error while copying file: " & ex.Message)
            End Try
        Next
    End Sub

 




    Private Sub convertSetup()
        'setup the processes fro the worker
        convertProcess.StartInfo.FileName = pathAAXToMP3EXE
        convertProcess.StartInfo.UseShellExecute = False
        convertProcess.StartInfo.RedirectStandardOutput = True
        convertProcess.StartInfo.RedirectStandardError = True
        convertProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        convertProcess.StartInfo.CreateNoWindow = True
        convertProcess.StartInfo.RedirectStandardOutput = True
        convertProcess.EnableRaisingEvents = True
        convertProcess.StartInfo.WorkingDirectory = Application.StartupPath


        ffmpegProcess.StartInfo.CreateNoWindow = True
        ffmpegProcess.StartInfo.FileName = pathFFMpeg
        ffmpegProcess.StartInfo.UseShellExecute = False
        ffmpegProcess.StartInfo.RedirectStandardError = True
        ffmpegProcess.StartInfo.RedirectStandardInput = True
        ffmpegProcess.EnableRaisingEvents = True

        'add a handler so that data recieved from the ffmpeg program can be parsed for time/bitrate info and displayed in statusbar
        AddHandler ffmpegProcess.ErrorDataReceived, New DataReceivedEventHandler(AddressOf ffmpegProcess_status)


        ThreadPool.QueueUserWorkItem(AddressOf convert) 'start worker
        Timer1.Enabled = True 'make sure timer to poll the file list for the combo is running
        stopWork = False 'make sure that stopwork is false. user can set to true to stop process. or on closing
    End Sub

    Private Sub convert(stateInfo As Object)
        'start conversion worker
        Dim oo As String 'conversion process arguments
        Dim pp As String ' ffmpeg process arguments
        Dim ouf As String ' output file name 
        'Path.GetFileName(item)

        Debug.Print("working dir:" & workingDir & " output folder:" & folderOutput & " Input folder: " & folderInput)
        For Each item In My.Computer.FileSystem.GetFiles(folderInput) 'start of loop of all files in input folder
            ouf = folderOutput & "\" & Path.GetFileName(item).Replace(Path.GetExtension(item), "." & opF & "")
            Debug.Print("Working on: " & item)
            'make sure file is an audible file and make sure output file of same name mp3 doesn't exist
            If Path.GetExtension(item.ToUpper) = ".AAX" Or Path.GetExtension(item.ToUpper) = ".AA" And My.Computer.FileSystem.FileExists(ouf) = False Then
                workingFile = "► " & item.ToString.Trim.Replace(Path.GetExtension(item), "." & opF)
                oo = "-i """ & item & """"
                pp = "-i pipe:0 """ & folderOutput & "\" & Path.GetFileName(item).Replace(Path.GetExtension(item), "." & opF) & """"

                Debug.Print("ouf: " & ouf)
                Debug.Print("oo: " & oo)
                Debug.Print("pp:" & pp)
                'define process arguments and start
                convertProcess.StartInfo.Arguments = oo
                ffmpegProcess.StartInfo.Arguments = pp
                convertProcess.Start()
                ffmpegProcess.Start()
                Try
                    ffmpegProcess.BeginErrorReadLine() ' start looking at data piped out of aax2mp3
                Catch ex As Exception
                    Debug.Print("Error: " & ex.Message)
                End Try

                Dim reader As StreamReader = convertProcess.StandardOutput 'set a streamreader to the output of aax2mp3
                Dim writer As StreamWriter = ffmpegProcess.StandardInput ' set a streamwriter to the output of ffmpeg
                Dim buffer As Char() = New Char(4096) {} ' define buffer size for data out or reader
                While Not reader.EndOfStream
                    reader.Read(buffer, 0, buffer.Length)
                    writer.Write(buffer)
                    If stopWork Then Exit While 'exit if user sets to true
                End While
                Debug.Print("End of stream conversion")
                Try
                    'close processes if user stops
                    If stopWork Then ffmpegProcess.CancelErrorRead()
                    If stopWork Then convertProcess.Kill()
                    If stopWork Then ffmpegProcess.Kill()
                    If stopWork Then Exit Sub
                    'if normal processing, close out the processes
                    ffmpegProcess.CancelErrorRead()
                    convertProcess.WaitForExit()
                    'ffmpegProcess.WaitForExit() ' stalls waiting
                    'convertProcess.Close()
                    'ffmpegProcess.Close()
                    If My.Computer.FileSystem.FileExists(ouf) Then ' delete the input file if the output file was created.
                        My.Computer.FileSystem.DeleteFile(item.Trim)
                    Else
                        MsgBox("Error: Output file wasn't created :-(") 'alert user if output file wasn't created.
                        ffmpegProcess.Close()
                        convertProcess.Close()
                        Exit Sub
                    End If
                Catch ex As Exception
                    workingFile = "Error: " & ex.Message ' will push message to the statusbar when polled, maybe.
                End Try
                Try
                    If My.Computer.FileSystem.FileExists(Application.StartupPath & "\converted.txt") Then
                        My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\converted.txt", Now & vbTab & ouf & vbCrLf, True)
                        If workingFile.Contains("Error:") Then My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\converted.txt", Now & vbTab & workingFile & vbCrLf, True)
                    End If

                Catch ex As Exception

                End Try
            Else
                Debug.Print("Couldn't process filename: " & item)

            End If
        Next
        ffmpegProcess.Close()
        convertProcess.Close()



        outputText = "DONE"

        done() 'processes are done, conversion is done, call status bar cleanup


    End Sub
    Private Sub done()
        'status bar cleanup when conversions are done
        lblBitrate.Text = ""
        lblMB.Text = ""
        lblTime.Text = ""
        lblFileName.Text = "DONE"
        Media.SystemSounds.Exclamation.Play()

    End Sub
    Private Sub ffmpegProcess_status(ByVal sender As Object, ByVal e As DataReceivedEventArgs)
        'gets data from FFMPEG process and pushes it to status bar.
        Try
            If e.Data IsNot Nothing Then
                Dim ss As String = e.Data
                Debug.Print(vbTab & vbTab & e.Data)
                'size=    4623kB time=00:09:51.64 bitrate=  64.0kbits/s    
                If ss.ToUpper.Contains("SIZE=") Then
                    ss = ss.ToUpper.Replace(" ", "").Replace("SIZE=", "").Replace("TIME", "").Replace("BITRATE", "")
                    ss = ss.ToLower
                    Dim spl() As String = Split(ss, "=")
                    If UBound(spl) > 1 Then
                        lblBitrate.Text = spl(2)
                        lblMB.Text = spl(0)
                        lblTime.Text = "Length:" & spl(1)
                        lblFileName.Text = workingFile
                        outputText = ""
                    End If
                Else

                    lblBitrate.Text = ""
                    lblMB.Text = ""
                    lblTime.Text = ""
                    lblFileName.Text = "Starting..."
                    'if log file exists then put all data that isn't conversion status into it. This is slow but worth it if something is failing to provide troubleshooting.
                    If My.Computer.FileSystem.FileExists(Application.StartupPath & "\log.txt") Then My.Computer.FileSystem.WriteAllText(Application.StartupPath & "\log.txt", e.Data & vbCrLf, True)
                End If
            Else

                lblBitrate.Text = ""
                lblMB.Text = ""
                lblTime.Text = ""
                lblFileName.Text = "DONE"
            End If
        Catch ex As Exception
            'fail silently
        End Try


    End Sub


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'refresh combo box
        parseInputDir(False)
    End Sub

  

    Private Sub cboFileList_DoubleClick(sender As Object, e As EventArgs) Handles cboFileList.DoubleClick
        'prompts user to delete file if double clicked.
        Dim ans As Integer
        Dim delfile As String = cboFileList.Items.Item(cboFileList.SelectedIndex)
        If Mid(delfile, 1, 1) <> "-" Then ' ignore if this is text instructions
            ans = MsgBox("Delete double clicked input file?" & vbCrLf & delfile, vbOKCancel)
            If ans = vbOK Then
                Try
                    My.Computer.FileSystem.DeleteFile(folderInput & "\" & delfile)
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

                parseInputDir(False)
            End If
        End If
    End Sub






    Private Sub Form1_DragDrop(sender As System.Object, e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop
        'setup drag target
        Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
        For Each item In files
            Try
                If Path.GetExtension(item).ToUpper = ".AAX" Or Path.GetExtension(item).ToUpper = ".AA" Then 'only add known types

                    If item.Contains("_ep") And Path.GetExtension(item).ToUpper.Contains(".AA") Then 'same filtering as open file dialog1
                        My.Computer.FileSystem.CopyFile(item, folderInput & "\" & Mid(Path.GetFileName(item), 1, InStr(Path.GetFileName(item), "_") - 1) & ".aax")
                        cboFileList.Items.Add(Mid(Path.GetFileName(item), 1, InStr(Path.GetFileName(item), "_") - 1) & ".aax")
                    Else
                        My.Computer.FileSystem.CopyFile(item, folderInput & "\" & Path.GetFileName(item))
                        cboFileList.Items.Add(Path.GetFileName(item))
                    End If
                End If

            Catch ex As Exception
                MsgBox("Error while copying file: " & ex.Message)
            End Try



        Next
        parseInputDir(False)
    End Sub

    Private Sub Form1_DragEnter(sender As System.Object, e As System.Windows.Forms.DragEventArgs) Handles Me.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then 'only allow file drops
            e.Effect = DragDropEffects.Copy
        End If

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        'refresh input dir but wait to do it for a bit. uses this to ensure program form has loaded before displaying message about existing files in the in and out dir
        Timer2.Enabled = False
        parseInputDir(True)

    End Sub

    Private Sub Form1_HelpButtonClicked(sender As Object, e As CancelEventArgs) Handles Me.HelpButtonClicked
        HelpForm.Show()
    End Sub
   

    Private Sub AddFilesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddFilesToolStripMenuItem.Click
        With OpenFileDialog1
            .Title = "Select files to add"
            .Multiselect = True
            .InitialDirectory = folderAudibleDownload
            .Filter = "Audible(aax)|*.AAX|Audible(aa)|*.AA" 'acceptible file formats for open dialog
            .ShowDialog()
        End With
    End Sub

    Private Sub InputFolderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles InputFolderToolStripMenuItem.Click
        Process.Start("explorer.exe", folderInput)
    End Sub

    Private Sub OutputFolderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OutputFolderToolStripMenuItem.Click
        Process.Start("explorer.exe", folderOutput)
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click
        cboFileList.Items.Clear()
        parseInputDir(False)
    End Sub

    Private Sub StopToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StopToolStripMenuItem.Click
        'stop button
        Timer1.Enabled = False
        Timer2.Enabled = False
        stopWork = True
        Media.SystemSounds.Asterisk.Play()

    End Sub

    Private Sub StartToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StartToolStripMenuItem.Click
        Dim dont As Boolean = False 'checked before starting conversion to prevent file duplicates during conversion process.
        parseInputDir(False)
        Timer1.Enabled = True
        Dim badlist As String = "A file already exists in output folder that is in the list, delete that first" & vbCrLf
        For Each item In cboFileList.Items 'poll items for dupes.
            If My.Computer.FileSystem.FileExists(folderOutput & "\" & item.ToString.ToLower.Replace(".aa", "." & opF).Replace("." & opF & "x", "." & opF)) Or Mid(item, 1, 1) = "-" Then
                dont = True ' stops convert from starting
                'build list of complaints

                badlist += item.ToString.ToLower.Replace(".aa", "." & opF).Replace("." & opF & "x", "." & opF) & " exists" & vbCrLf
                If Mid(item, 1, 1) = "-" Then badlist = "ADD FILES TO CONVERT FIRST"
            End If

        Next
        If dont Then 'show user complaints
            MsgBox(badlist)
        Else
            convertSetup()
            Media.SystemSounds.Asterisk.Play()
        End If

    End Sub


    Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        cboFileList.Height = Me.Height - 90
    End Sub

    Private Sub FileFormatToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FileFormatToolStripMenuItem.Click
        Dim extension As String = InputBox("Type 3 digit extension of file format you wish to use." & vbCrLf & " FFMPEG must be able to convert to this format. Default is ""mp3""." & vbCrLf & "m4a, aac, ogg, wav, etc also work. See ffmpeg documentation for more info. Changing audio formats will not produce audio quality better than the input file", "Select a file type:", opF)
        If Mid(extension, 1, 1) = "." Then extension = Mid(extension, 2)
        If extension.Trim = "" Then extension = "mp3"
        SaveSetting(Application.ProductName, "Output", "Format", extension)
        opF = extension
        Me.Text = Application.ProductName.Replace("MP3", extension.ToUpper)

    End Sub
End Class
