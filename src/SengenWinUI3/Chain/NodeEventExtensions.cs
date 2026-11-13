namespace SengenWinUI3;

/// <summary>全部の部品の出来事。同じ出来事への2回目の登録は例外になり、部品の破棄で登録を解く。</summary>
public static class NodeEventExtensions
{
    /// <summary>部品の大きさが変わったときに処理を呼ぶ。</summary>
    public static T OnSizeChanged<T>(this T node, Action<ViewSize> handler) where T : Node =>
        node.WireEvent(nameof(OnSizeChanged), NodeEvents.SizeChanged, 出来事 => handler(new ViewSize(new Px(出来事.NewSize.Width), new Px(出来事.NewSize.Height))));

    /// <summary>部品かその中にフォーカスがある間にキーが押されたときに処理を呼ぶ。</summary>
    public static T OnKeyDown<T>(this T node, Action<KeyInput> handler) where T : Node =>
        node.WireEvent(nameof(OnKeyDown), NodeEvents.KeyDown, 出来事 => handler(new KeyInput(出来事)));

    /// <summary>
    /// 部品かその中にフォーカスがある間にキーが離されたときに処理を呼ぶ。<paramref name="handledEventsToo"/> を true にすると、
    /// 子の部品が処理済みにした出来事も受ける。
    /// </summary>
    public static T OnKeyUp<T>(this T node, Action<KeyInput> handler, bool handledEventsToo = false) where T : Node =>
        node.WireEvent(nameof(OnKeyUp), handledEventsToo ? NodeHandledEvents.KeyUp : NodeEvents.KeyUp, 出来事 => handler(new KeyInput(出来事)));

    /// <summary>
    /// 部品かその中にフォーカスがあるとき、キーを子の部品が処理する前に処理を呼ぶ。<see cref="KeyInput.Handled"/> を true にすると、
    /// 子の部品の既定の処理(複数行の入力欄の Enter の改行・矢印のカーソルの移動・Tab のフォーカスの移動)と <see cref="OnKeyDown{T}"/> は起きない。
    /// </summary>
    public static T OnPreviewKeyDown<T>(this T node, Action<KeyInput> handler) where T : Node =>
        node.WireEvent(nameof(OnPreviewKeyDown), NodeEvents.PreviewKeyDown, 出来事 => handler(new KeyInput(出来事)));

    /// <summary>ポインターが部品の上に入ったときに処理を呼ぶ。</summary>
    public static T OnPointerEntered<T>(this T node, Action<PointerInput> handler) where T : Node =>
        node.WireEvent(nameof(OnPointerEntered), NodeEvents.PointerEntered, 出来事 => handler(new PointerInput(出来事, node.Element)));

    /// <summary>ポインターが部品の上から出たときに処理を呼ぶ。</summary>
    public static T OnPointerExited<T>(this T node, Action<PointerInput> handler) where T : Node =>
        node.WireEvent(nameof(OnPointerExited), NodeEvents.PointerExited, 出来事 => handler(new PointerInput(出来事, node.Element)));

    /// <summary>
    /// 部品の上でボタンが押されたか、指やペンが触れたときに処理を呼ぶ。<paramref name="handledEventsToo"/> を true にすると、
    /// ボタンのような子の部品が処理済みにした出来事も受ける。
    /// </summary>
    public static T OnPointerPressed<T>(this T node, Action<PointerInput> handler, bool handledEventsToo = false) where T : Node =>
        node.WireEvent(nameof(OnPointerPressed), handledEventsToo ? NodeHandledEvents.PointerPressed : NodeEvents.PointerPressed, 出来事 => handler(new PointerInput(出来事, node.Element)));

    /// <summary>
    /// 部品の上でボタンが離されたか、指やペンが離れたときに処理を呼ぶ。<paramref name="handledEventsToo"/> を true にすると、
    /// 子の部品が処理済みにした出来事も受ける。
    /// </summary>
    public static T OnPointerReleased<T>(this T node, Action<PointerInput> handler, bool handledEventsToo = false) where T : Node =>
        node.WireEvent(nameof(OnPointerReleased), handledEventsToo ? NodeHandledEvents.PointerReleased : NodeEvents.PointerReleased, 出来事 => handler(new PointerInput(出来事, node.Element)));

    /// <summary>
    /// 部品がポインターの捕まえを失ったときに処理を呼ぶ。<paramref name="handledEventsToo"/> を true にすると、
    /// 子の部品が処理済みにした出来事も受ける。
    /// </summary>
    public static T OnPointerCaptureLost<T>(this T node, Action<PointerInput> handler, bool handledEventsToo = false) where T : Node =>
        node.WireEvent(nameof(OnPointerCaptureLost), handledEventsToo ? NodeHandledEvents.PointerCaptureLost : NodeEvents.PointerCaptureLost, 出来事 => handler(new PointerInput(出来事, node.Element)));

    /// <summary>
    /// システムがポインターの操作を取り消したときに処理を呼ぶ。<paramref name="handledEventsToo"/> を true にすると、
    /// 子の部品が処理済みにした出来事も受ける。
    /// </summary>
    public static T OnPointerCanceled<T>(this T node, Action<PointerInput> handler, bool handledEventsToo = false) where T : Node =>
        node.WireEvent(nameof(OnPointerCanceled), handledEventsToo ? NodeHandledEvents.PointerCanceled : NodeEvents.PointerCanceled, 出来事 => handler(new PointerInput(出来事, node.Element)));

    /// <summary>ポインターのホイールが回ったときに処理を呼ぶ。回転の量は <see cref="PointerInput.MouseWheelDelta"/> で読む。</summary>
    public static T OnPointerWheelChanged<T>(this T node, Action<PointerInput> handler) where T : Node =>
        node.WireEvent(nameof(OnPointerWheelChanged), NodeEvents.PointerWheelChanged, 出来事 => handler(new PointerInput(出来事, node.Element)));

    /// <summary>部品が左で押されて離されたとき(クリックかタップ)に処理を呼ぶ。</summary>
    public static T OnTapped<T>(this T node, Action<TapInput> handler) where T : Node =>
        node.WireEvent(nameof(OnTapped), NodeEvents.Tapped, 出来事 => handler(new TapInput(出来事)));

    /// <summary>部品が右で押されて離されたとき(右クリックか長押し)に処理を呼ぶ。</summary>
    public static T OnRightTapped<T>(this T node, Action<RightTapInput> handler) where T : Node =>
        node.WireEvent(nameof(OnRightTapped), NodeEvents.RightTapped, 出来事 => handler(new RightTapInput(出来事)));

    /// <summary>ポインターが部品の上で動いたときに処理を呼ぶ。捕まえている間は、部品の外で動いたときも呼ぶ。</summary>
    public static T OnPointerMoved<T>(this T node, Action<PointerInput> handler) where T : Node =>
        node.WireEvent(nameof(OnPointerMoved), NodeEvents.PointerMoved, 出来事 => handler(new PointerInput(出来事, node.Element)));
}
