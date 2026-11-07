namespace SengenWinUI3;

/// <summary>子を何個でも持てる部品(<see cref="IHasChildren"/>)のチェーン。子を1つ捨てるのは <c>child.Destroy()</c>、付け替えるのは <c>child.Detach()</c> の後に新しい親へ入れる。</summary>
public static class ChildrenExtensions
{
    /// <summary>子を末尾へ順に足す。null は飛ばすので、構築の時に決まる条件を <c>condition ? node : null</c> で書ける。</summary>
    public static T Children<T>(this T parent, params Node?[] children) where T : Node, IHasChildren
    {
        IEnumerable<Node?> 子List = children;
        return parent.Children(子List);
    }

    /// <summary>子を末尾へ順に足す。null は飛ばす。</summary>
    public static T Children<T>(this T parent, IEnumerable<Node?> children) where T : Node, IHasChildren
    {
        parent.ThrowIfDestroyed();
        foreach (Node? 子 in children)
        {
            if (子 is not null)
            {
                parent.AddChild(子);
            }
        }
        return parent;
    }

    /// <summary>子を末尾へ1つ足す。親を持つ部品を渡すと例外を投げる。</summary>
    public static T AddChild<T>(this T parent, Node child) where T : Node, IHasChildren
    {
        parent.AdoptChild(child);
        parent.ChildPanel.Children.Add(child.Element);
        return parent;
    }

    /// <summary>子を <paramref name="index"/> の位置へ差し込む。親を持つ部品を渡すと例外を投げる。</summary>
    public static T InsertChild<T>(this T parent, int index, Node child) where T : Node, IHasChildren
    {
        parent.ThrowIfDestroyed();
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(index, parent.ChildPanel.Children.Count);
        parent.AdoptChild(child);
        parent.ChildPanel.Children.Insert(index, child.Element);
        return parent;
    }

    /// <summary>子を全部破棄する。子の本物はまとめて外す。</summary>
    public static T DestroyChildren<T>(this T parent) where T : Node, IHasChildren
    {
        parent.DestroyAllChildren();
        parent.ChildPanel.Children.Clear();
        return parent;
    }
}
