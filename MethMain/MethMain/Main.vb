Imports System.Management
Imports System.IO
Imports System.Net
Imports System.Drawing

Public Class Main

    Private Declare Function GetAsyncKeyState Lib "user32" (ByVal key As Integer) As Integer
    Private Declare Function apimouse_event Lib "user32.dll" Alias "mouse_event" (ByVal dwFlags As Int32, ByVal dX As Int32, ByVal dY As Int32, ByVal cButtons As Int32, ByVal dwExtraInfo As Int32) As Boolean
    Private Declare Function apiGetMessageExtraInfo Lib "user32" Alias "GetMessageExtraInfo" () As Int32
    Private Declare Function SetCursorPos Lib "user32" (ByVal x As Long, ByVal y As Long) As Long
    Private Declare Function GetCursorPos Lib "user32" (ByRef point As POINTAPI) As Long
    Public Declare Function eAPISMessage Lib "user32.dll" Alias "SendMessageA" (ByVal hWnd As Integer, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As String) As Integer
    Private Declare Sub keybd_event Lib "user32.dll" (ByVal bVk As Byte, ByVal bScan As Byte, ByVal dwFlags As Integer, ByVal dwExtraInfo As Integer)
    Public Declare Function MapVirtualKey Lib "user32" Alias "MapVirtualKeyA" (ByVal wCode As Integer, ByVal wMapType As Integer) As Integer
    Private Declare Function GetActiveWindow Lib "user32" () As Long
    Private Declare Function GetForegroundWindow Lib "user32" Alias "GetForegroundWindow" () As IntPtr
    Public Declare Auto Function GetWindowText Lib "user32" (ByVal hWnd As System.IntPtr, ByVal lpString As System.Text.StringBuilder, ByVal cch As Integer) As Integer
    Dim shouldRClick As Boolean


    Function GetCaption() As String
        Dim Caption As New System.Text.StringBuilder(256)
        Dim hWnd As IntPtr = GetForegroundWindow()
        GetWindowText(hWnd, Caption, Caption.Capacity)
        Return Caption.ToString()
    End Function

    Dim prefetchPath As String
    Dim toggle As Integer
    Dim extendtoggle As Integer
    Dim rnd As New Random
    Dim slot As Integer = 0
    Dim keypot As String
    Dim bind1 As String
    Dim bind2 As String
    Dim bind3 As String
    Dim bind4 As String
    Dim bind5 As String
    Dim bind6 As String
    Dim bind7 As String
    Dim bind8 As String
    Dim bind9 As String
    Public Const MOUSEEVENTF_LEFTDOWN = &H2
    Public Const MOUSEEVENTF_LEFTUP = &H4
    Public Const MOUSEEVENTF_MIDDLEDOWN = &H20
    Public Const MOUSEEVENTF_MIDDLEUP = &H40
    Public Const MOUSEEVENTF_RIGHTDOWN = &H8
    Public Const MOUSEEVENTF_RIGHTUP = &H10
    Public Const MOUSEEVENTF_MOVE = &H1

    Public bHook As Boolean
    Public bTimerEnd As Boolean
    Public timeLastClick As DateTime
    Public intervalClick As Integer
    Private WithEvents mHook As New Hook

    Dim isHeld_LMB As Boolean
    Dim shouldClick As Boolean = False
    Dim ignoreNextRelease As Boolean = False

    Dim directory As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\" + "AnyDesk.exe"



    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Opacity = 0
        Dim tmr As New Timer
        tmr.Interval = 1
        tmr.Start()
        AddHandler tmr.Tick, Sub()
                                 Me.Opacity += 0.1
                                 If Me.Opacity = 1 Then tmr.Stop()
                             End Sub

        ThirteenComboBox1.SelectedItem = "Human Clicks"
        mHook.HookMouse()

        prefetchPath = Environment.GetEnvironmentVariable("windir", EnvironmentVariableTarget.Machine) & "\Prefetch"

        Dim nameProcess() As System.Diagnostics.Process = System.Diagnostics.Process.GetProcesses
        Dim pList() As System.Diagnostics.Process = System.Diagnostics.Process.GetProcessesByName("Process")

        For Each file In IO.Directory.GetFiles(prefetchPath)
            If file.Contains("EXPLORER") Then IO.File.Delete(file)
        Next

        For Each file In IO.Directory.GetFiles(prefetchPath)
            If file.Contains("RUNDLL") Then IO.File.Delete(file)
        Next

        For Each file In IO.Directory.GetFiles(prefetchPath)
            If file.Contains("JNA") Then IO.File.Delete(file)
        Next

        For Each file In IO.Directory.GetFiles(prefetchPath)
            If file.Contains("METH") Then IO.File.Delete(file)
        Next

        For Each file In IO.Directory.GetFiles(prefetchPath)
            If file.Contains("MAIN") Then IO.File.Delete(file)
        Next

        For Each file In IO.Directory.GetFiles(prefetchPath)
            If file.Contains("EXPLORER") Then IO.File.Delete(file)
        Next

        For Each file In IO.Directory.GetFiles(prefetchPath)
            If file.Contains("ANYDESK") Then IO.File.Delete(file)
        Next

        For Each file In IO.Directory.GetFiles(prefetchPath)
            If file.Contains("WINLOGON") Then IO.File.Delete(file)
        Next

        For Each file In IO.Directory.GetFiles(prefetchPath)
            If file.Contains("SEARCH") Then IO.File.Delete(file)
        Next

        For Each file In IO.Directory.GetFiles(prefetchPath)
            If file.Contains("EXPLORER") Then IO.File.Delete(file)
        Next

        For Each file In IO.Directory.GetFiles(prefetchPath)
            If file.Contains("ANYDESK") Then IO.File.Delete(file)
        Next

        For Each file In IO.Directory.GetFiles(prefetchPath)
            If file.Contains("WINLOGON") Then IO.File.Delete(file)
        Next

        For Each file In IO.Directory.GetFiles(prefetchPath)
            If file.Contains("DLL") Then IO.File.Delete(file)
        Next

        Delay(1.5)

        For i = 0 To nameProcess.Length - 1
            If nameProcess(i).ProcessName.Contains("explorer") Then
                Dim exeName As String = nameProcess(i).ProcessName
                Dim proc() As Process = Process.GetProcessesByName(exeName)
                For Each temp As Process In proc
                    temp.Kill()
                Next
            End If
        Next




        Dim WC As New System.Net.WebClient
        Dim hw As New clsComputerInfo
        Dim hdd As String
        Dim cpu As String
        Dim hwid As String


        cpu = hw.GetProcessorId()
        hdd = hw.GetVolumeSerial("C")
        hwid = cpu + hdd

        Dim hwidEncrypted As String = Strings.UCase(Me.getMD5Hash(cpu & hdd))

        Dim mydickleisapickle As String = "http://vps143016.vps.ovh.ca/check?hwid=" + hwidEncrypted
        Dim myRequest As HttpWebRequest = DirectCast(WebRequest.Create(mydickleisapickle), HttpWebRequest)
        myRequest.Method = "GET"
        Dim myResponse As WebResponse = myRequest.GetResponse()
        Dim sr As New StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8)
        Dim result As String = sr.ReadToEnd()

        If result = "Fatass69dick" Then
            Me.Show()
        Else
            Melt3(1)
            Application.Exit()
        End If

        Dim Letters As New List(Of Integer)
        'add ASCII codes for numbers
        For i As Integer = 48 To 57
            Letters.Add(i)
        Next
        'lowercase letters
        For i As Integer = 97 To 122
            Letters.Add(i)
        Next
        'uppercase letters
        For i As Integer = 65 To 90
            Letters.Add(i)
        Next
        'select 8 random integers from number of items in Letters
        'then convert those random integers to characters and
        'add each to a string and display in Textbox
        Dim Rnd As New Random
        Dim SB As New System.Text.StringBuilder
        Dim Temp1 As Integer
        For count As Integer = 1 To 8
            Temp1 = Rnd.Next(0, Letters.Count)
            SB.Append(Chr(Letters(Temp1)))
        Next

        Me.Text = SB.ToString

        MainForm.Text = SB.ToString

        SelectedColor = Color.Red
        SelectedRainbowNumber = 0


        For Each file In IO.Directory.GetFiles(prefetchPath)
            If file.Contains("ANYDESK") Then IO.File.Delete(file)
        Next



    End Sub


    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub mHook_Mouse_Left_Down(ByVal ptLocat As System.Drawing.Point) Handles mHook.Mouse_Left_Down
        Dim acThread As New System.Threading.Thread(AddressOf Clicking)
        isHeld_LMB = True
        Me.shouldClick = True
        Dim shakeThread As New System.Threading.Thread(AddressOf Jitter)

        If Autoclicker.Enabled = True And FlatCheckBox3.Checked = True Then
            If shakeThread.IsAlive = False Then
                shakeThread.Start()
            Else
                shakeThread.Join()
            End If
        Else
            shakeThread.Abort()
        End If
    End Sub

    Private Sub mHook_Mouse_Left_Up(ByVal ptLocat As System.Drawing.Point) Handles mHook.Mouse_Left_Up
        isHeld_LMB = False
        If Not Me.ignoreNextRelease Then
            Me.shouldClick = False
        End If
        Me.ignoreNextRelease = False
    End Sub

    Sub Jitter()
        Randomize()
        Dim p As POINTAPI
        GetCursorPos(p)
        Cursor.Position = New Point(p.x + (rnd.Next(-(FlatTrackBar1.Value), (FlatTrackBar1.Value))), p.y + (rnd.Next(-(FlatTrackBar1.Value), FlatTrackBar1.Value)))
    End Sub

    Private Sub ThirteenButton1_Click(sender As Object, e As EventArgs) Handles ThirteenButton1.Click
        toggle = toggle + 1
        If toggle = 1 Then
            Autoclicker.Start()
            FlatLabel37.Text = "On"
        Else
            FlatLabel37.Text = "Off"
            Autoclicker.Stop()
            toggle = 0
        End If
    End Sub

    Private Sub ThirteenButton2_Click(sender As Object, e As EventArgs) Handles ThirteenButton2.Click
        toggle = toggle + 1
        If toggle = 1 Then
            FlatLabel38.Text = "On"
            WTap.Start()
        Else
            FlatLabel38.Text = "Off"
            WTap.Start()
            toggle = 0
        End If
    End Sub

    Private Sub ThirteenButton3_Click(sender As Object, e As EventArgs) Handles ThirteenButton3.Click
        toggle = toggle + 1
        If toggle = 1 Then
            STap.Start()
            FlatLabel39.Text = "On"
        Else
            STap.Stop()
            FlatLabel39.Text = "Off"
            toggle = 0
        End If
    End Sub

    Private Sub ThirteenButton4_Click(sender As Object, e As EventArgs) Handles ThirteenButton4.Click
        toggle = toggle + 1
        If toggle = 1 Then
            AutoPot.Start()
            FlatLabel40.Text = "On"
        Else
            FlatLabel40.Text = "Off"
            AutoPot.Stop()
            toggle = 0
        End If
    End Sub

    Private Sub ThirteenButton5_Click(sender As Object, e As EventArgs) Handles ThirteenButton5.Click
        toggle = toggle + 1
        If toggle = 1 Then
            AutoRod.Start()
            FlatLabel41.Text = "On"
        Else
            FlatLabel41.Text = "Off"
            AutoRod.Stop()
            toggle = 0
        End If
    End Sub

    Private Sub MinimumCPS_Scroll(sender As Object) Handles MinimumCPS.Scroll
        FlatLabel2.Text = MinimumCPS.Value
    End Sub

    Private Sub MaximumCPS_Scroll(sender As Object) Handles MaximumCPS.Scroll
        FlatLabel4.Text = MaximumCPS.Value
    End Sub



    Structure POINTAPI
        Dim x As Int32
        Dim y As Int32
    End Structure

    Sub Clicking()
        Randomize()
        If Autoclicker.Enabled Then
            apimouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, MOUSEEVENTF_LEFTDOWN, apiGetMessageExtraInfo)
            Threading.Thread.Sleep(rnd.Next(50, 70))
            ignoreNextRelease = True
            apimouse_event(MOUSEEVENTF_LEFTUP, 0, 0, MOUSEEVENTF_LEFTDOWN, apiGetMessageExtraInfo)
            Threading.Thread.Sleep(rnd.Next(50, 70))
        End If
    End Sub

    Sub Delay(ByVal dblSecs As Double)

        Const OneSec As Double = 1.0# / (1440.0# * 60.0#)
        Dim dblWaitTil As Date
        Now.AddSeconds(OneSec)
        dblWaitTil = Now.AddSeconds(OneSec).AddSeconds(dblSecs)
        Do Until Now > dblWaitTil
            Application.DoEvents()
        Loop

    End Sub

    Private Sub Autoclicker_Tick(sender As Object, e As EventArgs) Handles Autoclicker.Tick
        Dim minval As Integer
        Dim maxval As Integer

        minval = 1000 / (MinimumCPS.Value + MaximumCPS.Value * 0.2)
        maxval = 1000 / (MinimumCPS.Value + MaximumCPS.Value * 0.48)

        Dim acThread As New System.Threading.Thread(AddressOf Clicking)

        If ThirteenComboBox1.SelectedItem = "Simple Random" Then
            Autoclicker.Interval = rnd.Next(maxval, minval)
        Else
            If REMOVED Then
                Autoclicker.Interval = REMOVED
            Else
                Autoclicker.Interval = REMOVED
            End If
        End If

        If FlatCheckBox4.Checked = True Then
            If GetCaption.StartsWith("Minecraft") Then
                If shouldClick = True Then
                    If acThread.IsAlive = False Then
                        acThread.Start()
                    Else
                        acThread.Join()
                    End If
                Else
                    acThread.Abort()
                End If
            End If
        Else
            If shouldClick = True Then
                If acThread.IsAlive = False Then
                    acThread.Start()
                Else
                    acThread.Join()
                End If
            Else
                acThread.Abort()
            End If
        End If


    End Sub

    Private Sub WTap_Tick(sender As Object, e As EventArgs) Handles WTap.Tick
        WTap.Interval = rnd.Next(FlatTrackBar3.Value, FlatTrackBar2.Value)

        If GetAsyncKeyState(Keys.W) And Autoclicker.Enabled = True And Me.shouldClick = True = True Then
            InputHelper.SetKeyState(Keys.W, True)
            Delay(0.025)
            InputHelper.SetKeyState(Keys.W, False)
        End If
    End Sub

    Private Sub FlatTrackBar3_Scroll(sender As Object) Handles FlatTrackBar3.Scroll
        FlatLabel7.Text = FlatTrackBar3.Value
    End Sub

    Private Sub FlatTrackBar2_Scroll(sender As Object) Handles FlatTrackBar2.Scroll
        FlatLabel5.Text = FlatTrackBar2.Value
    End Sub

    Private Sub ThirteenButton8_Click(sender As Object, e As EventArgs) Handles ThirteenButton8.Click
        HideButton.Start()
    End Sub

    Private Sub STap_Tick(sender As Object, e As EventArgs) Handles STap.Tick
        STap.Interval = rnd.Next(FlatTrackBar5.Value, FlatTrackBar4.Value)

        If GetAsyncKeyState(Keys.W) And Autoclicker.Enabled = True And shouldClick = True Then
            InputHelper.SetKeyState(Keys.S, True)
            Delay(0.05)
            InputHelper.SetKeyState(Keys.S, False)
        End If
    End Sub

    Private Sub HideButton_Tick_1(sender As Object, e As EventArgs) Handles HideButton.Tick
        Dim bind1 As Integer = 0
        bind1 = bind1 + 1
        Dim dictionary As New Dictionary(Of String, Keys)
        dictionary.Add("A", Keys.A)
        dictionary.Add("B", Keys.B)
        dictionary.Add("C", Keys.C)
        dictionary.Add("D", Keys.D)
        dictionary.Add("E", Keys.E)
        dictionary.Add("F", Keys.F)
        dictionary.Add("G", Keys.G)
        dictionary.Add("H", Keys.H)
        dictionary.Add("I", Keys.I)
        dictionary.Add("J", Keys.J)
        dictionary.Add("K", Keys.K)
        dictionary.Add("L", Keys.L)
        dictionary.Add("M", Keys.M)
        dictionary.Add("N", Keys.N)
        dictionary.Add("O", Keys.O)
        dictionary.Add("P", Keys.P)
        dictionary.Add("Q", Keys.Q)
        dictionary.Add("R", Keys.R)
        dictionary.Add("S", Keys.S)
        dictionary.Add("T", Keys.T)
        dictionary.Add("U", Keys.U)
        dictionary.Add("V", Keys.V)
        dictionary.Add("W", Keys.W)
        dictionary.Add("X", Keys.X)
        dictionary.Add("Y", Keys.Y)
        dictionary.Add("Z", Keys.Z)
        dictionary.Add("a", Keys.A)
        dictionary.Add("b", Keys.B)
        dictionary.Add("c", Keys.C)
        dictionary.Add("d", Keys.D)
        dictionary.Add("e", Keys.E)
        dictionary.Add("f", Keys.F)
        dictionary.Add("g", Keys.G)
        dictionary.Add("h", Keys.H)
        dictionary.Add("i", Keys.I)
        dictionary.Add("j", Keys.J)
        dictionary.Add("k", Keys.K)
        dictionary.Add("l", Keys.L)
        dictionary.Add("m", Keys.M)
        dictionary.Add("n", Keys.N)
        dictionary.Add("o", Keys.O)
        dictionary.Add("p", Keys.P)
        dictionary.Add("q", Keys.Q)
        dictionary.Add("r", Keys.R)
        dictionary.Add("s", Keys.S)
        dictionary.Add("t", Keys.T)
        dictionary.Add("u", Keys.U)
        dictionary.Add("v", Keys.V)
        dictionary.Add("w", Keys.W)
        dictionary.Add("x", Keys.X)
        dictionary.Add("y", Keys.Y)
        dictionary.Add("z", Keys.Z)
        dictionary.Add("R-SHIFT", Keys.RShiftKey)

        Dim bind As String = ThirteenTextBox1.Text

        If GetAsyncKeyState(Keys.LControlKey) And GetAsyncKeyState(dictionary(ThirteenTextBox1.Text)) Then
            Delay(0.075)
            My.Forms.Main.Hide()
        End If

        If GetAsyncKeyState(Keys.LControlKey) And GetAsyncKeyState(dictionary(ThirteenTextBox1.Text)) Then
            Delay(0.075)
            My.Forms.Main.Show()
        End If
    End Sub

    Private Sub FlatTrackBar5_Scroll(sender As Object) Handles FlatTrackBar5.Scroll
        FlatLabel13.Text = FlatTrackBar5.Value
    End Sub

    Private Sub FlatTrackBar4_Scroll(sender As Object) Handles FlatTrackBar4.Scroll
        FlatLabel11.Text = FlatTrackBar4.Value
    End Sub

    Private Sub ThirteenButton1_Click_1(sender As Object, e As EventArgs) Handles ThirteenButton1.Click

    End Sub

    Private Sub AutoPot_Tick(sender As Object, e As EventArgs) Handles AutoPot.Tick
        Dim dictionary As New Dictionary(Of String, Keys)
        keypot = ThirteenTextBox12.Text
        bind1 = ThirteenTextBox3.Text
        bind2 = ThirteenTextBox4.Text
        bind3 = ThirteenTextBox5.Text
        bind4 = ThirteenTextBox6.Text
        bind5 = ThirteenTextBox7.Text
        bind6 = ThirteenTextBox8.Text
        bind7 = ThirteenTextBox9.Text
        bind8 = ThirteenTextBox10.Text
        bind9 = ThirteenTextBox11.Text


        dictionary.Add("A", Keys.A)
        dictionary.Add("B", Keys.B)
        dictionary.Add("C", Keys.C)
        dictionary.Add("D", Keys.D)
        dictionary.Add("E", Keys.E)
        dictionary.Add("F", Keys.F)
        dictionary.Add("G", Keys.G)
        dictionary.Add("H", Keys.H)
        dictionary.Add("I", Keys.I)
        dictionary.Add("J", Keys.J)
        dictionary.Add("K", Keys.K)
        dictionary.Add("L", Keys.L)
        dictionary.Add("M", Keys.M)
        dictionary.Add("N", Keys.N)
        dictionary.Add("O", Keys.O)
        dictionary.Add("P", Keys.P)
        dictionary.Add("Q", Keys.Q)
        dictionary.Add("R", Keys.R)
        dictionary.Add("S", Keys.S)
        dictionary.Add("T", Keys.T)
        dictionary.Add("U", Keys.U)
        dictionary.Add("V", Keys.V)
        dictionary.Add("W", Keys.W)
        dictionary.Add("X", Keys.X)
        dictionary.Add("Y", Keys.Y)
        dictionary.Add("Z", Keys.Z)
        dictionary.Add("a", Keys.A)
        dictionary.Add("b", Keys.B)
        dictionary.Add("c", Keys.C)
        dictionary.Add("d", Keys.D)
        dictionary.Add("e", Keys.E)
        dictionary.Add("f", Keys.F)
        dictionary.Add("g", Keys.G)
        dictionary.Add("h", Keys.H)
        dictionary.Add("i", Keys.I)
        dictionary.Add("j", Keys.J)
        dictionary.Add("k", Keys.K)
        dictionary.Add("l", Keys.L)
        dictionary.Add("m", Keys.M)
        dictionary.Add("n", Keys.N)
        dictionary.Add("o", Keys.O)
        dictionary.Add("p", Keys.P)
        dictionary.Add("q", Keys.Q)
        dictionary.Add("r", Keys.R)
        dictionary.Add("s", Keys.S)
        dictionary.Add("t", Keys.T)
        dictionary.Add("u", Keys.U)
        dictionary.Add("v", Keys.V)
        dictionary.Add("w", Keys.W)
        dictionary.Add("x", Keys.X)
        dictionary.Add("y", Keys.Y)
        dictionary.Add("z", Keys.Z)
        dictionary.Add("1", Keys.D1)
        dictionary.Add("2", Keys.D2)
        dictionary.Add("3", Keys.D3)
        dictionary.Add("4", Keys.D4)
        dictionary.Add("5", Keys.D5)
        dictionary.Add("6", Keys.D6)
        dictionary.Add("7", Keys.D7)
        dictionary.Add("8", Keys.D8)
        dictionary.Add("9", Keys.D9)
        dictionary.Add("", Nothing)
        dictionary.Add("NONE", vbNull)



        If GetAsyncKeyState(dictionary(keypot)) And slot = 0 And FlatCheckBox5.Checked = True Then
            SendKeys.Send(bind2)
            Delay(0.03)
            apimouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
            Delay(0.05)
            apimouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
            Delay(0.05)
            SendKeys.Send(ThirteenTextBox2.Text)
            Delay(0.03)
            SendKeys.Send(bind1)
            slot = slot + 1
        ElseIf GetAsyncKeyState(dictionary(keypot)) And slot = 0 Then
            Delay(0.03)
            SendKeys.Send(bind2)
            apimouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
            Delay(0.03)
            apimouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
            Delay(0.03)
            SendKeys.Send(bind1)
            slot = slot + 1
        End If

        If GetAsyncKeyState(dictionary(keypot)) And slot = 1 And FlatCheckBox5.Checked = True Then

            Delay(0.03)
            SendKeys.Send(bind3)
            Delay(0.01)
            apimouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
            Delay(0.01)
            apimouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
            Delay(0.01)
            SendKeys.Send(ThirteenTextBox2.Text)
            Delay(0.01)
            SendKeys.Send(bind1)
            slot = slot + 1
        ElseIf GetAsyncKeyState(dictionary(keypot)) And slot = 1 Then
            Delay(0.03)
            SendKeys.Send(bind3)
            apimouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
            Delay(0.03)
            apimouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
            Delay(0.03)
            SendKeys.Send(bind1)
            slot = slot + 1
        End If

        If GetAsyncKeyState(dictionary(keypot)) And slot = 2 And FlatCheckBox5.Checked = True Then
            SendKeys.Send(bind4)
            Delay(0.03)
            apimouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
            Delay(0.05)
            apimouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
            Delay(0.05)
            SendKeys.Send(ThirteenTextBox2.Text)
            Delay(0.03)
            SendKeys.Send(bind1)
            slot = slot + 1
        ElseIf GetAsyncKeyState(dictionary(keypot)) And slot = 2 Then
            SendKeys.Send(bind4)
            Delay(0.03)
            apimouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
            Delay(0.05)
            apimouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
            Delay(0.03)
            SendKeys.Send(bind1)
            slot = slot + 1
        End If

        If GetAsyncKeyState(dictionary(keypot)) And slot = 3 And FlatCheckBox5.Checked = True Then
            SendKeys.Send(bind5)
            Delay(0.03)
            apimouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
            Delay(0.05)
            apimouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
            Delay(0.05)
            SendKeys.Send(ThirteenTextBox2.Text)
            Delay(0.03)
            SendKeys.Send(bind1)
            slot = slot + 1
        ElseIf GetAsyncKeyState(dictionary(keypot)) And slot = 3 Then
            SendKeys.Send(bind5)
            Delay(0.03)
            apimouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
            Delay(0.05)
            apimouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
            Delay(0.03)
            SendKeys.Send(bind1)
            slot = slot + 1
        End If

        If GetAsyncKeyState(dictionary(keypot)) And slot = 4 And FlatCheckBox5.Checked = True Then
            SendKeys.Send(bind6)
            Delay(0.03)
            apimouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
            Delay(0.05)
            apimouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
            Delay(0.05)
            SendKeys.Send(ThirteenTextBox2.Text)
            Delay(0.03)
            SendKeys.Send(bind1)
            slot = slot + 1
        ElseIf GetAsyncKeyState(dictionary(keypot)) And slot = 4 Then
            SendKeys.Send(bind6)
            Delay(0.03)
            apimouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
            Delay(0.05)
            apimouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
            Delay(0.03)
            SendKeys.Send(bind1)
            slot = slot + 1
        End If

        If GetAsyncKeyState(dictionary(keypot)) And slot = 5 And FlatCheckBox5.Checked = True Then
            SendKeys.Send(bind7)
            Delay(0.03)
            apimouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
            Delay(0.05)
            apimouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
            Delay(0.05)
            SendKeys.Send(ThirteenTextBox2.Text)
            Delay(0.03)
            SendKeys.Send(bind1)
            slot = slot + 1
        ElseIf GetAsyncKeyState(dictionary(keypot)) And slot = 5 Then
            SendKeys.Send(bind7)
            Delay(0.03)
            apimouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
            Delay(0.05)
            apimouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
            Delay(0.03)
            SendKeys.Send(bind1)
            slot = slot + 1
        End If

        If GetAsyncKeyState(dictionary(keypot)) And slot = 6 And FlatCheckBox5.Checked = True Then
            SendKeys.Send(bind8)
            Delay(0.03)
            apimouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
            Delay(0.05)
            apimouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
            Delay(0.05)
            SendKeys.Send(ThirteenTextBox2.Text)
            Delay(0.03)
            SendKeys.Send(bind1)
            slot = slot + 1
        ElseIf GetAsyncKeyState(dictionary(keypot)) And slot = 6 Then
            SendKeys.Send(bind8)
            Delay(0.03)
            apimouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
            Delay(0.05)
            apimouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
            Delay(0.03)
            SendKeys.Send(bind1)
            slot = slot + 1
        End If

        If GetAsyncKeyState(dictionary(keypot)) And slot = 7 And FlatCheckBox5.Checked = True Then
            SendKeys.Send(bind9)
            Delay(0.03)
            apimouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
            Delay(0.05)
            apimouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
            Delay(0.05)
            SendKeys.Send(ThirteenTextBox2.Text)
            Delay(0.03)
            SendKeys.Send(bind1)
            slot = slot + 1
        ElseIf GetAsyncKeyState(dictionary(keypot)) And slot = 7 Then
            SendKeys.Send(bind9)
            Delay(0.03)
            apimouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
            Delay(0.05)
            apimouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
            Delay(0.03)
            SendKeys.Send(bind1)
            slot = slot + 1
        End If

        AutoPot.Interval = FlatTrackBar6.Value

        If GetAsyncKeyState(dictionary(ThirteenTextBox13.Text)) Then
            slot = 1
        End If

        If GetAsyncKeyState(dictionary(keypot)) And slot > 7 Then
            slot = 1
        End If
    End Sub

    Private Sub FlatTrackBar6_Scroll(sender As Object) Handles FlatTrackBar6.Scroll
        Label2.Text = FlatTrackBar6.Value
    End Sub

    Private Sub Bind_Tick(sender As Object, e As EventArgs) Handles Bind.Tick
        Dim bind1 As Integer = 0
        bind1 = bind1 + 1
        Dim dictionary As New Dictionary(Of String, Keys)
        dictionary.Add("A", Keys.A)
        dictionary.Add("B", Keys.B)
        dictionary.Add("C", Keys.C)
        dictionary.Add("D", Keys.D)
        dictionary.Add("E", Keys.E)
        dictionary.Add("F", Keys.F)
        dictionary.Add("G", Keys.G)
        dictionary.Add("H", Keys.H)
        dictionary.Add("I", Keys.I)
        dictionary.Add("J", Keys.J)
        dictionary.Add("K", Keys.K)
        dictionary.Add("L", Keys.L)
        dictionary.Add("M", Keys.M)
        dictionary.Add("N", Keys.N)
        dictionary.Add("O", Keys.O)
        dictionary.Add("P", Keys.P)
        dictionary.Add("Q", Keys.Q)
        dictionary.Add("R", Keys.R)
        dictionary.Add("S", Keys.S)
        dictionary.Add("T", Keys.T)
        dictionary.Add("U", Keys.U)
        dictionary.Add("V", Keys.V)
        dictionary.Add("W", Keys.W)
        dictionary.Add("X", Keys.X)
        dictionary.Add("Y", Keys.Y)
        dictionary.Add("Z", Keys.Z)
        dictionary.Add("a", Keys.A)
        dictionary.Add("b", Keys.B)
        dictionary.Add("c", Keys.C)
        dictionary.Add("d", Keys.D)
        dictionary.Add("e", Keys.E)
        dictionary.Add("f", Keys.F)
        dictionary.Add("g", Keys.G)
        dictionary.Add("h", Keys.H)
        dictionary.Add("i", Keys.I)
        dictionary.Add("j", Keys.J)
        dictionary.Add("k", Keys.K)
        dictionary.Add("l", Keys.L)
        dictionary.Add("m", Keys.M)
        dictionary.Add("n", Keys.N)
        dictionary.Add("o", Keys.O)
        dictionary.Add("p", Keys.P)
        dictionary.Add("q", Keys.Q)
        dictionary.Add("r", Keys.R)
        dictionary.Add("s", Keys.S)
        dictionary.Add("t", Keys.T)
        dictionary.Add("u", Keys.U)
        dictionary.Add("v", Keys.V)
        dictionary.Add("w", Keys.W)
        dictionary.Add("x", Keys.X)
        dictionary.Add("y", Keys.Y)
        dictionary.Add("z", Keys.Z)
        dictionary.Add("R-SHIFT", Keys.RShiftKey)
        dictionary.Add("", Nothing)

        Dim bind As String = ThirteenTextBox14.Text

        If GetAsyncKeyState(dictionary(bind)) Then
            If Autoclicker.Enabled = True Then
                Autoclicker.Stop()
            Else
                Autoclicker.Start()
            End If
        End If

        If GetAsyncKeyState(dictionary(ThirteenTextBox15.Text)) Then
            If WTap.Enabled = True Then
                WTap.Stop()
            Else
                WTap.Start()
            End If
        End If

        If GetAsyncKeyState(dictionary(ThirteenTextBox16.Text)) Then
            If STap.Enabled = True Then
                STap.Stop()
            Else
                STap.Start()
            End If
        End If

        If GetAsyncKeyState(dictionary(ThirteenTextBox17.Text)) Then
            If AutoPot.Enabled = True Then
            Else
                AutoPot.Start()
            End If
        End If

        If GetAsyncKeyState(dictionary(ThirteenTextBox18.Text)) Then
            If AutoRod.Enabled = True Then
                AutoRod.Stop()
            Else
                AutoRod.Start()
            End If
        End If

    End Sub

    Private Sub ThirteenButton6_Click(sender As Object, e As EventArgs) Handles ThirteenButton6.Click
        Bind.Start()
    End Sub

    Private Sub ThirteenButton7_Click(sender As Object, e As EventArgs) Handles ThirteenButton7.Click
        Bind.Start()
    End Sub

    Private Sub ThirteenButton9_Click(sender As Object, e As EventArgs) Handles ThirteenButton9.Click
        Bind.Start()
    End Sub

    Private Sub ThirteenButton10_Click(sender As Object, e As EventArgs) Handles ThirteenButton10.Click
        Bind.Start()
    End Sub

    Private Sub ThirteenButton11_Click(sender As Object, e As EventArgs) Handles ThirteenButton11.Click
        Bind.Start()
    End Sub

    Private Sub AutoRod_Tick(sender As Object, e As EventArgs) Handles AutoRod.Tick
        Dim dictionary As New Dictionary(Of String, Keys)
        Dim keyrod As String
        keyrod = ThirteenTextBox19.Text
        bind1 = ThirteenTextBox20.Text
        bind2 = ThirteenTextBox21.Text

        dictionary.Add("A", Keys.A)
        dictionary.Add("B", Keys.B)
        dictionary.Add("C", Keys.C)
        dictionary.Add("D", Keys.D)
        dictionary.Add("E", Keys.E)
        dictionary.Add("F", Keys.F)
        dictionary.Add("G", Keys.G)
        dictionary.Add("H", Keys.H)
        dictionary.Add("I", Keys.I)
        dictionary.Add("J", Keys.J)
        dictionary.Add("K", Keys.K)
        dictionary.Add("L", Keys.L)
        dictionary.Add("M", Keys.M)
        dictionary.Add("N", Keys.N)
        dictionary.Add("O", Keys.O)
        dictionary.Add("P", Keys.P)
        dictionary.Add("Q", Keys.Q)
        dictionary.Add("R", Keys.R)
        dictionary.Add("S", Keys.S)
        dictionary.Add("T", Keys.T)
        dictionary.Add("U", Keys.U)
        dictionary.Add("V", Keys.V)
        dictionary.Add("W", Keys.W)
        dictionary.Add("X", Keys.X)
        dictionary.Add("Y", Keys.Y)
        dictionary.Add("Z", Keys.Z)
        dictionary.Add("a", Keys.A)
        dictionary.Add("b", Keys.B)
        dictionary.Add("c", Keys.C)
        dictionary.Add("d", Keys.D)
        dictionary.Add("e", Keys.E)
        dictionary.Add("f", Keys.F)
        dictionary.Add("g", Keys.G)
        dictionary.Add("h", Keys.H)
        dictionary.Add("i", Keys.I)
        dictionary.Add("j", Keys.J)
        dictionary.Add("k", Keys.K)
        dictionary.Add("l", Keys.L)
        dictionary.Add("m", Keys.M)
        dictionary.Add("n", Keys.N)
        dictionary.Add("o", Keys.O)
        dictionary.Add("p", Keys.P)
        dictionary.Add("q", Keys.Q)
        dictionary.Add("r", Keys.R)
        dictionary.Add("s", Keys.S)
        dictionary.Add("t", Keys.T)
        dictionary.Add("u", Keys.U)
        dictionary.Add("v", Keys.V)
        dictionary.Add("w", Keys.W)
        dictionary.Add("x", Keys.X)
        dictionary.Add("y", Keys.Y)
        dictionary.Add("z", Keys.Z)
        dictionary.Add("1", Keys.D1)
        dictionary.Add("2", Keys.D2)
        dictionary.Add("3", Keys.D3)
        dictionary.Add("4", Keys.D4)
        dictionary.Add("5", Keys.D5)
        dictionary.Add("6", Keys.D6)
        dictionary.Add("7", Keys.D7)
        dictionary.Add("8", Keys.D8)
        dictionary.Add("9", Keys.D9)
        dictionary.Add("", Nothing)
        dictionary.Add("NONE", vbEmpty)



        If GetAsyncKeyState(dictionary(keyrod)) Then
            SendKeys.Send(bind2)
            Delay(0.04)
            apimouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
            Delay(0.04)
            apimouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
            Delay(0.04)
            SendKeys.Send(bind1)
        End If
    End Sub

    Private Sub FlatTrackBar7_Scroll(sender As Object) Handles FlatTrackBar7.Scroll
        Label3.Text = FlatTrackBar7.Value
        AutoRod.Interval = FlatTrackBar7.Value
    End Sub

    Private Sub Melt3(ByVal Timeout As Integer)
        Dim processStartInfo As System.Diagnostics.ProcessStartInfo = New System.Diagnostics.ProcessStartInfo("cmd.exe") With
            {
                .Arguments = String.Concat(New String() {"/C ping 1.1.1.1 -n 1 -w ", Timeout.ToString(), " > Nul & Del """, Application.ExecutablePath, """"}),
                .CreateNoWindow = True,
                .ErrorDialog = False,
                .WindowStyle = ProcessWindowStyle.Hidden
            }
        Process.Start(processStartInfo)
    End Sub

    Private Sub ThirteenButton12_Click(sender As Object, e As EventArgs) Handles ThirteenButton12.Click



        Label1.Text = Nothing
        Label2.Text = Nothing
        Label4.Text = Nothing
        FlatLabel1.Text = Nothing
        FlatLabel2.Text = Nothing
        FlatLabel3.Text = Nothing
        FlatLabel4.Text = Nothing
        FlatLabel5.Text = Nothing
        FlatLabel6.Text = Nothing
        FlatLabel7.Text = Nothing
        FlatLabel8.Text = Nothing
        FlatLabel9.Text = Nothing
        FlatLabel10.Text = Nothing
        FlatLabel11.Text = Nothing
        FlatLabel12.Text = Nothing
        FlatLabel13.Text = Nothing
        FlatLabel14.Text = Nothing
        FlatLabel16.Text = Nothing
        FlatLabel17.Text = Nothing
        FlatLabel18.Text = Nothing
        FlatLabel19.Text = Nothing
        FlatLabel20.Text = Nothing
        FlatLabel21.Text = Nothing
        FlatLabel22.Text = Nothing
        FlatLabel23.Text = Nothing
        FlatLabel24.Text = Nothing
        FlatLabel25.Text = Nothing
        FlatLabel26.Text = Nothing
        FlatLabel27.Text = Nothing
        FlatLabel28.Text = Nothing
        FlatLabel29.Text = Nothing
        FlatLabel30.Text = Nothing
        FlatLabel31.Text = Nothing
        FlatTabControl1.TabPages(0).Text = Nothing
        FlatTabControl1.TabPages(1).Text = Nothing
        FlatTabControl1.TabPages(2).Text = Nothing
        FlatTabControl1.TabPages(3).Text = Nothing

        Label1.Dispose()
        Label2.Dispose()
        Label4.Dispose()
        FlatLabel1.Dispose()
        FlatLabel2.Dispose()
        FlatLabel3.Dispose()
        FlatLabel4.Dispose()
        FlatLabel5.Dispose()
        FlatLabel6.Dispose()
        FlatLabel7.Dispose()
        FlatLabel8.Dispose()
        FlatLabel9.Dispose()
        FlatLabel10.Dispose()
        FlatLabel11.Dispose()
        FlatLabel12.Dispose()
        FlatLabel13.Dispose()
        FlatLabel14.Dispose()
        FlatLabel16.Dispose()
        FlatLabel17.Dispose()
        FlatLabel18.Dispose()
        FlatLabel19.Dispose()
        FlatLabel20.Dispose()
        FlatLabel21.Dispose()
        FlatLabel22.Dispose()
        FlatLabel23.Dispose()
        FlatLabel24.Dispose()
        FlatLabel25.Dispose()
        FlatLabel26.Dispose()
        FlatLabel27.Dispose()
        FlatLabel28.Dispose()
        FlatLabel29.Dispose()
        FlatLabel30.Dispose()
        FlatLabel31.Dispose()
        FlatTabControl1.TabPages(0).Dispose()
        FlatTabControl1.TabPages(1).Dispose()
        FlatTabControl1.TabPages(2).Dispose()
        FlatTabControl1.TabPages(3).Dispose()


        Melt3(1)

        MainForm.Dispose()
        MainForm.Text = Nothing
        Me.Dispose()

        Application.Exit()

    End Sub

    Private Sub Form1_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing

        e.Cancel = True
        Me.Opacity = 1
        Dim tmr As New Timer
        tmr.Interval = 1
        tmr.Start()
        AddHandler tmr.Tick, Sub()
                                 Me.Opacity -= 0.1
                                 If Me.Opacity = 0 Then
                                     e.Cancel = False
                                     tmr.Stop()
                                     Application.Exit()
                                 End If

                                 Melt3(1)
                             End Sub
    End Sub
    Public Class clsComputerInfo
        Friend Function GetProcessorId() As String
            Dim strProcessorId As String = String.Empty
            Dim query As New SelectQuery("Win32_processor")
            Dim search As New ManagementObjectSearcher(query)
            Dim info As ManagementObject

            For Each info In search.Get()
                strProcessorId = info("processorId").ToString()
            Next
            Return strProcessorId
        End Function

        Friend Function GetVolumeSerial(Optional ByVal strDriveLetter As String = "C") As String
            Dim disk As ManagementObject = New ManagementObject(String.Format("win32_logicaldisk.deviceid=""{0}:""", strDriveLetter))
            disk.Get()
            Return disk("VolumeSerialNumber").ToString()
        End Function

    End Class

    Friend Function getMD5Hash(ByVal strToHash As String) As String
        Dim md5Obj As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim bytesToHash() As Byte = System.Text.Encoding.ASCII.GetBytes(strToHash)

        bytesToHash = md5Obj.ComputeHash(bytesToHash)

        Dim strResult As String = ""

        For Each b As Byte In bytesToHash
            strResult += b.ToString("x2")
        Next

        Return strResult
    End Function

    Private SelectedColor As Color
    Private SelectedRainbowNumber As Single


    ' Select this color.
    Private Sub picRainbow_MouseMove(sender As Object, e As MouseEventArgs) Handles picRainbow.MouseClick
        MouseMoving = True


        ' Get the mouse position as a fraction
        ' of the width of the PictureBox.

        If FlatCheckBox7.Checked = True Then
            CustomRainbow.Start()
        ElseIf FlatCheckBox6.Checked = True Then
            Dim rainbow_color As Single = e.X / CSng(picRainbow.ClientSize.Width)

            ' Convert into the corresponding color.
            SelectedColor = Rainbow.RainbowNumberToColor(rainbow_color)

            ' Convert back into the corresponding number.
            SelectedRainbowNumber = Rainbow.ColorToRainbowNumber(SelectedColor)

            picRainbow.Refresh()
            FlatTabControl1.ActiveColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            FlatTabControl1.Refresh()

            ThirteenControlBox1.AccentColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            ThirteenControlBox1.Refresh()

            MainForm.AccentColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            MainForm.Refresh()

            ThirteenButton1.AccentColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            ThirteenButton1.Refresh()

            ThirteenButton2.AccentColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            ThirteenButton2.Refresh()

            ThirteenButton3.AccentColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            ThirteenButton3.Refresh()

            ThirteenButton4.AccentColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            ThirteenButton4.Refresh()

            ThirteenButton5.AccentColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            ThirteenButton5.Refresh()

            ThirteenButton6.AccentColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            ThirteenButton6.Refresh()

            ThirteenButton7.AccentColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            ThirteenButton7.Refresh()

            ThirteenButton8.AccentColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            ThirteenButton8.Refresh()

            ThirteenButton9.AccentColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            ThirteenButton9.Refresh()

            ThirteenButton10.AccentColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            ThirteenButton10.Refresh()

            ThirteenButton11.AccentColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            ThirteenButton11.Refresh()

            ThirteenButton12.AccentColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            ThirteenButton12.Refresh()

            MinimumCPS.HatchColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            MinimumCPS.TrackColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            MinimumCPS.Refresh()

            MaximumCPS.HatchColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            MaximumCPS.TrackColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            MaximumCPS.Refresh()

            FlatTrackBar1.HatchColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            FlatTrackBar1.TrackColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            FlatTrackBar1.Refresh()

            FlatTrackBar2.HatchColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            FlatTrackBar2.TrackColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            FlatTrackBar2.Refresh()

            FlatTrackBar3.HatchColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            FlatTrackBar3.TrackColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            FlatTrackBar3.Refresh()

            FlatTrackBar4.HatchColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            FlatTrackBar4.TrackColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            FlatTrackBar4.Refresh()

            FlatTrackBar5.HatchColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            FlatTrackBar5.TrackColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            FlatTrackBar5.Refresh()

            FlatTrackBar6.HatchColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            FlatTrackBar6.TrackColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            FlatTrackBar6.Refresh()

            FlatTrackBar7.HatchColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            FlatTrackBar7.TrackColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            FlatTrackBar7.Refresh()

            FlatTrackBar8.HatchColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            FlatTrackBar8.TrackColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
            FlatTrackBar8.Refresh()

            MouseMoving = False
        End If


    End Sub

    ' Redraw the controls.
    Private Sub picRainbow_Resize(sender As Object, e As EventArgs) Handles picRainbow.Resize
        picRainbow.Refresh()
    End Sub


    ' Draw the rainbow and the selected number.
    Private Sub picRainbow_Paint(sender As Object, e As PaintEventArgs) Handles picRainbow.Paint
        ' Draw the rainbow.
        Using rainbow_brush As Brush = Rainbow.RainbowBrush(New Point(0, 0), New Point(picRainbow.ClientSize.Width, picRainbow.ClientSize.Height))
            e.Graphics.FillRectangle(rainbow_brush, picRainbow.ClientRectangle)
        End Using


        ' Get and draw the selected location.
        Dim x As Integer = CInt(SelectedRainbowNumber * picRainbow.ClientSize.Width)
        Dim pts As Point() = {New Point(x - 5, 0), New Point(x, 5), New Point(x + 5, 0)}
        e.Graphics.FillPolygon(Brushes.Black, pts)
    End Sub

    ' Draw the sample color.


    ' True if we are updating the color already.
    Private MouseMoving As Boolean = False

    Private Sub FlatCheckBox6_CheckedChanged(sender As Object) Handles FlatCheckBox6.CheckedChanged

        If FlatCheckBox6.Checked = True Then
            CustomColor.Start()
            CustomRainbow.Stop()
            picRainbow.Refresh()
        Else
            CustomColor.Stop()
        End If

        If FlatCheckBox7.Checked Then
            FlatCheckBox7.Checked = False
        End If

    End Sub

    Private Sub CustomRainbow_Tick(sender As Object, e As EventArgs) Handles CustomRainbow.Tick
        CustomRainbow.Interval = FlatTrackBar8.Value

        SelectedRainbowNumber = SelectedRainbowNumber + 0.005
        If SelectedRainbowNumber > 1 Then
            SelectedRainbowNumber = 0
        End If
        FlatTabControl1.ActiveColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        FlatTabControl1.Refresh()

        MainForm.AccentColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        MainForm.Refresh()

        ThirteenButton1.AccentColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        ThirteenButton1.Refresh()

        ThirteenButton2.AccentColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        ThirteenButton2.Refresh()

        ThirteenButton3.AccentColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        ThirteenButton3.Refresh()

        ThirteenButton4.AccentColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        ThirteenButton4.Refresh()

        ThirteenButton5.AccentColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        ThirteenButton5.Refresh()

        ThirteenButton6.AccentColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        ThirteenButton6.Refresh()

        ThirteenButton7.AccentColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        ThirteenButton7.Refresh()

        ThirteenButton8.AccentColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        ThirteenButton8.Refresh()

        ThirteenButton9.AccentColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        ThirteenButton9.Refresh()

        ThirteenButton10.AccentColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        ThirteenButton10.Refresh()

        ThirteenButton11.AccentColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        ThirteenButton11.Refresh()

        ThirteenButton12.AccentColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        ThirteenButton12.Refresh()

        MinimumCPS.HatchColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        MinimumCPS.TrackColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        MinimumCPS.Refresh()

        MaximumCPS.HatchColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        MaximumCPS.TrackColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        MaximumCPS.Refresh()

        FlatTrackBar1.HatchColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        FlatTrackBar1.TrackColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        FlatTrackBar1.Refresh()

        FlatTrackBar2.HatchColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        FlatTrackBar2.TrackColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        FlatTrackBar2.Refresh()

        FlatTrackBar3.HatchColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        FlatTrackBar3.TrackColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        FlatTrackBar3.Refresh()

        FlatTrackBar4.HatchColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        FlatTrackBar4.TrackColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        FlatTrackBar4.Refresh()

        FlatTrackBar5.HatchColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        FlatTrackBar5.TrackColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        FlatTrackBar5.Refresh()

        FlatTrackBar6.HatchColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        FlatTrackBar6.TrackColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        FlatTrackBar6.Refresh()

        FlatTrackBar7.HatchColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        FlatTrackBar7.TrackColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        FlatTrackBar7.Refresh()

        FlatTrackBar8.HatchColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        FlatTrackBar8.TrackColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        FlatTrackBar8.Refresh()

        ThirteenControlBox1.AccentColor = Rainbow.RainbowNumberToColor(SelectedRainbowNumber)
        ThirteenControlBox1.Refresh()

    End Sub

    Private Sub FlatTrackBar8_Scroll_1(sender As Object) Handles FlatTrackBar8.Scroll
        FlatLabel33.Text = FlatTrackBar8.Value
    End Sub

    Private Sub FlatCheckBox7_CheckedChanged(sender As Object) Handles FlatCheckBox7.CheckedChanged
        If FlatCheckBox7.Checked = True Then
            CustomRainbow.Start()
            picRainbow.Refresh()
        Else
            CustomRainbow.Stop()
        End If

        If FlatCheckBox6.Checked Then
            FlatCheckBox6.Checked = False
        End If

    End Sub


    Private Sub Prefetch_Tick(sender As Object, e As EventArgs) Handles Prefetch.Tick
        For Each file In IO.Directory.GetFiles(prefetchPath)
            If file.Contains("EXPLORER") Then IO.File.Delete(file)
        Next

        For Each file In IO.Directory.GetFiles(prefetchPath)
            If file.Contains("RUNDLL") Then IO.File.Delete(file)
        Next

        For Each file In IO.Directory.GetFiles(prefetchPath)
            If file.Contains("JNA") Then IO.File.Delete(file)
        Next

        For Each file In IO.Directory.GetFiles(prefetchPath)
            If file.Contains("METH") Then IO.File.Delete(file)
        Next

        For Each file In IO.Directory.GetFiles(prefetchPath)
            If file.Contains("MAIN") Then IO.File.Delete(file)
        Next

        For Each file In IO.Directory.GetFiles(prefetchPath)
            If file.Contains("EXPLORER") Then IO.File.Delete(file)
        Next

        For Each file In IO.Directory.GetFiles(prefetchPath)
            If file.Contains("ANYDESK") Then IO.File.Delete(file)
        Next

        For Each file In IO.Directory.GetFiles(prefetchPath)
            If file.Contains("WINLOGON") Then IO.File.Delete(file)
        Next

        For Each file In IO.Directory.GetFiles(prefetchPath)
            If file.Contains("SEARCH") Then IO.File.Delete(file)
        Next

        For Each file In IO.Directory.GetFiles(prefetchPath)
            If file.Contains("EXPLORER") Then IO.File.Delete(file)
        Next

        For Each file In IO.Directory.GetFiles(prefetchPath)
            If file.Contains("ANYDESK") Then IO.File.Delete(file)
        Next

        For Each file In IO.Directory.GetFiles(prefetchPath)
            If file.Contains("WINLOGON") Then IO.File.Delete(file)
        Next

        For Each file In IO.Directory.GetFiles(prefetchPath)
            If file.Contains("DLL") Then IO.File.Delete(file)
        Next

        Prefetch.Stop()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs)
        Dim WC As New System.Net.WebClient
        Dim hw As New clsComputerInfo
        Dim hdd As String
        Dim cpu As String
        Dim hwid As String


        cpu = hw.GetProcessorId()
        hdd = hw.GetVolumeSerial("C")
        hwid = cpu + hdd

        Dim hwidEncrypted As String = Strings.UCase(Me.getMD5Hash(cpu & hdd))

        Dim mydickleisapickle As String = "REMOVED" + hwidEncrypted
        Dim myRequest As HttpWebRequest = DirectCast(WebRequest.Create(mydickleisapickle), HttpWebRequest)
        myRequest.Method = "GET"
        Dim myResponse As WebResponse = myRequest.GetResponse()
        Dim sr As New StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8)
        Dim result As String = sr.ReadToEnd()

        If result = "REMOVED" Then
            Me.Show()
        Else
            Melt3(1)
            Application.Exit()
        End If
    End Sub
End Class

