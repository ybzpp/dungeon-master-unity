using System.Collections.Generic;
using UnityEngine;

namespace DungeonMaster
{
    [CreateAssetMenu]
    public class Config : ScriptableObject
    {
        [Header("Party")]
        public int PartyCount = 5;

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

        [Header("Unlock")]
        public UnlockDataCollection UnlockDataCollection;
        public UnlockData[] Unlocks => UnlockDataCollection.Datas;
        public string[] StartUnlocks;

        [Header("Color")]
        public ColorPalette ColorPalette;

        [Header("Balance")]
        public int StartRerollPrice = 10;
        public int StartRerollPriceStep = 10;
        public int DropMoneyMax = 10;
        public int DropMoneyMin = 1;
        public int DropMoney => Random.Range(DropMoneyMin, DropMoneyMax);
    }
}