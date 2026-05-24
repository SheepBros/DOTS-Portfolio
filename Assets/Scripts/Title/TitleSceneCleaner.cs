using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Portfolio
{
    public class TitleSceneCleaner : ISceneCleaner
    {
        public UniTask CleanAsync()
        {
            Debug.Log("Cleaning Title Scene");
            return UniTask.CompletedTask;
        }
    }
}