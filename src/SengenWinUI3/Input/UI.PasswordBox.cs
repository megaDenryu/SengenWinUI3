namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>パスワードの入力欄。 見た目は Style で指定する。</summary>
    public static PasswordBox PasswordBox(string password, string placeholderText = "", Style? style = null) =>
        StyleIfGiven(new PasswordBox(password, placeholderText), style);
}
