namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>基準の左上からの横と縦の位置、外側を押して閉じるかを指定して作る。</summary>
    public static Popup Popup(Node content, Px horizontalOffset, Px verticalOffset, bool isLightDismissEnabled = false) =>
        new(content, horizontalOffset, verticalOffset, isLightDismissEnabled);
}
