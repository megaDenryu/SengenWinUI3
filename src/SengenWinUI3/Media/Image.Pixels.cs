using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Graphics.Imaging;
using WinUI = Microsoft.UI.Xaml;

namespace SengenWinUI3;

// 画素から作る画像の元を見せる部分。出どころを Uri で渡す部分は Image.cs にある。
public sealed partial class Image
{
    internal Image() : this(new ImageOriginalSize())
    {
    }

    internal Image(BgraPixels pixels) : this() => Set画素(pixels);

    /// <summary>画像を画素から作って見せる。前の画像は置き換える。</summary>
    public Image SetSource(BgraPixels pixels)
    {
        ThrowIfDestroyed();
        Set画素(pixels);
        return this;
    }

    /// <summary>
    /// <paramref name="bitmap"/> を画像として見せる。画像への取り込みを待つ間に、渡した <paramref name="bitmap"/> は使われる。
    /// 取り込みが済めば画像は <paramref name="bitmap"/> を持たないので、呼び出し側が <paramref name="bitmap"/> を破棄してよい。
    /// </summary>
    public async Task SetSourceAsync(SoftwareBitmap bitmap)
    {
        ThrowIfDestroyed();
        WinUI.Media.Imaging.SoftwareBitmapSource 取り込み先 = new();
        await 取り込み先.SetBitmapAsync(bitmap);
        if (IsDestroyed)
        {
            取り込み先.Dispose();
            throw new InvalidOperationException("画像の取り込みを待つ間に、画像の部品が破棄された");
        }
        本物.Source = 取り込み先;
    }

    private void Set画素(BgraPixels pixels)
    {
        if (pixels.Width <= 0 || pixels.Height <= 0)
        {
            throw new ArgumentException($"画素の幅 {pixels.Width} と高さ {pixels.Height} は1以上でなければならない", nameof(pixels));
        }
        if (pixels.PremultipliedBytes.Length != checked(pixels.Width * pixels.Height * 4))
        {
            throw new ArgumentException($"画素のバイト列の長さ {pixels.PremultipliedBytes.Length} が、幅 {pixels.Width} × 高さ {pixels.Height} × 4 と合わない", nameof(pixels));
        }
        WinUI.Media.Imaging.WriteableBitmap 絵 = new(pixels.Width, pixels.Height);
        using (Stream 書き込み先 = 絵.PixelBuffer.AsStream())
        {
            書き込み先.Write(pixels.PremultipliedBytes.Span);
        }
        絵.Invalidate();
        本物.Source = 絵;
    }
}
