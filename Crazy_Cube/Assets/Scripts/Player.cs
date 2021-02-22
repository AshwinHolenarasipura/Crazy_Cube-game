using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject Collect_Vfx;
    [SerializeField] private CameraFollow shake;
    [SerializeField] private ParticleSystem Dust;

    private Rigidbody2D myBody;
    private Animator anim;

    private int Dir;
    private float dashTime;
    private float speed = -2.3f;
    private float jumpPower = 3f;
    private string dir;
    private float dashSpeed, startDashTime;
    private bool IsGrounded, jumped, canDoubleJump;

    private static string L_WALL = "Left_Wall";
    private static string R_WALL = "Right_Wall";
    private static string GOLD_COIN = "Gold_Coin";
    private static string SILVER_KEY = "Silver_Key";
    private static string GOLDEN_KEY = "Golden_Key";
    private static string RIGHT = "Right";
    private static string LEFT = "Left";

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Dir = 0;
        dashSpeed = 1.4f;
        startDashTime = 0.05f;
        dashTime = startDashTime;
        dir = "Left";
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGrounded();
        playerJump();
        Dash();
    }
    private void FixedUpdate()
    {
        Mov();
    }
    void Mov()
    {
        myBody.velocity = new Vector2(speed, myBody.velocity.y);
        anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(L_WALL))
        {
            changeDir(-transform.localScale.x);
            speed = 2.3f;
            dir = RIGHT;
        }
        else if (collision.gameObject.CompareTag(R_WALL))
        {
            changeDir(-transform.localScale.x);
            speed = -2.3f;
            dir = LEFT;
        }
        if (collision.gameObject.CompareTag(GOLD_COIN) || collision.gameObject.CompareTag(SILVER_KEY)
            || collision.gameObject.CompareTag(GOLDEN_KEY))
        {
            Collect_Vfx.gameObject.SetActive(true);
            StartCoroutine(shine());
            Pool_Collect_Type type;
            string name = collision.gameObject.name;

            for (int i = 0; i < (int)Pool_Collect_Type.Max; i++)
            {
                type = (Pool_Collect_Type)i;
                string comp = type.ToString() + "(Clone)";
                collision.gameObject.SetActive(false);
                if (name == comp)
                {
                    Spawner_Collectibles.Instance.Cool_Collectible(collision.gameObject, type);
                }
                StartCoroutine(VfxDelay());
            }
        }
    }
    private IEnumerator shine()
    {
        yield return new WaitForSeconds(0.05f);
        Collect_Vfx.GetComponent<Animator>().Play("Shine");
    }
    void changeDir(float dir)
    {
        Vector3 temp = transform.localScale;
        temp.x = dir;
        transform.localScale = temp;
    }
    void CheckIfGrounded()
    {
        IsGrounded = Physics2D.Raycast(groundCheckPos.position, Vector2.down, 0.5f, groundLayer);
        if (IsGrounded)
        {
            if (jumped)
            {
                jumped = false;
                canDoubleJump = false;
            }
        }
    }
    void playerJump()
    {
        if (IsGrounded)
        {
            anim.SetBool("Jump", false);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumped = true;
                canDoubleJump = true;
                jumpPower = 3f;
                myBody.velocity = Vector2.up * jumpPower;
                anim.SetBool("Jump", true);
                CreateDust();
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (canDoubleJump)
                    {
                        jumpPower = 4.5f;
                        myBody.velocity = Vector2.up * jumpPower;
                        anim.SetBool("Jump", true);
                        canDoubleJump = false;
                    }
                }

            }
        }
    }
    void CreateDust()
    {
        Dust.Play();
    }
    private IEnumerator VfxDelay()
    {
        yield return new WaitForSeconds(0.2f);
        Collect_Vfx.gameObject.SetActive(false);
    }
    void Dash()
    {
        if (Dir == 0)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (dir == LEFT)
                {
                    Dir = 1;
                }
                else if (dir == RIGHT)
                {
                    Dir = 2;
                }
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                if (Dir == 1)
                {
                    speed = -2f;
                }
                else if (Dir == 2)
                {
                    speed = 2f;
                }
                Dir = 0;
                dashTime = startDashTime;
            }
            else
            {
                dashTime -= Time.deltaTime;

                if (Dir == 1 || Dir == 2)
                {
                    speed = speed * dashSpeed;
                }
            }
        }
    }
}
