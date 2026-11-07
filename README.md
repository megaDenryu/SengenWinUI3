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

## 部品と更新

| 実装済みの部品 | 作る関数と主な操作 |
|---|---|
| `StackPanel` | `StackPanel(style)`・`Children(...)`・`AddChild`・`InsertChild`・`DestroyChildren` |
| `TextBlock` | `TextBlock(text, style)`・`SetText` |
| `Button` | `Button(text, style)` または `Button(content, style)`・`OnClick`・`SetContent` |
| `Image` | `Image(uri, style)` または `Image(uri, new ImageDecodeWidth(width), style)`・`SetSource` |
| `ScrollViewer` | `ScrollViewer(content, style)`・`SetContent`・`ScrollToEnd`・`StickToEnd` |
| `VirtualList<TItem, TRow>` | `VirtualList<TItem, TRow>(createRow, style)`・`SetItems`・`ReplaceItem` など |
| `Flyout` | `Flyout(content)` を `button.Flyout(flyout)` で付ける |
| `Component` | sealed の派生を作り、`Build()` で根を返す。作る関数を使わず `new` で作る |

作る関数の Style 引数は省略でき、後から `.Style(style)` でも当てられる。作る時に決まる条件は、`Children` の引数の `condition ? node : null` で書く。

後の作業で `Grid`・`Border`・`Canvas`、入力と選択の部品、`ListView`、`Popup`・`MenuFlyout`・`ContentDialog`、`FontIcon`・`SymbolIcon`、図形・動きと影を足す。部品の範囲と契約は [設計文書](docs/設計.md) にある。

## 見た目

利用する側は `static readonly Style` に名前を付けて共有し、`With(new Style { ... })` で派生を作る。null の項目は当てず、部品が持たない項目を当てると例外になる。

| 部品 | 共通の幅・高さ・最小最大・Margin・寄せ方・Opacity に追加できる項目 |
|---|---|
| `StackPanel` | Padding・Background・BorderBrush・BorderThickness・CornerRadius・Orientation・Spacing |
| `TextBlock` | Padding・Foreground・FontSize・FontWeight・FontFamily・TextWrapping・IsTextSelectionEnabled |
| `Button`・`ScrollViewer` | Padding・Background・Foreground・BorderBrush・BorderThickness・CornerRadius・FontSize・FontWeight・FontFamily・HorizontalContentAlignment・VerticalContentAlignment |
| `Image`・`VirtualList` | 共通の項目のみ |

長さは `12.Px`、四辺の余白は `new Thickness(8.Px)` で渡す。Style はブラシとフォントを共有するため、同じ Style は1つの UI スレッドだけで使う。

## 寿命と出来事

`Detach()` は部品を今の親から外す。部品は資源を保持するので、利用する側は別の親へ入れ直せる。`Destroy()` は後片付けと子孫の破棄を行い、2回目は何もしない。破棄後の操作は例外になる。

利用する側は `node.Own(resource)`・`node.Own(detachedNode)`・`node.Own(floatingNode)` で資源と部品を預ける。チェーンの戻り値は元の具象型を保つ。利用する側でも解く資源の Dispose は、2回呼んでも害の無い形にする。Component の派生の中では `this.Own(...)` と書く。

`OnLoaded(Action)` と `OnUnloaded(Action)` は、読み込みと取り外しが起きるたびに処理を呼ぶ。同じ部品の同じ出来事への2回目の登録は例外になる。登録は部品の破棄で解く。利用する側は破棄時の後片付けを Own に預ける。

入力と選択の部品を足す際は、`On〜Changed` は利用者が値を変えたときだけ呼び、プログラムからの `Set〜` では呼ばない。

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

層・公開する型の契約・部品の範囲・性能の判断は [docs/設計.md](docs/設計.md) にある。C# の規約は ONE.O.NET のものを使い、読む入口は `.claude/commands/rule.md` である。

## ライセンス

MIT License。[LICENSE](LICENSE) を参照する。
