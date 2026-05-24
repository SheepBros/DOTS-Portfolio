using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Portfolio
{
    public class GameSceneResourceLoader : ISceneResourceLoader
    {
        public UniTask Load()
        {
            Debug.Log("Loading Game Scene Resources...");
            return UniTask.CompletedTask;
        }
    }
}