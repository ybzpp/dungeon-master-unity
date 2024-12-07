using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonMaster
{
    public class LoseScreen : ElementUI
    {
        [SerializeField] private Button _respawnButton;
        [SerializeField] private Button _lobbyButton;
        [SerializeField] private TMP_Text _rewardText;

        private void OnEnable()
        {
            RewardTextUpdate(GameHelper.RuntimeData.Reward);
        }

        private void Start()
        {
            _respawnButton.onClick.AddListener(Respawn);
            _lobbyButton.onClick.AddListener(Lobby);
        }

        private void OnDestroy()
        {
            _respawnButton.onClick.RemoveListener(Respawn);
            _lobbyButton.onClick.RemoveListener(Lobby);
        }

        public void RewardTextUpdate(Reward reward)
        {
            _rewardText.text = TextHelper.RewardTextConvert(reward);
        }

        public void Respawn()
        {
            GameHelper.NewEntity.Get<RespawnEvent>();
        }

        public void Lobby()
        {
            GameHelper.ChangeGameState(GameState.Lobby);
            GameHelper.NewEntity.Get<DestroyAllBoysEvent>();
        }
    }
}
