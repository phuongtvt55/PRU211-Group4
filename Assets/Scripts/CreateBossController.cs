using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateBossController : MonoBehaviour
{
    [SerializeField]
    GameObject boss;
    [SerializeField]
    Animator camAnimator;
    [SerializeField]
    private bool isCutScene;

    // Start is called before the first frame update

    public AudioClip intro;
    public AudioClip loop;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            camAnimator.SetBool(AnimationString.cutscene, true);
            isCutScene = true;  
            Invoke(nameof(StopCutScene), 3f);
            boss.SetActive(true);
            if (!SceneManager.GetActiveScene().name.Equals("MainMenu") || !SceneManager.GetActiveScene().name.Equals("AboutUs") || !SceneManager.GetActiveScene().name.Equals("BasicTutorial"))
            {
                Debug.Log("Enter");
                AudioMenu.instance.Play(intro, loop);
            }
        }
    }

    void StopCutScene()
    {
        camAnimator.SetBool(AnimationString.cutscene, false);
        isCutScene = false;
        Destroy(gameObject);
    }
}
