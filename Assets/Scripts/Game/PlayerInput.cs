using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Portfolio
{
    public class PlayerInput : IPlayerInput
    {
        private GameStateBridge _gameStateBridge;
        
        private PlayerInputDataBridge _playerInputDataBridge;
        
        private IWorldProvider _worldProvider;

        public PlayerInput(GameStateBridge gameStateBridge, PlayerInputDataBridge playerInputDataBridge,
            IWorldProvider worldProvider)
        {
            _gameStateBridge = gameStateBridge;
            _playerInputDataBridge = playerInputDataBridge;
            _worldProvider = worldProvider;
        }
        
        public void Tick()
        {
            if (!_worldProvider.IsWorldAlive)
            {
                return;
            }
            
            if (!_gameStateBridge.TryGet(out GameState state))
            {
                return;
            }

            if (state.IsPaused || state.IsGameOver)
            {
                _playerInputDataBridge.Set(default);
                return;
            }
            
            Keyboard keyboard = Keyboard.current;
            float2 move = float2.zero;

            if (keyboard != null)
            {
                if (keyboard.wKey.isPressed)
                {
                    move.y += 1f;
                }
                
                if (keyboard.sKey.isPressed)
                {
                    move.y -= 1f;
                }
                
                if (keyboard.dKey.isPressed)
                {
                    move.x += 1f;
                }
                
                if (keyboard.aKey.isPressed)
                {
                    move.x -= 1f;
                }
            }
            
            if (math.lengthsq(move) > 0.0001f)
            {
                move = math.normalize(move);
            }
            
            Mouse mouse = Mouse.current;
            float2 mouseWorldPosition = float2.zero;
            bool isFirePressed = false;

            if (mouse != null)
            {
                Vector2 mouseScreenPosition = mouse.position.ReadValue();
                
                Camera camera = Camera.main;
                if (camera != null)
                {
                    Vector3 screenPosition = new Vector3(mouseScreenPosition.x,
                        mouseScreenPosition.y, -camera.transform.position.z);
                    Vector3 worldPosition = camera.ScreenToWorldPoint(screenPosition);
                    mouseWorldPosition = new float2(worldPosition.x, worldPosition.y);
                }
                
                isFirePressed = mouse.leftButton.isPressed;
            }

            _playerInputDataBridge.Set(new PlayerInputData()
            {
                Move = move,
                ShootDirection = mouseWorldPosition,
                IsFirePressed = isFirePressed
            });
        }
    }
}