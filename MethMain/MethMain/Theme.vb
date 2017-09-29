﻿Imports System.Drawing.Drawing2D
Imports System.ComponentModel
Imports Meth.Main

Public Class Theme : Inherits ContainerControl
    Enum ColorSchemes
        Light
        Dark
    End Enum
    Event ColorSchemeChanged()
    Private _ColorScheme As ColorSchemes
    Public Property ColorScheme() As ColorSchemes
        Get
            Return _ColorScheme
        End Get
        Set(ByVal value As ColorSchemes)
            _ColorScheme = value
            RaiseEvent ColorSchemeChanged()
        End Set
    End Property
    Protected Sub OnColorSchemeChanged() Handles Me.ColorSchemeChanged
        Invalidate()
        Select Case ColorScheme
            Case ColorSchemes.Dark
                BackColor = Color.FromArgb(50, 50, 50)
                ForeColor = Color.White
            Case ColorSchemes.Light
                BackColor = Color.White
                ForeColor = Color.Black
        End Select
    End Sub
#Region " Properties "
    Private _AccentColor As Color
    Public Property AccentColor() As Color
        Get
            Return _AccentColor
        End Get
        Set(ByVal value As Color)
            _AccentColor = value
            OnAccentColorChanged()
        End Set
    End Property
#End Region
#Region " Constructor "
    Sub New()
        MyBase.New()
        DoubleBuffered = True
        Font = New Font("Segoe UI Semilight", 9.75F)
        'AccentColor = Color.FromArgb(150, 0, 150)
        AccentColor = Color.DodgerBlue
        ColorScheme = ColorSchemes.Dark
        ForeColor = Color.White
        BackColor = Color.FromArgb(50, 50, 50)
        MoveHeight = 32
    End Sub
#End Region
#Region " Events "
    Event AccentColorChanged()
#End Region
#Region " Overrides "
    Private MouseP As Point = New Point(0, 0)
    Private Cap As Boolean = False
    Private MoveHeight As Integer
    Private pos As Integer = 0
    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseDown(e)
        If e.Button = System.Windows.Forms.MouseButtons.Left And New Rectangle(0, 0, Width, MoveHeight).Contains(e.Location) Then
            Cap = True : MouseP = e.Location
        End If
    End Sub
    Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseMove(e)
        If Cap Then
            Parent.Location = MousePosition - MouseP
        End If
    End Sub
    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseUp(e) : Cap = False
    End Sub
    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()
        Dock = DockStyle.Fill
        Parent.FindForm().FormBorderStyle = FormBorderStyle.None
    End Sub
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim B As Bitmap = New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)
        MyBase.OnPaint(e)

        G.Clear(BackColor)
        G.DrawLine(New Pen(_AccentColor, 2), New Point(0, 30), New Point(Width, 30))
        G.DrawString(Text, Font, New SolidBrush(ForeColor), New Rectangle(8, 6, Width - 1, Height - 1), StringFormat.GenericDefault)
        G.DrawLine(New Pen(_AccentColor, 3), New Point(8, 27), New Point(8 + G.MeasureString(Text, Font).Width, 27))

        G.DrawRectangle(New Pen(Color.FromArgb(100, 100, 100)), New Rectangle(0, 0, Width - 1, Height - 1))

        e.Graphics.DrawImage(B, New Point(0, 0))
        G.Dispose() : B.Dispose()
    End Sub
    Protected Sub OnAccentColorChanged() Handles Me.AccentColorChanged
        Invalidate()
    End Sub
    Protected Overrides Sub OnTextChanged(e As EventArgs)
        MyBase.OnTextChanged(e)
        Invalidate()
    End Sub
    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Invalidate()
    End Sub
#End Region

End Class

