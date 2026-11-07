namespace Gallery;

// 見本のアプリの場面の一覧。Scene の派生の型を、このアセンブリから探す。場面を足すときに、一覧を書き換えずに済ませるためである。
internal static class SceneCatalog
{
    internal static IReadOnlyList<Type> SceneTypes { get; } =
        typeof(SceneCatalog).Assembly.GetTypes().Where(static 型 => 型.IsSubclassOf(typeof(Scene)) && !型.IsAbstract).OrderBy(static 型 => 型.Name, StringComparer.Ordinal).ToArray();

    internal static Type FindType(string 場面の名前) =>
        SceneTypes.FirstOrDefault(型 => 型.Name == 場面の名前)
            ?? throw new ArgumentException($"場面 {場面の名前} が無い。ある場面: {string.Join("・", SceneTypes.Select(static 型 => 型.Name))}");

    internal static Scene Create(Type 場面の型, SceneRun 場面の進め方) =>
        Activator.CreateInstance(場面の型, 場面の進め方) is Scene 場面
            ? 場面
            : throw new InvalidOperationException($"{場面の型.Name} は SceneRun を受けるコンストラクタを持つ Scene の派生でない");
}
