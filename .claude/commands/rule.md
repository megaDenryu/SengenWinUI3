# 役割と触る領域ごとに読むファイル

タスクに着手する前に、次の表の該当する行のファイルを全文読む。つまみ食いや要約読みをしない。表の行は足し算であり、「どの役も」の行に、触る領域の行を足して読む。触らない領域の行は読まない。

`~/.claude/規約/` は全プロジェクト共通の規約である。`~/.claude` が無い PC では、先に `~/.claude` のリポジトリを clone する。

C# の規約は ONE.O.NET(https://github.com/megaDenryu/ONE.O.NET)の `.claude/規約/コード/` にあり、このリポジトリはコピーを持たない。読み手は、このリポジトリと同じ親のフォルダに ONE.O.NET を clone し、下の表の相対パス(`../ONE.O.NET/...`)で読む。手元に無ければ、GitHub の同じパスを読む。

読み終えたら確認を返さず、そのままタスク本体に着手して完遂する。

| 役割・触る領域 | 読むファイル |
|---|---|
| どの役も | `~/.claude/規約/文章と語.md`。名前か文章を書く役は、あわせて `~/.claude/shared-knowledge/deprecated_words.csv` |
| コードを書く・検査する(C#) | `../ONE.O.NET/.claude/規約/コード/coding_rule.md`、`../ONE.O.NET/.claude/規約/コード/csharp_coding_rules.md`、`../ONE.O.NET/.claude/規約/コード/判別共用体の失敗パターン.md`、`~/.claude/規約/コード/設計.md`、`~/.claude/規約/コード/分割とコメント.md`、`~/.claude/規約/命名.md` |
| テストを書く・直す | `~/.claude/規約/テスト方法の選定.md` |
| 文書だけ書く | `~/.claude/skills/doc-writing/SKILL.md`、`README.md` |
| 検収・レビューをする | 検収する対象の行に加えて `~/.claude/規約/レビュー.md` |
| 親のセッション(将軍) | `.claude/規約/将軍.md` |