Public Class ThirteenControlBox : Inherits Control
    Enum ColorSchemes
        Light
        Dark
    End Enum
    Event ColorSchemeChanged()
    Private _ColorScheme As ColorSchemes
    Public Property ColorScheme() As ColorSchemes
        Get
            Return _ColorScheme
        End Get
        Set(ByVal value As ColorSchemes)
            _ColorScheme = value
            RaiseEvent ColorSchemeChanged()
        End Set
    End Property
    Protected Sub OnColorSchemeChanged() Handles Me.ColorSchemeChanged
        Invalidate()
        Select Case ColorScheme
            Case ColorSchemes.Dark
                BackColor = Color.FromArgb(50, 50, 50)
                ForeColor = Color.White
            Case ColorSchemes.Light
                BackColor = Color.White
                ForeColor = Color.Black
        End Select
    End Sub
    Private _AccentColor As Color
    Public Property AccentColor() As Color
        Get
            Return _AccentColor
        End Get
        Set(ByVal value As Color)
            _AccentColor = value
            Invalidate()
        End Set
    End Property

    Sub New()
        MyBase.New()
        DoubleBuffered = True
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.ResizeRedraw, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        ForeColor = Color.White
        BackColor = Color.FromArgb(50, 50, 50)
        AccentColor = Color.DodgerBlue
        ColorScheme = ColorSchemes.Dark
        Anchor = AnchorStyles.Top Or AnchorStyles.Right
    End Sub
    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        Size = New Size(100, 25)
    End Sub
    Enum ButtonHover
        Minimize
        Maximize
        Close
        None
    End Enum
    Dim ButtonState As ButtonHover = ButtonHover.None
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        Dim X As Integer = e.Location.X
        Dim Y As Integer = e.Location.Y
        If Y > 0 AndAlso Y < (Height - 2) Then
            If X > 0 AndAlso X < 34 Then
                ButtonState = ButtonHover.Minimize
            ElseIf X > 33 AndAlso X < 65 Then
                ButtonState = ButtonHover.Maximize
            ElseIf X > 64 AndAlso X < Width Then
                ButtonState = ButtonHover.Close
            Else
                ButtonState = ButtonHover.None
            End If
        Else
            ButtonState = ButtonHover.None
        End If
        Invalidate()
    End Sub
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim B As New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)
        MyBase.OnPaint(e)

        G.Clear(BackColor)
        Select Case ButtonState
            Case ButtonHover.None

            Case ButtonHover.Minimize
                G.FillRectangle(New SolidBrush(_AccentColor), New Rectangle(3, 0, 30, Height))
            Case ButtonHover.Maximize
                G.FillRectangle(New SolidBrush(_AccentColor), New Rectangle(34, 0, 30, Height))
            Case ButtonHover.Close
                G.FillRectangle(New SolidBrush(_AccentColor), New Rectangle(65, 0, 35, Height))
        End Select

        Dim ButtonFont As New Font("Marlett", 9.75F)
        'Close
        G.DrawString("r", ButtonFont, New SolidBrush(Color.FromArgb(200, 200, 200)), New Point(Width - 16, 7), New StringFormat With {.Alignment = StringAlignment.Center})
        'Maximize
        Select Case Parent.FindForm().WindowState
            Case FormWindowState.Maximized
                G.DrawString("2", ButtonFont, New SolidBrush(Color.FromArgb(200, 200, 200)), New Point(51, 7), New StringFormat With {.Alignment = StringAlignment.Center})
            Case FormWindowState.Normal
                G.DrawString("1", ButtonFont, New SolidBrush(Color.FromArgb(200, 200, 200)), New Point(51, 7), New StringFormat With {.Alignment = StringAlignment.Center})
        End Select
        'Minimize
        G.DrawString("0", ButtonFont, New SolidBrush(Color.FromArgb(200, 200, 200)), New Point(20, 7), New StringFormat With {.Alignment = StringAlignment.Center})


        e.Graphics.DrawImage(B, New Point(0, 0))
        G.Dispose() : B.Dispose()
    End Sub
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        Select Case ButtonState
            Case ButtonHover.Close
                Parent.FindForm().Close()
            Case ButtonHover.Minimize
                Parent.FindForm().WindowState = FormWindowState.Minimized
            Case ButtonHover.Maximize
                If Parent.FindForm().WindowState = FormWindowState.Normal Then
                    Parent.FindForm().WindowState = FormWindowState.Maximized
                Else
                    Parent.FindForm().WindowState = FormWindowState.Normal
                End If

        End Select
    End Sub
    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        ButtonState = ButtonHover.None : Invalidate()
    End Sub
