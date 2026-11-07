using SengenWinUI3;

namespace Gallery;

// 見本のアプリの場面の基底。場面を足すときは、Scenes/ に Scene の sealed の派生を1つのファイルで足すだけでよい(SceneCatalog が型から見つける)。
// 派生は主コンストラクタで SceneRun を受け、Build で画面を返し、自動で確かめる手順を Run.RunSteps に渡す。
internal abstract class Scene : Component
{
    protected Scene(SceneRun 場面の進め方)
    {
        Run = 場面の進め方;
        this.Own(場面の進め方.ReportOnDispose($"{GetType().Name} の根を破棄した"));
    }

    protected SceneRun Run { get; }
}
