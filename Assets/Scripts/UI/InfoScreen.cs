using TMPro;
using UnityEngine;

namespace DungeonMaster
{


    public class InfoScreen : ElementUI
    {
        [SerializeField] private TextMeshProUGUI _moneyText;

        private void Start()
        {
            Progress.OnChangeMoney += UpdateMoney;
            _moneyText.color = TextHelper.GoldColor;
        }

        private void OnDestroy()
        {
            Progress.OnChangeMoney -= UpdateMoney;
        }

        private void OnEnable()
        {
            UpdateMoney(Progress.Money);
        }

        public void UpdateMoney(int value)
        {
            _moneyText.text = TextHelper.MoneyTextConvert(value);
        }
    }
}
