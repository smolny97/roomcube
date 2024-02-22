using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staractive : MonoBehaviour
{
    [SerializeField] private bool star1, star2, star3;
    [SerializeField] private GameObject objstar1, objstar2, objstar3;
    void Start()
    {
        if (true || star1)
        {
            objstar1.active = true;
        }
        if (true || star2)
        {
            objstar2.active = true;
        }
        if (true || star3)
        {
            objstar2.active = true;
        }
    }
}
