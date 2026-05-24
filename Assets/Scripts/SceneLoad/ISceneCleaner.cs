using Cysharp.Threading.Tasks;

namespace Portfolio
{
    /// <summary>
    /// 씬이 로딩 될 때 기존 씬을 정리하는 인터페이스.
    /// </summary>
    public interface ISceneCleaner
    {
        UniTask CleanAsync();
    }
}