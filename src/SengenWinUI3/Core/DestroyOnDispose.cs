namespace SengenWinUI3;

// 部品の破棄で一緒に破棄するもの(Own で預けた部品・部品に付けた浮かぶ部品)を、購読の解除と同じ一覧に入れるための包み。
internal sealed class DestroyOnDispose(Action 破棄する) : IDisposable
{
    public void Dispose() => 破棄する();
}
