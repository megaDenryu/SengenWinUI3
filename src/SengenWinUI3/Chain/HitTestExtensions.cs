namespace SengenWinUI3;

/// <summary>部品がポインターの当たり判定を受けるかを決めるチェーン。元の具象型を保つ。</summary>
public static class HitTestExtensions
{
    /// <summary>false にすると、ポインターの出来事がこの部品に届かず、下の部品へ素通しになる。</summary>
    public static T IsHitTestVisible<T>(this T node, bool isHitTestVisible) where T : Node
    {
        node.ThrowIfDestroyed();
        if (node.Element.IsHitTestVisible != isHitTestVisible)
        {
            node.Element.IsHitTestVisible = isHitTestVisible;
        }
        return node;
    }
}
