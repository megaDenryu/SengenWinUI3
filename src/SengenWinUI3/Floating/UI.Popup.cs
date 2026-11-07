namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>基準の左上からのずれ、外側を押して閉じるかを指定して作る。</summary>
    public static Popup Popup(Node content, Point offset, bool isLightDismissEnabled = false) =>
        new(content, offset, isLightDismissEnabled);
}
