using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SkeletonMovement : MonoBehaviour
{
    public AIPath Aipath;

    [Header("SkeleZoon")]
    [SerializeField]
    private PlayerInSkeleAtckZoon HavePlayer;

    [SerializeField]
    private SkeleSeePlayer SeePlayer;

[Header ("EnemyMove")]
    [SerializeField] private Transform maxleft;
    [SerializeField] private Transform maxright;
    [SerializeField] private Transform Enemy;
    [SerializeField] private float speed;

    [Header ("TimerCount")]
    private float TimerCount;
    [SerializeField] private float StopTime;
    private bool moveTrue;
    
    [Header("SkeleAnim")]
    [SerializeField]
    private Animator Anim;

    [Header("CheckPlayer")]
    [SerializeField]
    PlayerInSkeleAtckZoon InatckZoon;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (!SeePlayer.seePLayer)
        {
            Aipath.enabled = false;
            if (!InatckZoon.havePlayer)
        {
            Anim.SetBool("move", true);
            Anim.SetBool("Atck", false);
            if (moveTrue)
            {
                if (Enemy.position.x >= maxleft.position.x)
                {
                    MoveDirection(-1);
                }
                else
                {
                    DirectionChange();
                }
            }
            else
            {
                if (Enemy.position.x <= maxright.position.x)
                {
                    MoveDirection(1);
                }
                else
                {
                    DirectionChange();
                }
            }
        }
        else
        {
            Anim.SetBool("Atck", true);
            Anim.SetBool("move", false);
        }
        }
        else if(SeePlayer.seePLayer){
             Aipath.enabled = true;
             AiPathSide();
        }
    }
    public void DirectionChange( )
    {
        TimerCount += Time.deltaTime;
        if(StopTime< TimerCount)
        moveTrue= !moveTrue;
    }
    public void MoveDirection(int _direction)
    {
        TimerCount=0;
        Enemy.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * _direction,
            transform.localScale.y,
            transform.localScale.z);
        Enemy.position    = new Vector3(
            Enemy.position.x + Time.deltaTime * _direction * speed,
            Enemy.position.y,
            Enemy.position.z);
    }

    public void AiPathSide()
    {
        if (Aipath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (Aipath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
