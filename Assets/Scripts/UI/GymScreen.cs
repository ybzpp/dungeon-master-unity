using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Leopotam.Ecs;

namespace DungeonMaster
{
    public class GymScreen : ElementUI
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _rerollButton;
        [SerializeField] private TMP_Text _rerollPriceText;
        [SerializeField] private GymBuyPopup _gymBuyPopup;

        private void OnEnable()
        {
            _gymBuyPopup.Hide();
            RerollPriceTextUpdate(GameHelper.RerollPrice);
        }

        private void Start()
        {
            _backButton.onClick.AddListener(BackToLobby);
            _rerollButton.onClick.AddListener(Reroll);
            _gymBuyPopup.OnHide += ShowReroll;
            GameHelper.SignalBus.OnChangeRerollPrice += RerollPriceTextUpdate;
        }

        private void OnDestroy()
        {
            _backButton.onClick.AddListener(BackToLobby);
            _rerollButton.onClick.RemoveListener(Reroll);
            _gymBuyPopup.OnHide -= ShowReroll;
            GameHelper.SignalBus.OnChangeRerollPrice -= RerollPriceTextUpdate;
        }

        public void ShowGymBuyPopup(BoyDataSO boyData)
        {
            _gymBuyPopup.Init(boyData);
            _rerollButton.gameObject.SetActive(false);
        }

        public void ShowReroll()
        {
            _rerollButton.gameObject.SetActive(true);
        }

        public void BackToLobby()
        {
            GameHelper.ChangeGameState(GameState.Lobby);
        }

        public void RerollPriceTextUpdate(int value)
        {
            _rerollPriceText.text = $"Reroll {value}{TextHelper.CoinsText}";
        }

        public void Reroll()
        {
            GameHelper.NewEntity.Get<RerollEvent>();
        }
    }
}
