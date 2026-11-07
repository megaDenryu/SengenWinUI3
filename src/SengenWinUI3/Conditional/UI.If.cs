using SengenWinUI3.State;

namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>
    /// <paramref name="condition"/> が真の間は <paramref name="whenTrue"/> を、偽の間は <paramref name="whenFalse"/> を見せる <see cref="ConditionalPresenter"/> を作る。
    /// <paramref name="whenFalse"/> を省くと、偽の間は何も見せない。親を持つ部品を渡すと例外を投げる。
    /// </summary>
    public static ConditionalPresenter If(IReadOnlyCell<bool> condition, Node whenTrue, Node? whenFalse = null) => new(condition, whenTrue, whenFalse);

    /// <summary>
    /// 部品の代わりに作る関数を受ける <see cref="ConditionalPresenter"/> を作る。その側が初めて見えることになったときに1回だけ関数を呼ぶので、めったに見せない重い部品を起動の時に作らずに済む。
    /// </summary>
    public static ConditionalPresenter If(IReadOnlyCell<bool> condition, Func<Node> createWhenTrue, Func<Node>? createWhenFalse = null) =>
        new(condition, createWhenTrue, createWhenFalse);
}
