namespace SengenWinUI3;

/// <summary>ダイアログのボタンを隠すか、指定した文字で表示するかを表す。</summary>
public union ContentDialogButton(HiddenDialogButton, DialogButtonText)
{
    internal string ToWinUI() => this switch
    {
        HiddenDialogButton => string.Empty,
        DialogButtonText 表示 => 表示.Text,
    };
}
