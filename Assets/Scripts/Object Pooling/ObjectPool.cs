/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoSingletonGeneric<ObjectPool>
{
    [System.Serializable]
    public class Pool
    {
        public PoolEnum PoolType;
        public GameObject prefab;
        public int size;

    }

    public List<Pool> pools;
    public Dictionary<PoolEnum, Queue<GameObject>> poolDictionary;


    private void Start()
    {
        poolDictionary = new Dictionary<PoolEnum, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);

            }

            poolDictionary.Add(pool.PoolType, objectPool);
        }
    }

    public GameObject SpawnFromPool(PoolEnum poolType, Vector3 pos, Quaternion rotation)
    {
        if (poolDictionary.ContainsKey(poolType))
        {
            Debug.LogWarning("Type doesn't exist");
            return null;
        }
        GameObject objToSpawn = poolDictionary[poolType].Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = pos;
        objToSpawn.transform.rotation = rotation;

        poolDictionary[poolType].Enqueue(objToSpawn);
        return objToSpawn;
    }
}
*/