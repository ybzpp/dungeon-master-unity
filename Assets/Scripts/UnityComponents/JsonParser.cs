using System.Collections.Generic;
using UnityEngine;

namespace DungeonMaster
{
    [System.Serializable]
    public class BoyDataCollection
    {
        public List<BoyData> boys;
    }

    public class JsonParser : MonoBehaviour
    {
        [SerializeField] private TextAsset jsonFile;
        [SerializeField] private BoyDataCollection boyDataCollection;
        [SerializeField] private BoyDataCollectionSO BoyDataCollectionSO;

        [ContextMenu("Test")]
        public void Test()
        {
            if (jsonFile == null)
            {
                Debug.LogError("JSON file is not assigned in the Inspector!");
                return;
            }

            ParseJson();
            DisplayBoyData();
        }

        private void ParseJson()
        {
            boyDataCollection = JsonUtility.FromJson<BoyDataCollection>(jsonFile.text);

            if (boyDataCollection == null || boyDataCollection.boys == null)
            {
                Debug.LogError("Failed to parse JSON or no boy data found.");
            }
            else
            {
                Debug.Log("Successfully loaded boy data.");
            }
        }

        private void DisplayBoyData()
        {
            if (boyDataCollection?.boys == null) return;

            foreach (var boy in boyDataCollection.boys)
            {
                foreach (var b in BoyDataCollectionSO.boys)
                {
                    if (b.Id == boy.id)
                    {
                        b.CopyTo(boy);
                        break;
                    }
                }

                Debug.Log($"Name: {boy.name}");
                Debug.Log($"Description: {boy.description}");
                Debug.Log($"Power Levels: {string.Join(", ", boy.powerLevels)}");
            }
        }
    }
}