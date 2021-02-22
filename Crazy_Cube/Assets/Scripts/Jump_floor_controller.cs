using System.Collections;
using UnityEngine;

public class Jump_floor_controller : MonoBehaviour
{
    [SerializeField] private GameObject trap, Dust;

    private float jumpForce = 8.2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.5f, 1f) * jumpForce;
            // player_cube.GetComponent<Rigidbody2D>().velocity = new Vector2(0.5f, 1f) * jumpForce; // right
            trap.gameObject.SetActive(true);
            Dust.gameObject.SetActive(true);
            StartCoroutine(Delay());
            StartCoroutine(Dust_Delay());
        }
    }
    private IEnumerator Dust_Delay()
    {
        yield return new WaitForSeconds(0.5f);
        Dust.gameObject.SetActive(false);
    }
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        trap.gameObject.SetActive(false);
    }
}
