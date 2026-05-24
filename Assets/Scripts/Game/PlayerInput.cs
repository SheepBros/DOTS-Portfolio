using Unity.Mathematics;
using UnityEngine;

namespace Portfolio
{
    public class PlayerInput : IPlayerInput
    {
        private GameStateBridge _gameStateBridge;
        
        private PlayerInputDataBridge _playerInputDataBridge;

        public PlayerInput(GameStateBridge gameStateBridge, PlayerInputDataBridge playerInputDataBridge)
        {
            _gameStateBridge = gameStateBridge;
            _playerInputDataBridge = playerInputDataBridge;
        }
        
        public void Tick()
        {
            if (!_gameStateBridge.TryGet(out GameState state))
            {
                return;
            }

            if (state.IsPaused || state.IsGameOver)
            {
                _playerInputDataBridge.Set(default);
                return;
            }
            
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            float2 move = new float2(horizontal, vertical);
            
            move = math.normalize(move);
            
            Vector3 mouseScreenPosition = Input.mousePosition;
            Vector3 mouseWorldPosition = Vector3.zero;
            
            Camera camera = Camera.main;
            if (camera != null)
            {
                mouseScreenPosition.z = -camera.transform.position.z;
                mouseWorldPosition = camera.ScreenToWorldPoint(mouseScreenPosition);
            }

            _playerInputDataBridge.Set(new PlayerInputData()
            {
                Move = move,
                ShootDirection = new float2(mouseWorldPosition.x, mouseWorldPosition.y),
                IsFirePressed = Input.GetMouseButton(0)
            });
        }
    }
}