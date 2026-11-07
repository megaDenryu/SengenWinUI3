namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>中身の部品を重ねて出す <see cref="SengenWinUI3.Flyout"/> を作る。親を持つ部品を渡すと例外を投げる。</summary>
    public static Flyout Flyout(Node content) => new(content);
}
