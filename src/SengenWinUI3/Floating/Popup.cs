using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>基準の部品から指定した位置へ、中身を重ねて出す。</summary>
public sealed class Popup : FloatingNode
{
    private static readonly IDisposable 待っていない = new CallOnDispose(static () => { });

    private readonly WinUI.Controls.Primitives.Popup 本物 = new();
    private readonly FloatingContent 中身置き場;
    private readonly Point 基準からのずれ;
    private IDisposable 読み込みの待ち = 待っていない; // 基準の読み込みを待つ購読。待っていなければ、解いても何もしない札

    internal Popup(Node content, Point offset, bool isLightDismissEnabled)
    {
        基準からのずれ = offset;
        本物.IsLightDismissEnabled = isLightDismissEnabled;
        中身置き場 = new(this, 中身 => 本物.Child = 中身);
        中身置き場.SetContent(content);
    }

    /// <summary>初回の基準へ寿命を結び付け、読み込み済みなら開く。未読み込みなら待ち、別の基準なら例外を投げる。</summary>
    public void Open(Node anchor)
    {
        ThrowIfDestroyed();
        anchor.ThrowIfDestroyed();
        AttachOnceTo(anchor, 閉じる);
        待機を解く();
        WinUI.FrameworkElement 基準 = anchor.Element;
        if (基準.IsLoaded)
        {
            開く(基準);
        }
        else
        {
            読み込みの待ち = this.WireRemovableEvent(基準, NodeEvents.Loaded, () => 読み込まれたら開く(基準));
        }
    }

    /// <summary>待機を取り消して閉じる。基準との寿命の結び付けと中身は保つ。</summary>
    public void Close()
    {
        ThrowIfDestroyed();
        閉じる();
    }

    /// <summary>中身を入れ替え、前の中身を破棄する。</summary>
    public Popup SetContent(Node content)
    {
        ThrowIfDestroyed();
        中身置き場.SetContent(content);
        return this;
    }

    internal override void OnDestroying()
    {
        CollectedExceptions 集めた例外 = new();
        集めた例外.Run(閉じる);
        集めた例外.Run(中身置き場.DestroyContent);
        本物.XamlRoot = null;
        集めた例外.ThrowIfAny();
    }

    private void 開く(WinUI.FrameworkElement 基準)
    {
        WinUI.XamlRoot 根 = 基準.XamlRoot ?? throw new InvalidOperationException("Popup の基準に XamlRoot が無い");
        Windows.Foundation.Point 位置 = 基準.TransformToVisual(根.Content).TransformPoint(new Windows.Foundation.Point(0, 0));
        本物.XamlRoot = 根;
        本物.HorizontalOffset = 位置.X + 基準からのずれ.X.Value;
        本物.VerticalOffset = 位置.Y + 基準からのずれ.Y.Value;
        本物.IsOpen = true;
    }

    private void 読み込まれたら開く(WinUI.FrameworkElement 基準)
    {
        待機を解く();
        開く(基準);
    }

    private void 待機を解く()
    {
        読み込みの待ち.Dispose();
        読み込みの待ち = 待っていない;
    }

    private void 閉じる() { 待機を解く(); 本物.IsOpen = false; }
}
