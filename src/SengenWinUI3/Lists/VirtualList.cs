using System.Collections.ObjectModel;
using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>
/// 見えている行の部品だけを作り、見えなくなった行の部品を別の項目で使い回す一覧。作る関数は <see cref="UI.VirtualList{TItem, TRow}(Func{TRow}, Style?)"/>。
/// スクロールは持たないので、<see cref="ScrollViewer"/> の中に置く(外側のスクロールの見えている範囲で行を作る)。
/// 行は使い回されて別の項目を見せるので、行の出来事は作るときに1回だけ配線し、ハンドラへは行がいま見せている項目の ID を渡す。
/// 行は <see cref="Component"/> の派生にし、Show で値を設定する。行の作者は Release で項目に結び付いた資源を解く。
/// </summary>
public sealed class VirtualList<TItem, TRow> : Node where TRow : Component, IRecyclableRow<TItem>
{
    private static readonly StyleTarget<WinUI.Controls.ItemsRepeater> 当て方 = new(FrameworkElementStyle.Layout);

    private readonly WinUI.Controls.ItemsRepeater 本物 = new();
    private readonly Func<TRow> 行を作る;
    private readonly Dictionary<WinUI.UIElement, TRow> 作った行Map = [];
    private readonly HashSet<TRow> 項目を見せている行Set = [];
    private readonly Stack<TRow> 使い回しを待つ行List = new();
    private ObservableCollection<渡す項目> 項目List = [];

    internal VirtualList(Func<TRow> createRow)
    {
        行を作る = createRow;
        本物.ItemTemplate = new RowElementFactory(項目を見せる行を用意する, 行を待ちへ入れる);
        本物.ItemsSource = 項目List;
    }

    internal override WinUI.FrameworkElement Element => 本物;

    /// <summary>項目を全部入れ替える。見せていた行は使い回しの待ちへ入る。</summary>
    public VirtualList<TItem, TRow> SetItems(IReadOnlyList<TItem> items)
    {
        ThrowIfDestroyed();
        項目List = new ObservableCollection<渡す項目>(items.Select(static 項目 => new 渡す項目(項目)));
        本物.ItemsSource = 項目List; // 注意: 1件ずつ足すと件数の回だけ ItemsRepeater へ知らせが飛ぶので、項目List 全体を差し替える
        return this;
    }

    /// <summary>末尾へ項目を1つ足す。</summary>
    public VirtualList<TItem, TRow> AddItem(TItem item)
    {
        ThrowIfDestroyed();
        項目List.Add(new 渡す項目(item));
        return this;
    }

    /// <summary><paramref name="index"/> の位置へ項目を差し込む。</summary>
    public VirtualList<TItem, TRow> InsertItem(int index, TItem item)
    {
        ThrowIfDestroyed();
        項目List.Insert(index, new 渡す項目(item));
        return this;
    }

    /// <summary><paramref name="index"/> の項目を置き換える。その項目を見せていた行は、新しい項目を見せ直す。</summary>
    public VirtualList<TItem, TRow> ReplaceItem(int index, TItem item)
    {
        ThrowIfDestroyed();
        項目List[index] = new 渡す項目(item);
        return this;
    }

    /// <summary><paramref name="index"/> の項目を外す。</summary>
    public VirtualList<TItem, TRow> RemoveItemAt(int index)
    {
        ThrowIfDestroyed();
        項目List.RemoveAt(index);
        return this;
    }

    internal override void ApplyStyle(Style style) => 当て方.Apply(style, 本物, this);

    // 行は作ってから一覧の破棄まで一覧の子であり、行だけを外す方法は無い。状態を変える前に例外を投げる。
    internal override void ThrowIfChildCannotLeave(Node child) =>
        throw new InvalidOperationException($"{GetType().Name} の行は一覧が持つ。行だけを Detach か Destroy できない");

    // 子孫(行)の破棄の前に、項目を見せている行を項目から離す。待っている行は離れてある。
    internal override void OnDestroying()
    {
        CollectedExceptions 集めた例外 = new();
        foreach (TRow 行 in 項目を見せている行Set)
        {
            集めた例外.Run(行.Release);
        }
        項目を見せている行Set.Clear();
        集めた例外.ThrowIfAny();
    }

    private WinUI.FrameworkElement 項目を見せる行を用意する(object 項目のデータ)
    {
        ThrowIfDestroyed();
        if (項目のデータ is not 渡す項目 渡す項目の値)
        {
            throw new InvalidOperationException($"{GetType().Name} の ItemsRepeater から、VirtualList に入っていない項目の行を求められた");
        }
        TRow 行 = 使い回しを待つ行List.TryPop(out TRow? 待っていた行) ? 待っていた行 : 行を新しく作る();
        行.Show(渡す項目の値.項目);
        _ = 項目を見せている行Set.Add(行);
        return 行.Element;
    }

    private void 行を待ちへ入れる(WinUI.UIElement 行の本物)
    {
        if (!IsDestroyed && 作った行Map.TryGetValue(行の本物, out TRow? 行) && 項目を見せている行Set.Remove(行))
        {
            行.Release();
            使い回しを待つ行List.Push(行);
        }
    }

    private TRow 行を新しく作る()
    {
        TRow 行 = 行を作る();
        AdoptChildPlacedElsewhere(行); // 行の本物は、ItemsRepeater が自分の子へ入れる
        作った行Map.Add(行.Element, 行);
        return 行;
    }

    // 項目を WinRT の境目へ渡す箱。項目が構造体でも、境目を値のまま越えずに済ませる(設計の文書の判断9)。
    private sealed class 渡す項目(TItem 項目)
    {
        internal TItem 項目 { get; } = 項目;
    }
}
