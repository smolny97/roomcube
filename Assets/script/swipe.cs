using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class swipe : MonoBehaviour
{
    private Vector2 fp, lp, move, checktouch;
    private Vector3 moveplayer, movebox;
    private int nesw=0,position, posbox;
    [SerializeField] private float speed=12;
    [SerializeField] private int x, y;
    [SerializeField] private GameObject player,wire;
    [SerializeField] private GameObject[] arrows = new GameObject[4];
    [SerializeField] private GameObject[] rooms, box;
    private bool TrueTouch = true, CheckSwipe;//TrueTouch проверка касания на комнату
    private void Start()
    {
        int j=0;
        while (TrueTouch)
        {
            if (!GameObject.Find("room" + j))
            {
                rooms = new GameObject[j];
                for (int i = 0; i < j; i++)
                {
                    rooms[i] = GameObject.Find("room" + i);
                }
                TrueTouch = false;
            }
            j++;
        }
        TrueTouch = true;
        j = 0;
        while (TrueTouch)
        {
            if (!GameObject.Find("box" + j))
            {
                box = new GameObject[j];
                for (int i = 0; i < j; i++)
                {
                    box[i] = GameObject.Find("box" + i);
                }
                TrueTouch = false;
            }
            j++;
        }
        TrueTouch = true;
        player = GameObject.Find("Player");
    }
    private void Update()
    {     
        CheckTouch();
        if (nesw == 0 && TrueTouch)
        {
            Swipe();           
        }
        else if (nesw != 0)
        {
            if (wire)
            {
                if (wire.GetComponent<BoxCollider2D>())
                {
                    wire.GetComponent<BoxCollider2D>().enabled = false;
                    Move();
                }
            }
            else
            {
                Move();
            }
           
        }
    }
    private void CheckTouch() // проверка касания на комнату
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {              
                case TouchPhase.Began:
                    checktouch.x = Camera.main.ScreenPointToRay(Input.GetTouch(0).position).origin.x;
                    checktouch.y = Camera.main.ScreenPointToRay(Input.GetTouch(0).position).origin.y;
                    if (transform.position.x + 1.5f > checktouch.x && transform.position.x - 1.5f < checktouch.x && transform.position.y - 1.5f < checktouch.y && transform.position.y + 1.5f > checktouch.y)
                    {
                        TrueTouch = true;
                    }
                    else
                    {
                        TrueTouch = false;
                    }
                    break;
            }
        }      
    }
    private void Swipe()// проверка можно ли комнате двигаться
    {
        CheckSwipe = true;
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fp = touch.position;
                lp = touch.position;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                lp = touch.position;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                if ((fp.x - lp.x) > 80 && transform.position.x != 0) // left swipe
                {
                    for (int i = 0; i < rooms.Length; i++)
                    {
                        if(rooms[i].transform.position.x == transform.position.x - 3 && rooms[i].transform.position.y == transform.position.y)
                        {
                            CheckSwipe = false;
                        }
                    }
                    if (CheckSwipe)
                    {
                        nesw = 4;
                        this.GetComponent<AudioSource>().enabled = true;
                        this.GetComponent<AudioSource>().Play();
                        move = new Vector2(transform.position.x - 3, transform.position.y);
                        if (player.transform.position.x < this.transform.position.x + 1.5f && player.transform.position.x > this.transform.position.x - 1.5f && player.transform.position.y < this.transform.position.y + 1.5f && player.transform.position.y > this.transform.position.y - 1.5f)
                        {
                            moveplayer = new Vector3(player.transform.position.x - 3, player.transform.position.y, player.transform.position.z);
                        }
                        for (int n = 0; n < box.Length; n++)
                        {
                            if (box[n].transform.position.x < this.transform.position.x + 1.5f && box[n].transform.position.x > this.transform.position.x - 1.5f && box[n].transform.position.y < this.transform.position.y + 1.5f && box[n].transform.position.y > this.transform.position.y - 1.5f)
                            {
                                posbox = n;
                                movebox = new Vector3(box[n].transform.position.x - 3, box[n].transform.position.y, box[n].transform.position.z);
                            }
                        }
                    }                    
                }
                else if ((fp.x - lp.x) < -80 && transform.position.x != x*3-3) // right swipe
                {
                    for (int i = 0; i < rooms.Length; i++)
                    {                     
                        if (rooms[i].transform.position.x == transform.position.x + 3 && rooms[i].transform.position.y == transform.position.y)
                        {
                            CheckSwipe = false;
                        }
                    }
                    if (CheckSwipe)
                    {
                        nesw = 2;
                        this.GetComponent<AudioSource>().enabled = true;
                        this.GetComponent<AudioSource>().Play();
                        move = new Vector2(transform.position.x + 3, transform.position.y);
                        if (player.transform.position.x < this.transform.position.x + 1.5f && player.transform.position.x > this.transform.position.x - 1.5f && player.transform.position.y < this.transform.position.y + 1.5f && player.transform.position.y > this.transform.position.y - 1.5f)
                        {
                            moveplayer = new Vector3(player.transform.position.x + 3, player.transform.position.y, player.transform.position.z);
                        }
                        for (int n = 0; n < box.Length; n++)
                        {
                            if (box[n].transform.position.x < this.transform.position.x + 1.5f && box[n].transform.position.x > this.transform.position.x - 1.5f && box[n].transform.position.y < this.transform.position.y + 1.5f && box[n].transform.position.y > this.transform.position.y - 1.5f)
                            {
                                posbox = n;
                                movebox = new Vector3(box[n].transform.position.x + 3, box[n].transform.position.y, box[n].transform.position.z);
                            }
                        }
                    }                   
                }
                else if ((fp.y - lp.y) < -80 && transform.position.y != y * 3-3) // up swipe
                {
                    for (int i = 0; i < rooms.Length; i++)
                    {
                        if (rooms[i].transform.position.x == transform.position.x && rooms[i].transform.position.y == transform.position.y + 3)
                        {
                            CheckSwipe = false;
                        }
                    }
                    if (CheckSwipe)
                    {
                        nesw = 1;
                        this.GetComponent<AudioSource>().enabled = true;
                        this.GetComponent<AudioSource>().Play();
                        move = new Vector2(transform.position.x, transform.position.y + 3);
                        if (player.transform.position.x < this.transform.position.x + 1.5f && player.transform.position.x > this.transform.position.x - 1.5f && player.transform.position.y < this.transform.position.y + 1.5f && player.transform.position.y > this.transform.position.y - 1.5f)
                        {
                            moveplayer = new Vector3(player.transform.position.x, player.transform.position.y + 3, player.transform.position.z);
                        }
                        for (int n = 0; n < box.Length; n++)
                        {
                            if (box[n].transform.position.x < this.transform.position.x + 1.5f && box[n].transform.position.x > this.transform.position.x - 1.5f && box[n].transform.position.y < this.transform.position.y + 1.5f && box[n].transform.position.y > this.transform.position.y - 1.5f)
                            {
                                posbox = n;
                                movebox = new Vector3(box[n].transform.position.x, box[n].transform.position.y + 3, box[n].transform.position.z);
                            }
                        }
                    }                    
                }
                else if ((fp.y - lp.y) > 80 && transform.position.y != 0) // down swipe
                {
                    for (int i = 0; i < rooms.Length; i++)
                    {
                        if (rooms[i].transform.position.x == transform.position.x && rooms[i].transform.position.y == transform.position.y - 3)
                        {
                            CheckSwipe = false;
                        }
                    }
                    if (CheckSwipe)
                    {
                        nesw = 3;
                        this.GetComponent<AudioSource>().enabled = true;
                        this.GetComponent<AudioSource>().Play();
                        move = new Vector2(transform.position.x, transform.position.y - 3);
                        if (player.transform.position.x < this.transform.position.x + 1.5f && player.transform.position.x > this.transform.position.x - 1.5f && player.transform.position.y < this.transform.position.y + 1.5f && player.transform.position.y > this.transform.position.y - 1.5f)
                        {
                            moveplayer = new Vector3(player.transform.position.x, player.transform.position.y - 3, player.transform.position.z);
                        }
                        for (int n = 0; n < box.Length; n++)
                        {
                            if (box[n].transform.position.x < this.transform.position.x + 1.5f && box[n].transform.position.x > this.transform.position.x - 1.5f && box[n].transform.position.y < this.transform.position.y + 1.5f && box[n].transform.position.y > this.transform.position.y - 1.5f)
                            {
                                posbox = n;
                                movebox = new Vector3(box[n].transform.position.x, box[n].transform.position.y - 3, box[n].transform.position.z);
                            }
                        }
                    }                  
                }
                TrueTouch = false;                
            }
        }
    }
    private void Move()
    {
        for (int i = 0; i < 4; i++)
        {
            if (arrows[i].activeSelf)
            {
                arrows[i].SetActive(false);
            }
        }
        this.transform.position = Vector2.MoveTowards(transform.position, move, Time.deltaTime * speed);
        if (player.transform.position.x < this.transform.position.x + 1.5f && player.transform.position.x > this.transform.position.x - 1.5f && player.transform.position.y < this.transform.position.y + 1.5f && player.transform.position.y > this.transform.position.y - 1.5f)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, moveplayer, Time.deltaTime * speed);
        }
        if (box.Length != 0)
        {
            if (box[posbox].transform.position.x < this.transform.position.x + 1.5f && box[posbox].transform.position.x > this.transform.position.x - 1.5f && box[posbox].transform.position.y < this.transform.position.y + 1.5f && box[posbox].transform.position.y > this.transform.position.y - 1.5f)
            {
                box[posbox].transform.position = Vector3.MoveTowards(box[posbox].transform.position, movebox, Time.deltaTime * speed);
            }
        }
        if (Vector2.Distance(transform.position, move) < 0.00002f)
        {
            Active();
            nesw = 0;
            if (wire)
            {
                if (wire.GetComponent<BoxCollider2D>())
                {
                    wire.GetComponent<BoxCollider2D>().enabled = true;
                }

            }
        }
    }
   
    private void Active()
    {
        for (int i = 0; i < rooms.Length; i++)
        {
            if (player.transform.position.x < rooms[i].transform.position.x + 1.5f && player.transform.position.x > rooms[i].transform.position.x - 1.5f && player.transform.position.y < rooms[i].transform.position.y + 1.5f && player.transform.position.y > rooms[i].transform.position.y - 1.5f)
            {
                arrows[0].transform.position = new Vector3(rooms[i].transform.position.x, rooms[i].transform.position.y + 1.5f,-2);
                arrows[1].transform.position = new Vector3(rooms[i].transform.position.x + 1.5f, rooms[i].transform.position.y, -2);
                arrows[2].transform.position = new Vector3(rooms[i].transform.position.x, rooms[i].transform.position.y - 1.5f, -2);
                arrows[3].transform.position = new Vector3(rooms[i].transform.position.x - 1.5f, rooms[i].transform.position.y, -2);
                position = i;
            }
        }
        
        for (int i = 0; i < rooms.Length; i++)
        {
           
            if (rooms[position].transform.localScale.z.ToString().Contains("5"))
            {
                arrows[0].SetActive(true);
            }
            if (rooms[i].transform.position.x == rooms[position].transform.position.x && rooms[i].transform.position.y == rooms[position].transform.position.y + 3)
            {
                if (rooms[i].transform.localScale.z.ToString().Contains("3") && rooms[position].transform.localScale.z.ToString().Contains("1"))
                {
                    arrows[0].SetActive(true);
                }
                for (int l = 0; l < box.Length; l++)
                {
                    print(position);
                    print(i);
                    if (rooms[position].transform.localScale.z.ToString().Contains("6") && rooms[i].transform.localScale.z.ToString().Contains("3") && box[l].transform.position.x - 1 == player.transform.position.x && box[l].transform.position.y + 0.5 == player.transform.position.y || rooms[position].transform.localScale.z.ToString().Contains("6") && rooms[i].transform.localScale.z.ToString().Contains("3") && box[l].transform.position.x + 1 == player.transform.position.x && box[l].transform.position.y + 0.5 == player.transform.position.y)
                    {
                        arrows[0].SetActive(true);
                    }
                }
            }
            if (rooms[i].transform.position.x == rooms[position].transform.position.x + 3 && rooms[i].transform.position.y == rooms[position].transform.position.y)
            {
                if (rooms[i].transform.localScale.z.ToString().Contains("4") && rooms[position].transform.localScale.z.ToString().Contains("2"))
                {
                    arrows[1].SetActive(true);
                }
            }
            if (rooms[i].transform.position.x == rooms[position].transform.position.x && rooms[i].transform.position.y == rooms[position].transform.position.y - 3)
            {
                if (rooms[i].transform.localScale.z.ToString().Contains("1") && rooms[position].transform.localScale.z.ToString().Contains("3"))
                {
                    arrows[2].SetActive(true);
                }
            }
            if (rooms[i].transform.position.x == rooms[position].transform.position.x - 3 && rooms[i].transform.position.y == rooms[position].transform.position.y)
            {
                if (rooms[i].transform.localScale.z.ToString().Contains("2") && rooms[position].transform.localScale.z.ToString().Contains("4"))
                {
                    arrows[3].SetActive(true);
                }
            }
        }
    } 
}
