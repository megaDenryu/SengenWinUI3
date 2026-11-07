namespace SengenWinUI3;

/// <summary>幅に収まらない文字のはみ出しの切り詰め方。折り返さない(NoWrap)文字に効く。</summary>
public enum TextTrimming
{
    /// <summary>切り詰めず、はみ出した部分を部品の外へ出す。</summary>
    None,
    /// <summary>はみ出した部分を単に切る。</summary>
    Clip,
    /// <summary>はみ出す文字から先を「…」にする。</summary>
    CharacterEllipsis,
    /// <summary>はみ出す語から先を「…」にする。</summary>
    WordEllipsis,
}
