using Cysharp.Threading.Tasks;

namespace Portfolio
{
    /// <summary>
    /// Scene을 로드하는 인터페이스.
    /// </summary>
    public interface ISceneLoader
    {
        bool IsLoading { get; }
        
        UniTask LoadScene(string sceneName);
    }
}