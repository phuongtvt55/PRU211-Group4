using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManage : MonoBehaviour
{
    public HealthBarController barController;
    Animator animator;
    [SerializeField]
    private int _maxHeath = 100;
    [SerializeField]
    private HealthBarPlayerScript playerHealthBar;
    [SerializeField]
    private GameManagerScript gameManagerScript;
    [SerializeField]
    private HealthBarPlayerScript bossHealthBar;
    public int MaxHeath
    {
        get
        {
            return _maxHeath;
        }
        set
        {
            _maxHeath = value;
        }
    }

    [SerializeField]
    private int _currentHeath = 100;
    public int CurrentHeath
    {
        get
        {
            return _currentHeath;
        }
        set
        {
            _currentHeath = value;
            if (_currentHeath <= 0)
            {
                if(gameObject.CompareTag("Player"))
                {
                   
                    gameManagerScript.gameOver();
                }
                else if (gameObject.CompareTag("Boss"))
                {
                    gameManagerScript.gameOver();
                }
                else
                    StartCoroutine(DestroyObject());
                IsAlive = false;
                
            }
        }
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }


    private bool _isAlive = true;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationString.isAlive, value);
            
        }
    }

    [SerializeField]
    private bool _isUntouchable = false;
    private float timeHit = 0;
    [SerializeField]
    private float untouchableTime;

    public bool IsUntouchable
    {
        get
        {
            return _isUntouchable;
        }
        set
        {
            _isUntouchable = value;
        }
    }

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        if (gameObject.CompareTag("Enemy"))
        {
            barController.SetHealth(CurrentHeath, MaxHeath);
        }

        if (gameObject.CompareTag("Boss"))
        {
            bossHealthBar.InitHealthBar(CurrentHeath);
        }

        if (gameObject.CompareTag("Player"))
        {
            playerHealthBar.InitHealthBar(CurrentHeath);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsUntouchable)
        {
            if(timeHit > untouchableTime)
            {
                IsUntouchable = false;
                timeHit = 0;
            }

            timeHit += Time.deltaTime;
        }
        //TakeDame(10);
    }

    public bool TakeDame(int damage)
    {
        if(IsAlive && !IsUntouchable)
        {
            CurrentHeath -= damage;
            IsUntouchable = true;
            if (gameObject.CompareTag("Enemy"))
            {                
                barController.SetHealth(CurrentHeath, MaxHeath);
            }
            if (gameObject.CompareTag("Boss"))
            {
                bossHealthBar.ActualHealth(CurrentHeath);
            }
            if (gameObject.CompareTag("Player"))
            {
                playerHealthBar.ActualHealth(CurrentHeath);
            }
            animator.SetTrigger(AnimationString.hitTrigger);
            return true;
        }
        
        return false;
        
    }
}
