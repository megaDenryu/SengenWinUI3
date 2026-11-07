using Windows.Foundation;
using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

/// <summary>日付を選ぶ部品。未選択は NoDate、選択した日は SelectedDate で渡す。</summary>
public sealed class CalendarDatePicker : Node, IControlNode
{
    private static readonly EventBinding<WinUI.Controls.CalendarDatePicker, TypedEventHandler<WinUI.Controls.CalendarDatePicker, WinUI.Controls.CalendarDatePickerDateChangedEventArgs>, EventArgs> 日付の出来事 = new(
        static (本物, 出来事を渡す) => (送信元, 出来事) => 出来事を渡す(EventArgs.Empty),
        static (本物, ハンドラ) => 本物.DateChanged += ハンドラ,
        static (本物, ハンドラ) => 本物.DateChanged -= ハンドラ);

    private readonly WinUI.Controls.CalendarDatePicker 本物 = new();
    private readonly InputValue<DateSelection> 日付;

    internal CalendarDatePicker(DateSelection date)
    {
        日付 = InputValue.Create(本物, date,
            static (本物, 値) => DateSelection.FromWinUI(本物.Date).Equals(値),
            static (本物, 値) => 本物.Date = 値.ToWinUI());
    }

    internal override WinUI.FrameworkElement Element => 本物;

    WinUI.Controls.Control IControlNode.Control => 本物;

    /// <summary>選んだ日を替えるか、未選択へ戻す。この操作では OnDateChanged を呼ばない。</summary>
    public CalendarDatePicker SetDate(DateSelection date)
    {
        ThrowIfDestroyed();
        日付.Set(date);
        return this;
    }

    /// <summary>利用者が選んだ日を知らせる。最後の設定値と等しい現在値は知らせない。2回目の登録は例外を投げる。</summary>
    public CalendarDatePicker OnDateChanged(Action<DateSelection> handler) =>
        this.WireEvent(nameof(OnDateChanged), 本物, 日付の出来事, () => 日付を知らせる(handler));

    internal override void ApplyStyle(Style style) => ControlStyle.All.Apply(style, 本物, this);

    private void 日付を知らせる(Action<DateSelection> 選んだ日付を受ける)
    {
        DateSelection 現在値 = DateSelection.FromWinUI(本物.Date);
        if (日付.IsUserChange(現在値)) 選んだ日付を受ける(現在値);
    }
}
