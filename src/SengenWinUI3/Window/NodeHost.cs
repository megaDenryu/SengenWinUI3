using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>生の WinUI の木に部品を置く移行用の入れ物。外側のコードは Element を配置し、根の変更と破棄はこの型の操作で行う。</summary>
public sealed class NodeHost : IContentOwner
{
    private readonly WinUI.Controls.ContentPresenter 本物 = new();
    private readonly ContentSlot 根置き場;
    private bool 破棄したか;

    /// <summary>根を1つ預かる。親を持つ部品は渡せない。</summary>
    public NodeHost(Node content)
    {
        根置き場 = new ContentSlot(this, 根の本物 => 本物.Content = 根の本物);
        根置き場.SetContent(content);
    }

    /// <summary>生の Grid の子や Window.Content へ配置する入れ物。中身を直接変更せず SetContent を使う。</summary>
    public WinUI.FrameworkElement Element => 本物;

    /// <summary>根を置き換え、前の根を破棄する。破棄した入れ物には呼べない。</summary>
    public NodeHost SetContent(Node content)
    {
        破棄済みなら例外を投げる();
        根置き場.SetContent(content);
        return this;
    }

    /// <summary>根を破棄して入れ物を空にする。2回目は何もしない。Unloaded では自動で破棄しない。</summary>
    public void Destroy()
    {
        if (破棄したか) return;
        破棄したか = true;
        根置き場.DestroyContent();
    }

    void IChildOwner.AdoptChild(Node child, Action 本物へ入れる)
    {
        破棄済みなら例外を投げる();
        child.BecomeChildOf(this, 本物へ入れる);
    }

    void IChildOwner.ReleaseChild(Node child) => 根置き場.Release(child);

    void IContentOwner.RemoveChildElement(Node child) => 根置き場.Release(child);

    private void 破棄済みなら例外を投げる()
    {
        if (破棄したか) throw new InvalidOperationException("NodeHost は破棄した後に操作された");
    }
}
