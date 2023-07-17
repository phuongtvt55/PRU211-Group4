using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioMenu : MonoBehaviour
{
    [SerializeField]
    AudioSource introSource;
    [SerializeField]
    AudioSource loopSource;

    public AudioClip intro;
    public AudioClip loop;

    public static AudioMenu instance;
    private void Awake()
    {
        
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  
        }
        else{  
            Destroy(gameObject);    
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Play(intro, loop);
    }

    public void Play(AudioClip intro, AudioClip loop)
    {
        introSource.clip = intro;
        introSource.Play();
        loopSource.clip = loop;
        loopSource.PlayScheduled(AudioSettings.dspTime + introSource.clip.length + 0.36);
    }
}
