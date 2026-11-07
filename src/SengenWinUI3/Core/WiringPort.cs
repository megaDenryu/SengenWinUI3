namespace SengenWinUI3;

/// <summary>
/// 部品が、構築の後にコンポジションルートから1回だけ受け取る配線(処理の組)を持つ。
/// 配線の2回目と、配線する前に配線先を読むことは、到達したらバグである状態として例外を投げる。
/// </summary>
public sealed class WiringPort<TWiring>(string ownerName) where TWiring : class
{
    private TWiring? 配線;

    /// <summary>配線を受け取る。2回目は例外を投げる。</summary>
    public void Wire(TWiring wiring)
    {
        if (配線 is not null)
        {
            throw new InvalidOperationException($"{ownerName} は配線済みである。Wire はコンポジションルートから1回だけ呼べる");
        }
        配線 = wiring;
    }

    /// <summary>配線先。配線する前に読むと例外を投げる。</summary>
    public TWiring Target => 配線 ?? throw new InvalidOperationException($"{ownerName} が配線されないまま操作された。コンポジションルートで Wire を呼ぶ");
}
