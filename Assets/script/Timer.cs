using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] private Animator[] obj;
    [SerializeField] private int[] number;
    [SerializeField] private float time=1;
    [SerializeField] private GameObject stars;
    private bool end = false;
    void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            obj[i].SetInteger("num2", number[i]);
            obj[i].SetInteger("number", number[i]);
        }       
    }
    void Update()
    {
        if (!end)
        {
            time -= Time.deltaTime;
            
        }
        if (end) { stars.active = false; }
        
        if (time < 0)
        {
            for (int i = 0; i < 4; i++)
            {
                obj[i].SetInteger("num2", -1);
            }
            time = 1;

            if(obj[0].GetInteger("number") == 0 && obj[1].GetInteger("number") == 0 && obj[2].GetInteger("number") == 0 && obj[3].GetInteger("number") == 0)
            {
                end = true;
            }
            else if (obj[0].GetInteger("number") - 1 == -1)
            {
                obj[0].SetInteger("number", 9);
                if (obj[1].GetInteger("number") - 1 == -1)
                {
                    obj[1].SetInteger("number", 5);
                    if (obj[2].GetInteger("number") - 1 == -1)
                    {
                        obj[2].SetInteger("number", 9);
                        if (obj[3].GetInteger("number") - 1 == -1)
                        {
                            
                            end = true;
                        }
                        else
                        {
                            obj[3].SetInteger("number", obj[3].GetInteger("number") - 1);
                        }
                    }
                    else
                    {
                        obj[2].SetInteger("number", obj[2].GetInteger("number") - 1);
                    }
                }
                else
                {
                    obj[1].SetInteger("number", obj[1].GetInteger("number") - 1);
                }
            }
            else
            {
                obj[0].SetInteger("number", obj[0].GetInteger("number") - 1);
            }

        }
    }
}
