using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManage : MonoBehaviour
{
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
            }
        }
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
    private float untouchableTime = 0.5f;

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
            animator.SetTrigger(AnimationString.hitTrigger);
            return true;
        }
        
        return false;
        
    }
}
