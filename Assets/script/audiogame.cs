using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiogame : MonoBehaviour
{
    [SerializeField] private AudioSource audioSrc;
    private GameObject obj;
    void Start()
    {
        obj = GameObject.Find("Audio Source");
        if (obj)
        {
            DontDestroyOnLoad(obj.gameObject);
            obj.name = "AudioGame";
            obj = null;
        }
    }
}
