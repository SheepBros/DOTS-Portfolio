namespace Portfolio
{
    /// <summary>
    /// 씬을 불러오는데 필요한 파라미터 가진 인터페이스.
    /// 씬에 필요한 로딩 요소
    /// </summary>
    public interface ISceneLoadRequest
    {
        string SceneName { get; }
    }
}