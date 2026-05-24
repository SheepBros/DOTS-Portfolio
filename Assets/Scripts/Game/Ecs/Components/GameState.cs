using Unity.Entities;

namespace Portfolio
{
    public struct GameState : IComponentData
    {
        public int Health;
        public int Score;
        public bool IsPaused;
        public bool IsGameOver;
    }
}