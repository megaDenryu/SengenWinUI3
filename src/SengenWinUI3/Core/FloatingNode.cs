namespace SengenWinUI3;

/// <summary>
/// 画面の木へ入れられず、ほかの部品に付けて出す部品(ポップアップなど)の基底。付けた先の部品の破棄で一緒に破棄される。
/// 付けずに作った浮かぶ部品は、利用する側が <see cref="Destroy"/> するか、<see cref="Component"/> の Own で預ける。
/// </summary>
public abstract class FloatingNode : IChildOwner
{
    private Action? 付けた先から外す; // 付けたときだけある

    private protected FloatingNode() { }

    internal bool IsDestroyed { get; private set; }

    /// <summary>付けた先から外し、中身の部品も破棄する。2回目は何もしない。</summary>
    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }
        IsDestroyed = true;
        CollectedExceptions 起きた例外 = new();
        if (付けた先から外す is { } 外すとき呼ぶ)
        {
            起きた例外.Run(外すとき呼ぶ);
        }
        起きた例外.Run(OnDestroying);
        起きた例外.ThrowIfAny();
    }

    internal void ThrowIfDestroyed()
    {
        if (IsDestroyed)
        {
            throw new InvalidOperationException($"{GetType().Name} は破棄した後に操作された");
        }
    }

    internal void ThrowIfAttached()
    {
        ThrowIfDestroyed();
        if (付けた先から外す is not null)
        {
            throw new InvalidOperationException($"{GetType().Name} はすでに別の部品に付けてある");
        }
    }

    // 付けた先の部品の破棄で一緒に破棄する。自分だけを破棄したときは、付けた先から外す。
    internal void AttachTo(Node anchor, Action 外すとき呼ぶ)
    {
        ThrowIfAttached();
        IDisposable 付けた先での後片付け = anchor.OwnRemovable(new CallOnDispose(Destroy));
        付けた先から外す = () =>
        {
            外すとき呼ぶ();
            付けた先での後片付け.Dispose();
        };
    }

    // 種類ごとの後片付け。持っている中身の部品を破棄する。
    internal abstract void OnDestroying();

    // 中身の部品が Detach か Destroy で離れるとき、中身の本物を自分の本物から外す。
    private protected abstract void ReleaseContent(Node content);

    void IChildOwner.AdoptChild(Node child, Action 本物へ入れる)
    {
        ThrowIfDestroyed();
        child.BecomeChildOf(this, 本物へ入れる);
    }

    void IChildOwner.ReleaseChild(Node child) => ReleaseContent(child);
}
