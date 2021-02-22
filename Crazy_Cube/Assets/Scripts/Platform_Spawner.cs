using System.Collections;
using UnityEngine;

public class Platform_Spawner : MonoBehaviour
{
    private float platformCount = 8;
    [SerializeField]
    private Transform[] platformPrefab;
    private float distanceBetweenPlatforms = Constants.Platforms.distanceBetweenPlatforms;

    private float lastPlatformPositionY = -1.62f;
    private float lastPlatformPositionX;
    [SerializeField]
    private GameObject[] collectables;
    private ArrayList xPositions;

    private float minX, maxX;


    private void Awake()
    {
        SetMinAndMaxX();
        xPositions = new ArrayList { Random.Range(0f, maxX),
        Random.Range(minX, 0f),
        Random.Range(1.0f, maxX),
        Random.Range(1.0f, maxX),
        Random.Range(minX, -1.0f)};
        int randomIndex = (int)Random.Range(0f, xPositions.Count - 1);
        lastPlatformPositionX = (float)xPositions[randomIndex];
        CreatePlatforms();
    }

    private void CreatePlatforms()
    {
        for (int i = 0; i < platformCount; i++)
        {
            PositionNewPlatform();
        }
    }

    private void SetMinAndMaxX()
    {
        Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        print("bound X : " + bounds.x);
        print("bound Y : " + bounds.y);

        maxX = bounds.x - 2f ;
        minX = -bounds.x + 2f;
    }

    public void PositionNewPlatform()
    {
        int i = Random.Range(0, 2);
        GameObject platform = Instantiate(platformPrefab[i].gameObject);
        Vector3 platformPosition = platform.transform.position;
        platformPosition.y = lastPlatformPositionY;

        int randomIndex = (int)Random.Range(0f, xPositions.Count - 1);
        if (lastPlatformPositionX < 0)
        {
            platformPosition.x = Random.Range(Constants.Platforms.randomHorizontalOffset, maxX);
        }
        else
        {
            platformPosition.x = Random.Range(minX, -Constants.Platforms.randomHorizontalOffset);
        }
        lastPlatformPositionX = platformPosition.x;

        platform.transform.position = platformPosition;
        lastPlatformPositionY += distanceBetweenPlatforms;
    }
    private struct Constants
    {
        public struct Platforms
        {
            public const float distanceBetweenPlatforms = 2f;
            public const float randomHorizontalOffset = 4f;
        }
    }
}
