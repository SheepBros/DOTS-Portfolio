using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Portfolio
{
    public class GameSceneCleaner : ISceneCleaner
    {
        public UniTask CleanAsync()
        {
            Debug.Log("Cleaning Game Scene");
            return UniTask.CompletedTask;
        }
    }
}