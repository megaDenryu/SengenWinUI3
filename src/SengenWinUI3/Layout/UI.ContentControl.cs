namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>中身を1つ持つ <see cref="SengenWinUI3.ContentControl"/> を作る。親を持つ部品を渡すと例外を投げる。</summary>
    public static ContentControl ContentControl(Node content, Style? style = null) => StyleIfGiven(new ContentControl(content), style);
}
