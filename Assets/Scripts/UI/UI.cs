using System;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonMaster
{
    public class ElementUI : MonoBehaviour, IView
    {
        public Action OnHide;
        public virtual void Init(Config config, RuntimeData runtimeData) { }
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
            OnHide?.Invoke();
        }
    }

    public class UI : ElementUI
    {
        public DungeonScreen Dungeon;
        public LobbyScreen Lobby;
        public GymScreen Gym;
        public InfoScreen Info;
        public PartyUI Party;
        public TransitionFadeScreen TransitionFade;
        public StartScreen StartScreen;
        public ShopScreen Shop;
        public LoseScreen Lose;
        public WinScreen Win;

        private List<IView> _screens;
        private RuntimeData _runtimeData;

        public override void Init(Config config, RuntimeData runtimeData)
        {
            base.Init(config, runtimeData);

            _runtimeData = runtimeData;
            _screens = new List<IView>
            {
                Dungeon,
                Lobby,
                Gym,
                Info,
                Party,
                TransitionFade,
                StartScreen,
                Shop,
                Lose,
                Win
            };

            foreach (var screen in _screens)
            {
                screen.Init(config, runtimeData);
            }
        }

        public void CloseAll()
        {
            foreach (var screen in _screens)
            {
                screen.Hide();
            }
        }

        public void ChangeGameState(GameState gameState)
        {
            CloseAll();
            switch (gameState)
            {
                case GameState.Start:
                    StartScreen.Show();
                    break;
                case GameState.Lobby:
                    Lobby.Show();
                    Info.Show();
                    break;
                case GameState.Gym:
                    Gym.Show();
                    Info.Show();
                    Party.Show();
                    break;
                case GameState.Dungeon:
                    Dungeon.LevelImagesUpdate(_runtimeData.DungeonLevel);
                    Dungeon.Show();
                    Info.Show();
                    break;
                case GameState.Shop:
                    Shop.Show();
                    Info.Show();
                    break;
                case GameState.Win:
                    Win.Show();
                    break;
                case GameState.Lose:
                    Lose.Show();
                    break;
                default:
                    break;
            }
        }
    }
}
