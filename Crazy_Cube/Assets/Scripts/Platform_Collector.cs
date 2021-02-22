using UnityEngine;

public class Platform_Collector : MonoBehaviour
{
    [SerializeField] private GameObject ps;
    private static string R_platform = "R_Platform";
    private static string L_platform = "L_Platform";
    private static string GOLD_COIN = "Gold_Coin";
    private static string SILVER_KEY = "Silver_Key";
    private static string GOLDEN_KEY = "Golden_Key";
    private int i = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Pool_Collect_Type m_type;
        if (i == 0 && collision.CompareTag(R_platform))
        {
            ps.GetComponent<PlatformSpawnerNew>().platformcount = 2;
            ps.GetComponent<PlatformSpawnerNew>().CreatePlatforms();
            ++i;
        }
        else if (collision.gameObject.CompareTag(GOLD_COIN) || collision.gameObject.CompareTag(SILVER_KEY)
                   || collision.gameObject.CompareTag(GOLDEN_KEY))
        {

            string name = collision.gameObject.name;
            for (int i = 0; i < (int)Pool_Collect_Type.Max; i++)
            {
                m_type = (Pool_Collect_Type)i;
                string comp = m_type.ToString() + "(Clone)";
                collision.gameObject.SetActive(false);
                if (name == comp)
                {
                    Spawner_Collectibles.Instance.Cool_Collectible(collision.gameObject, m_type);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        PoolObjectType type;

        if (i == 1 && col.CompareTag(R_platform) || col.CompareTag(L_platform))
        {
            string name = col.gameObject.name;
            for (int i = 0; i < (int)PoolObjectType.Max; i++)
            {
                type = (PoolObjectType)i;
                string comp = type.ToString() + "(Clone)";
                col.gameObject.SetActive(false);
                if (name == comp)
                {
                    Pool_Manager.Instance.CoolObj(col.gameObject, type);
                }
            }
            i = 0;
        }
    }
}
