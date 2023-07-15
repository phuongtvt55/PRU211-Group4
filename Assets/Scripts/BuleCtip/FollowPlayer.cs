using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public bool followPlayer;
    void OnTriggerEnter2D(Collider2D Player)
    {
        if (Player.tag == "Player")
        {
            followPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D Player)
    {
        if (Player.tag == "Player")
        {
            followPlayer = false;
        }
    }
}
