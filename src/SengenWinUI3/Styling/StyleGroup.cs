namespace SengenWinUI3;

// IStyleGroup の実装。項目の印と、その項目だけを当てる処理を組にする。
internal sealed class StyleGroup<TElement>(StyleItems items, Action<Style, TElement> apply) : IStyleGroup<TElement>
{
    public StyleItems Items => items;

    public void Apply(Style style, TElement element) => apply(style, element);
}
