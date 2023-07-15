using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class WizardMovement : MonoBehaviour
{
    public AIPath Aipath;

    [Header("WizradZoon")]
    [SerializeField]
    private PlayerInSkeleAtckZoon HavePlayer;

    [SerializeField]
    private SkeleSeePlayer SeePlayer;

    [SerializeField]
    private DamageManage damageManage;

    [Header("EnemyMove")]
    [SerializeField]
    private Transform FirstPlace;

    [SerializeField]
    private Transform Enemy;

    [SerializeField]
    private float speed;

    [Header("TimerCount")]
    private float TimerCount;

    [SerializeField]
    private float StopTime;
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
        Alive();
    }

    private void Alive()
    {
        if (!damageManage.IsAlive)
        {
            Aipath.enabled = false;
        }
    }
    public void Move()
    {
        if (!SeePlayer.seePLayer)
        {
            Aipath.enabled = false;
            AImovetoSkeleton();
            if (!InatckZoon.havePlayer)
            {
                NotAtck();
                if (moveTrue)
                {
                    if (Enemy.position.x >= FirstPlace.position.x)
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
                    if (Enemy.position.x <= FirstPlace.position.x)
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
                Atck();
            }
        }
        else if (SeePlayer.seePLayer)
        {
            Aipath.enabled = true;
            MovefollowAI();
            AiPathSide();
            if (!InatckZoon.havePlayer)
            {
                NotAtck();
            }
            else
            {
                Atck();
            }
        }

    }

    public void Atck()
    {
        Anim.SetBool("Atck", true);
        Anim.SetBool("move", false);
    }
    public void NotAtck()
    {
        Anim.SetBool("move", true);
        Anim.SetBool("Atck", false);
    }
    public void DirectionChange()
    {
        Anim.SetBool("move", false);
        if (Enemy.position.x != FirstPlace.position.x)
        {
            moveTrue = true;
        }
    }
    public void MoveDirection(int _direction)
    {
        Enemy.localScale = new Vector3(
            Mathf.Abs(transform.localScale.x) * _direction,
            transform.localScale.y,
            transform.localScale.z
        );
        Enemy.position = new Vector3(
            Enemy.position.x + Time.deltaTime * _direction * speed,
            Enemy.position.y,
            Enemy.position.z
        );
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

    public void MovefollowAI()
    {
        if (Enemy.localScale.x > 0)
        {
            Enemy.transform.position = new Vector3(
            (Aipath.transform.position.x) - 1,
            transform.position.y,
            transform.position.z
        );
        }
        else
        {
            Enemy.transform.position = new Vector3(
           (Aipath.transform.position.x) + 1,
           transform.position.y,
           transform.position.z);
        }

    }

    public void AImovetoSkeleton()
    {
        if (Enemy.localScale.x > 0)
        {
            Aipath.transform.position = new Vector3(
            (Enemy.transform.position.x)+1,
            transform.position.y,
            transform.position.z
            );
        }
        else
        {
            Aipath.transform.position = new Vector3(
            (Enemy.transform.position.x)-1,
            transform.position.y,
            transform.position.z
            );
        }

    }
}
