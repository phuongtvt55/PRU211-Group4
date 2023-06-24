using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    [SerializeField]
    private float timeExit;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeExit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
