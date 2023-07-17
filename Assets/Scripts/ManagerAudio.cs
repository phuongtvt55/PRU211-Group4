using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerAudio : MonoBehaviour
{
    public AudioClip intro;
    public AudioClip loop;
    // Start is called before the first frame update
    void Start()
    {
        if (!SceneManager.GetActiveScene().name.Equals("MainMenu") || !SceneManager.GetActiveScene().name.Equals("AboutUs") || !SceneManager.GetActiveScene().name.Equals("BasicTutorial"))
        {
            Debug.Log("Enter");
            AudioMenu.instance.Play(intro, loop);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
