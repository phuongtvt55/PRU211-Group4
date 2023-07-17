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
        
            AudioMenu.instance.Play(intro, loop);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
