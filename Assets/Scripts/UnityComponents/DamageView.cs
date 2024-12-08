using LeopotamGroup.Globals;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DungeonMaster
{
    public class DamageView : MonoBehaviour, IPooledObject
    {
        public TextMeshPro DamageText;
        public float LifeTime = 2f;

        public void Init(int value, Vector3 dir)
        {
            DamageText.text = value.ToString();
            transform.forward = dir;
            gameObject.SetActive(true);

            StartCoroutine(DisableCoroutine());
        }

        public IEnumerator DisableCoroutine()
        {
            yield return new WaitForSeconds(LifeTime);
            gameObject.SetActive(false);
        }

        public void OnObjectSpawn()
        {
            gameObject.SetActive(false);
        }

        public void SetPool(Queue<GameObject> pool)
        {
        }
    }
}
