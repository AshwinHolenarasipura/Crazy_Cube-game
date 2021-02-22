using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol_Pig_Enemy : MonoBehaviour
{
    [SerializeField] private Transform castPos;

    private Rigidbody2D rb;
    private float movSpeed, basecastdist;
    private const string LEFT = "left";
    private const string RIGHT = "right";
    private string facingdir;

    private Vector3 baseScale;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        baseScale = transform.localScale;
        facingdir = RIGHT;
        movSpeed = 1f;
        basecastdist = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        check();
    }
    void check()
    {
        float Vx = movSpeed;
        if (facingdir == LEFT)
        {
            Vx = -movSpeed;
        }

        rb.velocity = new Vector2(Vx, rb.velocity.y);

        if (isHittingWall() || isNearEdge())
        {
            if (facingdir == LEFT)
            {
                changePlayerFacing(RIGHT);
            }
            else
            {
                changePlayerFacing(LEFT);
            }
        }
    }
    bool isHittingWall()
    {
        bool val = false;
        float castDist = basecastdist;
        if (facingdir == LEFT)
        {
            castDist = -basecastdist;
        }

        Vector3 targetPos = castPos.position;
        targetPos.x += castDist;

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = true;
        }
        else
        {
            val = false;
        }

        return val;
    }
    bool isNearEdge()
    {
        bool val = true;
        float castDist = basecastdist;

        Vector3 targetPos = castPos.position;
        targetPos.y -= castDist;

        if (Physics2D.Linecast(castPos.position, targetPos, 1 << LayerMask.NameToLayer("Ground")))
        {
            val = false;
        }
        else
        {
            val = true;
        }

        return val;
    }
    void changePlayerFacing(string newDir) {
        Vector3 newScale = baseScale;

        if (newDir == LEFT)
        {
            newScale.x = -baseScale.x;
        }
        else
        {
            newScale.x = baseScale.x;
        }
        transform.localScale = newScale;
        facingdir = newDir;
    }
}
