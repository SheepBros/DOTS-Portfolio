namespace Portfolio
{
    public class ApplicationController : IApplicationController
    {
        public bool IsQuitting { get; private set; }
        
        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}