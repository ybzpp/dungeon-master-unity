using TMPro;
using UnityEngine.UI;
using UnityEngine;
using DungeonMaster;

namespace DungeonMaster
{
    public class UnlockButton : ElementUI
    {
        [SerializeField] private Image _ico;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private TMP_Text _descriptionText;

        private Button _button;
        private string _id;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(Unlock);
            _priceText.color = TextHelper.GoldColor;
            GameHelper.SignalBus.OnChangeUnlockPrice += UnlockPriceUpdate;
        }
         
        private void OnDestroy()
        {
            _button.onClick.RemoveListener(Unlock);
            GameHelper.SignalBus.OnChangeUnlockPrice -= UnlockPriceUpdate;
        }

        public void Unlock()
        {
            var price = UnlockerService.UnlockPrice;
            if (GameHelper.TryBuy(price))
            {
                Progress.Unlock(_id);
                gameObject.SetActive(false);
                GameHelper.SignalBus.OnChangeUnlockPrice?.Invoke();
            }
        }

        public void Init(string id, string description, Sprite ico)
        {
            _id = id;
            _ico.sprite = ico;
            _descriptionText.text = description;
            UnlockPriceUpdate();
            gameObject.SetActive(true);
        }

        public void UnlockPriceUpdate()
        {
            _priceText.text = TextHelper.MoneyTextConvert(UnlockerService.UnlockPrice);
        }
    }
}