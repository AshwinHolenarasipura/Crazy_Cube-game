using System;
using System.Collections.Generic;
using UnityEngine;

public enum PoolObjectType
{
    Platform_1,
    Platform_2,
    Platform_3,
    Platform_4,
    Platform_6,
    Max
}
[Serializable]
public class PoolInfo
{
    public PoolObjectType type;
    public int amount = 0;
    public GameObject prefab;
    public GameObject container;
    [HideInInspector]
    public List<GameObject> pool = new List<GameObject>();
}

public class Pool_Manager : Singleton<Pool_Manager>
{
    [SerializeField] private List<PoolInfo> listofPool;
    //   private Vector3 default_pos = new Vector3(-100, -100, -100);

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < listofPool.Count; i++)
        {
            FillPool(listofPool[i]);
        }
    }

    void FillPool(PoolInfo pl)
    {
        for (int i = 0; i < pl.amount; i++)
        {
            GameObject ob = null;
            ob = Instantiate(pl.prefab, pl.container.transform);
            ob.gameObject.SetActive(false);
            // ob.transform.position = default_pos;
            pl.pool.Add(ob);
        }
    }

    public GameObject GetPoolObject(PoolObjectType type)
    {
        PoolInfo selected = GetPoolByType(type);
        List<GameObject> pool = selected.pool;
        GameObject obI = null;
        if (pool.Count > 0)
        {
            obI = pool[pool.Count - 1];
            pool.Remove(obI);
        }
        else
        {
            obI = Instantiate(selected.prefab, selected.container.transform);
        }
        return obI;
    }
    public void CoolObj(GameObject ob, PoolObjectType type)
    {
        // ob.SetActive(false);
        // ob.transform.position = default_pos;
        PoolInfo sel = GetPoolByType(type);
        List<GameObject> pool = sel.pool;
        if (!pool.Contains(ob))
        {
            pool.Add(ob);
        }
    }
    private PoolInfo GetPoolByType(PoolObjectType type)
    {
        for (int i = 0; i < listofPool.Count; i++)
        {
            if (type == listofPool[i].type) return listofPool[i];
        }
        return null;
    }
}
