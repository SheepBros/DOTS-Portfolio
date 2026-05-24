using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Portfolio
{
    public class TitleSceneResourceProcess : ISceneResourceProcess
    {
        public async UniTask LoadAsync(ISceneLoadRequest request)
        {
            Debug.Log("Loading Title Scene Resources...");
        }
    }
}