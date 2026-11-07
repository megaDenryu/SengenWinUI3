namespace SengenWinUI3;

// 出した1件のトーストの状態。UI スレッドだけが触る(別のスレッドからの操作は、置き場が UI スレッドへ送る)。
// まだ画面に出ていない間の SetText・SetDetail・Close も受け、出すときに反映する。
internal sealed class ToastEntry(ToastMessage message, ToastHostSettings settings, ToastLayer layer, Action<ToastEntry> closingStarted)
{
    private enum Phase
    {
        NotShownYet,
        Shown,
        Closing,
        Closed,
    }

    private Phase 段階 = Phase.NotShownYet;
    private string 文 = message.Text;
    private ToastDetail 詳細 = message.Detail;
    private ToastItemView? 画面; // 出したときだけある
    private ToastExpiry? 時計; // 出したときだけある

    // 出す。出す前に閉じられていたら出さず、false を返す。
    internal bool Show()
    {
        if (段階 != Phase.NotShownYet)
        {
            return false;
        }
        bool 閉じるボタンを付ける = message.CloseButton switch
        {
            CloseButtonChoice.Show => true,
            CloseButtonChoice.Hide => false,
            CloseButtonChoice.HostDefault => settings.ShowCloseButton,
            _ => throw new InvalidOperationException($"知らない閉じるボタンの選び方 {message.CloseButton}"),
        };
        ToastItemView 出す画面 = new(message, 文, 詳細, settings, 閉じるボタンを付ける, Close);
        画面 = 出す画面;
        layer.Add(出す画面.Element);
        if (layer.IsOpen)
        {
            ToastMotion.SlideIn(出す画面.Element, layer.SlideDistance, settings.TransitionDuration);
        }
        時計 = new ToastExpiry();
        時計.StartFor(消え方を決める(), Close);
        段階 = Phase.Shown;
        return true;
    }

    internal void SetText(string text)
    {
        文 = text;
        画面?.SetText(text);
    }

    internal void SetDetail(string detail)
    {
        詳細 = new ToastDetailText(detail);
        画面?.SetDetail(詳細);
    }

    internal void Close()
    {
        switch (段階)
        {
            case Phase.NotShownYet:
                段階 = Phase.Closed;
                break;
            case Phase.Shown:
                閉じ始める();
                break;
        }
    }

    // 置き場を閉じるときに、動きを待たずに片付ける。
    internal void Abandon()
    {
        時計?.Stop();
        段階 = Phase.Closed;
    }

    private ToastLifetime 消え方を決める() => message.Lifetime switch
    {
        TimedLifetime 時間 => 時間,
        UntilClosedLifetime 閉じるまで => 閉じるまで,
        HostDefaultLifetime => settings.Lifetimes.Of(message.Kind),
    };

    private void 閉じ始める()
    {
        段階 = Phase.Closing;
        時計?.Stop();
        closingStarted(this);
        if (画面 is not { } 閉じる画面)
        {
            return;
        }
        if (layer.IsOpen)
        {
            ToastMotion.SlideOut(閉じる画面.Element, layer.SlideDistance, settings.TransitionDuration, () => 取り除く(閉じる画面));
            return;
        }
        取り除く(閉じる画面);
    }

    private void 取り除く(ToastItemView 取り除く画面)
    {
        layer.Remove(取り除く画面.Element);
        段階 = Phase.Closed;
    }
}
