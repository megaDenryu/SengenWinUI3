namespace SengenWinUI3;

/// <summary>ポインターの捕まえ。捕まえた部品は、ポインターが部品の外へ出ても動きと離しの出来事を受ける。</summary>
public static class PointerCaptureExtensions
{
    /// <summary>出来事を起こしたポインターを、この部品に捕まえる。捕まえられたら true を返す。</summary>
    public static bool CapturePointer(this Node node, PointerInput input)
    {
        node.ThrowIfDestroyed();
        return node.Element.CapturePointer(input.Pointer);
    }

    /// <summary>出来事を起こしたポインターの捕まえを、この部品から解く。</summary>
    public static void ReleasePointerCapture(this Node node, PointerInput input)
    {
        node.ThrowIfDestroyed();
        node.Element.ReleasePointerCapture(input.Pointer);
    }

    /// <summary>この部品が捕まえているポインターを全部解く。</summary>
    public static void ReleasePointerCaptures(this Node node)
    {
        node.ThrowIfDestroyed();
        node.Element.ReleasePointerCaptures();
    }
}
