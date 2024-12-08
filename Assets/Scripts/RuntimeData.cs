namespace DungeonMaster
{
    [System.Serializable]
    public class RuntimeData
    {
        public GameState GameState;
        public int DungeonLevel;
        public int RerollCount;
        public Reward Reward;
    }

    [System.Serializable]
    public class Reward
    {
        public int Coins;
        public int Exp;
    }

    public enum GameState
    {
        Lobby,
        Gym,
        Dungeon,
        Start,
        Shop,
        Lose,
        Win,
    }

    public enum GameStateResult
    {
        Continue,
        Win,
        Lose
    }
}