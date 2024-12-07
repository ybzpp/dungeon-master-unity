using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonMaster
{
    public class WinScreen : ElementUI
    {
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _lobbyButton;
        [SerializeField] private TMP_Text _rewardText;

        private void OnEnable()
        {
            RewardTextUpdate(GameHelper.RuntimeData.Reward);
        }

        private void Start()
        {
            _nextButton.onClick.AddListener(Next);
            _lobbyButton.onClick.AddListener(Lobby);
        }

        private void OnDestroy()
        {
            _nextButton.onClick.RemoveListener(Next);
            _lobbyButton.onClick.RemoveListener(Lobby);
        }

        public void RewardTextUpdate(Reward reward)
        {
            _rewardText.text = TextHelper.RewardTextConvert(reward);
        }

        public void Next()
        {
            GameHelper.NextDungeonRoom();
            GameHelper.ChangeGameState(GameState.Dungeon);
        }

        public void Lobby()
        {
            GameHelper.ChangeGameState(GameState.Lobby);
        }
    }
}
