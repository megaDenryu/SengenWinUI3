namespace SengenWinUI3;

// 1つの出来事のネイティブハンドラと、その登録の寿命を組にする。
internal sealed class EventBinding<TElement, THandler, TValue>
{
    private readonly Func<TElement, Action<TValue>, THandler> ハンドラを作る;
    private readonly Func<TElement, THandler, IDisposable> ハンドラを登録する;

    internal EventBinding(Func<TElement, Action<TValue>, THandler> createHandler, Action<TElement, THandler> subscribe, Action<TElement, THandler> unsubscribe)
        : this(createHandler, (本物, ハンドラ) =>
        {
            subscribe(本物, ハンドラ);
            return new CallOnDispose(() => unsubscribe(本物, ハンドラ));
        }) { }

    // 登録時に返る札で解除する出来事にも使う。
    internal EventBinding(Func<TElement, Action<TValue>, THandler> createHandler, Func<TElement, THandler, IDisposable> subscribe)
    {
        ハンドラを作る = createHandler;
        ハンドラを登録する = subscribe;
    }

    internal IDisposable Subscribe(TElement element, Action<TValue> handler) => ハンドラを登録する(element, ハンドラを作る(element, handler));
}
