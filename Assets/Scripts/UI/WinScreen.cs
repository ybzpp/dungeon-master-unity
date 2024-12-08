﻿using Leopotam.Ecs;
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
        private RuntimeData _runtimeData;

        private void OnEnable()
        {
            RewardTextUpdate(_runtimeData.Reward);
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
            GameHelper.NewEntity.Get<ContinueDungeonEvent>();
            GameHelper.ChangeGameState(GameState.Dungeon);
        }

        public void Lobby()
        {
            GameHelper.ChangeGameState(GameState.Lobby);
        }
        public override void Init(Config config, RuntimeData runtimeData)
        {
            base.Init(config, runtimeData);
            _runtimeData = runtimeData;
        }
    }
}
