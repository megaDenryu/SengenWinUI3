namespace SengenWinUI3;

public static partial class UI
{
    /// <summary>日付を選ぶ CalendarDatePicker を作る。日付の範囲と暦は WinUI の既定に従う。</summary>
    public static CalendarDatePicker CalendarDatePicker(DateSelection date, Style? style = null) =>
        StyleIfGiven(new CalendarDatePicker(date), style);
}
