using SengenWinUI3.State;
using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>
/// ほかの部品を組み合わせて1つの単位にした部品の基底。派生は <c>sealed</c> にし、<see cref="Build"/> で中身の木を返す。
/// 自分では WinUI の部品を増やさず、親の木へ入るのは <see cref="Build"/> が返した根の本物である。当てた <see cref="Style"/> は根へ渡す。
/// </summary>
public abstract class Component : Node
{
    private readonly Lazy<Node> 根;

    /// <summary>根はここでは作らず、初めて本物が要ったときに作る。</summary>
    protected Component()
    {
        根 = new Lazy<Node>(根を構築する, LazyThreadSafetyMode.None); // 注意: 基底のコンストラクタから Build を呼ぶと、派生のフィールドが初期化される前に派生の処理が走る
    }

    internal sealed override WinUI.FrameworkElement Element => 根.Value.Element;

    /// <summary>中身の木を返す。初めて本物が要ったとき(親の木へ入れるとき)に1回だけ呼ばれる。</summary>
    protected abstract Node Build();

    /// <summary>自分で作ったタイマーや購読を預け、この部品の破棄で一緒に Dispose する。</summary>
    protected void Own(IDisposable resource) => OwnDisposable(resource);

    /// <summary>木に入れていないフィールドの部品を預け、この部品の破棄で一緒に破棄する。</summary>
    protected void Own(Node detachedNode) => OwnDisposable(new CallOnDispose(detachedNode.Destroy));

    /// <summary>
    /// セルを結ぶ。結んだ時に1回、セルの値が変わるたびに <paramref name="apply"/> を UI スレッドで呼ぶ。
    /// 返した値を Dispose すると解ける。Dispose しなくても、この部品の破棄で解ける。使い回す行が、項目ごとに結び直すために使う(Show で結び、Release で解く)。
    /// </summary>
    protected IDisposable Observe<TValue>(IReadOnlyCell<TValue> cell, Action<TValue> apply) => SubscribeCell(cell, apply);

    internal sealed override void ApplyStyle(Style style) => 根.Value.ApplyStyle(style);

    internal sealed override void RemoveChildElement(Node child) =>
        throw new InvalidOperationException($"{GetType().Name} の根は外せない。根の本物は {GetType().Name} の本物そのものである");

    private Node 根を構築する()
    {
        Node 構築した根 = Build();
        AdoptChild(構築した根, static () => { }); // 根の本物は、この部品の本物として親が入れる
        return 構築した根;
    }
}
