using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicoptions : MonoBehaviour
{
    [SerializeField] private AudioSource[] obj;
    [SerializeField] private AudioSource mus;
    void Start()
    {
        for(int i = 0; i < obj.Length; i++)
        {
            obj[i].volume = PlayerPrefs.GetInt("voiceobj");
        }
        if (mus)
        {
            mus.volume = PlayerPrefs.GetInt("voicemus");
        }
    }
}
