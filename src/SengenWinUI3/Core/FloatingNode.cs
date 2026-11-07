namespace SengenWinUI3;

/// <summary>画面の木へ入れられず、ほかの部品に付けて出す部品(ポップアップなど)の基底。付けた先の部品の破棄で一緒に破棄される。</summary>
public abstract class FloatingNode : IChildOwner
{
    private bool 付けたか;

    private protected FloatingNode() { }

    internal bool IsDestroyed { get; private set; }

    /// <summary>中身の部品も破棄する。2回目は何もしない。</summary>
    public void Destroy()
    {
        if (IsDestroyed)
        {
            return;
        }
        IsDestroyed = true;
        OnDestroying();
    }

    internal void ThrowIfDestroyed()
    {
        if (IsDestroyed)
        {
            throw new InvalidOperationException($"{GetType().Name} は破棄した後に操作された");
        }
    }

    // 付けた先の部品の破棄で一緒に破棄する。1つの浮かぶ部品は1つの部品にだけ付けられる。
    internal void AttachTo(Node node)
    {
        ThrowIfDestroyed();
        if (付けたか)
        {
            throw new InvalidOperationException($"{GetType().Name} はすでに別の部品に付けてある");
        }
        付けたか = true;
        node.OwnDisposable(new DestroyOnDispose(Destroy));
    }

    // 種類ごとの後片付け。持っている中身の部品を破棄する。
    internal abstract void OnDestroying();

    // 中身の部品が Detach か Destroy で離れるとき、中身の本物を自分の本物から外す。
    private protected abstract void ReleaseContent(Node content);

    void IChildOwner.ReleaseChild(Node child) => ReleaseContent(child);
}
