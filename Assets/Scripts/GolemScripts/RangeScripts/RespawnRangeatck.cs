using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnRangeatck : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject RangeAtck;

     private void SpawnObject()
    {
        GameObject newObject = Instantiate(RangeAtck, transform.position, transform.rotation);
    }
    public void Respawn()
    {
        SpawnObject();
    }
}
