using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyScript : MonoBehaviour
{
    AudioSource pickup;
    DamageManage damageManage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pickup = GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            damageManage = collision.GetComponent<DamageManage>();
            if(damageManage.CurrentHeath + 10 <= damageManage.MaxHeath)
            {
                AudioSource.PlayClipAtPoint(pickup.clip, gameObject.transform.position, pickup.volume);
                damageManage.TakeDame(-10);
                Destroy(gameObject);
            }
        }
    }
}
