# SengenWinUI3

SengenWinUI3 は、WinUI 3 の部品を C# の式で構築・配線・更新するための薄いライブラリである。利用する側は部品の木を組み、出来事を処理へつなぎ、`Set〜` を呼んで表示を更新する。

核は部品の型と作る関数・型を保つチェーン・`Style`・値の型・親子と破棄を持つ。値の入れ物・購読と結ぶ仕組み・UI スレッドへ送る仕組み・実行中の条件表示・トースト・ウインドウごとの状態は、利用する側が自分で書く。

同じ考え方のライブラリに TypeScript(DOM)版の SengenUI と、Rust(egui)版の [SengenEgui](https://github.com/megaDenryu/SengenEgui) がある。

## 使い方

利用する側は、`using SengenWinUI3;` と `using static SengenWinUI3.UI;` で型と作る関数を取り込む。関数は自前の型で部品を返し、本物の WinUI の部品は公開しない。すべての公開の操作は UI スレッドで呼ぶ。

```csharp
using SengenWinUI3;
using static SengenWinUI3.UI;

sealed class EditScreen : Component
{
    private static readonly Style 画面の見た目 = new()
    {
        Padding = new Thickness(16.Px),
        Spacing = 12.Px,
    };
    private readonly TextBlock 結果 = TextBlock("保存前");

    protected override Node Build() => StackPanel(画面の見た目).Children(
        TextBlock("キャラクターを編集する"),
        結果,
        Button("保存").OnClick(保存する));

    private void 保存する() => 結果.SetText("保存した");
}
```

アプリの `Window` を持つファイルでは、`window.SetContent(new EditScreen())` で根を置く。2回目の `SetContent` では前の根を破棄し、ウインドウが閉じたときも根を破棄する。

Component の派生のコンストラクタでは、自分へ OnLoaded・OnUnloaded・OnSizeChanged・Style など本物を読む操作を呼ばない。その場で Build が走るためである。利用する側は構築した後のチェーンか、Build で作る根や子で登録する。Own と IsDestroyed は本物を読まない。

## 生の WinUI の画面へ置く

移行中の生の WinUI の木へ部品を置くときは NodeHost を使う。NodeHost は根を1つ持ち、Element で入れ物だけを公開する。次の例の window はアプリの Window である。

```csharp
var 文章 = TextBlock("移行中の画面");
var 入れ物 = new NodeHost(文章);
var 生の格子 = new Microsoft.UI.Xaml.Controls.Grid();
生の格子.Children.Add(入れ物.Element);
window.Content = 生の格子;
window.Closed += (_, _) => 入れ物.Destroy();
```

利用する側は SetContent(Node) で根を置き換え、Destroy() で根を破棄する。一時的な Unloaded では根を自動で破棄しない。利用する側は入れ物の Content を直接変更せず、この2つの操作を使う。生の型は完全修飾名か名前空間の別名で書き、SengenWinUI3 と同じ名前の型を衝突させない。

WinUI の型を公開の署名へ出すのは、移行の境目となる NodeHost と、Window を受ける WindowExtensions だけである。後の Popup.Open に渡す基準には、入れ物の中の文章のような Node を使える。Popup は基準が読み込み済みなら今開き、未読み込みなら内部で読み込みを待つ。同じ基準で Close と Open を何度でも行える契約である。Popup の位置は開いたときに1回だけ決まり、開いている間に基準が動いても付いていかない。

## 部品と更新

| 実装済みの部品 | 作る関数と主な操作 |
|---|---|
| `StackPanel` | `StackPanel(style)`・`Children(...)`・`AddChild`・`InsertChild`・`DestroyChildren` |
| `TextBlock` | `TextBlock(text, style)`・`SetText` |
| `Button` | `Button(text, style)` または `Button(content, style)`・`OnClick`・`SetContent`・`SetColors(ButtonColors)` |
| `Image` | `Image(uri, style)` または `Image(uri, new ImageDecodeWidth(width), style)`・`SetSource`。画素から作る元として `Image(BgraPixels, style)`・`SetSource(BgraPixels)`(`BgraPixels` は幅・高さと、アルファを掛けた BGRA のバイト列)、元をまだ持たない `Image(style)` もある |
| `ScrollViewer` | `ScrollViewer(content, style)`・`SetContent`・`ScrollToEnd`・`StickToEnd` |
| `VirtualList<TItem, TRow>` | `VirtualList<TItem, TRow>(createRow, style)`・`SetItems`・`ReplaceItem` など |
| `Grid` | `Grid(style)`・`Grid(rowDefinitions, columnDefinitions, style)`・`Children`・`AddChild`・`InsertChild`・`DestroyChildren`・`SetRowDefinitions`・`SetColumnDefinitions`(作るときと同じ数の `GridLength[]` で行の高さと列の幅を実行中に替える。数が違えば例外) |
| `Border` | `Border(content, style)`・`SetContent` |
| `Canvas` | `Canvas(style)`・`Children`・`AddChild`・`InsertChild`・`DestroyChildren` |
| `ContentControl` | `ContentControl(content, style)`・`SetContent`。中身を置くだけの Control で、フォーカスを受け、Tab キーで選べる |
| `LayoutPanel` | `LayoutPanel(layout, style)`・`Children`・`AddChild`・`InsertChild`・`DestroyChildren`。子の測り方と置き方を `NonVirtualizingLayout`(利用する側が派生して `MeasureOverride`・`ArrangeOverride` を書く。子は `NonVirtualizingLayoutContext` の `ChildCount`・`Measure(index, availableSize)`・`DesiredSize(index)`・`Arrange(index, rect)` で測り、置く)に任せる |
| `TextBox` | `TextBox(text, placeholderText, acceptsReturn, textWrapping, style)`・`SetText`・`Text`(今の文字を読む)・`OnTextChanged`・`SetColors(TextBoxColors)`・`Selection`(`TextSelection`。カーソルの位置は `Start`)・`SetSelection(selection)`(変更を通知しない)・`IsComposing`(日本語入力の変換中なら true) |
| `PasswordBox` | `PasswordBox(password, placeholderText, style)`・`SetPassword`・`OnPasswordChanged` |
| `NumberBox` | `NumberBox(value, minimum, maximum, style)`・`SetValue`・`OnValueChanged` |
| `Slider` | `Slider(value, minimum, maximum, stepFrequency, style)`・`SetValue`・`Value`(今の値を読む)・`OnValueChanged`・`SetSmallChange`・`SetLargeChange`・`IsThumbToolTipEnabled`・`SetColors(SliderColors)` |
| `CheckBox` | `CheckBox(isChecked, content, style)`・`SetIsChecked`・`OnCheckedChanged` |
| `ToggleButton` | `ToggleButton(isChecked, content, style)`・`SetIsChecked`・`OnCheckedChanged`・`SetColors(ToggleButtonColors)` |
| `ToggleSwitch` | `ToggleSwitch(isOn, header, style)`・`SetIsOn`・`OnToggled` |
| `HyperlinkButton` | `HyperlinkButton(content, style)` または `HyperlinkButton(content, navigateUri, style)`・`SetContent`・`OnClick` |
| `ListView` | `ListView(children, selectedIndex, style)`・`SetSelectedIndex`・`OnSelectionChanged`。`children` は `Node?[]` で受ける |
| `ComboBox` | `ComboBox(children, selectedIndex, style)`・`SetSelectedIndex`・`OnSelectionChanged` |
| `RadioButtons` | `RadioButtons(children, selectedIndex, style)`・`SetSelectedIndex`・`OnSelectionChanged` |
| `FontIcon` | `FontIcon(glyph, style)`・`SetGlyph` |
| `SymbolIcon` | `SymbolIcon(symbol, style)`・`SetSymbol` |
| `ProgressBar` | `ProgressBar(progress, style)`・`SetProgress` |
| `ProgressRing` | `ProgressRing(isActive, progress, style)`・`SetProgress`・`SetIsActive` |
| `Rectangle` | `Rectangle(style)`・`SetDropShadow(ColorShadow)`・`ClearDropShadow()`・`BeginFillColorAnimation(duration, keyFrames)`(`ColorKeyFrame(Color, KeyTime)` の列を直線でつないだ塗りの色を止めるまで繰り返す。この長方形専用のブラシを持つので `Style` の共有ブラシの色は変わらない)・`StopFillColorAnimation()`。角の丸みは Style の `CornerRadius`。ただし四隅が別の値の `PerCornerRadius` は例外になる |
| `Ellipse` | `Ellipse(style)`。固有の操作は無い |
| `Path` | `Path(PathData, style)`・`SetData`。`PathData` は SVG の道の文字列 |
| `Viewbox` | `Viewbox(content, style)`・`SetContent`。中身を縦横比を保って自分の大きさに合わせる |
| `Line` | `Line(start, end, style)`・`SetStart`・`SetEnd`(`start` と `end` は `Point`) |
| `CalendarDatePicker` | `CalendarDatePicker(date, style)`・`SetDate`・`OnDateChanged` |
| `ColorPicker` | `ColorPicker(color, style)`・`SetColor`・`OnColorChanged` |
| `Popup` | `Popup(content, offset, isLightDismissEnabled)`(`offset` は `Point`)または `PopupAbove(content, isLightDismissEnabled)`(基準の上へ左端をそろえて出す)・`Open(anchor)`・`Close`・`SetContent` |
| `ContentDialog` | `ContentDialog(title, content, buttons)`(`buttons` は `ContentDialogButtons` で、`Primary`・`Secondary`・`Close` を名前付きで渡す)・`ShowAsync(anchor)`・`Hide`・`SetTitle`・`SetContent` |
| `Flyout` | `Flyout(content)` か `MenuFlyout(...)` を `button.Flyout(...)` で付ける |
| `MenuFlyout` | `MenuFlyout(params MenuFlyoutEntry[] items)`・`Hide`。`ContextFlyout(menu)` で右クリックの形にも付ける |
| `Component` | sealed の派生を作り、`Build()` で根を返す。作る関数を使わず `new` で作る |
| `NodeHost` | `new NodeHost(content)`・`Element`・`SetContent`・`Destroy`。生の木へ置く入れ物 |

作る関数の Style 引数は省略でき、後から `.Style(style)` でも当てられる。作る時に決まる条件は、`Children` の引数の `condition ? node : null` で書く。

チェーンは型を保つ拡張メソッドで付ける。配置は、Grid の中の位置を `GridRow`・`GridColumn`・`GridRowSpan`・`GridColumnSpan` で、Canvas の中の位置を `CanvasLeft`・`CanvasTop` で書く。どの部品にも、右クリックで開くメニューを `ContextFlyout(menu)` で、文字の説明を `ToolTip(text)` で付けられる。押せる部品(`Button`・`HyperlinkButton`)は、`button.Flyout(flyout)` か `button.Flyout(menu)` で、押したら開く浮かぶ部品を付ける。影と動きは、`Shadow(depth)`・`NoShadow()`・`OpacityTransition(duration)`・`TranslationTransition(duration)`・`ScaleTransition(duration)`・`RotationTransition(duration)`・`Translation(offset)`(`offset` は `Point`)・`Scale(x, y)`・`Rotation(angle)`(`angle` は `Degrees`。`90.Degrees` と書く)・`CenterPoint(center)`(`center` は `Point`)で付ける。不透明度を繰り返し上下させるのは `PulseOpacity(lowest, period)`(`lowest` は `Ratio`)と `StopPulse()` である。キーは `OnKeyDown`(子が処理した後)と `OnPreviewKeyDown`(子が処理する前。`Handled` を立てると子の既定の処理が起きない)、フォーカスは `OnGotFocus`・`OnLostFocus`、ポインターは `OnPointerMoved`・`OnPointerPressed`・`OnPointerReleased`・`OnPointerCaptureLost`・`OnPointerCanceled`・`OnPointerWheelChanged`・`CapturePointer(input)`・`ReleasePointerCapture(input)`・`ReleasePointerCaptures()` で扱い、タップは `OnTapped`・`OnRightTapped`、キーの離しは `OnKeyUp` で受ける。

`OnKeyUp`・`OnPointerPressed`・`OnPointerReleased`・`OnPointerCaptureLost`・`OnPointerCanceled` は、`handledEventsToo: true` を渡すと、子の部品が処理済みにした出来事も受ける。同じ部品の同じ名前の出来事への登録は、この引数の値が違っても1回だけである。`PointerInput` は、出来事を受けた部品の左上からの `X`・`Y` と、ボタンの変化の種類 `UpdateKind`(`PointerUpdateKind`)・ホイールの量 `MouseWheelDelta`・別の部品の左上からの位置 `GetPosition(node)` を持つ。`KeyInput` の `IsOriginalSource(node)` は、キーの出来事を最初に受けたのが `node` 自身かを返す。

そのほかの共通のチェーンは次のとおりである。`Cursor(InputSystemCursorShape)` はポインターが上にあるときの OS のカーソルを決める。`Clip(rect)` は表示を `Rect` で切り抜く。`IsHitTestVisible(bool)` は当たり判定を受けるかを決め、`IsTabStop(bool)`(Control の部品)は Tab キーで止まるかを決める。`UpdateLayout()` は配置の計算をその場で終わらせる。`IsLoaded()` は部品が木に読み込まれているか(WinUI の `IsLoaded`)を返す。`IsTextBoxFocused()` は、部品の載っている画面でフォーカスが TextBox にあるかを返す。`OnKeyboardAcceleratorInvoked(key, modifiers, handler)` はキーの組み合わせ(`VirtualKeyModifiers` は `None`・`Control`・`Menu`・`Shift`・`Windows` の組み合わせ)を受け、`KeyboardAcceleratorInput.Handled` を立てると他へ伝えない。ドラッグ&ドロップは、`AllowDrop(bool)` と `OnDragOver`・`OnDrop` で受ける。`DragInput` は `ContainsStorageItems`・`SetAcceptedOperation(DataPackageOperation)`・`GetDeferral()`(読み終えたら `DragOperationDeferral.Complete()`)・`GetStorageItemsAsync()`(ドロップされた項目を、場所を持つファイル `DroppedFile`(`FileInfo` を持つ)かそれ以外 `DroppedOtherItem` かを区別した `DroppedItem` の並びで返す)を持つ。

状態ごとの色(ふだん・重ねた・押した・押せない)を持つ `Button`・`ToggleButton`・`TextBox`・`Slider` は、`SetColors` で WinUI の lightweight styling の資源を書く。`TextBoxColors` の状態ごとの色は、背景・枠線に加えて文字の `Foreground` と、何も入力していないときの文字の `PlaceholderForeground` を持つ。資源はテンプレートが読み込みのときに1回引くので、`SetColors` は木へ入れる前に呼び、読み込まれた後に呼ぶと例外になる。

部品の範囲と契約は [設計文書](docs/設計.md) にある。

## 見た目

利用する側は `static readonly Style` に名前を付けて共有し、`With(new Style { ... })` で派生を作る。null の項目は当てず、部品が持たない項目を当てると例外になる。

| 部品 | 共通の幅・高さ・最小最大・Margin・寄せ方・Opacity に追加できる項目 |
|---|---|
| `StackPanel` | Padding・Background・BorderBrush・BorderThickness・CornerRadius・Orientation・Spacing |
| `LayoutPanel` | Background |
| `TextBlock` | Padding・Foreground・FontSize・FontWeight・FontFamily・TextWrapping・IsTextSelectionEnabled・TextAlignment・TextTrimming・LineHeight |
| `Button`・`ScrollViewer`・`ContentControl` | Padding・Background・Foreground・BorderBrush・BorderThickness・CornerRadius・FontSize・FontWeight・FontFamily・HorizontalContentAlignment・VerticalContentAlignment |
| `Grid` | Padding・Background・BorderBrush・BorderThickness・CornerRadius・RowSpacing・ColumnSpacing |
| `Border` | Padding・Background・BorderBrush・BorderThickness・CornerRadius |
| `Canvas` | Background |
| `ListView`・`ComboBox`・`RadioButtons`・`TextBox`・`PasswordBox`・`NumberBox`・`Slider`・`CheckBox`・`ToggleButton`・`ToggleSwitch`・`HyperlinkButton`・`ProgressBar`・`ProgressRing`・`CalendarDatePicker`・`ColorPicker` | Padding・Background・Foreground・BorderBrush・BorderThickness・CornerRadius・FontSize・FontWeight・FontFamily・HorizontalContentAlignment・VerticalContentAlignment |
| `FontIcon` | Foreground・FontSize・FontWeight・FontFamily |
| `SymbolIcon` | Foreground。大きさは Width・Height で指定する |
| `Rectangle`・`Ellipse`・`Line`・`Path` | Fill・Stroke・StrokeThickness・StrokeLineCap・StrokeLineJoin。`Rectangle` はさらに CornerRadius |
| `Image`・`VirtualList`・`Viewbox` | 共通の項目のみ |

浮かぶ部品の `Popup`・`ContentDialog`・`MenuFlyout` は Style を受けない。`Popup` の中身の部品には Style を当てられる。

長さは `12.Px`、四辺の余白は `new Thickness(8.Px)` で渡す。`Style` の `CornerRadius` は四隅が同じなら `8.Px`、四隅が別なら `new PerCornerRadius(TopLeft, TopRight, BottomRight, BottomLeft)`(左上・右上・右下・左下の順)で渡す。切り抜きの範囲と `NonVirtualizingLayoutContext.Arrange` で子を置く範囲は `Rect(X, Y, Width, Height)` で渡す。色の項目の `Brush` は `Color` のほか、`LinearGradientBrush(StartPoint, EndPoint, GradientStops)` を受ける。`StartPoint` と `EndPoint` は塗る図形の外接する長方形の中の割合の位置 `RelativePoint(Ratio X, Ratio Y)`(左上が (0, 0)、右下が (1, 1))、`GradientStop` は `Color` と `Ratio` の `Offset` の組である。横と縦の2つの長さの組は `Point` の値で渡す(`new Point(0.Px, 40.Px)`)。Style はブラシとフォントを共有するため、同じ Style は1つの UI スレッドだけで使う。

## 寿命と出来事

`Detach()` は部品を今の親から外す。部品は資源を保持するので、利用する側は別の親へ入れ直せる。`Destroy()` は、親から外し、預けた資源を登録順に解き、OnDestroying、子孫の破棄の順に行う。2回目の Destroy は何もしない。破棄後の操作は例外になる。

利用する側は `node.Own(resource)`・`node.Own(detachedNode)`・`node.Own(floatingNode)` で資源と部品を預ける。チェーンの戻り値は元の具象型を保つ。利用する側でも解く資源の Dispose は、2回呼んでも害の無い形にする。Component の派生の中では `this.Own(...)` と書く。

Own(Node) は、預ける時点で親の無い部品を受ける。その後は預け先と同じ寿命の木へ出し入れでき、預け先の破棄時には現在の親から外して破棄する。UI スレッドへ送った反映の処理では、利用する側は Node の公開の IsDestroyed を読んで、破棄済みなら反映しない。

`OnLoaded(Action)` と `OnUnloaded(Action)` は、読み込みと取り外しが起きるたびに処理を呼ぶ。同じ部品の同じ出来事への2回目の登録は例外になる。登録は部品の破棄で解く。利用する側は破棄時の後片付けを Own に預ける。

入力と選択の部品を足す際は、On〜Changed は利用者が値を変えたときだけ呼ぶ。部品は最後にプログラムが Set〜 で設定した値を保持し、現在値がその値と等しい出来事は、後から届いた場合も通知しない。内側の共通型 InputValue と、本物の現在値を読み直す契約を使う。

## 行の使い回しと見本

件数に上限の無い一覧は `VirtualList` で書く。行は Component と `IRecyclableRow<TItem>` を実装し、`Show(item)` で子の Set〜 を呼ぶ。行の作者は、項目に結び付いた資源を `Release()` で解く。核は行の購読を持たない。

VirtualList はスクロールを持たないので、利用する側は ScrollViewer の中に置く。ScrollViewer には高さの決まる配置が必要であり、StackPanel の中では高さが無限になって使い回しが効かない場合がある。

`samples/Gallery/Scenes/VirtualListScene.cs` は、10000件の表示と項目の置き換えを示す。Gallery の場面の一覧は Scene の派生型を自動で列挙する。

## ビルド

ビルドには `global.json` で固定した .NET SDK を使う。

```powershell
# 実行場所: リポジトリのルート
dotnet build SengenWinUI3.sln --no-incremental
```

## 設計と規約

層・公開する型の契約・部品の範囲・性能の判断は [docs/設計.md](docs/設計.md) にある。C# の規約を読む入口は `.claude/commands/rule.md` である。

## ライセンス

MIT License。[LICENSE](LICENSE) を参照する。
