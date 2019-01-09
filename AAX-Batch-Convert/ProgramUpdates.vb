
Module ProgramUpdates
    Dim UpgradeCheckURL As String = "https://sourceforge.net/p/aax-batch-convert-mp3/wiki/Current%20version/"
    Dim UpgradeCheckFile As String = Application.StartupPath & "\versionRSS_DL.txt"
    Function verCheckTF() As Boolean
        Dim out As String = verCheckBE(UpgradeCheckURL, UpgradeCheckFile)
        Select Case Mid(out, 1, 1)
            Case "+"
                Return True
            Case "-"
                Return False
            Case "="
                Return False
            Case Else
                Return False
        End Select
    End Function
    Function verCheck() As String
        Dim out As String = verCheckBE(UpgradeCheckURL, UpgradeCheckFile)
        Select Case Mid(out, 1, 1)
            Case "+"
                Return out
            Case "-"
                Return out
            Case "="
                Return out
            Case Else
                Return "ERROR: " & out
        End Select
    End Function

    Private Function verCheckBE(surl As String, outputFile As String) As String
        Dim varName As String = "{VERSION:}"
        Dim Terminator As String = "{:VERSION}"
        Dim err As String = ""
        If My.Computer.FileSystem.FileExists(outputFile) Then My.Computer.FileSystem.DeleteFile(outputFile)
        Try
            'adding this apparently fixes issue with
            System.Net.ServicePointManager.SecurityProtocol = Net.SecurityProtocolType.Tls12

            My.Computer.Network.DownloadFile(surl, outputFile)


        Catch e As System.Net.WebException
            Debug.Print("Web error: " & e.Message)
            err = e.Message
        End Try
        If My.Computer.FileSystem.FileExists(outputFile) Then
            Dim versionNew As String = My.Computer.FileSystem.ReadAllText(outputFile)
            Debug.Print(versionNew)
            Dim temp As String
            Try
                If versionNew.Length > 0 Then

                    temp = Mid(versionNew, InStr(versionNew, varName))
                    versionNew = Mid(temp, varName.Length + 1, InStr(temp, Terminator) - Terminator.Length - 1)
                Else
                    versionNew = "0.0.0.0"
                End If
            Catch ex As Exception
                Debug.Print("Error during version parse " & ex.Message)
                versionNew = "0.0.0.0"
            End Try


            If versionNew <> My.Application.GetType.Assembly.GetName.Version.ToString Then
                Dim verCurrent() As String = Split(My.Application.GetType.Assembly.GetName.Version.ToString, ".")
                Dim verNew() As String = Split(versionNew, ".")

                If UBound(verNew) = UBound(verCurrent) Then
                    Debug.Print("Initial version match complete, version is not the same, continuing")
                    If CInt(verNew(0)) > CInt(verCurrent(0)) Then 'major version higher
                        Debug.Print("Major version higher")
                        Return "+" & versionNew
                    Else
                        If CInt(verNew(1)) > CInt(verCurrent(1)) Then 'minor version higher
                            Debug.Print("Minor version higher")
                            Return "+" & versionNew
                        Else
                            If CInt(verNew(2)) > CInt(verCurrent(2)) Then
                                Debug.Print("MinorMinor version higher")
                                Return "+" & versionNew
                            Else
                                If CInt(verNew(3)) > CInt(verCurrent(3)) Then
                                    Debug.Print("MinorMinorMinor version higher")
                                    Return "+" & versionNew
                                Else
                                    Debug.Print("Fail test, version must be lower or same")
                                    'variable=versionNew
                                    Return "-" & versionNew
                                End If

                            End If
                        End If

                    End If
                Else
                    Debug.Print("Upper bounds don't match")
                    Return "Server version info corrupt: " & versionNew
                End If
            Else
                Debug.Print("Initial version match complete, version is the same")
                Return "=" & versionNew
            End If
        Else 'version file doesn't exist
            Return "Downloaded file doesn't exist " & err
        End If
    End Function

End Module
