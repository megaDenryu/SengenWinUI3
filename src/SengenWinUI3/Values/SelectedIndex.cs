namespace SengenWinUI3;

/// <summary>0 始まりの選択済みの位置。項目数の上限は、部品へ設定するときに確認する。</summary>
public sealed class SelectedIndex : IEquatable<SelectedIndex>
{
    private SelectedIndex(int index) => Index = index;

    /// <summary>0 始まりの位置。</summary>
    public int Index { get; }

    /// <summary>負の値をエラーとして返し、0 以上なら選択済みの位置を作る。</summary>
    public static SelectedIndexResult TryFromIndex(int index) =>
        index >= 0 ? new SelectedIndex(index) : new SelectedIndexError(index);

    // WinUI の未選択を除いた位置だけを、SelectionIndex.FromWinUI から受け取る。
    internal static SelectedIndex FromWinUI(int index) => new(index);

    /// <summary>位置が等しいかを返す。</summary>
    public bool Equals(SelectedIndex? other) => other is not null && Index == other.Index;

    /// <summary>同じ型で位置が等しいかを返す。</summary>
    public override bool Equals(object? obj) => obj is SelectedIndex 位置 && Equals(位置);

    /// <summary>位置のハッシュ値を返す。</summary>
    public override int GetHashCode() => Index.GetHashCode();
}
