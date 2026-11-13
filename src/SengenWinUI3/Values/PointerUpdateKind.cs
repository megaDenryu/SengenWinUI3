namespace SengenWinUI3;

/// <summary>ポインターの出来事を起こしたボタンの変化。名前は WinUI の PointerUpdateKind にそろえる。ここに無い変化は <see cref="Other"/> になる。</summary>
public enum PointerUpdateKind
{
    Other,
    LeftButtonPressed,
    LeftButtonReleased,
    RightButtonPressed,
    RightButtonReleased,
    MiddleButtonPressed,
    MiddleButtonReleased,
    XButton1Pressed,
    XButton1Released,
    XButton2Pressed,
    XButton2Released,
}
