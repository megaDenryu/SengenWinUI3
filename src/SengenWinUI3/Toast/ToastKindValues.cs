namespace SengenWinUI3;

// 種類ごとに1つずつ持つ値の組。設定の「種類ごとの消える時間」と「種類ごとの見た目」に使う。
internal readonly record struct ToastKindValues<T>(T Success, T Info, T Warning, T Error)
{
    internal T Of(ToastKind kind) => kind switch
    {
        ToastKind.Success => Success,
        ToastKind.Info => Info,
        ToastKind.Warning => Warning,
        ToastKind.Error => Error,
        _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, null),
    };

    internal ToastKindValues<T> With(ToastKind kind, T value) => kind switch
    {
        ToastKind.Success => this with { Success = value },
        ToastKind.Info => this with { Info = value },
        ToastKind.Warning => this with { Warning = value },
        ToastKind.Error => this with { Error = value },
        _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, null),
    };
}
