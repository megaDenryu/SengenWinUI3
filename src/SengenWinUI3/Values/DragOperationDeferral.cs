using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>
/// ドロップされたデータを非同期に読み終えるまで、ドラッグの元に待たせる札。<see cref="Complete"/> を呼ぶと待たせるのをやめる。
/// 名前は WinUI の DragOperationDeferral にそろえる。
/// </summary>
public sealed class DragOperationDeferral
{
    private readonly WinUI.DragOperationDeferral 本物;

    internal DragOperationDeferral(WinUI.DragOperationDeferral 本物) => this.本物 = 本物;

    /// <summary>ドラッグの元を待たせるのをやめる。</summary>
    public void Complete() => 本物.Complete();
}
