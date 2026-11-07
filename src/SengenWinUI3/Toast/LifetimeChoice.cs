namespace SengenWinUI3;

// メッセージが持つ消え方。足していなければ置き場の種類ごとの既定にする。
internal union LifetimeChoice(HostDefaultLifetime, TimedLifetime, UntilClosedLifetime);

internal sealed record HostDefaultLifetime;
