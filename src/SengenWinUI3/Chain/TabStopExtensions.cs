namespace SengenWinUI3;

/// <summary>本物が Control の部品(<see cref="IControlNode"/>)を、Tab キーによる移動の停止位置に含めるかを決めるチェーン。</summary>
public static class TabStopExtensions
{
    /// <summary>true にすると、Tab キーでフォーカスを移すときにこの部品に止まる。</summary>
    public static T IsTabStop<T>(this T node, bool isTabStop) where T : Node, IControlNode
    {
        node.ThrowIfDestroyed();
        if (node.Control.IsTabStop != isTabStop)
        {
            node.Control.IsTabStop = isTabStop;
        }
        return node;
    }
}
