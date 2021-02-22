using System;
using System.Collections.Generic;
using UnityEngine;

public enum Pool_Collect_Type
{
    Gold_Coin,
    Silver_Key,
    Golden_Key,
    Max
}
[Serializable]
public class Collectibles_Info
{
    public Pool_Collect_Type type;
    public int c_amount = 0;
    public GameObject c_prefab;
    public GameObject c_container;
    [HideInInspector] public List<GameObject> c_pool = new List<GameObject>();
}
public class Spawner_Collectibles : Singleton<Spawner_Collectibles>
{
    [SerializeField] private List<Collectibles_Info> info;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < info.Count; i++)
        {
            Fill(info[i]);
        }
    }
    void Fill(Collectibles_Info _info)
    {
        for (int i = 0; i < _info.c_amount; i++)
        {
            GameObject ob = null;
            ob = Instantiate(_info.c_prefab, _info.c_container.transform);
            ob.gameObject.SetActive(false);
            _info.c_pool.Add(ob);
        }
    }
    public GameObject Get_Collectibles(Pool_Collect_Type c_type)
    {
        Collectibles_Info selected = CollectiblesInfo(c_type);
        List<GameObject> pool = selected.c_pool;
        GameObject c_Obi = null;
        if (pool.Count > 0)
        {
            c_Obi = pool[pool.Count - 1];
            pool.Remove(c_Obi);
        }
        else
        {
            c_Obi = Instantiate(selected.c_prefab, selected.c_container.transform);
        }
        return c_Obi;
    }
    public void Cool_Collectible(GameObject ob, Pool_Collect_Type type)
    {
        Collectibles_Info sel = CollectiblesInfo(type);
        List<GameObject> obj = sel.c_pool;
        if (!obj.Contains(ob))
        {
            obj.Add(ob);
        }
    }
    private Collectibles_Info CollectiblesInfo(Pool_Collect_Type type)
    {
        for (int i = 0; i < info.Count; i++)
        {
            if (type == info[i].type) return info[i];
        }
        return null;
    }
}
