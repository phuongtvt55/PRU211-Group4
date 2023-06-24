using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public int attackDame = 10;
    [SerializeField]
    private Vector2 knockBack = Vector2.zero;
    

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
            }
        }
       
    }
}
