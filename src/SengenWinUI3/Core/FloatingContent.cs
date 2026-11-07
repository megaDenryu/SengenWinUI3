using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// 中身の部品を1つ持つ浮かぶ部品(Flyout・Popup・ContentDialog)の中身置き場。浮かぶ部品は子List を持たないので、この型が中身の持ち主になり、浮かぶ部品の破棄で中身を破棄する。
// 中身を持たない浮かぶ部品(MenuFlyout)は、この型を持たない。
internal sealed class FloatingContent : IContentOwner
{
    private readonly FloatingNode 浮かぶ部品;
    private readonly ContentSlot 中身置き場;

    internal FloatingContent(FloatingNode 持ち主の浮かぶ部品, Action<WinUI.UIElement?> 本物へ置く)
    {
        浮かぶ部品 = 持ち主の浮かぶ部品;
        中身置き場 = new(this, 本物へ置く);
    }

    // 前の中身があれば、新しい中身を置いた後に破棄する。
    internal void SetContent(Node content) => 中身置き場.SetContent(content);

    // 浮かぶ部品の OnDestroying から呼ぶ。
    internal void DestroyContent() => 中身置き場.DestroyContent();

    void IChildOwner.AdoptChild(Node child, Action 本物へ入れる)
    {
        浮かぶ部品.ThrowIfDestroyed();
        child.BecomeChildOf(this, 本物へ入れる);
    }

    void IChildOwner.ReleaseChild(Node child) => 中身置き場.Release(child);

    void IContentOwner.RemoveChildElement(Node child) => 中身置き場.Release(child);
}
