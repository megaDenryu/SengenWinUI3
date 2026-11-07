namespace SengenWinUI3;

// 項目操作後の選択位置は操作が終わるまで決まらないため、項目操作中だけ同期する通知を抑える。
// SetSelectedIndex の通知は、時間の印ではなく InputValue の最後の設定値との比較で除く。
internal sealed class SelectionItems
{
    private readonly IList<object> 項目List;
    private readonly Func<SelectionIndex> 現在の選択位置;
    private readonly InputValue<SelectionIndex> 選択;
    private bool 項目の操作中か;

    internal SelectionItems(IList<object> items, Func<SelectionIndex> getSelectedIndex, Action<SelectionIndex> setSelectedIndex)
    {
        項目List = items;
        現在の選択位置 = getSelectedIndex;
        選択 = new(getSelectedIndex(), setSelectedIndex);
    }

    internal int Count => 項目List.Count;

    internal void SetSelectedIndex(SelectionIndex selectedIndex)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(selectedIndex.ToWinUI(), Count);
        選択.Set(selectedIndex);
    }

    internal void Insert(int index, Node child)
    {
        bool 前の操作中か = 項目の操作中か;
        項目の操作中か = true;
        try
        {
            項目List.Insert(index, child.Element);
        }
        finally
        {
            操作後の選択をそろえる(前の操作中か);
        }
    }

    internal void Remove(Node child)
    {
        bool 前の操作中か = 項目の操作中か;
        項目の操作中か = true;
        try
        {
            _ = 項目List.Remove(child.Element);
        }
        finally
        {
            操作後の選択をそろえる(前の操作中か);
        }
    }

    internal void Clear()
    {
        bool 前の操作中か = 項目の操作中か;
        項目の操作中か = true;
        try
        {
            項目List.Clear();
        }
        finally
        {
            操作後の選択をそろえる(前の操作中か);
        }
    }

    internal void NotifyUserChange(Action<SelectionIndex> handler)
    {
        if (項目の操作中か) return;
        SelectionIndex 今の位置 = 現在の選択位置();
        if (選択.IsUserChange(今の位置)) handler(今の位置);
    }

    private void 操作後の選択をそろえる(bool 前の操作中か)
    {
        try
        {
            選択.Set(現在の選択位置());
        }
        finally
        {
            項目の操作中か = 前の操作中か;
        }
    }
}
