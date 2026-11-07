namespace SengenWinUI3;

/// <summary>部品のフォーカスの出来事。部品の中の部品にフォーカスが入ったときも、親の部品の出来事として届く。同じ出来事への2回目の登録は例外になる。</summary>
public static class NodeFocusEventExtensions
{
    /// <summary>部品かその中の部品にフォーカスが入ったときに処理を呼ぶ。</summary>
    public static T OnGotFocus<T>(this T node, Action handler) where T : Node =>
        node.WireEvent(nameof(OnGotFocus), NodeEvents.GotFocus, handler);

    /// <summary>部品かその中の部品からフォーカスが外れたときに処理を呼ぶ。</summary>
    public static T OnLostFocus<T>(this T node, Action handler) where T : Node =>
        node.WireEvent(nameof(OnLostFocus), NodeEvents.LostFocus, handler);
}
