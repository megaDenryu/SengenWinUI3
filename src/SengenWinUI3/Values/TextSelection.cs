namespace SengenWinUI3;

/// <summary>入力欄の文字の選択範囲。長さが 0 なら、範囲でなくカーソルの位置だけを表す。位置は 0 始まりで、文字の並びの先頭から数える。</summary>
public readonly record struct TextSelection(int Start, int Length)
{
    /// <summary>範囲を持たない、カーソルだけの位置。</summary>
    public static TextSelection CaretAt(int index) => new(index, 0);

    /// <summary>カーソルだけで、範囲を選んでいないか。</summary>
    public bool IsCaret => Length == 0;

    /// <summary>選択の終わりの次の位置。カーソルだけなら <see cref="Start"/> と同じである。</summary>
    public int End => Start + Length;
}
