using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
    [Header ("EnemyMove")]
    [SerializeField] private Transform maxleft;
    [SerializeField] private Transform maxright;
    [SerializeField] private Transform Enemy;
    [SerializeField] private float speed;

    [Header ("TimerCount")]
    private float TimerCount;
    [SerializeField] private float StopTime;
    private bool moveTrue;
    [Header ("EnemyMoveAnima")]
    [SerializeField] private Animator anima;

    [Header ("CheckPlayer")]
    [SerializeField] PlayerInSkeleAtckZoon InatckZoon;

    private void Awake() {
        anima = GetComponent<Animator>();
    }

    private void Update() {
        if(!InatckZoon.havePlayer){
            anima.SetBool("move",true);
            anima.SetBool("Atck",false);
            if(moveTrue){
                if(Enemy.position.x >= maxleft.position.x){
                    MoveDirection(-1);
                }else{
                    DirectionChange();
                }
            }
            else{
                if(Enemy.position.x<= maxright.position.x){
                    MoveDirection(1);
                }else{
                    DirectionChange();
                }
            }
        }else{
            anima.SetBool("Atck",true);
            anima.SetBool("move",false);
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

}
