Imports System.Runtime.InteropServices
Imports System.Threading

Public Class Form1

    <DllImport("gdi32.dll", SetLastError:=True)>
    Public Shared Function BitBlt(hdcDest As IntPtr, nXDest As Integer, nYDest As Integer, nWidth As Integer, nHeight As Integer, hdcSrc As IntPtr, nXSrc As Integer, nYSrc As Integer, dwRop As UInteger) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Public Shared Function GetDC(hWnd As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Public Shared Function GetSystemMetrics(nIndex As Integer) As Integer
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Public Shared Function ReleaseDC(hWnd As IntPtr, hDC As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Public Shared Function GetDesktopWindow() As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Public Shared Function GetWindowDC(hWnd As IntPtr) As IntPtr
    End Function

    <DllImport("gdi32.dll", SetLastError:=True)>
    Public Shared Function StretchBlt(hdcDest As IntPtr, nXOriginDest As Integer, nYOriginDest As Integer, nWidthDest As Integer, nHeightDest As Integer, hdcSrc As IntPtr, nXOriginSrc As Integer, nYOriginSrc As Integer, nWidthSrc As Integer, nHeightSrc As Integer, dwRop As UInteger) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Public Shared Function GetWindowRect(hWnd As IntPtr, ByRef lpRect As RECT) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Public Shared Function GetCursorPos(ByRef lpPoint As POINT) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Public Shared Function DrawIcon(ByVal hDC As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal hIcon As IntPtr) As Boolean
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Public Shared Function LoadIcon(ByVal hInstance As IntPtr, ByVal lpIconName As IntPtr) As IntPtr
    End Function

    <StructLayout(LayoutKind.Sequential)>
    Public Structure RECT
        Public Left As Integer
        Public Top As Integer
        Public Right As Integer
        Public Bottom As Integer
    End Structure

    Const SRCCOPY As UInteger = &HCC0020 'hack(?)

    Dim rand As New Random()

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Dim t1 As New Thread(AddressOf Melter)
        'Dim t2 As New Thread(AddressOf Melter2)
        'Dim t3 As New Thread(AddressOf PayloadPIP1)
        Dim t4 As New Thread(AddressOf payloadDrawErrors)
        Dim t4v2 As New Thread(AddressOf payloadDrawErrors2)

        't1.Start()
        't2.Start()
        't3.Start()
        t4.Start()
        t4v2.Start()
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

        While True
            StretchBlt(hdc, 50, 50, rekt.Right - 100, rekt.Bottom - 100, hdc, 0, 0, rekt.Right, rekt.Bottom, SRCCOPY)
            Thread.Sleep(1)
        End While

        ReleaseDC(hwnd, hdc)

    End Function

    Public Function PayloadPIP2()
        Dim hwnd As IntPtr = GetDesktopWindow()
        Dim hdc As IntPtr = GetWindowDC(hwnd)
        Dim i As Integer = 0
        Dim rekt As RECT
        GetWindowRect(hwnd, rekt)

        While True
            StretchBlt(hdc, 50, 50, rekt.Right - i, rekt.Bottom - i, hdc, 0, 0, rekt.Right, rekt.Bottom, SRCCOPY)
            i = i + 1
            Thread.Sleep(1)
        End While

        ReleaseDC(hwnd, hdc)

    End Function

    Public Function PayloadPIP3()
        Dim hwnd As IntPtr = GetDesktopWindow()
        Dim hdc As IntPtr = GetWindowDC(hwnd)

        Dim rekt As RECT
        GetWindowRect(hwnd, rekt)
        Dim i As Integer = 0
        While True
            StretchBlt(hdc, 50, 50, rekt.Right - (Math.Sin(i / 10) * 100), rekt.Bottom - (Math.Sin(i / 100) * 100), hdc, 0, 0, rekt.Right, rekt.Bottom, SRCCOPY)
            i = i + 1
        End While

        ReleaseDC(hwnd, hdc)

    End Function

    Const SM_CXICON As Integer = 11
    Const SM_CYICON As Integer = 12
    Const IDI_ERROR As Integer = 32513
    Const IDI_WARNING As Integer = 32515
    Const IDI_WINLOGO As Integer = 32517
    Const IDI_SHIELD As Integer = 32518

    Public Function payloadDrawErrors()
        Dim gyatt As Integer = 1

        Dim ix As Integer = GetSystemMetrics(SM_CXICON) / 2
        Dim iy As Integer = GetSystemMetrics(SM_CYICON) / 2

        Dim hwnd As IntPtr = GetDesktopWindow()
        Dim hdc As IntPtr = GetWindowDC(hwnd)

        While True
            'GetCursorPos(cursor)

            'DrawIcon(hdc, cursor.X - ix, cursor.Y - iy, LoadIcon(IntPtr.Zero, CType(IDI_ERROR, IntPtr)))

            Dim scrw As Integer = GetSystemMetrics(0) ' SM_CXSCREEN
            Dim scrh As Integer = GetSystemMetrics(1) ' SM_CYSCREEN

            'If rand.Next(5) = 1 Then
            DrawIcon(hdc, Math.Abs(Math.Sin(gyatt) * scrw + (Math.Sin(gyatt) * gyatt)), Math.Abs(Math.Cos(gyatt) * scrh + (Math.Sin(gyatt) * gyatt)), LoadIcon(IntPtr.Zero, CType(IDI_WARNING, IntPtr)))
            'ElseIf rand.Next(5) = 2 Then
            'DrawIcon(hdc, Math.Abs(Math.Cos(gyatt) * 1000 + (Math.Sin(gyatt) * 10)), Math.Abs(Math.Sin(gyatt) * 1000 + (Math.Sin(gyatt) * 10)), LoadIcon(IntPtr.Zero, CType(IDI_WINLOGO, IntPtr)))
            'ElseIf rand.Next(5) = 3 Then
            DrawIcon(hdc, Math.Abs(Math.Sin(gyatt) * scrw + (Math.Sin(gyatt) * gyatt)), Math.Abs(Math.Sin(gyatt) * scrh + (Math.Sin(gyatt) * gyatt)), LoadIcon(IntPtr.Zero, CType(IDI_SHIELD, IntPtr)))
            'End If

            'Thread.Sleep(1)
            gyatt = gyatt + 1
        End While

        ReleaseDC(hwnd, hdc)
    End Function

    Public Function payloadDrawErrors2()
        Dim gyatt As Integer = 1

        Dim ix As Integer = GetSystemMetrics(SM_CXICON) / 2
        Dim iy As Integer = GetSystemMetrics(SM_CYICON) / 2

        Dim hwnd As IntPtr = GetDesktopWindow()
        Dim hdc As IntPtr = GetWindowDC(hwnd)
        While True
            'GetCursorPos(cursor)

            'DrawIcon(hdc, cursor.X - ix, cursor.Y - iy, LoadIcon(IntPtr.Zero, CType(IDI_ERROR, IntPtr)))

            Dim scrw As Integer = GetSystemMetrics(0) ' SM_CXSCREEN
            Dim scrh As Integer = GetSystemMetrics(1) ' SM_CYSCREEN

            'If rand.Next(5) = 1 Then
            DrawIcon(hdc, Math.Abs(Math.Sin(gyatt) * scrw + (Math.Sin(gyatt) * gyatt)), Math.Abs(Math.Cos(gyatt) * scrh + (Math.Sin(gyatt) * gyatt)), LoadIcon(IntPtr.Zero, CType(IDI_ERROR, IntPtr)))
            'ElseIf rand.Next(5) = 2 Then
            'DrawIcon(hdc, Math.Abs(Math.Cos(gyatt) * 1000 + (Math.Sin(gyatt) * 10)), Math.Abs(Math.Sin(gyatt) * 1000 + (Math.Sin(gyatt) * 10)), LoadIcon(IntPtr.Zero, CType(IDI_WINLOGO, IntPtr)))
            'ElseIf rand.Next(5) = 3 Then
            DrawIcon(hdc, Math.Abs(Math.Sin(gyatt) * scrw + (Math.Sin(gyatt) * gyatt)), Math.Abs(Math.Sin(gyatt) * scrh + (Math.Sin(gyatt) * gyatt)), LoadIcon(IntPtr.Zero, CType(IDI_WINLOGO, IntPtr)))
            'End If

            'Thread.Sleep(1)
            gyatt = gyatt + 2
        End While

        ReleaseDC(hwnd, hdc)
    End Function

End Class