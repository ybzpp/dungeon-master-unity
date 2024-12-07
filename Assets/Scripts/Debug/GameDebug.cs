using System.Collections;
using UnityEngine;

namespace DungeonMaster
{
    public class GameDebug : MonoBehaviour
    {
        public enum DebugType
        {
            None,
            Dungeon,
        }

        public DebugType Type;
        public Boy[] PartyBoy;
        public int Level;
        public bool StartAutoBattle;

        private void Start()
        {
            StartCoroutine(DebugCoroutine());
        }

        [ContextMenu("Lose")]
        public void Lose()
        {
            for (int i = 0; i < GameHelper.LocationManager.Dungeon.Boys.Count; i++)
            {
                GameHelper.Dungeon.Boys[i].Dead();
            }
        }

        [ContextMenu("Win")]
        public void Win()
        {
            for (int i = 0; i < GameHelper.Dungeon.Boys.Count; i++)
            {
                GameHelper.Dungeon.Boys[i].Dead();
            }
        }

        public IEnumerator DebugCoroutine()
        {
            yield return null;

            switch (Type)
            {
                case DebugType.None:
                    break;
                case DebugType.Dungeon:
                    foreach (var prefab in PartyBoy)
                    {
                        var boy = Instantiate(prefab);
                        boy.Init(GameHelper.NewEntity);
                        GameHelper.Dungeon.AddBoy(boy);
                    }
                    GameHelper.RuntimeData.DungeonLevel = Level;
                    GameHelper.ChangeGameState(GameState.Dungeon);
                    GameHelper.BattleManager.Init();
                    if (StartAutoBattle) GameHelper.BattleManager.BattleLogic();
                    break;
                default:
                    break;
            }
        }
    }
}