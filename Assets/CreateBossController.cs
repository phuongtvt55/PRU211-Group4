using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBossController : MonoBehaviour
{
    [SerializeField]
    GameObject boss;
    [SerializeField]
    Animator camAnimator;
    [SerializeField]
    private bool isCutScene;

    // Start is called before the first frame update
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            camAnimator.SetBool(AnimationString.cutscene, true);
            isCutScene = true;  
            Invoke(nameof(StopCutScene), 3f);
            boss.SetActive(true);        
        }
    }

    void StopCutScene()
    {
        camAnimator.SetBool(AnimationString.cutscene, false);
        isCutScene = false;
        Destroy(gameObject);
    }
}
