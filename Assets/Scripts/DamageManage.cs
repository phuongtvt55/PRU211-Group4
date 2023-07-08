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
            if(_currentHeath <= 0)
            {
                IsAlive = false;
                StartCoroutine(DestroyObject());
            }
        }
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(1);
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

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        if (gameObject.CompareTag("Knight") || gameObject.CompareTag("Boss"))
        {
            barController.SetHealth(CurrentHeath, MaxHeath);
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
            if (gameObject.CompareTag("Knight") || gameObject.CompareTag("Boss"))
            {                
                barController.SetHealth(CurrentHeath, MaxHeath);
            }
            animator.SetTrigger(AnimationString.hitTrigger);
            return true;
        }
        
        return false;
        
    }
}
