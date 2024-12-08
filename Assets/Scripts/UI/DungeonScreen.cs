using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Leopotam.Ecs;

namespace DungeonMaster
{
    public class DungeonScreen : ElementUI
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _startBattleButton;
        [SerializeField] private Image[] _levelImages;
        [SerializeField] private Transform _currentLevelImage;
        [SerializeField] private TMP_Text _levelText;

        private void OnEnable()
        {
            _startBattleButton.gameObject.SetActive(true);
        }

        private void Start()
        {
            _backButton.onClick.AddListener(BackToLobby);
            _startBattleButton.onClick.AddListener(StartBattle);
        }

        private void OnDestroy()
        {
            _backButton.onClick.RemoveAllListeners();
            _startBattleButton.onClick.RemoveAllListeners();
        }

        public void BackToLobby()
        {
            GameHelper.ChangeGameState(GameState.Lobby);
        }

        public void StartBattle()
        {
            GameHelper.NewEntity.Get<StartBattleEvent>();
            _startBattleButton.gameObject.SetActive(false);
        }

        public void LevelImagesUpdate(int level)
        {
            if (level < _levelImages.Length)
            {
                for (int i = 0; i < _levelImages.Length; i++)
                {
                    _levelImages[i].color = level > i ? Color.green : Color.white;
                }
                _currentLevelImage.position = _levelImages[level].transform.position;
            }

            _levelText.text = $"level {level + 1}";
        }
    }
}
