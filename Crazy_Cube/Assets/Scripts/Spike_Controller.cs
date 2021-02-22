using UnityEngine;

public class Spike_Controller : MonoBehaviour
{
    private Vector3 startPos;
    [SerializeField] private float frequency, magnitude, offset;

    // Start is called before the first frame update
    private void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        movSpike();
    }
    void movSpike()
    {
        transform.position = startPos + Vector3.up * Mathf.Sin(frequency * Time.time + offset) * magnitude;
    }
}
