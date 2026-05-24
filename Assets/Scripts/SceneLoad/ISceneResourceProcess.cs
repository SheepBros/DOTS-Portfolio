using Cysharp.Threading.Tasks;

namespace Portfolio
{
    public interface ISceneResourceProcess
    {
        UniTask LoadAsync(ISceneLoadRequest request);
    }
}