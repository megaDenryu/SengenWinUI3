using System.Numerics;
using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>配置を変えずに、部品の根の位置・拡大率・回転を繰り返し設定するチェーン。</summary>
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

    /// <summary>回転の角度を設定する。回転の中心は <see cref="CenterPoint{T}"/> で決める。同じ値は設定し直さない。</summary>
    public static T Rotation<T>(this T node, Degrees angle) where T : Node
    {
        node.ThrowIfDestroyed();
        WinUI.FrameworkElement 本物 = node.Element;
        float 角度 = float.CreateChecked(angle.Value);
        if (本物.Rotation != 角度)
        {
            本物.Rotation = 角度;
        }
        return node;
    }

    /// <summary>
    /// 回転と拡大の中心を、部品の左上からの横縦の長さで設定する。部品の真ん中にするには、大きさが決まった後に
    /// <see cref="NodeEventExtensions.OnSizeChanged{T}"/> の中で幅と高さの半分を渡す。同じ値は設定し直さない。
    /// </summary>
    public static T CenterPoint<T>(this T node, Point center) where T : Node
    {
        node.ThrowIfDestroyed();
        WinUI.FrameworkElement 本物 = node.Element;
        Vector3 中心 = new(float.CreateChecked(center.X.Value), float.CreateChecked(center.Y.Value), 本物.CenterPoint.Z);
        if (本物.CenterPoint != 中心)
        {
            本物.CenterPoint = 中心;
        }
        return node;
    }
}
