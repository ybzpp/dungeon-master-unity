using TMPro;

namespace DungeonMaster
{
    public class MoneyUI : ElementUI
    {
        public TMP_Text MoneyText;

        private void Start()
        {
            Progress.OnChangeMoney += MoneyUpdate;
            MoneyText.color = TextHelper.GoldColor;
        }

        private void OnDestroy()
        {
            Progress.OnChangeMoney -= MoneyUpdate;
        }

        public void MoneyUpdate(int value)
        {
            MoneyText.text = TextHelper.MoneyTextConvert(value);
        }
    }
}
