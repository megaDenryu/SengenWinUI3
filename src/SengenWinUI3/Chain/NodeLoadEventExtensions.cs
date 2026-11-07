namespace SengenWinUI3;

/// <summary>部品の読み込みと取り外しの出来事。同じ部品の同じ出来事への2回目の登録は例外になる。</summary>
public static class NodeLoadEventExtensions
{
    /// <summary>部品が木へ読み込まれるたびに処理を呼ぶ。登録は部品の破棄で解く。</summary>
    public static T OnLoaded<T>(this T node, Action handler) where T : Node =>
        node.WireEvent(nameof(OnLoaded), NodeEvents.Loaded, handler);

    /// <summary>部品が木から取り外されるたびに処理を呼ぶ。登録は部品の破棄で解く。</summary>
    public static T OnUnloaded<T>(this T node, Action handler) where T : Node =>
        node.WireEvent(nameof(OnUnloaded), NodeEvents.Unloaded, handler);
}
