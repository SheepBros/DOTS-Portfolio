using System;

namespace Portfolio
{
    public class SceneLoadEvent : IDisposable
    {
        public Action OnPreLoad = null;
        public Action OnPostLoad = null;

        public void Dispose()
        {
            OnPreLoad = null;
            OnPostLoad = null;
        }
    }
}