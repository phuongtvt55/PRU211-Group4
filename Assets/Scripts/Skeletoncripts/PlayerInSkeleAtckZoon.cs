using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInSkeleAtckZoon : MonoBehaviour
{  
    public bool havePlayer;
    
    void OnTriggerEnter2D(Collider2D Player)
    {
        if(Player.tag =="Player"){
            havePlayer=true;
        }
    }
    private void OnTriggerExit2D(Collider2D Player) {
        if(Player.tag =="Player"){
            havePlayer=false;
        }
    }
}
