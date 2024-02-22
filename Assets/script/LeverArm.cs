using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class LeverArm : MonoBehaviour
{
    private Vector2 checktouch;
    [SerializeField] private GameObject player;
    [SerializeField] private Animator Arm;
    [SerializeField] private GameObject[] rooms;
    [SerializeField] private int[] door;
    [SerializeField] private Animator[] doors;
    [SerializeField] private bool timer = false,notime;
    [SerializeField] private float time,time2;
    private void Start()
    {        
        player = GameObject.Find("Player");
    }
    private void Update()
    {           
        if (timer)
        {
            if (!notime)
            {
                time -= Time.deltaTime;
                if (time < 0)
                {
                    time = time2;
                    Door();
                    timer = false;
                    Arm.SetBool("active", false);
                }
            }
        }       
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    checktouch.x = Camera.main.ScreenPointToRay(Input.GetTouch(0).position).origin.x;
                    checktouch.y = Camera.main.ScreenPointToRay(Input.GetTouch(0).position).origin.y;
                    
                    if (transform.position.x + 0.5f > checktouch.x && transform.position.x - 0.5f < checktouch.x && transform.position.y - 0.5f < checktouch.y && transform.position.y + 0.5f > checktouch.y)
                    {
                        if (player.transform.position.x == transform.position.x && player.transform.position.y < transform.position.y && player.transform.position.y > transform.position.y-1.5f)
                        {
                            this.GetComponent<AudioSource>().enabled = true;
                            if (Arm.GetBool("active"))
                            {
                                Arm.SetBool("active", false);
                                this.GetComponent<AudioSource>().Play();
                            }
                            else
                            {
                                timer = true;
                                Arm.SetBool("active", true);
                                this.GetComponent<AudioSource>().Play();
                            }
                            Door();
                        }
                    }
                }
            }
        }     
    }
    private void Door()
    {
        string s="";
        for (int i = 0; i < rooms.Length; i++)
        {
            if (Regex.IsMatch(rooms[i].transform.localScale.z.ToString(), "4") && door[i] == 4)
            {
                s = Regex.Replace(rooms[i].transform.localScale.z.ToString(), "4", "", RegexOptions.IgnoreCase);                
                doors[i].SetBool("active", false);
            }
            else if (!Regex.IsMatch(rooms[i].transform.localScale.z.ToString(), "4") && door[i] == 4)
            {
                s = rooms[i].transform.localScale.z.ToString() + "4";              
                doors[i].SetBool("active", true);
            }
            else if (Regex.IsMatch(rooms[i].transform.localScale.z.ToString(), "2") && door[i] == 2)
            {
                s = Regex.Replace(rooms[i].transform.localScale.z.ToString(), "2", "", RegexOptions.IgnoreCase);
                doors[i].SetBool("active", false);
            }
            else if (!Regex.IsMatch(rooms[i].transform.localScale.z.ToString(), "2") && door[i] == 2)
            {
                s = rooms[i].transform.localScale.z.ToString() + "2";
                doors[i].SetBool("active", true);
            }
            GameObject.Find("door" + i).GetComponent<AudioSource>().enabled = true;
            GameObject.Find("door" + i).GetComponent<AudioSource>().Play();
            rooms[i].transform.localScale = new Vector3(rooms[i].transform.localScale.x, rooms[i].transform.localScale.y, Convert.ToInt32(s));
        }                    
    }
}
