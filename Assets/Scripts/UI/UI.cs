using System;
using UnityEngine;

namespace DungeonMaster
{
    public class ElementUI : MonoBehaviour
    {
        public Action OnHide;
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

        public void CloseAll()
        {
            Gym.Hide();
            Lobby.Hide();
            Dungeon.Hide();
            Info.Hide();
            Party.Hide();
            StartScreen.Hide();
            Shop.Hide();
            Lose.Hide();
            Win.Hide();
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
                    Dungeon.LevelImagesUpdate(GameHelper.Level);
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
