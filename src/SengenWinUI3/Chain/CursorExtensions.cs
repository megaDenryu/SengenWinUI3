using System.Runtime.CompilerServices;
using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>ポインターが部品の上にあるときのカーソル。</summary>
public static class CursorExtensions
{
    /// <summary>
    /// ポインターが部品の上にある間のカーソルを、OS のカーソルの形にする。繰り返し呼ぶと、前の形を新しい形で置き換える。
    /// 部品の子の部品が自分のカーソルを持っていれば、子の上では子のカーソルになる。
    /// </summary>
    public static T Cursor<T>(this T node, InputSystemCursorShape shape) where T : Node
    {
        node.ThrowIfDestroyed();
        Microsoft.UI.Input.InputSystemCursor カーソル = Microsoft.UI.Input.InputSystemCursor.Create(shape.ToWinUI());
        node.OwnDisposable(カーソル);
        カーソルを設定する(node.Element, カーソル);
        return node;
    }

    // 注意: WinUI の ProtectedCursor は UIElement の派生の中でしか設定できない。派生を作れない部品(Border・TextBlock など)にも当てるため、アクセス制御を越える UnsafeAccessor で設定の操作を直接呼ぶ。
    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "set_ProtectedCursor")]
    private static extern void カーソルを設定する(WinUI.UIElement 対象, Microsoft.UI.Input.InputCursor カーソル);
}