End Class
Public Class ThirteenButton : Inherits Button
    Enum MouseState
        None
        Over
        Down
    End Enum
    Enum ColorSchemes
        Light
        Dark
    End Enum
    Private _ColorScheme As ColorSchemes
    Public Property ColorScheme() As ColorSchemes
        Get
            Return _ColorScheme
        End Get
        Set(ByVal value As ColorSchemes)
            _ColorScheme = value
            Invalidate()
        End Set
    End Property

    Dim State As MouseState = MouseState.None
    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)

        State = MouseState.Over : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseState.Down : Invalidate()
    End Sub
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over : Invalidate()
    End Sub

    Private _AccentColor As Color
    Public Property AccentColor() As Color
        Get
            Return _AccentColor
        End Get
        Set(ByVal value As Color)
            _AccentColor = value
            OnAccentColorChanged()
        End Set
    End Property

    Event AccentColorChanged()

    Sub New()
        MyBase.New()
        Font = New Font("Segoe UI Semilight", 9.75F)
        ForeColor = Color.White
        BackColor = Color.FromArgb(50, 50, 50)
        AccentColor = Color.DodgerBlue
        ColorScheme = ColorSchemes.Dark
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim B As New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)
        MyBase.OnPaint(e)
        Dim BGColor As Color
        Select Case ColorScheme
            Case ColorSchemes.Dark
                BGColor = Color.FromArgb(50, 50, 50)
            Case ColorSchemes.Light
                BGColor = Color.White
        End Select

        Select Case State
            Case MouseState.None
                G.Clear(BGColor)
            Case MouseState.Over
                G.Clear(AccentColor)
            Case MouseState.Down
                G.Clear(AccentColor)
                G.FillRectangle(New SolidBrush(Color.FromArgb(50, Color.Black)), New Rectangle(0, 0, Width - 1, Height - 1))
        End Select


        G.DrawRectangle(New Pen(Color.FromArgb(100, 100, 100)), New Rectangle(0, 0, Width - 1, Height - 1))

        Dim ButtonString As New StringFormat() With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center}
        Select Case ColorScheme
            Case ColorSchemes.Dark
                G.DrawString(Text, Font, Brushes.White, New Rectangle(0, 0, Width - 1, Height - 1), ButtonString)
            Case ColorSchemes.Light
                G.DrawString(Text, Font, Brushes.Black, New Rectangle(0, 0, Width - 1, Height - 1), ButtonString)
        End Select

        e.Graphics.DrawImage(B, New Point(0, 0))
        G.Dispose() : B.Dispose()
    End Sub
    Protected Sub OnAccentColorChanged() Handles Me.AccentColorChanged
        Invalidate()
    End Sub
End Class

Public Class ThirteenTextBox : Inherits TextBox
    Enum ColorSchemes
        Light
        Dark
    End Enum
    Event ColorSchemeChanged()
    Private _ColorScheme As ColorSchemes
    Public Property ColorScheme() As ColorSchemes
        Get
            Return _ColorScheme
        End Get
        Set(ByVal value As ColorSchemes)
            _ColorScheme = value
            RaiseEvent ColorSchemeChanged()
        End Set
    End Property

    Sub New()
        BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Font = New Font("Segoe UI Semilight", 9.75F)
        BackColor = Color.FromArgb(35, 35, 35)
        ForeColor = Color.White
        ColorScheme = ColorSchemes.Dark
    End Sub

    Protected Sub OnColorSchemeChanged() Handles Me.ColorSchemeChanged
        Invalidate()
        Select Case ColorScheme
            Case ColorSchemes.Dark
                BackColor = Color.FromArgb(35, 35, 35)
                ForeColor = Color.White
            Case ColorSchemes.Light
                BackColor = Color.White
                ForeColor = Color.Black
        End Select
    End Sub
