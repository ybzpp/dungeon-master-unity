using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Leopotam.Ecs;

namespace DungeonMaster
{
    public class LobbyScreen : ElementUI
    {
        [SerializeField] private TMP_Text _boysCountText;
        [SerializeField] private Button _gymButton;
        [SerializeField] private Button _dungeonButton;
        [SerializeField] private Button _shopButton;

        private void OnEnable()
        {
            UpdateBoysCount(BoysService.Boys.Count, GameHelper.Config.MaxPartySize);
        }

        private void Start()
        {
            _gymButton.onClick.AddListener(ShowGym);
            _dungeonButton.onClick.AddListener(ShowDungeon);
            _shopButton.onClick.AddListener(ShowShop);

        }

        private void OnDestroy()
        {
            _gymButton.onClick.RemoveListener(ShowGym);
            _dungeonButton.onClick.RemoveListener(ShowDungeon);
            _shopButton.onClick.RemoveListener(ShowShop);
        }

        public void UpdateBoysCount(int value, int max)
        {
            _boysCountText.text = $"{value}/{max}";
            _boysCountText.color = value == 0 ? TextHelper.RedColor : Color.white;
        }

        public void ShowGym()
        {
            GameHelper.ChangeGameState(GameState.Gym);
        }

        public void ShowShop()
        {
            GameHelper.ChangeGameState(GameState.Shop);
        }

        public void ShowDungeon()
        {
            GameHelper.NewEntity.Get<StartDungeonEvent>();
        }
    }
}
