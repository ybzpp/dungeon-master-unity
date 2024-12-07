using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoyDataCollectionSO : ScriptableObject
{
    public List<BoyDataSO> boys;
    public BoyDataSO randomData => boys[Random.Range(0, boys.Count)];
}
