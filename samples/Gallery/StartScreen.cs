namespace Gallery;

// 起動したときに見せる画面。場面の一覧か、名前で選んだ1つの場面である。
internal union StartScreen(SceneMenuScreen, NamedSceneScreen);

internal sealed record SceneMenuScreen;

internal sealed record NamedSceneScreen(Type 場面の型);
