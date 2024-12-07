using Leopotam.Ecs;
using LeopotamGroup.Globals;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DungeonMaster
{
    public static class GameHelper
    {
        public static UI UI => Service<UI>.Get();
        public static Config Config => Service<Config>.Get();
        public static EcsWorld World => Service<EcsWorld>.Get();
        public static EcsEntity NewEntity => World.NewEntity();
        public static SignalBus SignalBus => Service<SignalBus>.Get();
        public static RuntimeData RuntimeData => Service<RuntimeData>.Get();
        public static BattleManager BattleManager => Service<BattleManager>.Get();
        public static LocationManager LocationManager => Service<LocationManager>.Get();
        public static Gym Gym => LocationManager.Gym;
        public static Dungeon Dungeon => LocationManager.Dungeon;
        public static PartyView PartyView => LocationManager.Party;
        public static List<Boy> Boys => Dungeon.Boys;
        public static List<Boy> BadBoys => Dungeon.BadBoys;
        public static Boy RandomBoy => Boys[Random.Range(0, Boys.Count)];
        public static Boy RandomBadBoy => BadBoys[Random.Range(0, BadBoys.Count)];
        public static Vector2Int BoysCount => new Vector2Int(Boys.Count, Config.PartyCount);
        public static int BoyInGymCount => Mathf.Clamp(Progress.DeathCount + 1, 1, Config.MaxBoyInGym);
        public static bool IsInit => World != null && World.IsAlive();
        public static int Level => RuntimeData.DungeonLevel;
        public static int RerollPrice => Config.StartRerollPrice + Config.StartRerollPriceStep * RuntimeData.RerollCount;
        public static int UnlockPrice => UnlockerService.GetUnlockPrice();

        public static void ChangeGameState(GameState state)
        {
            NewEntity.Get<ChangeGameStateEvent>().state = state;
        }

        public static void StartDungeon()
        {
            if (Boys.Count > 0)
            {
                Service<EcsWorld>.Get().NewEntity().Get<ResetProgressEvent>();
                ChangeGameState(GameState.Dungeon);
            }
        }

        public static void Lose()
        {
            Progress.DeathCount++;
            Progress.Money += RuntimeData.Reward.Coins;
            Progress.ClickerExp += RuntimeData.Reward.Exp;
            ChangeGameState(GameState.Lose);
        }

        public static void Win()
        {
            //if (Level != 0 && Level % 9 == 0)
            //{
            //    BattleManager.Win();
            //    Progress.Money += RuntimeData.Reward.Coins;
            //    Progress.ClickerExp += RuntimeData.Reward.Exp;
            //    ChangeGameState(GameState.Win);
            //}
            //else
            {
                NextDungeonRoom();
            }
        }

        public static void NextDungeonRoom()
        {
            UI.TransitionFade.Transition(() =>
            {
                NewEntity.Get<NextDungeonRoomEvent>();
                NewEntity.Get<DestroyEnemyDeadEvent>();
            });
        }

        public static void AddBoyToParty(Boy boy)
        {
            boy.Entity.Get<PartyTag>();
            boy.transform.position = PartyView.GetPointByIndex(BoysCount.x);
            Dungeon.AddBoy(boy);
        }

        public static List<Boy> GetPartyBoys(bool isParty)
        {
            return isParty ? Boys : BadBoys;
        }

        public static Boy GetRandomPartyBoy(bool isParty)
        {
            return isParty ? RandomBoy : RandomBadBoy;
        }

        public static bool IsGameOver()
        {
            if (BadBoys.Where(x => !x.IsDead).Count() == 0)
            {
                Win();
                return true;
            }
            else if (Boys.Where(x => !x.IsDead).Count() == 0)
            {
                Lose();
                return true;
            }
            return false;
        }

        public static bool TryBuy(int value)
        {
            if (value > Progress.Money) return false;
            Progress.Money -= value;
            return true;
        }

        public static bool CanBuy(int value)
        {
            if (value > Progress.Money) return false;
            return true;
        }

        public static void DamageFx(int damage, Vector3 pos)
        {
            //TODO: edit to object pool
            var prefab = Service<Config>.Get().DamagePrefab;
            var view = Object.Instantiate(prefab, pos, Quaternion.identity);
            view.Init(damage);
        }
    }
}