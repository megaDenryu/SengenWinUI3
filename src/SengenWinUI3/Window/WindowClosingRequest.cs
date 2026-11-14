namespace SengenWinUI3;

/// <summary>ウインドウが閉じようとしていることの知らせ。<see cref="SecondaryWindow.OnClosing"/> の処理が受け、<see cref="Cancel"/> で閉じるのを取り消せる。</summary>
public sealed class WindowClosingRequest
{
    private readonly Action 取り消す;

    internal WindowClosingRequest(Action 取り消す)
    {
        this.取り消す = 取り消す;
    }

    /// <summary>閉じるのを取り消す。ウインドウは閉じずに残る。</summary>
    public void Cancel() => 取り消す();
}
