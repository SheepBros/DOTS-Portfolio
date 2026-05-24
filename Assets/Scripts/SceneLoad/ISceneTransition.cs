using Cysharp.Threading.Tasks;

namespace Portfolio
{
    public interface ISceneTransition
    {
        UniTask LoadScene(ISceneLoadRequest request, ISceneCleaner cleaner);
    }
}