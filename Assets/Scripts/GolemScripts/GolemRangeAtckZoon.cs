using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemRangeAtckZoon : MonoBehaviour
{
    public bool InRangeAtck;
    void OnTriggerEnter2D(Collider2D Player)
    {
        if(Player.tag =="Player"){
            InRangeAtck=true;
        }
    }
    private void OnTriggerExit2D(Collider2D Player) {
        if(Player.tag =="Player"){
            InRangeAtck=false;
        }
    }
}
