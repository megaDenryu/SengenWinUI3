# SengenWinUI3

WinUI 3 の部品を、C# のコードで構築・配線・更新するための部品のライブラリである。XAML で部品の木を書かず、データバインディングも使わない。利用する側は、部品の木を C# の式で組み(構築)、操作と処理をつなぎ(配線)、表示を変えるときは更新の操作を明示的に呼ぶ(更新)。

同じ考え方のライブラリに、TypeScript(DOM)版の SengenUI と、Rust(egui)版の [SengenEgui](https://github.com/megaDenryu/SengenEgui) がある。

## 状態

このライブラリは、WinUI 3 の部品の薄い包みとして一般に必要な関数と型を、利用する側(OneOnet のネイティブ版 [ONE.O.NET](https://github.com/megaDenryu/ONE.O.NET))から逆算せずにそろえる。それより後に足りないと分かったものは、利用する側で使われた形から足す。

| 部品 | 作るもの |
|---|---|
| `文章.From文字(文字)` | 画面の幅で折り返し、利用者が選んで写せる文章(`TextBlock`) |
| `見出し.From文字(文字)` | 画面の名前を示す見出し(`TextBlock`) |
| `小見出し.From文字(文字)` | 画面の中の1つのまとまりの頭に置く、見出しより1段小さい見出し(`TextBlock`) |
| `横の並び.From部品(部品…)` | ボタンの組のように、1行に横へ並べる並び(`StackPanel`) |
| `縦の並び.From部品(部品…)` | 入力の欄や文章のまとまりを、上から下へ並べる並び(`StackPanel`) |
| `縦にスクロールする画面.From部品(部品…)` | 部品を縦に並べて読みやすい幅で左に寄せ、収まらない分をスクロールで見せる画面(`ScrollViewer`) |

## 使い方

部品は WinUI 3 の部品をそのまま返す。返した部品の設定を後から変えてもよく、WinUI 3 の部品と入れ子にしてもよい。

```csharp
using SengenWinUI3;

var 画面 = 縦にスクロールする画面.From部品(
    見出し.From文字("キャラクターを直す"),
    文章.From文字("保存するまでファイルに書かない。"),
    横の並び.From部品(保存のボタン, やめるボタン));
```

部品の並びを条件で変えるときは、配列の式で組む。WinUI 3 の解析器(CsWinRT1032)は、`IEnumerable` へ渡す集まりの式を止めるため、並びの部品は配列を受け取る。

```csharp
縦にスクロールする画面.From部品([見出し.From文字("最初のキャラクターを作る"), .. 足りないものの部品List]);
```

## ビルド

ビルドには、`global.json` で固定した .NET SDK(.NET 11 RC2)を導入する。

```powershell
# 実行場所: リポジトリのルート
dotnet build SengenWinUI3.sln --no-incremental
```

## 規約

C# の規約は ONE.O.NET のものを使い、このリポジトリにはコピーを持たない。読む入口は `.claude/commands/rule.md` である。

## ライセンス

MIT License。[LICENSE](LICENSE) を見る。
