using System.Numerics;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml.Hosting;
using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// Rectangle の描かれた形(アルファ)から光の形を取る、Composition の DropShadow を載せた SpriteVisual。
// Rectangle の大きさが変わるたびに、光の大きさと形を取り直す。作った直後は見えない。
// 前提: UI スレッドで作る。形の元の Rectangle に塗りか輪郭が無いと、アルファが無いので光は出ない。
internal sealed class ShadowLayer : IDisposable
{
    private readonly WinUI.Shapes.Rectangle 形の元;
    private readonly DropShadow 影;
    private readonly SpriteVisual 入れ物;

    internal ShadowLayer(WinUI.Shapes.Rectangle 形の元)
    {
        this.形の元 = 形の元;
        Compositor 合成器 = ElementCompositionPreview.GetElementVisual(形の元).Compositor;
        影 = 合成器.CreateDropShadow();
        入れ物 = 合成器.CreateSpriteVisual();
        入れ物.Shadow = 影;
        入れ物.IsVisible = false;
        ElementCompositionPreview.SetElementChildVisual(形の元, 入れ物);
        形の元.SizeChanged += 大きさが変わった;
        大きさと形を取り直す();
    }

    internal void Show(ColorShadow shadow)
    {
        影.Color = shadow.Color.ToWinUI();
        影.BlurRadius = float.CreateChecked(shadow.BlurRadius.Value);
        影.Offset = new Vector3(float.CreateChecked(shadow.Offset.X.Value), float.CreateChecked(shadow.Offset.Y.Value), 0f);
        入れ物.IsVisible = true;
    }

    internal void Hide() => 入れ物.IsVisible = false;

    public void Dispose()
    {
        形の元.SizeChanged -= 大きさが変わった;
        ElementCompositionPreview.SetElementChildVisual(形の元, null);
        入れ物.Dispose();
        影.Dispose();
    }

    private void 大きさが変わった(object 送り手, WinUI.SizeChangedEventArgs 出来事) => 大きさと形を取り直す();

    // 大きさがまだ無い(配置の前)ときは何もしない。配置の後の大きさの変更で呼び直される。
    private void 大きさと形を取り直す()
    {
        if (形の元.ActualSize.X <= 0f || 形の元.ActualSize.Y <= 0f)
        {
            return;
        }
        入れ物.Size = 形の元.ActualSize;
        影.Mask = 形の元.GetAlphaMask();
    }
}
