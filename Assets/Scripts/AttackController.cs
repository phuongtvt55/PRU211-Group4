using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public int attackDame = 10;
    public Vector2 knockBack = Vector2.zero;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageManage damage = collision.GetComponent<DamageManage>();
        if (damage != null)
        {
            bool hit = damage.TakeDame(attackDame);    
            if (hit)
            {
                Debug.Log(attackDame);
                Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(knockBack.x, rb.velocity.y + knockBack.y);
            }
        }
       
    }
}