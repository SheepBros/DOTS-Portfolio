using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Portfolio
{
    public class TitleSceneResourceProcess : ISceneResourceProcess
    {
        public UniTask LoadAsync(ISceneLoadRequest request)
        {
            Debug.Log("Loading Title Scene Resources...");
            return UniTask.CompletedTask;
        }
    }
}