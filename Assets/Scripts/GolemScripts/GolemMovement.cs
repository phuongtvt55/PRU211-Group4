using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemMovement : MonoBehaviour
{
    [Header("GolemZoon")]
    [SerializeField]
    private PlayerInSkeleAtckZoon HavePlayer;
    
    [SerializeField] private RespawnRangeatck respawn;

    [SerializeField]
    private SkeleSeePlayer SeePlayer;

    [SerializeField]
    private GolemRangeAtckZoon RangeAtckZoon;


    [SerializeField]
    [Header("Hp")]
    private DamageManage damageManage;
    [Header("GolemAnim")]
    [SerializeField]
    private Animator Anim;
    private void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        Active();
        CloseAtck();
        RangeAtck();
    } 
IEnumerator DelayAction(float delayTime)
{
   //Wait for the specified delay time before continuing.
   yield return new WaitForSeconds(delayTime);
 
   //Do the action after the delay time has finished.
}
    public void CloseAtck()
    {
        if (HavePlayer.havePlayer)
        {
            Anim.SetBool("CloseAtck",true);
        }
        else
        {
            Anim.SetBool("CloseAtck",false);
        }
    }

    public void RangeAtck()
    {
        if (RangeAtckZoon.InRangeAtck)
        {

            Anim.SetBool("RangeAtck",true);
        }
        else
        {
            Anim.SetBool("RangeAtck",false);
        }
    }
    public void Respawn(){ 
            respawn.Respawn();
    }
    public void Active( )
    {
        if (SeePlayer.seePLayer)
        {
            Anim.SetBool("SeePlayer",true);
        }
        else
        {
            Anim.SetBool("SeePlayer",false);
        }
    }
}
