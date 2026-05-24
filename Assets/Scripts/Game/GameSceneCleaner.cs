using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Portfolio
{
    public class GameSceneCleaner : ISceneCleaner
    {
        public async UniTask CleanAsync()
        {
            await UniTask.WaitForSeconds(1f, delayTiming: PlayerLoopTiming.FixedUpdate);
        }
    }
}