Imports System.Runtime.InteropServices

Class MeCab
    Implements IDisposable

    <DllImport("libmecab.dll", CallingConvention:=CallingConvention.Cdecl)>
    Public Shared Function mecab_new2(ByVal arg As String) As IntPtr
    End Function

    <DllImport("libmecab.dll", CallingConvention:=CallingConvention.Cdecl)>
    Public Shared Function mecab_sparse_tostr(ByVal m As IntPtr, ByVal str As String) As IntPtr
    End Function

    <DllImport("libmecab.dll", CallingConvention:=CallingConvention.Cdecl)>
    Public Shared Sub mecab_destroy(ByVal m As IntPtr)
    End Sub

    Private ptrMeCab As IntPtr

    Sub New()
        Me.New(String.Empty)
    End Sub

    Sub New(ByVal Arg As String)
        ptrMeCab = mecab_new2(Arg)
    End Sub

    Public Function Parse(ByVal [String] As String) As String
        Dim ptrResult As IntPtr = mecab_sparse_tostr(ptrMeCab, [String])
        Dim strResult As String = Marshal.PtrToStringAnsi(ptrResult)
        Return strResult
    End Function

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        mecab_destroy(ptrMeCab)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        Dispose()
    End Sub
End Class

Class CaboCha
    Implements IDisposable

    <DllImport("libcabocha.dll", CallingConvention:=CallingConvention.Cdecl)>
    Public Shared Function cabocha_new2(ByVal arg As String) As IntPtr
    End Function

    <DllImport("libcabocha.dll", CallingConvention:=CallingConvention.Cdecl)>
    Public Shared Function cabocha_sparse_tostr(ByVal m As IntPtr, ByVal str As String) As IntPtr
    End Function

    <DllImport("libcabocha.dll", CallingConvention:=CallingConvention.Cdecl)>
    Public Shared Sub cabocha_destroy(ByVal m As IntPtr)
    End Sub

    Private ptrCaboCha As IntPtr

    Sub New()
        Me.New(String.Empty)
    End Sub

    Sub New(ByVal arg As String)
        ptrCaboCha = cabocha_new2(arg)
    End Sub

    Public Function Parse(ByVal [String] As String) As String
        Dim ptrResult As IntPtr = cabocha_sparse_tostr(ptrCaboCha, [String])
        Dim strResult As String = Marshal.PtrToStringAnsi(ptrResult)
        Return strResult
    End Function

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        cabocha_destroy(ptrCaboCha)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        Dispose()
    End Sub
End Class

Public Class Class1
    Public Shared Sub Main(ByVal args() As String)

        Using m As New MeCab()
            Dim sentence As String = "太郎はこの本を二郎を見た女性に渡した。"
            Dim mecabOut As String = m.Parse(sentence)
            Dim lines() As String = mecabOut.Split(ControlChars.Lf)
            For Each line As String In lines
                Console.WriteLine(line)
            Next
        End Using

        Using c As New CaboCha("-f1 -I0 -O4")
            Dim sentence As String = "太郎は花子が読んでいる本を次郎に渡した"
            Dim cabochaOut As String = c.Parse(sentence)
            Dim lines() As String = cabochaOut.Split(ControlChars.Lf)
            For Each line As String In lines
                Console.WriteLine(line)
            Next
        End Using
    End Sub
End Class
