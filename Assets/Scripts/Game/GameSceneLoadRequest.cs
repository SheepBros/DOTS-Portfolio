namespace Portfolio
{
    public class GameSceneLoadRequest : ISceneLoadRequest
    {
        public string SceneName => StringConst.GameScene;
        
        public ISceneResourceLoader CreateResourceLoader()
        {
            return new GameSceneResourceLoader();
        }
    }
}