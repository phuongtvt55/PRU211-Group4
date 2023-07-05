using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeScripts : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Transform Range;
    [SerializeField]
    private float speed;

    private void Update()
    {
        MoveDirection();
    }
    public void MoveDirection()
    {
        Range.localScale = new Vector3(
           Mathf.Abs(transform.localScale.x),
           transform.localScale.y,
           transform.localScale.z
       );
        Range.position = new Vector3(
            Range.position.x + Time.deltaTime * speed,
            Range.position.y,
            Range.position.z
        );
    }
    void OnTriggerEnter2D(Collider2D Player)
    {
        if (Player.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

}
