using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>基準の XamlRoot に確認の画面を出し、閉じた結果を返す。</summary>
public sealed class ContentDialog : FloatingNode
{
    private readonly WinUI.Controls.ContentDialog 本物 = new();
    private readonly FloatingContent 中身置き場;
    private readonly ElementValue<WinUI.Controls.ContentDialog, string> 見出し;

    internal ContentDialog(string title, Node content, ContentDialogButtons buttons)
    {
        見出し = new(本物, static (本物, 文字) => 本物.Title is string 現在の文字 && 現在の文字 == 文字,
            static (本物, 文字) => 本物.Title = 文字);
        見出し.Set(title);
        本物.PrimaryButtonText = buttons.Primary.ToWinUI();
        本物.SecondaryButtonText = buttons.Secondary.ToWinUI();
        本物.CloseButtonText = buttons.Close.ToWinUI();
        中身置き場 = new(this, 中身 => 本物.Content = 中身);
        中身置き場.SetContent(content);
    }

    /// <summary>基準の XamlRoot へ表示する。同じ基準では繰り返し表示でき、別の基準へは付け替えない。</summary>
    public async Task<ContentDialogResult> ShowAsync(Node anchor)
    {
        ThrowIfDestroyed();
        anchor.ThrowIfDestroyed();
        WinUI.XamlRoot 根 = anchor.Element.XamlRoot ?? throw new InvalidOperationException("ContentDialog の基準に XamlRoot が無い");
        AttachOnceTo(anchor, 本物.Hide);
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

    /// <summary>Enter を押したときに押されるボタンを決める。決めないと、どのボタンも既定にならない。</summary>
    public ContentDialog SetDefaultButton(ContentDialogDefaultButton defaultButton)
    {
        ThrowIfDestroyed();
        本物.DefaultButton = defaultButton.ToWinUI();
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
        CollectedExceptions 集めた例外 = new();
        集めた例外.Run(本物.Hide);
        集めた例外.Run(中身置き場.DestroyContent);
        集めた例外.ThrowIfAny();
    }
}
