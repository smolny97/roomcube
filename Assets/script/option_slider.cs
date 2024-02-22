using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class option_slider : MonoBehaviour
{
    [SerializeField] private AudioSource audioSrc;
    private float musicVolume = 1f;
    private void Start()
    {
        audioSrc = GameObject.Find("AudioGame").GetComponent<AudioSource>();
    }
    private void Update()
    {
        audioSrc.volume = musicVolume;
    }
    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }
        
}
