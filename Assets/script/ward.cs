using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ward : MonoBehaviour
{
    public ClickArrow pup;
    private void Start()
    {
        pup = GameObject.Find("Player").GetComponent<ClickArrow>();
    }
    public void open()
    {
        pup.Open();
    }
    public void close()
    {
        pup.Close();
    }
}
