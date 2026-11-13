namespace SengenWinUI3;

/// <summary>部品が今、木に読み込まれているかを調べるチェーン。</summary>
public static class LoadedStateExtensions
{
    /// <summary>部品が木に読み込まれて、使える状態か(WinUI の IsLoaded)。部品を別の場所へ付け替える間は、取り外しの出来事より先に新しい場所の読み込みが届くことがあり、そのとき取り外しの出来事の中でも true になる。</summary>
    public static bool IsLoaded(this Node node)
    {
        node.ThrowIfDestroyed();
        return node.Element.IsLoaded;
    }
}
