using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// ItemsRepeater へ渡す行の作り手。ItemsRepeater は見えている範囲の行だけをこれに求め、範囲から外れた行をこれへ返す。
// 注意: WinRT のインターフェースを実装する型は、CsWinRT がその型の WinRT の包みを生成するので partial にする。型引数を持たせないのも同じ理由である。
internal sealed partial class RowElementFactory(Func<object, WinUI.UIElement> 項目を見せる行を用意する, Action<WinUI.UIElement> 行を待ちへ入れる) : WinUI.IElementFactory
{
    public WinUI.UIElement GetElement(WinUI.ElementFactoryGetArgs args) => 項目を見せる行を用意する(args.Data);

    public void RecycleElement(WinUI.ElementFactoryRecycleArgs args) => 行を待ちへ入れる(args.Element);
}
