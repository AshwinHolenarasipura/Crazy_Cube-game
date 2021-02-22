using UnityEngine;

public class PlatformSpawnerNew : MonoBehaviour
{
    public int platformcount = 5;
    [SerializeField] private GameObject[] Right_platforms_prefabs, Left_platforms_prefabs;
    [SerializeField] private Transform start_platform;

    private float lastPlatformPositionY, Extra_Pos;
    private bool IsSpawnedRight, IsSpawnedLeft;

    // Start is called before the first frame update
    void Start()
    {
        lastPlatformPositionY = start_platform.position.y;
        IsSpawnedLeft = true;
        Extra_Pos = 2.8f;
        CreatePlatforms();
    }
    public void CreatePlatforms()
    {
        for (int i = 0; i < platformcount; i++)
        {
            PositionNewPlatform(lastPlatformPositionY);
        }
    }
    private void PositionNewPlatform(float posY)
    {
        PoolObjectType type;
        int num = Random.Range(0, 3);
        if (IsSpawnedLeft)
        {
            string name = Right_platforms_prefabs[num].name;
            for (int i = 0; i < (int)PoolObjectType.Max; i++)
            {
                type = (PoolObjectType)i;
                string comp = type.ToString();
                if (name == comp)
                {
                    spawn(type, posY);
                    IsSpawnedLeft = false;
                    IsSpawnedRight = true;
                }
            }
        }
        else if (IsSpawnedRight)
        {
            string name = Left_platforms_prefabs[num].name;
            for (int i = 0; i < (int)PoolObjectType.Max; i++)
            {
                type = (PoolObjectType)i;
                string comp = type.ToString();
                if (name == comp)
                {
                    spawn(type, posY);
                    IsSpawnedLeft = true;
                    IsSpawnedRight = false;
                }
            }
        }
    }
    void spawn(PoolObjectType m_type, float pos_y)
    {
        GameObject obj = Pool_Manager.Instance.GetPoolObject(m_type);
        obj.transform.position = new Vector3(obj.transform.position.x, pos_y + Extra_Pos, 0f);
        obj.gameObject.SetActive(true);
        lastPlatformPositionY = obj.transform.position.y;
        if (obj.GetComponent<Collectibles>()) obj.GetComponent<Collectibles>().Spawner();
    }
}
