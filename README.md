# SengenWinUI3

WinUI 3 の部品を、C# のコードで構築・配線・更新するための部品のライブラリである。XAML で部品の木を書かず、データバインディングも使わない。利用する側は、部品の木を C# の式で組み(構築)、操作と処理をつなぎ(配線)、表示を変えるときは更新の操作を明示的に呼ぶ(更新)。

同じ考え方のライブラリに、TypeScript(DOM)版の SengenUI と、Rust(egui)版の [SengenEgui](https://github.com/megaDenryu/SengenEgui) がある。

## 状態

部品はまだ無い。このライブラリは、先に想像で部品を作らず、利用する側(OneOnet のネイティブ版 [ONE.O.NET](https://github.com/megaDenryu/ONE.O.NET))で同じ形の組み立てが繰り返し現れたときに、その形を切り出して足していく。使い方は、最初の部品を足したときに書く。

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
