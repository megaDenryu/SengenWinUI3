namespace SengenWinUI3;

// 1種類の部品の当て方の並び。部品が持つ項目の印を並びから作るので、印と実際に当てる処理がずれない。
// 部品の型ごとに static readonly で1つ持ち、ApplyStyle はこの Apply を呼ぶだけにする。
internal sealed class StyleTarget<TElement>
{
    private readonly IStyleGroup<TElement>[] 当て方List;
    private readonly StyleItems 持つ項目;

    internal StyleTarget(params IStyleGroup<TElement>[] 当て方List)
    {
        this.当て方List = 当て方List;
        持つ項目 = 当て方List.Aggregate(StyleItems.None, static (合わせた印, 当て方) => 合わせた印 | 当て方.Items);
    }

    // 部品が持たない項目があれば、何も当てずに例外を投げる。
    internal void Apply(Style style, TElement 本物, Node 部品)
    {
        style.ThrowIfUnsupported(持つ項目, 部品);
        foreach (IStyleGroup<TElement> 当て方 in 当て方List)
        {
            当て方.Apply(style, 本物);
        }
    }
}