End Class

Public Class ThirteenTabControl : Inherits TabControl
    Private _AccentColor As Color
    Public Property AccentColor() As Color
        Get
            Return _AccentColor
        End Get
        Set(ByVal value As Color)
            _AccentColor = value
            Invalidate()
        End Set
    End Property

    Private MainColor As Color
    Private ClearColor As Color
    Enum ColorSchemes
        Light
        Dark
    End Enum
    Event ColorSchemeChanged()
    Private _ColorScheme As ColorSchemes
    Public Property ColorScheme() As ColorSchemes
        Get
            Return _ColorScheme
        End Get
        Set(ByVal value As ColorSchemes)
            _ColorScheme = value
            RaiseEvent ColorSchemeChanged()
        End Set
    End Property
    Protected Sub OnColorSchemeChanged() Handles Me.ColorSchemeChanged
        Invalidate()
        Select Case ColorScheme
            Case ColorSchemes.Dark
                ClearColor = Color.FromArgb(50, 50, 50)
                MainColor = Color.FromArgb(35, 35, 35)
                ForeColor = Color.White
            Case ColorSchemes.Light
                ClearColor = Color.White
                MainColor = Color.FromArgb(200, 200, 200)
                ForeColor = Color.Black
        End Select
    End Sub
    Sub New()

        MyBase.New()
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.ResizeRedraw, True)
        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        Dim toggle As Integer

        toggle = toggle + 1

        BackColor = Color.FromArgb(50, 50, 50)
        ForeColor = Color.White
        AccentColor = Color.DodgerBlue
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim B As New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)
        MyBase.OnPaint(e)

        Try : SelectedTab.BackColor = MainColor : Catch : End Try
        G.Clear(ClearColor)

        For i As Integer = 0 To TabPages.Count - 1
            'If Not i = SelectedIndex Then
            Dim TabRect As New Rectangle(GetTabRect(i).X, GetTabRect(i).Y + 3, GetTabRect(i).Width + 2, GetTabRect(i).Height)
            G.FillRectangle(New SolidBrush(MainColor), TabRect)
            G.DrawString(TabPages(i).Text, New Font("Segoe UI Semilight", 9.75F), New SolidBrush(ForeColor), TabRect, New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Center})
            'End If
        Next

        G.FillRectangle(New SolidBrush(MainColor), 0, ItemSize.Height, Width, Height)

        If Not SelectedIndex = -1 Then
            Dim TabRect As New Rectangle(GetTabRect(SelectedIndex).X - 2, GetTabRect(SelectedIndex).Y, GetTabRect(SelectedIndex).Width + 4, GetTabRect(SelectedIndex).Height)
            G.FillRectangle(New SolidBrush(AccentColor), TabRect)
            G.DrawString(TabPages(SelectedIndex).Text, New Font("Segoe UI Semilight", 9.75F), New SolidBrush(ForeColor), TabRect, New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
        End If

        e.Graphics.DrawImage(B, New Point(0, 0))
        G.Dispose() : B.Dispose()
    End Sub
End Class

Public Class ThirteenComboBox : Inherits ComboBox
#Region " Control Help - Properties & Flicker Control "
    Enum ColorSchemes
        Light
        Dark
    End Enum
    Private _ColorScheme As ColorSchemes
    Public Property ColorScheme() As ColorSchemes
        Get
            Return _ColorScheme
        End Get
        Set(ByVal value As ColorSchemes)
            _ColorScheme = value
            Invalidate()
        End Set
    End Property

    Private _AccentColor As Color
    Public Property AccentColor() As Color
        Get
            Return _AccentColor
        End Get
        Set(ByVal value As Color)
            _AccentColor = value
            Invalidate()
        End Set
    End Property

    Private _StartIndex As Integer = 0
    Private Property StartIndex As Integer
        Get
            Return _StartIndex
        End Get
        Set(ByVal value As Integer)
            _StartIndex = value
            Try
                MyBase.SelectedIndex = value
            Catch
            End Try
            Invalidate()
        End Set
    End Property
    Sub ReplaceItem(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles Me.DrawItem
        e.DrawBackground()
        Try
            If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
                e.Graphics.FillRectangle(New SolidBrush(_AccentColor), e.Bounds)
            Else
                Select Case ColorScheme
                    Case ColorSchemes.Dark
                        e.Graphics.FillRectangle(New SolidBrush(Color.FromArgb(35, 35, 35)), e.Bounds)
                    Case ColorSchemes.Light
                        e.Graphics.FillRectangle(New SolidBrush(Color.White), e.Bounds)
                End Select
            End If
            Select Case ColorScheme
                Case ColorSchemes.Dark
                    e.Graphics.DrawString(MyBase.GetItemText(MyBase.Items(e.Index)), e.Font, Brushes.White, e.Bounds)
                Case ColorSchemes.Light
                    e.Graphics.DrawString(MyBase.GetItemText(MyBase.Items(e.Index)), e.Font, Brushes.Black, e.Bounds)
            End Select
        Catch
        End Try
    End Sub
    Protected Sub DrawTriangle(ByVal Clr As Color, ByVal FirstPoint As Point, ByVal SecondPoint As Point, ByVal ThirdPoint As Point, ByVal G As Graphics)
        Dim points As New List(Of Point)()
        points.Add(FirstPoint)
        points.Add(SecondPoint)
        points.Add(ThirdPoint)
        G.FillPolygon(New SolidBrush(Clr), points.ToArray())
    End Sub

#End Region

    Sub New()
        MyBase.New()
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.ResizeRedraw, True)
        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.DoubleBuffer, True)
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        BackColor = Color.FromArgb(50, 50, 50)
        ForeColor = Color.White
        AccentColor = Color.DodgerBlue
        ColorScheme = ColorSchemes.Dark
        DropDownStyle = ComboBoxStyle.DropDownList
        Font = New Font("Segoe UI Semilight", 9.75F)
        StartIndex = 0
        DoubleBuffered = True
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        Dim B As New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)
        Dim Curve As Integer = 2

        G.SmoothingMode = SmoothingMode.HighQuality

        Select Case ColorScheme
            Case ColorSchemes.Dark
                G.Clear(Color.FromArgb(50, 50, 50))
                G.DrawLine(New Pen(Color.White, 2), New Point(Width - 18, 10), New Point(Width - 14, 14))
                G.DrawLine(New Pen(Color.White, 2), New Point(Width - 14, 14), New Point(Width - 10, 10))
                G.DrawLine(New Pen(Color.White), New Point(Width - 14, 15), New Point(Width - 14, 14))
            Case ColorSchemes.Light
                G.Clear(Color.White)
                G.DrawLine(New Pen(Color.FromArgb(100, 100, 100), 2), New Point(Width - 18, 10), New Point(Width - 14, 14))
                G.DrawLine(New Pen(Color.FromArgb(100, 100, 100), 2), New Point(Width - 14, 14), New Point(Width - 10, 10))
                G.DrawLine(New Pen(Color.FromArgb(100, 100, 100)), New Point(Width - 14, 15), New Point(Width - 14, 14))
        End Select
        G.DrawRectangle(New Pen(Color.FromArgb(100, 100, 100)), New Rectangle(0, 0, Width - 1, Height - 1))


        Try
            Select Case ColorScheme
                Case ColorSchemes.Dark
                    G.DrawString(Text, Font, Brushes.White, New Rectangle(7, 0, Width - 1, Height - 1), New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Near})
                Case ColorSchemes.Light
                    G.DrawString(Text, Font, Brushes.Black, New Rectangle(7, 0, Width - 1, Height - 1), New StringFormat With {.LineAlignment = StringAlignment.Center, .Alignment = StringAlignment.Near})
            End Select
        Catch
        End Try

        e.Graphics.DrawImage(B.Clone(), 0, 0)
        G.Dispose() : B.Dispose()
    End Sub
