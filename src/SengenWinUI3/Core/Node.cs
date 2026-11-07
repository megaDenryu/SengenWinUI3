using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>画面の木に入れられるものすべての基底。本物の WinUI の部品を1つ持ち、外へは見せない。外からは <see cref="Component"/> だけを継承できる。</summary>
public abstract class Node : IChildOwner
{
    private readonly NodeFamily 親子関係 = new();
    private readonly CleanupList 後片付けの台帳 = new();
    private readonly EventWiringMarks 受けた出来事 = new();

    private protected Node() { }

    internal abstract WinUI.FrameworkElement Element { get; }

    internal bool IsDestroyed { get; private set; }

    /// <summary>今の親から外す。購読は残すので、別の親へ入れ直せる。親が無ければ何もしない。</summary>
    public void Detach()
    {
        ThrowIfDestroyed();
        親子関係.LeaveOwner(this);
    }

    /// <summary>
    /// 自分と子孫の購読を解き、自分の本物を親の本物から外す。2回目は何もしない。
    /// 途中で例外が起きても後片付けを全部済ませてから、起きた例外を <see cref="AggregateException"/> にまとめて投げる。
    /// </summary>
    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }
        親子関係.ThrowIfCannotLeave(this); // 外せない子(一覧の行・Component の根)は、何も変えずに例外を投げる
        CollectedExceptions 起きた例外 = new();
        起きた例外.Run(() => 親子関係.LeaveOwner(this)); // 注意: 外すときに自分の本物を読むので、破棄の印を付ける前に外す
        内側を破棄する(起きた例外);
        起きた例外.ThrowIfAny();
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

    // 子が自分から離れられるか。外せない子を持つ部品は、状態を変える前に例外を投げるよう上書きする。
    internal virtual void ThrowIfChildCannotLeave(Node child) { }

    // 子の本物を、自分の本物のどこから外すか。子を登録する部品は必ず上書きする。
    internal virtual void RemoveChildElement(Node child) =>
        throw new InvalidOperationException($"{GetType().Name} は子の本物を外す方法を持たない");

    // 自分の本物を持ち主の本物へ入れてから、持ち主を覚える。入れるのに失敗したら覚えない。
    internal void BecomeChildOf(IChildOwner owner, Action 本物へ入れる)
    {
        ThrowIfDestroyed();
        親子関係.ThrowIfOwned(this);
        本物へ入れる();
        親子関係.SetOwner(owner);
    }

    // 本物のどこへ入れた子も、これで登録する。
    internal void AdoptChild(Node child, Action 本物へ入れる)
    {
        ThrowIfDestroyed();
        child.BecomeChildOf(this, 本物へ入れる);
        親子関係.AddChild(child);
    }

    // 本物は別の仕組み(親の本物そのもの・ItemsRepeater・見せるときに入れる入れ物)が入れる子を、登録だけする。
    internal void AdoptChildPlacedElsewhere(Node child) => AdoptChild(child, static () => { });

    // 子孫の本物は1つずつ外さない。子を全部破棄した後に、子の破棄が例外を投げても、本物の子をまとめて外す。
    internal void DestroyAllChildren(Action 本物の子をまとめて外す)
    {
        ThrowIfDestroyed();
        CollectedExceptions 起きた例外 = new();
        子を内側から破棄する(親子関係.TakeChildren(), 起きた例外);
        起きた例外.Run(本物の子をまとめて外す);
        起きた例外.ThrowIfAny();
    }

    internal void OwnDisposable(IDisposable resource)
    {
        ThrowIfDestroyed();
        後片付けの台帳.Add(resource);
    }

    // 返した値を Dispose すると先に解ける。Dispose しなくても部品の破棄で解ける。
    internal IDisposable OwnRemovable(IDisposable resource)
    {
        ThrowIfDestroyed();
        return 後片付けの台帳.AddRemovable(resource);
    }

    internal void MarkWiredOnce(string eventName)
    {
        ThrowIfDestroyed();
        受けた出来事.Mark(eventName, this);
    }

    // 1回だけの配線を外したとき(付けた Flyout だけを破棄したとき)に印を戻し、また配線できるようにする。破棄の途中からも呼ばれる。
    internal void UnmarkWired(string eventName) => 受けた出来事.Unmark(eventName);

    // 出来事を受ける入口。2回目は例外にし、購読を解く処理を後片付けの台帳へ入れる。処理の先頭で破棄済みなら何もしないことは、呼び出し側の処理が書く。
    internal void WireEvent(string eventName, Action subscribe, Action unsubscribe)
    {
        MarkWiredOnce(eventName);
        subscribe();
        後片付けの台帳.Add(new CallOnDispose(unsubscribe));
    }

    void IChildOwner.AdoptChild(Node child, Action 本物へ入れる) => AdoptChild(child, 本物へ入れる);

    void IChildOwner.ThrowIfCannotRelease(Node child) => ThrowIfChildCannotLeave(child);

    void IChildOwner.ReleaseChild(Node child)
    {
        親子関係.RemoveChild(child); // 注意: 本物から外すのが例外を投げても、破棄した子が子List に残って2回破棄されないよう、先に外す
        RemoveChildElement(child);
    }

    private void 内側を破棄する(CollectedExceptions 起きた例外)
    {
        IsDestroyed = true;
        後片付けの台帳.DisposeAll(起きた例外);
        起きた例外.Run(OnDestroying);
        子を内側から破棄する(親子関係.TakeChildren(), 起きた例外);
    }

    private static void 子を内側から破棄する(List<Node>? 破棄する子List, CollectedExceptions 起きた例外)
    {
        foreach (Node 子 in 破棄する子List ?? [])
        {
            子.親子関係.ForgetOwner();
            子.内側を破棄する(起きた例外);
        }
    }
}
