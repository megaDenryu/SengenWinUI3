using SengenWinUI3.State;
using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>画面の木に入れられるものすべての基底。本物の WinUI の部品を1つ持ち、外へは見せない。外からは <see cref="Component"/> だけを継承できる。</summary>
public abstract class Node : IChildOwner
{
    private IChildOwner? 持ち主;
    private List<Node>? 子List;
    private List<IDisposable>? 預かったList; // 購読の解除と、一緒に破棄するもの
    private NodeEvents 受けた出来事;

    private protected Node() { }

    internal abstract WinUI.FrameworkElement Element { get; }

    internal bool IsDestroyed { get; private set; }

    /// <summary>今の親から外す。購読は残すので、別の親へ入れ直せる。親が無ければ何もしない。</summary>
    public void Detach()
    {
        ThrowIfDestroyed();
        持ち主?.ReleaseChild(this);
        持ち主 = null;
    }

    /// <summary>自分と子孫の購読を解き、自分の本物を親の本物から外す。2回目は何もしない。</summary>
    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }
        持ち主?.ReleaseChild(this); // 注意: 外すときに自分の本物を読むので、破棄の印を付ける前に外す
        持ち主 = null;
        内側を破棄する();
    }

    internal void ThrowIfDestroyed()
    {
        if (IsDestroyed)
        {
            throw new InvalidOperationException($"{GetType().Name} は破棄した後に操作された");
        }
    }

    // 種類ごとの見た目の当て方。既定は、どの項目も持たない部品として、null でない項目があれば例外を投げる。
    internal virtual void ApplyStyle(Style style) => style.ThrowIfUnsupported(StyleItems.None, this);

    // 種類ごとの後片付け。子孫を破棄する前に呼ばれる。
    internal virtual void OnDestroying() { }

    // 子の本物を、自分の本物のどこから外すか。子を登録する部品は必ず上書きする。
    internal virtual void RemoveChildElement(Node child) =>
        throw new InvalidOperationException($"{GetType().Name} は子の本物を外す方法を持たない");

    internal void SetOwner(IChildOwner owner)
    {
        ThrowIfOwned();
        持ち主 = owner;
    }

    // WinUI の部品は親を1つしか持てない。黙って前の親から外すと、どこで付け替えたかがコードから読めなくなるので例外にする。
    internal void ThrowIfOwned()
    {
        ThrowIfDestroyed();
        if (持ち主 is not null)
        {
            throw new InvalidOperationException($"{GetType().Name} はすでに親を持つ。別の親へ入れるときは、先に Detach を呼ぶ");
        }
    }

    // 本物のどこへ入れた子も、これで登録する。破棄でたどる経路はこの一覧だけである。
    internal void AdoptChild(Node child)
    {
        ThrowIfDestroyed();
        child.SetOwner(this);
        (子List ??= []).Add(child);
    }

    // 子孫の本物は1つずつ外さない。自分の本物がまとめて木から外れるか、呼び出し側が本物の子をまとめて外すためである。
    internal void DestroyAllChildren()
    {
        ThrowIfDestroyed();
        List<Node>? 破棄する子List = 子List;
        子List = null;
        子を内側から破棄する(破棄する子List);
    }

    internal void OwnDisposable(IDisposable resource)
    {
        ThrowIfDestroyed();
        (預かったList ??= []).Add(resource);
    }

    internal void SubscribeCell<V>(IReadOnlyCell<V> cell, Action<V> apply)
    {
        ThrowIfDestroyed();
        OwnDisposable(new CellSubscriptionOnUiThread<V>(this, cell, apply));
    }

    internal void MarkEventWired(NodeEvents wiredEvent)
    {
        ThrowIfDestroyed();
        if ((受けた出来事 & wiredEvent) != NodeEvents.None)
        {
            throw new InvalidOperationException($"{GetType().Name} の {wiredEvent} は受けてある。On〜 は1つの部品に1回だけ呼べる");
        }
        受けた出来事 |= wiredEvent;
    }

    // 葉の部品が自分の出来事(OnClick など)を2回受けたかを調べる。
    private protected void ThrowIfWiredTwice(ref bool wired, string eventName)
    {
        ThrowIfDestroyed();
        if (wired)
        {
            throw new InvalidOperationException($"{GetType().Name} の {eventName} は受けてある。On〜 は1つの部品に1回だけ呼べる");
        }
        wired = true;
    }

    void IChildOwner.ReleaseChild(Node child)
    {
        RemoveChildElement(child);
        _ = 子List?.Remove(child);
    }

    private void 内側を破棄する()
    {
        IsDestroyed = true;
        List<IDisposable>? 解くList = 預かったList;
        List<Node>? 破棄する子List = 子List;
        預かったList = null;
        子List = null;
        foreach (IDisposable 預かったもの in 解くList ?? [])
        {
            預かったもの.Dispose();
        }
        OnDestroying();
        子を内側から破棄する(破棄する子List);
    }

    private static void 子を内側から破棄する(List<Node>? 破棄する子List)
    {
        foreach (Node 子 in 破棄する子List ?? [])
        {
            子.持ち主 = null;
            子.内側を破棄する();
        }
    }
}
