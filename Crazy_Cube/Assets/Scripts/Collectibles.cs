using System.Collections.Generic;
using UnityEngine;

public class Collectibles: MonoBehaviour
{
    [SerializeField] private List<GameObject> gb, collectibles;

    public void Spawner()
    {
        for (int i = 0; i < gb.Count; i++)
        {
            collectibles_Spawner(gb[i].transform);
        }
    }
    private void collectibles_Spawner(Transform pos)
    {
        Pool_Collect_Type type;
        int num = Random.Range(0, 3);
        string name = collectibles[num].name;
        for (int i = 0; i < (int)Pool_Collect_Type.Max; i++)
        {
            type = (Pool_Collect_Type)i;
            string comp = type.ToString();
            if (name == comp)
            {
                GameObject obj = Spawner_Collectibles.Instance.Get_Collectibles(type);
                obj.transform.position = pos.position;
                obj.gameObject.SetActive(true);
            }
        }
    }
}
