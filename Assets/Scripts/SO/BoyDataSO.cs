using DungeonMaster;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoyDataSO : ScriptableObject
{
    public string Id;
    public string Description;
    public string Name;
    public int Price;
    public Sprite Ico;
    public Boy Prefab;
    public List<string> Skills;

    [Header("Balance")]
    public float CritChance = .1f;
    public float CritMultiplier = 2f;
    public List<int> PowerLevels;

    public void CopyTo(BoyData data)
    {
        Description = data.description;
        Name = data.name;
        Price = data.price;
        Skills = data.skills;
        PowerLevels = data.powerLevels;

#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }
}

[System.Serializable]
public class BoyData
{
    public string id;
    public string name;
    public string description;
    public List<string> skills; 
    public List<int> powerLevels;
    public int price;
}

[System.Serializable]
public class Skill
{
    public string id;
    public string name;
    public string effect;
}
