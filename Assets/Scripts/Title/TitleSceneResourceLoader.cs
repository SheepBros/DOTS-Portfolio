using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Portfolio
{
    public class TitleSceneResourceLoader : ISceneResourceLoader
    {
        public UniTask Load()
        {
            Debug.Log("Loading Title Scene Resources...");
            return UniTask.CompletedTask;
        }
    }
}