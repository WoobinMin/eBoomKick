using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    private ObjectPooler() { }
    private static ObjectPooler instance;

    public static ObjectPooler Instance { get { return instance; } }

    public int[] pooledAmounts;
    public GameObject[] pooledObjectPrefabs;

    private Dictionary<string, List<GameObject>> pooledObjects;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        pooledObjects = new Dictionary<string, List<GameObject>>();

        for (int i = 0; i < pooledObjectPrefabs.Length; ++i)
        {
            string key = pooledObjectPrefabs[i].name;
            string holderName = string.Concat(key, "Holder");
            GameObject holder = new GameObject(holderName);

            for (int j = 0; j < pooledAmounts[i]; ++j)
            {
                GameObject obj = Instantiate(pooledObjectPrefabs[i]);
                obj.SetActive(false);

                if (!pooledObjects.ContainsKey(key))
                {
                    holder.transform.parent = this.transform;
                    holder.transform.localScale = Vector3.one;
                    pooledObjects[key] = new List<GameObject>();
                }

                obj.transform.parent = holder.transform;

                pooledObjects[key].Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(string key)
    {
        for (int i = 0; i < pooledObjects[key].Count; ++i)
        {
            if (!pooledObjects[key][i].activeSelf)
            {
                return pooledObjects[key][i];
            }
        }

        return null;
    }

}