namespace Portfolio
{
    public class TitleSceneLoadRequest : ISceneLoadRequest
    {
        public string SceneName => StringConst.TitleScene;
        
        public ISceneResourceLoader CreateResourceLoader()
        {
            return new TitleSceneResourceLoader();
        }
    }
}