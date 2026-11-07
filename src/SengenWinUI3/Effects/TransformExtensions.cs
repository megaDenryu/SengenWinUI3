using System.Numerics;
using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>配置を変えずに、部品の根の位置と拡大率を繰り返し設定するチェーン。</summary>
public static class TransformExtensions
{
    /// <summary>影の奥行きを保ち、配置位置からの横縦のずれを設定する。同じ値は設定し直さない。</summary>
    public static T Translation<T>(this T node, Point offset) where T : Node
    {
        node.ThrowIfDestroyed();
        WinUI.FrameworkElement 本物 = node.Element;
        Vector3 位置 = new(float.CreateChecked(offset.X.Value), float.CreateChecked(offset.Y.Value), 本物.Translation.Z);
        if (本物.Translation != 位置)
        {
            本物.Translation = 位置;
        }
        return node;
    }

    /// <summary>奥行き方向の拡大率を保ち、横縦の拡大率を設定する。拡大率は長さ同士の比なので、単位の無い double で受ける。</summary>
    public static T Scale<T>(this T node, double x, double y) where T : Node
    {
        node.ThrowIfDestroyed();
        WinUI.FrameworkElement 本物 = node.Element;
        Vector3 拡大率 = new(float.CreateChecked(x), float.CreateChecked(y), 本物.Scale.Z);
        if (本物.Scale != 拡大率)
        {
            本物.Scale = 拡大率;
        }
        return node;
    }
}
