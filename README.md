# SengenWinUI3

WinUI 3 の部品を、C# のコードで構築・配線・更新するための部品のライブラリである。XAML で部品の木を書かず、データバインディングも使わない。利用する側は、部品の木を C# の式で組み(構築)、操作と処理をつなぎ(配線)、表示を変えるときは更新の操作を明示的に呼ぶ(更新)。

同じ考え方のライブラリに、TypeScript(DOM)版の SengenUI と、Rust(egui)版の [SengenEgui](https://github.com/megaDenryu/SengenEgui) がある。

## 状態

このライブラリは、先に想像で部品を作らず、利用する側(OneOnet のネイティブ版 [ONE.O.NET](https://github.com/megaDenryu/ONE.O.NET))で同じ形の組み立てが繰り返し現れたときに、その形を切り出して足していく。今ある部品は、ONE.O.NET の画面で2回以上同じ値で組んでいた、文と見出しと並びの形である。

| 部品 | 作るもの |
|---|---|
| `文.From本文(本文)` | 画面の幅で折り返し、利用者が選んで写せる文(`TextBlock`) |
| `見出し.From本文(本文)` | 画面の名前を示す見出し(`TextBlock`) |
| `小見出し.From本文(本文)` | 画面の中の1つのまとまりの頭に置く、見出しより1段小さい見出し(`TextBlock`) |
| `横の並び.From部品(部品…)` | ボタンの組のように、1行に横へ並べる並び(`StackPanel`) |
| `縦の並び.From部品(部品…)` | 入力の欄や文のまとまりを、上から下へ並べる並び(`StackPanel`) |
| `縦に流れる画面.From部品(部品…)` | 部品を縦に並べて読みやすい幅で左に寄せ、収まらない分をスクロールで見せる画面(`ScrollViewer`) |

## 使い方

部品は WinUI 3 の部品をそのまま返す。返した部品の設定を後から変えてもよく、WinUI 3 の部品と入れ子にしてもよい。

```csharp
using SengenWinUI3;

var 画面 = 縦に流れる画面.From部品(
    見出し.From本文("キャラクターを直す"),
    文.From本文("保存するまでファイルに書かない。"),
    横の並び.From部品(保存のボタン, やめるボタン));
```

部品の並びを条件で変えるときは、配列の式で組む。WinUI 3 の解析器(CsWinRT1032)は、`IEnumerable` へ渡す集まりの式を止めるため、並びの部品は配列を受け取る。

```csharp
縦に流れる画面.From部品([見出し.From本文("最初のキャラクターを作る"), .. 足りないもののまとまり]);
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
