Imports System.Drawing.Imaging

Public Class MyFadeEffect : Inherits Button

    Private ImgAtt As New ImageAttributes
    Private StartFade As Boolean, Alpha As Single = 0.0, SpeedFade As Single = 0.0
    Private ImgHover As Bitmap, GrHover As Graphics, _SetImage As Image

    Private Property SetImage() As Image
        Get
            Return _SetImage
        End Get
        Set(value As Image)
            _SetImage = value
            Invalidate()
        End Set
    End Property


    Sub New()
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.UserPaint, True)
        DoubleBuffered = True
        Size = New Size(150, 50)
    End Sub


    Private Sub DrawFadeEffect(G As Graphics)
        SpeedFade = 0.11

        Dim _Matrix As New ColorMatrix(New Single()() {
                                       New Single() {1, 0, 0, 0, 0},
                                       New Single() {0, 1, 0, 0, 0},
                                       New Single() {0, 0, 1, 0, 0},
                                       New Single() {0, 0, 0, Alpha, 0},
                                       New Single() {0, 0, 0, 0, 1}})
        ImgAtt.SetColorMatrix(_Matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap)

        ImgHover = New Bitmap(Width, Height)
        GrHover = Graphics.FromImage(ImgHover)

        Dim _Brush As New SolidBrush(Color.FromArgb(150, Color.Blue))

        GrHover.FillRectangle(_Brush, 0, 0, Width, Height)

        G.DrawImage(ImgHover, New Rectangle(0, 0, ImgHover.Width, ImgHover.Height),
                    0, 0, ImgHover.Width, ImgHover.Height, GraphicsUnit.Pixel, ImgAtt)

        ImgHover.Dispose()
        GrHover.Dispose()
    End Sub


    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        e.Graphics.DrawRectangle(Pens.Red, 0, 0, Width - 1, Height - 1)
        TextRenderer.DrawText(e.Graphics, Text, Font, ClientRectangle, ForeColor)

    End Sub

    Protected Overrides Sub OnPaintBackground(e As PaintEventArgs)
        MyBase.OnPaintBackground(e)
        DrawFadeEffect(e.Graphics)
    End Sub


    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)
        StartFade = True
        TmrFade.Enabled = True
        Invalidate()
    End Sub


    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        StartFade = False
        TmrFade.Enabled = True
        Invalidate()
    End Sub

    Private Sub TmrFade_Tick(sender As Object, e As EventArgs) Handles TmrFade.Tick
        If StartFade Then
            Alpha += SpeedFade
        Else
            Alpha -= SpeedFade
        End If

        SetImage = ImgHover

        If Alpha >= 1 Then
            Alpha = 1
            TmrFade.Enabled = False
        ElseIf Alpha <= 0 Then
            Alpha = 0
            TmrFade.Enabled = False
        End If

    End Sub

    Friend WithEvents TmrFade As New Timer With {.Enabled = False, .Interval = 1}
End Class
