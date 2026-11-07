namespace SengenWinUI3;

/// <summary>
/// 画面の木へ入れられず、ほかの部品に付けて出す部品(ポップアップなど)の基底。付けた先の部品の破棄で一緒に破棄される。
/// 付けずに作った浮かぶ部品は、利用する側が <see cref="Destroy"/> するか、Node の Own チェーンで預ける。
/// </summary>
public abstract class FloatingNode : ILifetimeOwner
{
    private static readonly Action 何もしない = static () => { };

    private readonly Lifetime 寿命;
    private 付けた先の状態 付けた先 = new 付けていない();

    private protected FloatingNode() => 寿命 = new(GetType().Name);

    /// <summary>付けた先から外し、中身の部品があれば破棄する。2回目は何もしない。</summary>
    public void Destroy()
    {
        if (寿命.IsDestroyed)
        {
            return;
        }
        CollectedExceptions 集めた例外 = new();
        寿命.Destroy(集めた例外);
        集めた例外.Run(付けた先 switch
        {
            付けていない => 何もしない,
            付けてある 付けた状態 => 付けた状態.付けた先から外す,
        });
        集めた例外.Run(OnDestroying);
        集めた例外.ThrowIfAny();
    }

    internal void ThrowIfDestroyed() => 寿命.ThrowIfDestroyed();

    internal void ThrowIfAttached()
    {
        ThrowIfDestroyed();
        bool 付けてあるか = 付けた先 switch
        {
            付けていない => false,
            付けてある => true,
        };
        if (付けてあるか)
        {
            throw new InvalidOperationException($"{GetType().Name} はすでに別の部品に付けてある");
        }
    }

    // 付けた先の部品の破棄で一緒に破棄する。自分だけを破棄したときは、外すとき呼ぶ処理を呼んでから付けた先の後片付けList から外れる。
    internal void AttachTo(Node anchor, Action 外すとき呼ぶ)
    {
        ThrowIfAttached();
        IDisposable 付けた先での後片付け = anchor.OwnRemovable(new CallOnDispose(Destroy));
        付けた先 = new 付けてある(anchor, () =>
        {
            外すとき呼ぶ();
            付けた先での後片付け.Dispose();
        });
    }

    // 開くたびに基準を受ける部品(Popup・ContentDialog)が呼ぶ。初回は基準へ付け、同じ基準なら何もせず、別の基準なら例外を投げる。
    internal void AttachOnceTo(Node anchor, Action 外すとき呼ぶ)
    {
        ThrowIfDestroyed();
        bool 同じ基準へ付けてあるか = 付けた先 switch
        {
            付けていない => false,
            付けてある 付けた状態 when ReferenceEquals(付けた状態.基準, anchor) => true,
            付けてある => throw new InvalidOperationException($"{GetType().Name} は別の基準へ付け替えられない"),
        };
        if (!同じ基準へ付けてあるか)
        {
            AttachTo(anchor, 外すとき呼ぶ);
        }
    }

    // 種類ごとの後片付け。付けた先から外した後に呼ばれる。
    internal abstract void OnDestroying();

    Lifetime ILifetimeOwner.Lifetime => 寿命;

    private sealed record 付けていない;
    private sealed record 付けてある(Node 基準, Action 付けた先から外す);
    private union 付けた先の状態(付けていない, 付けてある);
}
