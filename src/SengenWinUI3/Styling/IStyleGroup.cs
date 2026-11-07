namespace SengenWinUI3;

// Style の項目の1つの群(置き方・余白・文字・背景・枠など)を、受け取る本物の型へ当てる当て方。印と当て方を1か所に置く。
// 受け取る型について反変なので、FrameworkElement への当て方を TextBlock や StackPanel の当て方の並びにそのまま入れられる。
internal interface IStyleGroup<in TElement>
{
    StyleItems Items { get; } // この当て方が当てる項目の印

    void Apply(Style style, TElement element);
}
