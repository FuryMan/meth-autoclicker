
Imports System.Linq
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Public Class Rainbow
        Private Sub New()
        End Sub
        ' Return a rainbow brush.
        ' The calling code should dispose of the brush.
        Public Shared Function RainbowBrush(point1 As Point, point2 As Point) As LinearGradientBrush
            Dim rainbow_brush As New LinearGradientBrush(point1, point2, Color.Red, Color.Red)

            ' Define the colors along the gradient.
            Dim color_blend As New ColorBlend()
            color_blend.Colors = New Color() {Color.Red, Color.Yellow, Color.Lime, Color.Aqua, Color.Blue, Color.Fuchsia,
                Color.Red}
            color_blend.Positions = New Single() {0, 1 / 6.0F, 2 / 6.0F, 3 / 6.0F, 4 / 6.0F, 5 / 6.0F,
                1}
            rainbow_brush.InterpolationColors = color_blend

            Return rainbow_brush
        End Function

        ' Map a color to a rainbow number between 0 and 1 on the
        ' Red-Yellow-Lime-Aqua-Blue-Fuchsia-Red rainbow.
        Public Shared Function ColorToRainbowNumber(clr As Color) As Single
            ' See which color is weakest.
            Dim r As Integer = clr.R
            Dim g As Integer = clr.G
            Dim b As Integer = clr.B
            If (r <= g) AndAlso (r <= b) Then
                ' Red is weakest. It's mostly blue and green.
                g -= r
                b -= r
                If g + b = 0 Then
                    Return 0
                End If
                Return (2 / 6.0F * g + 4 / 6.0F * b) / (g + b)
            ElseIf (g <= r) AndAlso (g <= b) Then
                ' Green is weakest. It's mostly red and blue.
                r -= g
                b -= g
                If r + b = 0 Then
                    Return 0
                End If
                Return (1.0F * r + 4 / 6.0F * b) / (r + b)
            Else
                ' Blue is weakest. It's mostly red and green.
                r -= b
                g -= b
                If r + g = 0 Then
                    Return 0
                End If
                Return (0F * r + 2 / 6.0F * g) / (r + g)
            End If
        End Function

        ' Map a rainbow number between 0 and 1 to a color on the
        ' Red-Yellow-Lime-Aqua-Blue-Fuchsia-Red rainbow.
        Public Shared Function RainbowNumberToColor(number As Single) As Color
            Dim r As Byte = 0, g As Byte = 0, b As Byte = 0
            If number < 1 / 6.0F Then
                ' Mostly red with some green.
                r = 255
                g = CByte(r * (number - 0) / (2 / 6.0F - number))
            ElseIf number < 2 / 6.0F Then
                ' Mostly green with some red.
                g = 255
                r = CByte(g * (2 / 6.0F - number) / (number - 0))
            ElseIf number < 3 / 6.0F Then
                ' Mostly green with some blue.
                g = 255
                b = CByte(g * (2 / 6.0F - number) / (number - 4 / 6.0F))
            ElseIf number < 4 / 6.0F Then
                ' Mostly blue with some green.
                b = 255
                g = CByte(b * (number - 4 / 6.0F) / (2 / 6.0F - number))
            ElseIf number < 5 / 6.0F Then
                ' Mostly blue with some red.
                b = 255
                r = CByte(b * (4 / 6.0F - number) / (number - 1.0F))
            Else
                ' Mostly red with some blue.
                r = 255
                b = CByte(r * (number - 1.0F) / (4 / 6.0F - number))
            End If
            Return Color.FromArgb(r, g, b)
        End Function
    End Class


'=======================================================
'Service provided by Telerik (www.telerik.com)
'Conversion powered by NRefactory.
'Twitter: @telerik
'Facebook: facebook.com/telerik
'=======================================================
