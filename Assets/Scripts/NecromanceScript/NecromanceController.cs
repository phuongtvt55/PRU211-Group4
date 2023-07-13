using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromanceController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public Transform player;
    public bool isFacingRight = false;
    DamageManage necromanceDamageManager;
    private GameObject bringerOfDealth;
    [SerializeField]
    private GameObject knight;
    private bool isStage2 = false;
    private bool goStageOneTime = false;
    private bool isSpawnEneny = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        necromanceDamageManager = GetComponent<DamageManage>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        animator.SetFloat(AnimationString.distance, distance);

        
        if(necromanceDamageManager.CurrentHeath <= 50 && !isStage2 && necromanceDamageManager.IsAlive)
        {
            isStage2 = true;
            animator.SetBool("isStage2", isStage2);
            animator.SetBool("oneTime", goStageOneTime);
            StartCoroutine(SpawnEnemy());
        }
        if (isStage2 && isSpawnEneny)
        {
            animator.SetBool("oneTime", goStageOneTime);
            goStageOneTime = true;
            DamageManage dameBringer;
            if (bringerOfDealth != null)
            {
                dameBringer = bringerOfDealth.GetComponent<DamageManage>();
                if (dameBringer.CurrentHeath > 0)
                {
                    necromanceDamageManager.IsUntouchable = true;
                }
            }
            
        }
        if (!necromanceDamageManager.IsAlive)
        {
            isStage2 = false;
        }


    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(4);
        bringerOfDealth = Instantiate(knight, transform.position, Quaternion.identity);
        isSpawnEneny = true;    
    }

    public void FollowPlayer()
    {
        if((player.position.x > transform.position.x && !isFacingRight) || (player.position.x < transform.position.x && isFacingRight))
        {
            isFacingRight = !isFacingRight;
            transform.localScale *= new Vector2(-1, 1);
        }
    }
}