End Class

<DefaultEvent("CheckedChanged")> Public Class ThirteenRadioButton : Inherits Control

#Region " Control Help - MouseState & Flicker Control"
    Enum MouseState
        None
        Over
        Down
    End Enum
    Enum ColorSchemes
        Dark
        Light
    End Enum

    Private State As MouseState = MouseState.None
    Private _ColorScheme As ColorSchemes
    Public Property ColorScheme() As ColorSchemes
        Get
            Return _ColorScheme
        End Get
        Set(ByVal value As ColorSchemes)
            _ColorScheme = value
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnMouseEnter(ByVal e As System.EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over
        Invalidate()
    End Sub
    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseState.Down
        Invalidate()
    End Sub
    Protected Overrides Sub OnMouseLeave(ByVal e As System.EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None
        Invalidate()
    End Sub
    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over
        Invalidate()
    End Sub
    Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
        MyBase.OnResize(e)
        Height = 18
    End Sub
    Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
        MyBase.OnTextChanged(e)
        Width = CreateGraphics().MeasureString(Text, Font).Width + (2 * 3.5) + (Height * 2)
        Invalidate()
    End Sub
    Private _Checked As Boolean
    Property Checked() As Boolean
        Get
            Return _Checked
        End Get
        Set(ByVal value As Boolean)
            _Checked = value
            InvalidateControls()
            RaiseEvent CheckedChanged(Me)
            Invalidate()
        End Set
    End Property
    Protected Overrides Sub OnClick(ByVal e As EventArgs)
        If Not _Checked Then Checked = True
        MyBase.OnClick(e)
    End Sub
    Event CheckedChanged(ByVal sender As Object)
    Protected Overrides Sub OnCreateControl()
        MyBase.OnCreateControl()
        InvalidateControls()
    End Sub
    Private Sub InvalidateControls()
        If Not IsHandleCreated OrElse Not _Checked Then Return

        For Each C As Control In Parent.Controls
            If C IsNot Me AndAlso TypeOf C Is ThirteenRadioButton Then
                DirectCast(C, ThirteenRadioButton).Checked = False
            End If
        Next
    End Sub
#End Region

    Sub New()
        MyBase.New()
        SetStyle(ControlStyles.UserPaint Or ControlStyles.SupportsTransparentBackColor, True)
        ColorScheme = ColorSchemes.Dark
        BackColor = Color.FromArgb(50, 50, 50)
        ForeColor = Color.White
        DoubleBuffered = True
        Size = New Size(177, 18)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim B As New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)
        Dim RadioBtnRectangle = New Rectangle(0, 0, Height - 1, Height - 1)

        G.SmoothingMode = SmoothingMode.HighQuality
        G.Clear(BackColor)

        Select Case ColorScheme
            Case ColorSchemes.Light
                G.FillEllipse(New SolidBrush(Color.FromArgb(215, Color.Black)), RadioBtnRectangle)
            Case ColorSchemes.Dark
                G.FillEllipse(New SolidBrush(Color.FromArgb(215, Color.White)), RadioBtnRectangle)
        End Select

        If Checked Then
            Select Case ColorScheme
                Case ColorSchemes.Dark
                    G.FillEllipse(New SolidBrush(Color.Black), New Rectangle(4, 4, Height - 9, Height - 9))
                Case ColorSchemes.Light
                    G.FillEllipse(New SolidBrush(Color.White), New Rectangle(4, 4, Height - 9, Height - 9))
            End Select
        End If

        G.DrawString(Text, Font, New SolidBrush(ForeColor), New Point(22, 1), New StringFormat With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Near})

        e.Graphics.DrawImage(B.Clone(), 0, 0)
        G.Dispose() : B.Dispose()
    End Sub

