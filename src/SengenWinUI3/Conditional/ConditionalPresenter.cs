using SengenWinUI3.State;
using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>
/// 真偽のセルを受け、真のときの部品と偽のときの部品の片方だけを見せる部品。作る関数は <see cref="UI.If(IReadOnlyCell{bool}, Node, Node?)"/> と
/// <see cref="UI.If(IReadOnlyCell{bool}, Func{Node}, Func{Node}?)"/>。見せない側の部品も破棄せずに持ち、この部品の破棄で一緒に破棄する。
/// </summary>
public sealed class ConditionalPresenter : Node
{
    private static readonly StyleTarget<WinUI.Controls.Grid> 当て方 = new(FrameworkElementStyle.Layout);

    private readonly WinUI.Controls.Grid 本物 = new();
    private readonly 出し分けの側 真の側;
    private readonly 出し分けの側 偽の側;

    internal ConditionalPresenter(IReadOnlyCell<bool> condition, Node whenTrue, Node? whenFalse)
    {
        真の側 = new 出し分けの側(this, whenTrue, null);
        偽の側 = new 出し分けの側(this, whenFalse, null);
        _ = SubscribeCell(condition, 見せる側を選ぶ);
    }

    internal ConditionalPresenter(IReadOnlyCell<bool> condition, Func<Node> createWhenTrue, Func<Node>? createWhenFalse)
    {
        真の側 = new 出し分けの側(this, null, createWhenTrue);
        偽の側 = new 出し分けの側(this, null, createWhenFalse);
        _ = SubscribeCell(condition, 見せる側を選ぶ);
    }

    internal override WinUI.FrameworkElement Element => 本物;

    internal override void ApplyStyle(Style style) => 当て方.Apply(style, 本物, this);

    // 見せない側の本物は Grid に入れていないので、見せている側のときだけ外れる。
    internal override void RemoveChildElement(Node child)
    {
        _ = 本物.Children.Remove(child.Element);
        真の側.Forget(child);
        偽の側.Forget(child);
    }

    // 見せない側を Visibility で隠さず Grid から外すのは、利用する側が中の部品へ .Visible で決めた見せ方を上書きしないためである。
    private void 見せる側を選ぶ(bool 真か)
    {
        Node? 見せる部品 = (真か ? 真の側 : 偽の側).Prepare();
        本物.Children.Clear();
        if (見せる部品 is not null)
        {
            本物.Children.Add(見せる部品.Element);
        }
    }

    // 片方の側。部品を受けた側は初めから持ち、作る関数を受けた側は初めて見せるときに1回だけ作る。どちらも無い側は何も見せない。
    private sealed class 出し分けの側
    {
        private readonly ConditionalPresenter 持ち主;
        private Node? 部品;
        private Func<Node>? 作る関数;

        internal 出し分けの側(ConditionalPresenter 持ち主, Node? 部品, Func<Node>? 作る関数)
        {
            this.持ち主 = 持ち主;
            this.作る関数 = 作る関数;
            if (部品 is not null)
            {
                子として預ける(部品);
            }
        }

        internal Node? Prepare()
        {
            if (部品 is null && 作る関数 is { } 作る)
            {
                作る関数 = null;
                子として預ける(作る());
            }
            return 部品;
        }

        // 部品が Detach か Destroy で離れたら、その側はもう何も見せない。
        internal void Forget(Node 離れた子)
        {
            if (ReferenceEquals(離れた子, 部品))
            {
                部品 = null;
            }
        }

        // 本物は見せるときに Grid へ入れるので、ここでは子として登録だけする。
        private void 子として預ける(Node 預ける部品)
        {
            持ち主.AdoptChild(預ける部品, static () => { });
            部品 = 預ける部品;
        }
    }
}
