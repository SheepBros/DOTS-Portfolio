using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Portfolio
{
    public class LoadingView : MonoBehaviour, ILoadingView
    {
        [SerializeField]
        private GameObject _loadingObject;

        [Inject]
        void Bind()
        {
            Hide().Forget();
        }

        public async UniTask Show()
        {
            _loadingObject.SetActive(true);

            await UniTask.WaitForSeconds(1f, delayTiming: PlayerLoopTiming.FixedUpdate);
        }

        public async UniTask Hide()
        {
            await UniTask.WaitForSeconds(0.5f, delayTiming: PlayerLoopTiming.FixedUpdate);
            
            _loadingObject.SetActive(false);
        }
    }
}