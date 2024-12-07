using UnityEngine;

namespace DungeonMaster
{
    public class LocationManager : MonoBehaviour
    {
        public Dungeon Dungeon => _dungeon;
        public Lobby Lobby => _lobby;
        public Gym Gym => _gym;
        public PartyView Party => _party;

        [SerializeField] private Dungeon _dungeon;
        [SerializeField] private Lobby _lobby;
        [SerializeField] private Gym _gym;
        [SerializeField] private PartyView _party;

        public void CloseAll()
        {
            _gym.gameObject.SetActive(false);
            _lobby.gameObject.SetActive(false);
            _dungeon.gameObject.SetActive(false);
            _party.gameObject.SetActive(false);
        }

        public void ChangeGameState(GameState gameState)
        {
            CloseAll();
            switch (gameState)
            {
                case GameState.Start:
                    break;
                case GameState.Lobby:
                    _lobby.gameObject.SetActive(true);
                    _party.gameObject.SetActive(true);
                    break;
                case GameState.Gym:
                    _gym.gameObject.SetActive(true);
                    _party.gameObject.SetActive(true);
                    _gym.Init();
                    break;
                case GameState.Dungeon:
                    _dungeon.gameObject.SetActive(true);
                    if (Application.isPlaying) 
                        _dungeon.Init(GameHelper.RuntimeData.DungeonLevel);
                    break;
                case GameState.Shop:
                    _lobby.gameObject.SetActive(true);
                    break;
                case GameState.Lose:
                    _dungeon.gameObject.SetActive(true);
                    break;
                case GameState.Win:
                    _dungeon.gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }
}
