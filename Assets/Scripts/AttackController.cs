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
        Debug.Log("Enter");
        GameObject parentObject = transform.parent.gameObject;
        
        DamageManage damage = collision.GetComponent<DamageManage>();
        if (damage != null)
        {
            bool hit = damage.TakeDame(attackDame);    
            if (hit)
            {
             
                Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
                
                if (!collision.CompareTag("Boss"))
                {
                    if(parentObject.transform.localScale.x > 0)
                    {
                        rb.velocity = new Vector2(knockBack.x, rb.velocity.y + knockBack.y);
                    }
                    else
                    {
                        rb.velocity = new Vector2(knockBack.x * -1, rb.velocity.y + knockBack.y);
                    }
                   

                }
               
            }
        }
       
    }
}
