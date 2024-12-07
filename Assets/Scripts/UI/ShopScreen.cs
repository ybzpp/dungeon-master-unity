using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonMaster
{
    public partial class ShopScreen : ElementUI
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private UnlockButton _prefab;
        [SerializeField] private Transform _parent;

        private List<UnlockButton> _unlockButtons = new List<UnlockButton>();

        private void OnEnable()
        {
            Init();
        }

        private void Start()
        {
            _backButton.onClick.AddListener(Back);
        }

        private void OnDestroy()
        {
            _backButton.onClick.RemoveListener(Back);
        }

        public void Back()
        {
            GameHelper.ChangeGameState(GameState.Lobby);
        }

        public void Init()
        {
            foreach (var item in _unlockButtons)
            {
                item.gameObject.SetActive(false);
            }

            var unlocks = UnlockerService.GetAvailableUnlocks();
            for (int i = 0; i < unlocks.Count; i++)
            {
                if (i < _unlockButtons.Count)
                {
                    _unlockButtons[i].Init(unlocks[i].id, unlocks[i].description, unlocks[i].ico);
                }
                else
                {
                    var btn = Instantiate(_prefab, _parent);
                    _unlockButtons.Add(btn);
                    btn.Init(unlocks[i].id, unlocks[i].description, unlocks[i].ico);
                }
            }
        }
    }
}
