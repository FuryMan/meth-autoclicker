Imports System.Runtime.InteropServices
Imports Meth
Imports Microsoft.VisualBasic.CompilerServices

Public NotInheritable Class InputHelper
    Private Sub New()
        MyBase.New()
    End Sub

    <DllImport("user32.dll", CharSet:=CharSet.None, ExactSpelling:=False)>
    Private Shared Function GetKeyboardLayout(ByVal idThread As UInteger) As IntPtr
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.None, ExactSpelling:=False)>
    Private Shared Function MapVirtualKeyEx(ByVal uCode As UInteger, ByVal uMapType As UInteger, ByVal dwhkl As IntPtr) As UInteger
    End Function

    Public Shared Sub PressKey(ByVal Key As Keys, Optional ByVal HardwareKey As Boolean = False)
        If (Not HardwareKey) Then
            InputHelper.SetKeyState(Key, False)
            InputHelper.SetKeyState(Key, True)
            Return
        End If
        InputHelper.SetHardwareKeyState(Key, False)
        InputHelper.SetHardwareKeyState(Key, True)
    End Sub

    Private Shared Function ReplaceBadKeys(ByVal Key As Keys) As Keys
        Dim key1 As Keys = Key
        If (key1.HasFlag(Keys.Control)) Then
            key1 = key1 And (Keys.KeyCode Or Keys.LButton Or Keys.RButton Or Keys.Cancel Or Keys.MButton Or Keys.XButton1 Or Keys.XButton2 Or Keys.Back Or Keys.Tab Or Keys.LineFeed Or Keys.Clear Or Keys.[Return] Or Keys.Enter Or Keys.ShiftKey Or Keys.ControlKey Or Keys.Menu Or Keys.Pause Or Keys.Capital Or Keys.CapsLock Or Keys.KanaMode Or Keys.HanguelMode Or Keys.HangulMode Or Keys.JunjaMode Or Keys.FinalMode Or Keys.HanjaMode Or Keys.KanjiMode Or Keys.Escape Or Keys.IMEConvert Or Keys.IMENonconvert Or Keys.IMEAccept Or Keys.IMEAceept Or Keys.IMEModeChange Or Keys.Space Or Keys.Prior Or Keys.PageUp Or Keys.[Next] Or Keys.PageDown Or Keys.[End] Or Keys.Home Or Keys.Left Or Keys.Up Or Keys.Right Or Keys.Down Or Keys.[Select] Or Keys.Print Or Keys.Execute Or Keys.Snapshot Or Keys.PrintScreen Or Keys.Insert Or Keys.Delete Or Keys.Help Or Keys.D0 Or Keys.D1 Or Keys.D2 Or Keys.D3 Or Keys.D4 Or Keys.D5 Or Keys.D6 Or Keys.D7 Or Keys.D8 Or Keys.D9 Or Keys.A Or Keys.B Or Keys.C Or Keys.D Or Keys.E Or Keys.F Or Keys.G Or Keys.H Or Keys.I Or Keys.J Or Keys.K Or Keys.L Or Keys.M Or Keys.N Or Keys.O Or Keys.P Or Keys.Q Or Keys.R Or Keys.S Or Keys.T Or Keys.U Or Keys.V Or Keys.W Or Keys.X Or Keys.Y Or Keys.Z Or Keys.LWin Or Keys.RWin Or Keys.Apps Or Keys.Sleep Or Keys.NumPad0 Or Keys.NumPad1 Or Keys.NumPad2 Or Keys.NumPad3 Or Keys.NumPad4 Or Keys.NumPad5 Or Keys.NumPad6 Or Keys.NumPad7 Or Keys.NumPad8 Or Keys.NumPad9 Or Keys.Multiply Or Keys.Add Or Keys.Separator Or Keys.Subtract Or Keys.[Decimal] Or Keys.Divide Or Keys.F1 Or Keys.F2 Or Keys.F3 Or Keys.F4 Or Keys.F5 Or Keys.F6 Or Keys.F7 Or Keys.F8 Or Keys.F9 Or Keys.F10 Or Keys.F11 Or Keys.F12 Or Keys.F13 Or Keys.F14 Or Keys.F15 Or Keys.F16 Or Keys.F17 Or Keys.F18 Or Keys.F19 Or Keys.F20 Or Keys.F21 Or Keys.F22 Or Keys.F23 Or Keys.F24 Or Keys.NumLock Or Keys.Scroll Or Keys.LShiftKey Or Keys.RShiftKey Or Keys.LControlKey Or Keys.RControlKey Or Keys.LMenu Or Keys.RMenu Or Keys.BrowserBack Or Keys.BrowserForward Or Keys.BrowserRefresh Or Keys.BrowserStop Or Keys.BrowserSearch Or Keys.BrowserFavorites Or Keys.BrowserHome Or Keys.VolumeMute Or Keys.VolumeDown Or Keys.VolumeUp Or Keys.MediaNextTrack Or Keys.MediaPreviousTrack Or Keys.MediaStop Or Keys.MediaPlayPause Or Keys.LaunchMail Or Keys.SelectMedia Or Keys.LaunchApplication1 Or Keys.LaunchApplication2 Or Keys.OemSemicolon Or Keys.Oem1 Or Keys.Oemplus Or Keys.Oemcomma Or Keys.OemMinus Or Keys.OemPeriod Or Keys.OemQuestion Or Keys.Oem2 Or Keys.Oemtilde Or Keys.Oem3 Or Keys.OemOpenBrackets Or Keys.Oem4 Or Keys.OemPipe Or Keys.Oem5 Or Keys.OemCloseBrackets Or Keys.Oem6 Or Keys.OemQuotes Or Keys.Oem7 Or Keys.Oem8 Or Keys.OemBackslash Or Keys.Oem102 Or Keys.ProcessKey Or Keys.Packet Or Keys.Attn Or Keys.Crsel Or Keys.Exsel Or Keys.EraseEof Or Keys.Play Or Keys.Zoom Or Keys.NoName Or Keys.Pa1 Or Keys.OemClear Or Keys.Shift Or Keys.Alt) Or Keys.ControlKey
        End If
        If (key1.HasFlag(Keys.Shift)) Then
            key1 = key1 And (Keys.KeyCode Or Keys.LButton Or Keys.RButton Or Keys.Cancel Or Keys.MButton Or Keys.XButton1 Or Keys.XButton2 Or Keys.Back Or Keys.Tab Or Keys.LineFeed Or Keys.Clear Or Keys.[Return] Or Keys.Enter Or Keys.ShiftKey Or Keys.ControlKey Or Keys.Menu Or Keys.Pause Or Keys.Capital Or Keys.CapsLock Or Keys.KanaMode Or Keys.HanguelMode Or Keys.HangulMode Or Keys.JunjaMode Or Keys.FinalMode Or Keys.HanjaMode Or Keys.KanjiMode Or Keys.Escape Or Keys.IMEConvert Or Keys.IMENonconvert Or Keys.IMEAccept Or Keys.IMEAceept Or Keys.IMEModeChange Or Keys.Space Or Keys.Prior Or Keys.PageUp Or Keys.[Next] Or Keys.PageDown Or Keys.[End] Or Keys.Home Or Keys.Left Or Keys.Up Or Keys.Right Or Keys.Down Or Keys.[Select] Or Keys.Print Or Keys.Execute Or Keys.Snapshot Or Keys.PrintScreen Or Keys.Insert Or Keys.Delete Or Keys.Help Or Keys.D0 Or Keys.D1 Or Keys.D2 Or Keys.D3 Or Keys.D4 Or Keys.D5 Or Keys.D6 Or Keys.D7 Or Keys.D8 Or Keys.D9 Or Keys.A Or Keys.B Or Keys.C Or Keys.D Or Keys.E Or Keys.F Or Keys.G Or Keys.H Or Keys.I Or Keys.J Or Keys.K Or Keys.L Or Keys.M Or Keys.N Or Keys.O Or Keys.P Or Keys.Q Or Keys.R Or Keys.S Or Keys.T Or Keys.U Or Keys.V Or Keys.W Or Keys.X Or Keys.Y Or Keys.Z Or Keys.LWin Or Keys.RWin Or Keys.Apps Or Keys.Sleep Or Keys.NumPad0 Or Keys.NumPad1 Or Keys.NumPad2 Or Keys.NumPad3 Or Keys.NumPad4 Or Keys.NumPad5 Or Keys.NumPad6 Or Keys.NumPad7 Or Keys.NumPad8 Or Keys.NumPad9 Or Keys.Multiply Or Keys.Add Or Keys.Separator Or Keys.Subtract Or Keys.[Decimal] Or Keys.Divide Or Keys.F1 Or Keys.F2 Or Keys.F3 Or Keys.F4 Or Keys.F5 Or Keys.F6 Or Keys.F7 Or Keys.F8 Or Keys.F9 Or Keys.F10 Or Keys.F11 Or Keys.F12 Or Keys.F13 Or Keys.F14 Or Keys.F15 Or Keys.F16 Or Keys.F17 Or Keys.F18 Or Keys.F19 Or Keys.F20 Or Keys.F21 Or Keys.F22 Or Keys.F23 Or Keys.F24 Or Keys.NumLock Or Keys.Scroll Or Keys.LShiftKey Or Keys.RShiftKey Or Keys.LControlKey Or Keys.RControlKey Or Keys.LMenu Or Keys.RMenu Or Keys.BrowserBack Or Keys.BrowserForward Or Keys.BrowserRefresh Or Keys.BrowserStop Or Keys.BrowserSearch Or Keys.BrowserFavorites Or Keys.BrowserHome Or Keys.VolumeMute Or Keys.VolumeDown Or Keys.VolumeUp Or Keys.MediaNextTrack Or Keys.MediaPreviousTrack Or Keys.MediaStop Or Keys.MediaPlayPause Or Keys.LaunchMail Or Keys.SelectMedia Or Keys.LaunchApplication1 Or Keys.LaunchApplication2 Or Keys.OemSemicolon Or Keys.Oem1 Or Keys.Oemplus Or Keys.Oemcomma Or Keys.OemMinus Or Keys.OemPeriod Or Keys.OemQuestion Or Keys.Oem2 Or Keys.Oemtilde Or Keys.Oem3 Or Keys.OemOpenBrackets Or Keys.Oem4 Or Keys.OemPipe Or Keys.Oem5 Or Keys.OemCloseBrackets Or Keys.Oem6 Or Keys.OemQuotes Or Keys.Oem7 Or Keys.Oem8 Or Keys.OemBackslash Or Keys.Oem102 Or Keys.ProcessKey Or Keys.Packet Or Keys.Attn Or Keys.Crsel Or Keys.Exsel Or Keys.EraseEof Or Keys.Play Or Keys.Zoom Or Keys.NoName Or Keys.Pa1 Or Keys.OemClear Or Keys.Control Or Keys.Alt) Or Keys.ShiftKey
        End If
        If (key1.HasFlag(Keys.Alt)) Then
            key1 = key1 And (Keys.KeyCode Or Keys.LButton Or Keys.RButton Or Keys.Cancel Or Keys.MButton Or Keys.XButton1 Or Keys.XButton2 Or Keys.Back Or Keys.Tab Or Keys.LineFeed Or Keys.Clear Or Keys.[Return] Or Keys.Enter Or Keys.ShiftKey Or Keys.ControlKey Or Keys.Menu Or Keys.Pause Or Keys.Capital Or Keys.CapsLock Or Keys.KanaMode Or Keys.HanguelMode Or Keys.HangulMode Or Keys.JunjaMode Or Keys.FinalMode Or Keys.HanjaMode Or Keys.KanjiMode Or Keys.Escape Or Keys.IMEConvert Or Keys.IMENonconvert Or Keys.IMEAccept Or Keys.IMEAceept Or Keys.IMEModeChange Or Keys.Space Or Keys.Prior Or Keys.PageUp Or Keys.[Next] Or Keys.PageDown Or Keys.[End] Or Keys.Home Or Keys.Left Or Keys.Up Or Keys.Right Or Keys.Down Or Keys.[Select] Or Keys.Print Or Keys.Execute Or Keys.Snapshot Or Keys.PrintScreen Or Keys.Insert Or Keys.Delete Or Keys.Help Or Keys.D0 Or Keys.D1 Or Keys.D2 Or Keys.D3 Or Keys.D4 Or Keys.D5 Or Keys.D6 Or Keys.D7 Or Keys.D8 Or Keys.D9 Or Keys.A Or Keys.B Or Keys.C Or Keys.D Or Keys.E Or Keys.F Or Keys.G Or Keys.H Or Keys.I Or Keys.J Or Keys.K Or Keys.L Or Keys.M Or Keys.N Or Keys.O Or Keys.P Or Keys.Q Or Keys.R Or Keys.S Or Keys.T Or Keys.U Or Keys.V Or Keys.W Or Keys.X Or Keys.Y Or Keys.Z Or Keys.LWin Or Keys.RWin Or Keys.Apps Or Keys.Sleep Or Keys.NumPad0 Or Keys.NumPad1 Or Keys.NumPad2 Or Keys.NumPad3 Or Keys.NumPad4 Or Keys.NumPad5 Or Keys.NumPad6 Or Keys.NumPad7 Or Keys.NumPad8 Or Keys.NumPad9 Or Keys.Multiply Or Keys.Add Or Keys.Separator Or Keys.Subtract Or Keys.[Decimal] Or Keys.Divide Or Keys.F1 Or Keys.F2 Or Keys.F3 Or Keys.F4 Or Keys.F5 Or Keys.F6 Or Keys.F7 Or Keys.F8 Or Keys.F9 Or Keys.F10 Or Keys.F11 Or Keys.F12 Or Keys.F13 Or Keys.F14 Or Keys.F15 Or Keys.F16 Or Keys.F17 Or Keys.F18 Or Keys.F19 Or Keys.F20 Or Keys.F21 Or Keys.F22 Or Keys.F23 Or Keys.F24 Or Keys.NumLock Or Keys.Scroll Or Keys.LShiftKey Or Keys.RShiftKey Or Keys.LControlKey Or Keys.RControlKey Or Keys.LMenu Or Keys.RMenu Or Keys.BrowserBack Or Keys.BrowserForward Or Keys.BrowserRefresh Or Keys.BrowserStop Or Keys.BrowserSearch Or Keys.BrowserFavorites Or Keys.BrowserHome Or Keys.VolumeMute Or Keys.VolumeDown Or Keys.VolumeUp Or Keys.MediaNextTrack Or Keys.MediaPreviousTrack Or Keys.MediaStop Or Keys.MediaPlayPause Or Keys.LaunchMail Or Keys.SelectMedia Or Keys.LaunchApplication1 Or Keys.LaunchApplication2 Or Keys.OemSemicolon Or Keys.Oem1 Or Keys.Oemplus Or Keys.Oemcomma Or Keys.OemMinus Or Keys.OemPeriod Or Keys.OemQuestion Or Keys.Oem2 Or Keys.Oemtilde Or Keys.Oem3 Or Keys.OemOpenBrackets Or Keys.Oem4 Or Keys.OemPipe Or Keys.Oem5 Or Keys.OemCloseBrackets Or Keys.Oem6 Or Keys.OemQuotes Or Keys.Oem7 Or Keys.Oem8 Or Keys.OemBackslash Or Keys.Oem102 Or Keys.ProcessKey Or Keys.Packet Or Keys.Attn Or Keys.Crsel Or Keys.Exsel Or Keys.EraseEof Or Keys.Play Or Keys.Zoom Or Keys.NoName Or Keys.Pa1 Or Keys.OemClear Or Keys.Shift Or Keys.Control) Or Keys.Menu
        End If
        Return key1
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.None, ExactSpelling:=False, SetLastError:=True)>
    Private Shared Function SendInput(ByVal nInputs As UInteger, ByVal pInputs As InputHelper.INPUT(), ByVal cbSize As Integer) As UInteger
    End Function

    Public Shared Sub SetHardwareKeyState(ByVal Key As Keys, ByVal KeyUp As Boolean)
        Key = InputHelper.ReplaceBadKeys(Key)
        Dim kEYBDINPUT As InputHelper.KEYBDINPUT = New InputHelper.KEYBDINPUT() With
        {
            .wVk = 0,
            .time = 0,
            .dwFlags = Conversions.ToUInteger(Operators.OrObject(InputHelper.KEYEVENTF.SCANCODE, If(KeyUp, InputHelper.KEYEVENTF.KEYUP, 0))),
            .dwExtraInfo = IntPtr.Zero
        }
        Dim nPUTUNION As InputHelper.INPUTUNION = New InputHelper.INPUTUNION() With
        {
            .ki = kEYBDINPUT
        }
        Dim nPUT As InputHelper.INPUT = New InputHelper.INPUT() With
        {
            .type = 1,
            .U = nPUTUNION
        }
        InputHelper.SendInput(1, New InputHelper.INPUT() {nPUT}, Marshal.SizeOf(GetType(InputHelper.INPUT)))
    End Sub

    Public Shared Sub SetKeyState(ByVal Key As Keys, ByVal KeyUp As Boolean)
        Key = InputHelper.ReplaceBadKeys(Key)
        Dim kEYBDINPUT As InputHelper.KEYBDINPUT = New InputHelper.KEYBDINPUT() With
        {
            .wVk = CUShort(Key),
            .wScan = 0,
            .time = 0,
            .dwFlags = Conversions.ToUInteger(If(KeyUp, InputHelper.KEYEVENTF.KEYUP, 0)),
            .dwExtraInfo = IntPtr.Zero
        }
        Dim nPUTUNION As InputHelper.INPUTUNION = New InputHelper.INPUTUNION() With
        {
            .ki = kEYBDINPUT
        }
        Dim nPUT As InputHelper.INPUT = New InputHelper.INPUT() With
        {
            .type = 1,
            .U = nPUTUNION
        }
        InputHelper.SendInput(1, New InputHelper.INPUT() {nPUT}, Marshal.SizeOf(GetType(InputHelper.INPUT)))
    End Sub

    Private Structure HARDWAREINPUT
        Public uMsg As Integer

        Public wParamL As Short

        Public wParamH As Short
    End Structure

    Private Structure INPUT
        Public type As Integer

        Public U As InputHelper.INPUTUNION
    End Structure

    Private Enum INPUTTYPE As UInteger
        MOUSE
        KEYBOARD
        HARDWARE
    End Enum

    <StructLayout(LayoutKind.Explicit)>
    Private Structure INPUTUNION
        <FieldOffset(0)>
        Public mi As InputHelper.MOUSEINPUT

        <FieldOffset(0)>
        Public ki As InputHelper.KEYBDINPUT

        <FieldOffset(0)>
        Public hi As InputHelper.HARDWAREINPUT
    End Structure

    Private Structure KEYBDINPUT
        Public wVk As UShort

        Public wScan As Short

        Public dwFlags As UInteger

        Public time As Integer

        Public dwExtraInfo As IntPtr
    End Structure

    <Flags>
    Private Enum KEYEVENTF As UInteger
        EXTENDEDKEY = 1
        KEYUP = 2
        UNICODE = 4
        SCANCODE = 8
    End Enum

    Private Structure MOUSEINPUT
        Public dx As Integer

        Public dy As Integer

        Public mouseData As Integer

        Public dwFlags As Integer

        Public time As Integer

        Public dwExtraInfo As IntPtr
    End Structure
End Class