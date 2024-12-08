using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolController : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string Tag;
        public GameObject Prefab;
        public int Size;
        
    }

    [SerializeField] private List<Pool> _pools;
    private Dictionary<string, Queue<GameObject>> _poolDictionary;

    void Start()
    {
        _poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in _pools)
        {
            var objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.Size; i++)
            {
                var obj = Instantiate(pool.Prefab, transform);
                IPooledObject pooledObj = obj.GetComponent<IPooledObject>();

                if (pooledObj != null)
                {
                    pooledObj.SetPool(objectPool);
                    objectPool.Enqueue(obj);
                }
                obj.SetActive(false);
            }

            _poolDictionary.Add(pool.Tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!_poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag "+ tag + " doesnt excist.");
            return null;
        }

        var queue = _poolDictionary[tag];
        var objectToSpawn = queue.Dequeue();

        objectToSpawn.SetActive(false);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
            pooledObj.SetPool(queue);
        }
        
        _poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
}
