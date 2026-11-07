namespace SengenWinUI3;

/// <summary>未選択か、0 始まりの選択済みの位置。未選択には <see cref="NoSelection"/> を使う。</summary>
public union SelectionIndex(NoSelection, SelectedIndex)
{
    internal int ToWinUI() => this switch
    {
        NoSelection _ => -1,
        SelectedIndex 位置 => 位置.Index,
    };

    internal static SelectionIndex FromWinUI(int index) => index switch
    {
        -1 => new NoSelection(),
        >= 0 => SelectedIndex.FromWinUI(index),
        _ => throw new ArgumentOutOfRangeException(nameof(index), index, "WinUI の選択の位置が不正である"),
    };
}
