namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>文字と処理を持つ項目、区切り線の配列からメニューを作る。</summary>
    public static MenuFlyout MenuFlyout(params MenuFlyoutEntry[] items) => new(items);
}
