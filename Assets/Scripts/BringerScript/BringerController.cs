using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public Transform player;
    public bool isFacingRight = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        animator.SetFloat(AnimationString.distance, distance);
    }

    public void FollowPlayer()
    {
        if ((player.position.x > transform.position.x && !isFacingRight) || (player.position.x < transform.position.x && isFacingRight))
        {
            isFacingRight = !isFacingRight;
            transform.localScale *= new Vector2(-1, 1);
        }
    }
}
