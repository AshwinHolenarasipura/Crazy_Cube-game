using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform player, cast;
    [SerializeField] float agroRange, movSpeed;

    private Animator anim;
    private Rigidbody2D rb;
    private bool isLeft;
    private bool isAgro;
    private bool searching;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isLeft = false;
        isAgro = false;
        searching = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        check();
    }
    private void check()
    {
        if (CanSeePlayer(agroRange))
        {
            isAgro = true;
        }
        else
        {
            if (isAgro)
            {
                if (!searching)
                {
                    searching = true;
                    StartCoroutine(delay());
                }
            }
        }
        if (isAgro)
        {
            ChasePlayer();
        }
    }
    private IEnumerator delay()
    {
        yield return new WaitForSeconds(3f);
        StopChasingPlayer();
    }
    private void ChasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            anim.Play("Pig_guard_move");
            rb.velocity = new Vector2(movSpeed, 0);
            transform.localScale = new Vector2(0.012f, transform.localScale.y);
            isLeft = false;
        }
        else if (transform.position.x > player.position.x)
        {
            anim.Play("Pig_guard_move");
            rb.velocity = new Vector2(-movSpeed, 0);
            transform.localScale = new Vector2(-0.012f, transform.localScale.y);
            isLeft = true;
        }
    }
    private void StopChasingPlayer()
    {
        isAgro = false;
        searching = false;
        anim.Play("Pig_guard_idle");
        rb.velocity = Vector2.zero;
    }
    bool CanSeePlayer(float distance)
    {
        bool val = false;
        var castDist = distance;

        if (isLeft)
        {
            castDist = -distance;
        }

        Vector2 enpos = cast.position + Vector3.right * castDist;


        RaycastHit2D hit = Physics2D.Linecast(cast.position, enpos, 1 << LayerMask.NameToLayer("Action"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else
            {
                val = false;
            }
        }
        return val;
    }
}
