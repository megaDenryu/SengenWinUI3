namespace SengenWinUI3;

/// <summary>色の動きの途中経過の1点。動きが始まって <see cref="KeyTime"/> が過ぎたときに <see cref="Value"/> の色になり、次の点との間は直線で変わる。</summary>
/// <param name="Value">その時点の色。</param>
/// <param name="KeyTime">動きの始まりからの時間。</param>
public readonly record struct ColorKeyFrame(Color Value, TimeSpan KeyTime);