End Class
<DefaultEvent("CheckedChanged")> Public Class ThirteenCheckBox : Inherits Control

#Region " Control Help - MouseState & Flicker Control"
    Enum ColorSchemes
        Light
        Dark
    End Enum
    Enum MouseState
        None
        Over
        Down
    End Enum
    Private _ColorScheme As ColorSchemes
    Public Property ColorScheme() As ColorSchemes
        Get
            Return _ColorScheme
        End Get
        Set(ByVal value As ColorSchemes)
            _ColorScheme = value
            Invalidate()
        End Set
    End Property

    Private State As MouseState = MouseState.None
    Protected Overrides Sub OnMouseEnter(ByVal e As System.EventArgs)
        MyBase.OnMouseEnter(e)
        State = MouseState.Over
        Invalidate()
    End Sub
    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseDown(e)
        State = MouseState.Down
        Invalidate()
    End Sub
    Protected Overrides Sub OnMouseLeave(ByVal e As System.EventArgs)
        MyBase.OnMouseLeave(e)
        State = MouseState.None
        Invalidate()
    End Sub
    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        MyBase.OnMouseUp(e)
        State = MouseState.Over
        Invalidate()
    End Sub
    Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
        MyBase.OnTextChanged(e)
        Width = CreateGraphics().MeasureString(Text, Font).Width + (2 * 3) + Height
        Invalidate()
    End Sub
    Private _Checked As Boolean
    Property Checked() As Boolean
        Get
            Return _Checked
        End Get
        Set(ByVal value As Boolean)
            _Checked = value
            Invalidate()
        End Set
    End Property
    Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
        MyBase.OnResize(e)
        Height = 17
    End Sub
    Protected Overrides Sub OnClick(ByVal e As System.EventArgs)
        _Checked = Not _Checked
        RaiseEvent CheckedChanged(Me)
        MyBase.OnClick(e)
    End Sub
    Event CheckedChanged(ByVal sender As Object)
