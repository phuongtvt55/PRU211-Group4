using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int attackDame = 10;
    [SerializeField]
    private Vector2 moveSpeed = new Vector2 (10, 0);
    public Vector2 knockBack = Vector2.zero;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageManage damage = collision.GetComponent<DamageManage>();
        if (damage != null)
        {
            bool hit = damage.TakeDame(attackDame);
            if (hit)
            {

                Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
                Debug.Log(rb.velocity.x);
                Debug.Log(knockBack.x);
                if (!collision.CompareTag("Boss"))
                {
                    rb.velocity = new Vector2(knockBack.x, rb.velocity.y + knockBack.y);

                }
                Destroy(gameObject);
            }
        }

    }
}
