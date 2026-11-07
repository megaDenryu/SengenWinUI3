namespace SengenWinUI3;

/// <summary>全部の部品へ後片付けを預けるチェーン。受けた部品をそのままの型で返す。</summary>
public static class NodeLifetimeExtensions
{
    /// <summary>部品の破棄で資源を登録順に Dispose し、その後に OnDestroying と子孫の破棄を行う。利用側でも解く資源は、Dispose を2回呼んでも害の無い形にする。</summary>
    public static T Own<T>(this T node, IDisposable resource) where T : Node
    {
        node.OwnDisposable(resource);
        return node;
    }

    /// <summary>預ける時点で親が無い部品を預ける。その後は同じ寿命の木へ出し入れでき、預け先の破棄時には現在の親から外して破棄する。</summary>
    public static T Own<T>(this T node, Node detachedNode) where T : Node
    {
        node.OwnDisposable(new CallOnDispose(detachedNode.Destroy));
        return node;
    }

    /// <summary>浮かぶ部品を預け、この部品の破棄で一緒に破棄する。</summary>
    public static T Own<T>(this T node, FloatingNode floatingNode) where T : Node
    {
        node.OwnDisposable(new CallOnDispose(floatingNode.Destroy));
        return node;
    }
}
