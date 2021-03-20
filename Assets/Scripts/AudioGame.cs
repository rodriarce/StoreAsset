using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioGame : MonoBehaviour
{

    public AudioSource gameAudio;
    public AudioClip clipAudio;

    // Start is called before the first frame update
    void Start()
    {
        StartAudio();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAudio()
    {
        gameAudio.clip = clipAudio;
        gameAudio.Play();
        gameAudio.loop = true;
    }


}
