using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>子の部品を選択肢にして、1つを選ぶ部品。ItemsSource は持たない。</summary>
public sealed class RadioButtons : Node, IHasChildren, IControlNode
{
    private static readonly EventBinding<WinUI.Controls.RadioButtons, WinUI.Controls.SelectionChangedEventHandler, EventArgs> 選択の出来事 = new(
        static (本物, 呼ぶ) => (送信元, 出来事) => 呼ぶ(EventArgs.Empty),
        static (本物, ハンドラ) => 本物.SelectionChanged += ハンドラ,
        static (本物, ハンドラ) => 本物.SelectionChanged -= ハンドラ);

    private readonly WinUI.Controls.RadioButtons 本物 = new();
    private readonly SelectionItems 項目と選択;

    internal RadioButtons()
    {
        ElementValue<WinUI.Controls.RadioButtons, SelectionIndex> 選択の設定 = new(本物,
            static (本物, 位置) => 本物.SelectedIndex == 位置.ToWinUI(),
            static (本物, 位置) => 本物.SelectedIndex = 位置.ToWinUI());
        項目と選択 = new(本物.Items, () => SelectionIndex.FromWinUI(本物.SelectedIndex), 選択の設定.Set);
    }

    internal override WinUI.FrameworkElement Element => 本物;
    WinUI.Controls.Control IControlNode.Control => 本物;
    int IHasChildren.ChildCount => 項目と選択.Count;
    void IHasChildren.InsertChildElement(int index, Node child) => 項目と選択.Insert(index, child);
    void IHasChildren.RemoveChildElement(Node child) => 項目と選択.Remove(child);
    void IHasChildren.ClearChildElements() => 項目と選択.Clear();

    /// <summary>選択の位置を替える。項目数以上の位置は例外になり、この設定による変更は通知しない。</summary>
    public RadioButtons SetSelectedIndex(SelectionIndex selectedIndex)
    {
        ThrowIfDestroyed();
        項目と選択.SetSelectedIndex(selectedIndex);
        return this;
    }

    /// <summary>項目操作中は通知せず、現在の位置が最後の設定値と異なるときに通知する。2回目の登録は例外になる。</summary>
    public RadioButtons OnSelectionChanged(Action<SelectionIndex> handler) =>
        this.WireEvent(nameof(OnSelectionChanged), 本物, 選択の出来事, () => 項目と選択.NotifyUserChange(handler));

    internal override void ApplyStyle(Style style) => ControlStyle.All.Apply(style, 本物, this);
}
