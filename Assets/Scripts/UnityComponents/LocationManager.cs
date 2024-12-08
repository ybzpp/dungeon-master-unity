using System.Collections.Generic;
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

        private Config _config;
        private RuntimeData _runtimeData;

        private readonly Dictionary<GameState, List<IView>> _stateToLocations = new();

        public void Init(Config config, RuntimeData runtimeData)
        {
            _config = config;
            _runtimeData = runtimeData;

            _stateToLocations[GameState.Lobby] = new List<IView> { _lobby, _party };
            _stateToLocations[GameState.Gym] = new List<IView> { _gym, _party };
            _stateToLocations[GameState.Dungeon] = new List<IView> { _dungeon };
            _stateToLocations[GameState.Shop] = new List<IView> { _lobby };
            _stateToLocations[GameState.Lose] = new List<IView> { _dungeon };
            _stateToLocations[GameState.Win] = new List<IView> { _dungeon };
        }

        public void CloseAll()
        {
            foreach (var locations in _stateToLocations.Values)
            {
                foreach (var location in locations)
                {
                    location.Hide();
                }
            }
        }

        public void ChangeGameState(GameState gameState)
        {
            CloseAll();

            if (_stateToLocations.TryGetValue(gameState, out var locations))
            {
                foreach (var location in locations)
                {
                    location.Show();
                    location.Init(_config, _runtimeData);
                }
            }
            else
            {
                Debug.LogError($"No locations for game state: {gameState}");
            }
        }
    }
}
