namespace SengenWinUI3;

/// <summary>
/// 出すトーストの内容。種類と文を必ず持ち、詳細・ボタン・消え方・閉じるボタンの有無は <c>With〜</c> で足す。足さない項目は、置き場の既定になる。
/// 値は変えられず、<c>With〜</c> は新しい値を返す。
/// </summary>
public sealed class ToastMessage
{
    private readonly ToastDetail 詳細;
    private readonly ToastButton ボタン;
    private readonly LifetimeChoice 消え方;
    private readonly CloseButtonChoice 閉じるボタン;

    /// <summary>種類と文でトーストの内容を作る。</summary>
    public ToastMessage(ToastKind kind, string text)
        : this(kind, text, new NoToastDetail(), new NoToastButton(), new HostDefaultLifetime(), CloseButtonChoice.HostDefault)
    {
    }

    private ToastMessage(ToastKind kind, string text, ToastDetail detail, ToastButton button, LifetimeChoice lifetime, CloseButtonChoice closeButton)
    {
        ArgumentNullException.ThrowIfNull(text);
        Kind = kind;
        Text = text;
        詳細 = detail;
        ボタン = button;
        消え方 = lifetime;
        閉じるボタン = closeButton;
    }

    /// <summary>種類。</summary>
    public ToastKind Kind { get; }

    /// <summary>文。</summary>
    public string Text { get; }

    internal ToastDetail Detail => 詳細;

    internal ToastButton Button => ボタン;

    internal LifetimeChoice Lifetime => 消え方;

    internal CloseButtonChoice CloseButton => 閉じるボタン;

    /// <summary>詳細の文を足した新しい内容を返す。</summary>
    public ToastMessage WithDetail(string detail)
    {
        ArgumentNullException.ThrowIfNull(detail);
        return new ToastMessage(Kind, Text, new ToastDetailText(detail), ボタン, 消え方, 閉じるボタン);
    }

    /// <summary>ボタンを1つ足した新しい内容を返す。押すと <paramref name="onClick"/> を呼び、トーストを閉じる。</summary>
    public ToastMessage WithButton(string text, Action onClick)
    {
        ArgumentNullException.ThrowIfNull(text);
        ArgumentNullException.ThrowIfNull(onClick);
        return new ToastMessage(Kind, Text, 詳細, new ToastButtonContent(text, onClick), 消え方, 閉じるボタン);
    }

    /// <summary>消え方を決めた新しい内容を返す。</summary>
    public ToastMessage WithLifetime(ToastLifetime lifetime)
    {
        LifetimeChoice 選んだ消え方 = lifetime switch
        {
            TimedLifetime 時間 => 時間,
            UntilClosedLifetime 閉じるまで => 閉じるまで,
        };
        return new ToastMessage(Kind, Text, 詳細, ボタン, 選んだ消え方, 閉じるボタン);
    }

    /// <summary>閉じるボタンを付けるかを決めた新しい内容を返す。</summary>
    public ToastMessage WithCloseButton(bool show) =>
        new(Kind, Text, 詳細, ボタン, 消え方, show ? CloseButtonChoice.Show : CloseButtonChoice.Hide);
}
