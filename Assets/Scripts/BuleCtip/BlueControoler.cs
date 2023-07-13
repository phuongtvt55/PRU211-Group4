using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueControoler : MonoBehaviour
{
    public AIPath Aipath;

    [SerializeField]
    private SeePlayer SeePlayer;
    [SerializeField]
    private FollowPlayer follow;
    [SerializeField] private DamageManage damage;
    [Header("EnemyMove")]
    [SerializeField]
    private Transform maxleft;

    [SerializeField]
    private Transform maxright;

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

    private void Awake()
    {
        Anim = GetComponent<Animator>();
    }


    private void Update()
    {
        if (!follow.followPlayer && damage.IsAlive)
        {
            Aipath.enabled = false;
            AImovetoSkeleton();
            if (!SeePlayer.seePLayer)
            {
                NotAtck();
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
                Atck();
            }
        }
        else if (follow.followPlayer && damage.IsAlive)
        {
            Aipath.enabled = true;
            MovefollowAI();
            AiPathSide();
            if (!SeePlayer.seePLayer)
            {
                NotAtck();
            }
            else
            {
                Atck();
            }
        }
        isAlive();

    }
    private void isAlive()
    {
        if (!damage.IsAlive)
        {
            Aipath.enabled = false;
        }
    }
    public void Move()
    {
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

    public void Atck()
    {
        Anim.SetBool("Attack", true);
        Anim.SetBool("canWalk", false);
    }
    public void NotAtck()
    {
        Anim.SetBool("canWalk", true);
        Anim.SetBool("Attack", false);
    }

    public void DirectionChange()
    {
        Anim.SetBool("canWalk", false);
        TimerCount += Time.deltaTime;
        if (StopTime < TimerCount)
            moveTrue = !moveTrue;
    }

    public void MoveDirection(int _direction)
    {
        Anim.SetBool("canWalk", true);
        TimerCount = 0;
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
            (Enemy.transform.position.x) + 1,
            transform.position.y,
            transform.position.z
            );
        }
        else
        {
            Aipath.transform.position = new Vector3(
            (Enemy.transform.position.x) - 1,
            transform.position.y,
            transform.position.z
            );
        }

    }
}