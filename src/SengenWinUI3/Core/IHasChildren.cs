namespace SengenWinUI3;

/// <summary>子を何個でも持てる部品の目印。子の本物を入れる方法は部品ごとに持ち、外からは実装できない。</summary>
public interface IHasChildren
{
    internal int ChildCount { get; }
    internal void InsertChildElement(int index, Node child);
    internal void ClearChildElements();
}
