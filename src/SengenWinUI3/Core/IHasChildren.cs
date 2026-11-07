namespace SengenWinUI3;

/// <summary>子を何個でも持てる部品。本物の子は登録した子だけにし、全削除ではその本物だけを外す。外からは実装できない。</summary>
public interface IHasChildren
{
    internal int ChildCount { get; }
    internal void InsertChildElement(int index, Node child);
    internal void RemoveChildElement(Node child);
    internal void ClearChildElements();
}
