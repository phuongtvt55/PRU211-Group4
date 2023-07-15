
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeleSeePlayer : MonoBehaviour
{
    public bool seePLayer;
    void OnTriggerEnter2D(Collider2D Player)
    {
        if(Player.tag =="Player"){
            seePLayer=true;
        }
    }
    private void OnTriggerExit2D(Collider2D Player) {
        if(Player.tag =="Player"){
            Debug.Log("out");
            seePLayer=false;
        }
    }
}
