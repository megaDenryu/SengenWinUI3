using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>
/// ほかの部品を組み合わせて1つの単位にした部品の基底。派生は <c>sealed</c> にし、<see cref="Build"/> で中身の木を返す。
/// 自分では WinUI の部品を増やさず、親の木へ入るのは <see cref="Build"/> が返した根の本物である。当てた <see cref="Style"/> は根へ渡す。
/// 派生のコンストラクタでは、Style や OnLoaded など本物を読む操作を自分へ呼ばない。その場で Build が走るためである。Own は本物を読まないので呼べる。
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

    internal sealed override void ApplyStyle(Style style) => 根.Value.ApplyStyle(style);

    internal sealed override void ThrowIfChildCannotLeave(Node child) =>
        throw new InvalidOperationException($"{GetType().Name} の根は外せない。根の本物は {GetType().Name} の本物そのものである");

    private Node 根を構築する()
    {
        Node 構築した根 = Build();
        AdoptChildPlacedElsewhere(構築した根); // 根の本物は、この部品の本物として親が入れる
        return 構築した根;
    }
}