#End Region

    Sub New()
        MyBase.New()
        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)

        ColorScheme = ColorSchemes.Dark
        BackColor = Color.FromArgb(50, 50, 50)
        ForeColor = Color.White
        Size = New Size(147, 17)
        DoubleBuffered = True
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim B As New Bitmap(Width, Height)
        Dim G As Graphics = Graphics.FromImage(B)
        G.SmoothingMode = SmoothingMode.HighQuality
        Dim CheckBoxRectangle As New Rectangle(0, 0, Height - 1, Height - 1)

        G.Clear(BackColor)

        Select Case ColorScheme
            Case ColorSchemes.Light
                G.FillRectangle(New SolidBrush(Color.FromArgb(215, Color.Black)), CheckBoxRectangle)
            Case ColorSchemes.Dark
                G.FillRectangle(New SolidBrush(Color.FromArgb(215, Color.White)), CheckBoxRectangle)
        End Select

        If Checked Then
            Dim chkPoly As Rectangle = New Rectangle(CheckBoxRectangle.X + CheckBoxRectangle.Width / 4, CheckBoxRectangle.Y + CheckBoxRectangle.Height / 4, CheckBoxRectangle.Width \ 2, CheckBoxRectangle.Height \ 2)
            Dim Poly() As Point = {New Point(chkPoly.X, chkPoly.Y + chkPoly.Height \ 2), New Point(chkPoly.X + chkPoly.Width \ 2, chkPoly.Y + chkPoly.Height), New Point(chkPoly.X + chkPoly.Width, chkPoly.Y)}
            Select Case ColorScheme
                Case ColorSchemes.Dark
                    For i = 0 To Poly.Length - 2 : G.DrawLine(New Pen(Color.Black, 2), Poly(i), Poly(i + 1)) : Next
                Case ColorSchemes.Light
                    For i = 0 To Poly.Length - 2 : G.DrawLine(New Pen(Color.White, 2), Poly(i), Poly(i + 1)) : Next
            End Select
        End If

        G.DrawString(Text, Font, New SolidBrush(ForeColor), New Point(18, 2), New StringFormat With {.Alignment = StringAlignment.Near, .LineAlignment = StringAlignment.Near})

        e.Graphics.DrawImage(B.Clone(), 0, 0)
        G.Dispose() : B.Dispose()

    End Sub

End Class