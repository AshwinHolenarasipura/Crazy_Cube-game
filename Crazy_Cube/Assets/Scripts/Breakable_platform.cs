using System.Collections;
using UnityEngine;

public class Breakable_platform : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D box;
    private static string PLAYER = "Player";

    private void Start()
    {
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(PLAYER) && collision.transform.position.y > transform.position.y || collision.transform.position.y < transform.position.y)
        {

            StartCoroutine(_break());
            StartCoroutine(_delay());
        }
    }
    private IEnumerator _break()
    {
        yield return new WaitForSeconds(0.15f);
        anim.Play("destroy_tile");
        box.enabled = false;
    }
    private IEnumerator _delay()
    {
        yield return new WaitForSeconds(0.8f);
        gameObject.SetActive(false);
    }
}
