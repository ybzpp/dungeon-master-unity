using LeopotamGroup.Globals;
using TMPro;
using UnityEngine;

namespace DungeonMaster
{
    public class DamageView : MonoBehaviour
    {
        public TextMeshPro DamageText;
        public float LifeTime = 2f;

        public void Init(int value)
        {
            DamageText.text = value.ToString();
            transform.forward = Service<LocationManager>.Get().Dungeon.Camera.transform.forward;
            Destroy(gameObject, LifeTime);
        }
    }
}
