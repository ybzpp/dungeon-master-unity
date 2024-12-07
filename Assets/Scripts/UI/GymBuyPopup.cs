using Leopotam.Ecs;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace DungeonMaster
{
    public class GymBuyPopup : ElementUI
    {
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _cancelButton;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private TextMeshProUGUI _statsText;
        [SerializeField] private Image _ico;
        private BoyDataSO _boyData;

        private void Start()
        {
            _buyButton.onClick.AddListener(Buy);
            _cancelButton.onClick.AddListener(Hide);
            _priceText.color = TextHelper.GoldColor;
        }

        private void OnDestroy()
        {
            _buyButton.onClick.RemoveAllListeners();
            _cancelButton.onClick.RemoveAllListeners();
        }

        public void Buy()
        {
            GameHelper.NewEntity.Get<BoyBuyEvent>() = new BoyBuyEvent()
            {
                Price = _boyData.Price,
                Popup = this
            };
        }

        public void Init(BoyDataSO data)
        {
            _boyData = data;
            _priceText.text = TextHelper.MoneyTextConvert(data.Price);
            _statsText.text = TextHelper.StatsTextConvert(data.PowerLevels[0]);
            _nameText.text = data.Name;
            _descriptionText.text = data.Description;
            _ico.sprite = data.Ico;
            _buyButton.interactable = GameHelper.CanBuy(data.Price);
            Show();
        }
    }
}
