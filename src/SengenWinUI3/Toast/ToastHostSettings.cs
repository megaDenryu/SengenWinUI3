namespace SengenWinUI3;

/// <summary>
/// トースト置き場の設定。既定は、右上・隅から 20・幅 280〜350・間隔 10・新しいものが下・出入り 0.3 秒・消える時間は成功 3 秒・エラー 5 秒・警告 4 秒・情報 3 秒・数の上限なし・閉じるボタンあり。
/// 種類ごとの既定の見た目は色の少ない地味な見た目であり、色は利用する側が <see cref="WithKindStyle"/> で渡す。値は変えられず、<c>With〜</c> は新しい設定を返す。
/// </summary>
public sealed record ToastHostSettings
{
    private static readonly Style 既定の見た目 = new()
    {
        Background = new Color(255, 40, 40, 40),
        BorderBrush = new Color(255, 120, 120, 120),
        Foreground = new Color(255, 240, 240, 240),
    };

    /// <summary>積む隅。</summary>
    public ToastPosition Position { get; init; } = ToastPosition.TopRight;

    /// <summary>積む隅に接する2辺の、ウインドウの縁からの余白。</summary>
    public Px EdgeMargin { get; init; } = 20.Px;

    /// <summary>1件の最小の幅。</summary>
    public Px MinWidth { get; init; } = 280.Px;

    /// <summary>1件の最大の幅。</summary>
    public Px MaxWidth { get; init; } = 350.Px;

    /// <summary>トーストの間隔。</summary>
    public Px Spacing { get; init; } = 10.Px;

    /// <summary>新しいトーストを積む側。</summary>
    public ToastStackOrder StackOrder { get; init; } = ToastStackOrder.NewestAtBottom;

    /// <summary>出入りの動きの時間。</summary>
    public TimeSpan TransitionDuration { get; init; } = TimeSpan.FromSeconds(0.3);

    /// <summary>閉じるボタンの既定。メッセージが決めていればそちらに従う。</summary>
    public bool ShowCloseButton { get; init; } = true;

    internal ToastKindValues<ToastLifetime> Lifetimes { get; init; } = new(
        new TimedLifetime(TimeSpan.FromSeconds(3)),
        new TimedLifetime(TimeSpan.FromSeconds(3)),
        new TimedLifetime(TimeSpan.FromSeconds(4)),
        new TimedLifetime(TimeSpan.FromSeconds(5)));

    internal ToastKindValues<ToastKindStyle> KindStyles { get; init; } = new(
        new ToastKindStyle("OK", 既定の見た目),
        new ToastKindStyle("i", 既定の見た目),
        new ToastKindStyle("!", 既定の見た目),
        new ToastKindStyle("x", 既定の見た目));

    internal ToastCountLimit CountLimit { get; init; } = new UnlimitedToasts();

    /// <summary>積む隅を替えた新しい設定を返す。</summary>
    public ToastHostSettings WithPosition(ToastPosition position) => this with { Position = position };

    /// <summary>積む隅の余白を替えた新しい設定を返す。</summary>
    public ToastHostSettings WithEdgeMargin(Px margin) => this with { EdgeMargin = margin };

    /// <summary>1件の幅の範囲を替えた新しい設定を返す。</summary>
    public ToastHostSettings WithWidth(Px minWidth, Px maxWidth) => this with { MinWidth = minWidth, MaxWidth = maxWidth };

    /// <summary>トーストの間隔を替えた新しい設定を返す。</summary>
    public ToastHostSettings WithSpacing(Px spacing) => this with { Spacing = spacing };

    /// <summary>新しいトーストを積む側を替えた新しい設定を返す。</summary>
    public ToastHostSettings WithStackOrder(ToastStackOrder order) => this with { StackOrder = order };

    /// <summary>出入りの動きの時間を替えた新しい設定を返す。</summary>
    public ToastHostSettings WithTransitionDuration(TimeSpan duration) => this with { TransitionDuration = duration };

    /// <summary>閉じるボタンの既定を替えた新しい設定を返す。</summary>
    public ToastHostSettings WithCloseButton(bool show) => this with { ShowCloseButton = show };

    /// <summary>1つの種類の既定の消え方を替えた新しい設定を返す。</summary>
    public ToastHostSettings WithLifetime(ToastKind kind, ToastLifetime lifetime) => this with { Lifetimes = Lifetimes.With(kind, lifetime) };

    /// <summary>1つの種類の見た目(印と色)を替えた新しい設定を返す。</summary>
    public ToastHostSettings WithKindStyle(ToastKind kind, ToastKindStyle style) => this with { KindStyles = KindStyles.With(kind, style) };

    /// <summary>同時に出すトーストの数の上限を決めた新しい設定を返す。超えたら古いものから閉じる。</summary>
    public ToastHostSettings WithMaxCount(int count)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(count, 1);
        return this with { CountLimit = new LimitedToasts(count) };
    }

    /// <summary>同時に出すトーストの数に上限を決めない新しい設定を返す。</summary>
    public ToastHostSettings WithoutMaxCount() => this with { CountLimit = new UnlimitedToasts() };
}
