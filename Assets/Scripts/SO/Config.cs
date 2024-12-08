using System.Collections.Generic;
using UnityEngine;

namespace DungeonMaster
{
    [CreateAssetMenu]
    public class Config : ScriptableObject
    {
        [Header("Party")]
        public int MaxPartySize = 5;

        [Header("Gym")]
        public int MaxBoyInGym = 5;
        public BoyDataCollectionSO BoyDataCollection;

        [Header("Prefabs")]
        public DamageView DamagePrefab;
        public HealthView HealthPrefab;
        public Transform SelectPrefab;
        public UI UiPrefab;
        public LocationManager LocationManagerPrefab;
        public BattleManager BattleManagerPrefab;
        public ObjectPoolController ObjectPoolPrefab;

        [Header("Unlock")]
        public UnlockDataCollection UnlockDataCollection;
        public UnlockData[] Unlocks => UnlockDataCollection.Datas;
        public string[] StartUnlocks;

        [Header("Color")]
        public ColorPalette ColorPalette;

        [Header("Level")]
        public int FinalBossLevel = 99;

        [Header("Balance")]
        public int StartRerollPrice = 10;
        public int StartRerollPriceStep = 10;
        public int DropMoneyMax = 10;
        public int DropMoneyMin = 1;
        public int DropMoney => Random.Range(DropMoneyMin, DropMoneyMax);
        public int DropExpMax = 10;
        public int DropExpMin = 1;
        public int DropExp => Random.Range(DropExpMin, DropExpMax);
    }
}