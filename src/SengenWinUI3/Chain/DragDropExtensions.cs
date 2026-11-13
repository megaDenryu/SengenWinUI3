using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>ドラッグ&ドロップでデータを受けるチェーン。どれも受けた部品をそのままの型で返す。</summary>
public static class DragDropExtensions
{
    /// <summary>部品にデータをドロップできるかを決める。ドロップを受けるには、true にしたうえで <see cref="OnDragOver{T}"/> で操作を許す。</summary>
    public static T AllowDrop<T>(this T node, bool allowDrop) where T : Node
    {
        node.ThrowIfDestroyed();
        WinUI.FrameworkElement 本物 = node.Element;
        if (本物.AllowDrop != allowDrop)
        {
            本物.AllowDrop = allowDrop;
        }
        return node;
    }

    /// <summary>ドラッグされているデータが部品の上にあるあいだ、処理を呼ぶ。<see cref="DragInput.SetAcceptedOperation"/> で許した操作だけが、ドロップできる。</summary>
    public static T OnDragOver<T>(this T node, Action<DragInput> handler) where T : Node =>
        node.WireEvent(nameof(OnDragOver), NodeDragEvents.DragOver, 出来事 => handler(new DragInput(出来事)));

    /// <summary>データが部品の上にドロップされたときに処理を呼ぶ。ドロップを受ける操作を <see cref="OnDragOver{T}"/> で許してあるときだけ届く。</summary>
    public static T OnDrop<T>(this T node, Action<DragInput> handler) where T : Node =>
        node.WireEvent(nameof(OnDrop), NodeDragEvents.Drop, 出来事 => handler(new DragInput(出来事)));
}
