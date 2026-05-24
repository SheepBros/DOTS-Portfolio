using Cysharp.Threading.Tasks;

namespace Portfolio
{
    /// <summary>
    /// 씬에 사용될 데이터 / 로직 초기화를 처리하는 인터페이스.
    /// </summary>
    public interface ISceneResourceLoader
    {
        UniTask Load();
    }
}