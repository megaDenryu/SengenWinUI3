namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>基準の左上からのずれ、外側を押して閉じるかを指定して作る。</summary>
    public static Popup Popup(Node content, Point offset, bool isLightDismissEnabled = false) =>
        new(content, new OffsetFromAnchor(offset), isLightDismissEnabled);

    /// <summary>基準の上へ、左端をそろえて出す Popup を作る。位置は WinUI が決めるので、ずれは指定できない。上に収まらないときの置き直しも WinUI に従う。</summary>
    public static Popup PopupAbove(Node content, bool isLightDismissEnabled = false) =>
        new(content, new AboveAnchor(), isLightDismissEnabled);
}
