using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class victory : MonoBehaviour
{
    [SerializeField] private float x1, y1, x2, y2;
    [SerializeField] private int lev,n=1;
    [SerializeField] private GameObject[] rooms;
    [SerializeField] private GameObject vic, stars;
    private bool st;
    private void Start()
    {
        bool prob = true;
        int j = 0;
        while (prob)
        {
            if (!GameObject.Find("room" + j))
            {
                rooms = new GameObject[j];
                for (int i = 0; i < j; i++)
                {
                    rooms[i] = GameObject.Find("room" + i);
                }
                prob = false;
            }
            j++;
        }
    }
    private void Update()
    {   
        if (transform.position.x == x1 && transform.position.y == y1)
        {
            for (int i = 0; i < rooms.Length; i++)
            {
                if (rooms[i].transform.position.x == x2 && rooms[i].transform.position.y == y2 && rooms[i].transform.localScale.z.ToString().Contains("2"))
                {
                    vic.active = true;
                    if (st)
                    {
                        stars.active = true;
                        n++;
                    }
                    if (st)
                    {
                        
                        n++;
                    }
                    PlayerPrefs.SetInt("levelstar" + lev, n);
                    PlayerPrefs.Save();
                }
            }
        }   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "star")
        {
            Destroy(collision.gameObject);
            star();
        }
    }
    private void star()
    {
        st = true;
    }
}
