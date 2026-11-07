namespace SengenWinUI3;

/// <summary>日付の未選択と、選択した日を区別する。</summary>
public union DateSelection(NoDate, SelectedDate)
{
    // 日付だけを渡すので、夏時間の切り替えが多い深夜を避け、ローカル時刻の正午へ写す。
    internal DateTimeOffset? ToWinUI() => this switch
    {
        NoDate _ => null,
        SelectedDate 日 => new DateTimeOffset(日.Date.ToDateTime(new TimeOnly(12, 0), DateTimeKind.Local)),
    };

    internal static DateSelection FromWinUI(DateTimeOffset? 日時) =>
        日時 is { } 日 ? new SelectedDate(DateOnly.FromDateTime(日.DateTime)) : new NoDate();
}
