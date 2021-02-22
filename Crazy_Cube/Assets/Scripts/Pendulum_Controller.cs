using UnityEngine;

public class Pendulum_Controller : MonoBehaviour
{
    [SerializeField] private GameObject Pend;
    [SerializeField] private float movspeed, leftangle, rightangle;

    Rigidbody2D RgB;
    bool moving_clockwise;

    // Start is called before the first frame update
    void Start()
    {
        RgB = Pend.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mov();
    }
    void MoveDir()
    {
        if (Pend.transform.rotation.z > rightangle)
        {
            moving_clockwise = false;
        }
        else if (Pend.transform.rotation.z < leftangle)
        {
            moving_clockwise = true;
        }
    }
    void mov()
    {
        MoveDir();
        if (moving_clockwise)
        {
            RgB.angularVelocity = movspeed;
        }
        else
        {
            RgB.angularVelocity = -movspeed;
        }
    }
}
