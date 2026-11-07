using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>部品の根に ThemeShadow と奥行きを設定するチェーン。</summary>
public static class ShadowExtensions
{
    /// <summary>横縦の位置を保ち、ThemeShadow と Translation の奥行きを設定する。繰り返し呼べる。</summary>
    public static T Shadow<T>(this T node, Px depth) where T : Node
    {
        node.ThrowIfDestroyed();
        float 奥行き = float.CreateChecked(depth.Value);
        WinUI.FrameworkElement 本物 = node.Element;
        if (本物.Shadow is not WinUI.Media.ThemeShadow)
        {
            本物.Shadow = new WinUI.Media.ThemeShadow();
        }
        System.Numerics.Vector3 位置 = 本物.Translation;
        if (位置.Z != 奥行き)
        {
            位置.Z = 奥行き;
            本物.Translation = 位置;
        }
        return node;
    }

    /// <summary>影を外し、横縦の位置を保って奥行きを 0 にする。繰り返し呼べる。</summary>
    public static T NoShadow<T>(this T node) where T : Node
    {
        node.ThrowIfDestroyed();
        WinUI.FrameworkElement 本物 = node.Element;
        本物.Shadow = null;
        System.Numerics.Vector3 位置 = 本物.Translation;
        if (位置.Z != 0)
        {
            位置.Z = 0;
            本物.Translation = 位置;
        }
        return node;
    }
}
