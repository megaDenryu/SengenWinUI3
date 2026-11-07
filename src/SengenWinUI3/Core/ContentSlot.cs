using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// 中身を1つ持つ部品(ScrollViewer・Button・Border・浮かぶ部品・ウインドウ)の中身置き場。状態は今の中身の部品1つだけである。
// 中身の本物を持ち主の本物へ置いてから登録し、置くのに失敗したら登録しない。入れ替えるときは、新しい中身を置いた後に前の中身を破棄する。
internal sealed class ContentSlot(IContentOwner 持ち主, Action<WinUI.UIElement?> 本物へ置く)
{
    private Node? 中身;

    internal void SetContent(Node content)
    {
        Node? 前の中身 = 中身;
        持ち主.AdoptChild(content, () => 本物へ置く(content.Element));
        中身 = content;
        前の中身?.Destroy(); // 前の中身はもう今の中身でないので、持ち主の本物は空にしない
    }

    // 持ち主の ReleaseChild から呼ぶ。今の中身のときだけ、本物から外す。
    internal void Release(Node child)
    {
        if (!ReferenceEquals(child, 中身))
        {
            return;
        }
        本物へ置く(null);
        中身 = null;
    }

    // 子List を持たない持ち主(浮かぶ部品の中身・ウインドウ)が、自分の破棄のときに呼ぶ。
    internal void DestroyContent() => 中身?.Destroy();
}
