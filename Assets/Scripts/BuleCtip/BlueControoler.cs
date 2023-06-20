using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueControoler : MonoBehaviour
{

    [SerializeField]
    private SeePlayer SeePlayer;

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
        if (!SeePlayer.seePLayer) {
            Move();
            NotAtck();
        }
        else
        {
            Atck();
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
}
