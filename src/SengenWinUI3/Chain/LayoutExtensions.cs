namespace SengenWinUI3;

/// <summary>配置の計算を促すチェーン。</summary>
public static class LayoutExtensions
{
    /// <summary>
    /// 配置の計算をその場で終わらせる。木へ足した直後の部品へフォーカスを移すときなど、配置が済んでいないと成り立たない操作の前に呼ぶ。
    /// 配置を急いで進めるので、繰り返す処理の中で呼ばない。
    /// </summary>
    public static T UpdateLayout<T>(this T node) where T : Node
    {
        node.ThrowIfDestroyed();
        node.Element.UpdateLayout();
        return node;
    }
}
