using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>基準の部品から指定した位置へ、中身を重ねて出す。</summary>
public sealed class Popup : FloatingNode
{
    private readonly WinUI.Controls.Primitives.Popup 本物 = new();
    private readonly ContentSlot 中身置き場;
    private readonly Px 横の位置;
    private readonly Px 縦の位置;
    private 接続状態 接続 = new 未接続();
    private bool 読み込みを待つか;

    internal Popup(Node content, Px horizontalOffset, Px verticalOffset, bool isLightDismissEnabled)
    {
        横の位置 = horizontalOffset;
        縦の位置 = verticalOffset;
        本物.IsLightDismissEnabled = isLightDismissEnabled;
        中身置き場 = new(this, 中身 => 本物.Child = 中身);
        中身置き場.SetContent(content);
    }

    /// <summary>初回の基準へ寿命を結び付け、読み込み済みなら開く。未読み込みなら待ち、別の基準なら例外を投げる。</summary>
    public void Open(Node anchor)
    {
        ThrowIfDestroyed();
        anchor.ThrowIfDestroyed();
        Node 基準 = 接続 switch
        {
            未接続 => 接続する(anchor),
            接続済み 状態 when ReferenceEquals(状態.Anchor, anchor) => 状態.Anchor,
            接続済み => throw new InvalidOperationException("Popup は別の基準へ付け替えられない"),
        };
        待機を解く();
        if (基準.Element.IsLoaded)
        {
            開く(基準.Element);
        }
        else
        {
            基準.Element.Loaded += 読み込まれたら開く;
            読み込みを待つか = true;
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
        CollectedExceptions 起きた例外 = new();
        起きた例外.Run(閉じる);
        起きた例外.Run(中身置き場.DestroyContent);
        接続 = new 未接続();
        本物.XamlRoot = null;
        起きた例外.ThrowIfAny();
    }

    private protected override void ReleaseContent(Node content) => 中身置き場.Release(content);

    private Node 接続する(Node 基準)
    {
        AttachTo(基準, 閉じる);
        接続 = new 接続済み(基準);
        return 基準;
    }

    private void 開く(WinUI.FrameworkElement 基準)
    {
        WinUI.XamlRoot 根 = 基準.XamlRoot ?? throw new InvalidOperationException("Popup の基準に XamlRoot が無い");
        Windows.Foundation.Point 位置 = 基準.TransformToVisual(根.Content).TransformPoint(new Windows.Foundation.Point(0, 0));
        本物.XamlRoot = 根;
        本物.HorizontalOffset = 位置.X + 横の位置.Value;
        本物.VerticalOffset = 位置.Y + 縦の位置.Value;
        本物.IsOpen = true;
    }

    private void 読み込まれたら開く(object 送信元, WinUI.RoutedEventArgs 出来事)
    {
        待機を解く();
        if (!IsDestroyed && 接続 is 接続済み 状態) 開く(状態.Anchor.Element);
    }

    private void 待機を解く()
    {
        if (読み込みを待つか && 接続 is 接続済み 状態) 状態.Anchor.Element.Loaded -= 読み込まれたら開く;
        読み込みを待つか = false;
    }

    private void 閉じる() { 待機を解く(); 本物.IsOpen = false; }
    private sealed record 未接続;
    private sealed record 接続済み(Node Anchor);
    private union 接続状態(未接続, 接続済み);
}
