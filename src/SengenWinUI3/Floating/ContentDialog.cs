using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>基準の XamlRoot に確認の画面を出し、閉じた結果を返す。</summary>
public sealed class ContentDialog : FloatingNode
{
    private readonly WinUI.Controls.ContentDialog 本物 = new();
    private readonly ContentSlot 中身置き場;
    private readonly ElementValue<WinUI.Controls.ContentDialog, string> 見出し;
    private 接続状態 接続 = new 未接続();

    internal ContentDialog(string title, Node content, ContentDialogButton primaryButtonText,
        ContentDialogButton secondaryButtonText, ContentDialogButton closeButtonText)
    {
        見出し = new(本物, static (本物, 文字) => 本物.Title is string 現在の文字 && 現在の文字 == 文字,
            static (本物, 文字) => 本物.Title = 文字);
        見出し.Set(title);
        本物.PrimaryButtonText = primaryButtonText.ToWinUI();
        本物.SecondaryButtonText = secondaryButtonText.ToWinUI();
        本物.CloseButtonText = closeButtonText.ToWinUI();
        中身置き場 = new(this, 中身 => 本物.Content = 中身);
        中身置き場.SetContent(content);
    }

    /// <summary>基準の XamlRoot へ表示する。同じ基準では繰り返し表示でき、別の基準へは付け替えない。</summary>
    public async Task<ContentDialogResult> ShowAsync(Node anchor)
    {
        ThrowIfDestroyed();
        anchor.ThrowIfDestroyed();
        WinUI.XamlRoot 根 = anchor.Element.XamlRoot ?? throw new InvalidOperationException("ContentDialog の基準に XamlRoot が無い");
        _ = 接続 switch
        {
            未接続 => 接続する(anchor),
            接続済み 状態 when ReferenceEquals(状態.Anchor, anchor) => 状態.Anchor,
            接続済み => throw new InvalidOperationException("ContentDialog は別の基準へ付け替えられない"),
        };
        本物.XamlRoot = 根;
        WinUI.Controls.ContentDialogResult 結果 = await 本物.ShowAsync();
        return 結果 switch
        {
            WinUI.Controls.ContentDialogResult.Primary => new ContentDialogPrimaryResult(),
            WinUI.Controls.ContentDialogResult.Secondary => new ContentDialogSecondaryResult(),
            WinUI.Controls.ContentDialogResult.None => new ContentDialogCloseResult(),
            _ => throw new InvalidOperationException("WinUI が未知の ContentDialogResult を返した"),
        };
    }

    /// <summary>見出しの文字を替える。</summary>
    public ContentDialog SetTitle(string title)
    {
        ThrowIfDestroyed();
        見出し.Set(title);
        return this;
    }

    /// <summary>中身を入れ替え、前の中身を破棄する。</summary>
    public ContentDialog SetContent(Node content)
    {
        ThrowIfDestroyed();
        中身置き場.SetContent(content);
        return this;
    }

    /// <summary>表示中なら閉じる。</summary>
    public void Hide() { ThrowIfDestroyed(); 本物.Hide(); }

    internal override void OnDestroying()
    {
        CollectedExceptions 起きた例外 = new();
        起きた例外.Run(本物.Hide);
        起きた例外.Run(中身置き場.DestroyContent);
        接続 = new 未接続();
        起きた例外.ThrowIfAny();
    }

    private protected override void ReleaseContent(Node content) => 中身置き場.Release(content);

    private Node 接続する(Node 基準)
    {
        AttachTo(基準, 本物.Hide);
        接続 = new 接続済み(基準);
        return 基準;
    }

    private sealed record 未接続;
    private sealed record 接続済み(Node Anchor);
    private union 接続状態(未接続, 接続済み);
}
