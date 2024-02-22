using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickArrow : MonoBehaviour
{
    [SerializeField] private GameObject[] arrows = new GameObject[4];
    [SerializeField] private GameObject player,waymus,wardmus,ladder;
    [SerializeField] private Animator play;
    [SerializeField] private GameObject[] rooms, box;
    [SerializeField] private float speed;
    private Vector2 checktouch;
    private Vector3 move=new Vector3(-10,-10,-1),movebox,movetop;
    private int position, positionbox,w,sd=0;
    [SerializeField] private int[] closet;//комнаты со шкафами они связаны
    private bool stopmove = false, active = false, downtop = false;
    private Animator wardrobe;
    [SerializeField] private GameObject[] wardrobeobj;
    private void Start()
    {
        int j = 0;
        while (!stopmove)
        {
            if (!GameObject.Find("room" + j))
            {
                rooms = new GameObject[j];
                for (int i = 0; i < j; i++)
                {
                    rooms[i] = GameObject.Find("room" + i);
                }
                stopmove = true;
            }
            j++;
        }
        stopmove = false;
        j = 0;
        while (!stopmove)
        {
            if (!GameObject.Find("box" + j))
            {
                box = new GameObject[j];
                for (int i = 0; i < j; i++)
                {
                    box[i] = GameObject.Find("box" + i);
                }
                stopmove = true;
            }
            j++;
        }
        stopmove = false;
        Active();
    }
    private void Update()
    {
        //this.gameObject.SetActive(false);
        if (Input.touchCount > 0)
        {
            ClickActive();
        }
        if (stopmove)
        {        
            MoveRoom();
        }
        if (downtop)
        {
            MoveTop();
        }
    }
    private void ClickActive()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    checktouch.x = Camera.main.ScreenPointToRay(Input.GetTouch(0).position).origin.x;
                    checktouch.y = Camera.main.ScreenPointToRay(Input.GetTouch(0).position).origin.y;
                    if (arrows[0].activeSelf && arrows[0].transform.position.x + 0.5f > checktouch.x && arrows[0].transform.position.x - 0.5f < checktouch.x && arrows[0].transform.position.y - 0.5f < checktouch.y && arrows[0].transform.position.y + 0.5f > checktouch.y)
                    {
                        if (transform.position.x % 3 == 0 && (transform.position.y + 0.5f) % 3 == 0 || transform.position.x % 3 == 0 && (transform.position.y - 0.5f) % 3 == 0)
                        {
                            if (rooms[position].transform.localScale.z.ToString().Contains("5"))
                            {
                                for (w = 0; w < closet.Length; w++)
                                {
                                    if (closet[w] == position)
                                    {
                                        wardrobe = wardrobeobj[w].GetComponent<Animator>();
                                        wardrobe.SetBool("open", true);
                                        wardmus.active = true;
                                        return;
                                    }
                                }
                                Active();
                                return;
                            }
                            //if (true)
                            //{
                            //    Ladder();
                            //}
                            else
                            {
                                play.SetBool("ladder", true);
                                ladder.GetComponent<AudioSource>().enabled = true;
                                ladder.GetComponent<AudioSource>().Play();
                                move = new Vector3(transform.position.x, transform.position.y + 3, -0.002f);
                                transform.position = new Vector3(transform.position.x, transform.position.y, -0.002f);
                                stopmove = true;
                                return;
                            }
                        }
                        if ((transform.position.x + 1) % 3 == 0 && (this.transform.position.y + 0.5f) % 3 == 0 || (this.transform.position.x - 1) % 3 == 0 && (this.transform.position.y + 0.5f) % 3 == 0)
                        {
                            if (rooms[position].transform.localScale.z.ToString().Contains("6"))
                            {
                                if ((transform.position.x + 1) % 3 == 0)
                                {
                                    waymus.active = true;
                                    move = new Vector3(transform.position.x + 1, transform.position.y, -1);
                                    movetop = new Vector3(transform.position.x + 1, transform.position.y + 3, -0.002f);
                                    play.SetBool("way", true);
                                }
                                else if ((transform.position.x - 1) % 3 == 0)
                                {
                                    waymus.active = true;
                                    move = new Vector3(transform.position.x - 1, transform.position.y, -1);
                                    movetop = new Vector3(transform.position.x - 1, transform.position.y + 3, -0.002f);
                                    play.SetBool("way2", true);
                                }
                                transform.position = new Vector3(transform.position.x, transform.position.y, -0.002f);
                                ladder.GetComponent<AudioSource>().enabled = true;
                                ladder.GetComponent<AudioSource>().Play();
                                downtop = true;
                            }
                        }

                    }
                    else if (arrows[1].activeSelf && arrows[1].transform.position.x + 0.5f > checktouch.x && arrows[1].transform.position.x - 0.5f < checktouch.x && arrows[1].transform.position.y - 0.5f < checktouch.y && arrows[1].transform.position.y + 0.5f > checktouch.y)
                    {
                        if ((transform.position.y + 0.5f) % 3 == 0)
                        {
                            for (int i = 0; i < box.Length; i++)
                            {
                                if (box[i].transform.position.x == transform.position.x + 3 && box[i].transform.position.y == transform.position.y - 0.5f)
                                {
                                    move = new Vector3(transform.position.x + 2, transform.position.y, -1);
                                    waymus.active = true;
                                }
                                else if (box[i].transform.position.x == transform.position.x + 2 && box[i].transform.position.y == transform.position.y - 0.5f)
                                {
                                    move = new Vector3(transform.position.x + 1, transform.position.y, -1);
                                    waymus.active = true;
                                }

                            }
                            if (move == new Vector3(-10, -10, -1))
                            {
                                waymus.active = true;
                                if (transform.position.x % 3 == 0)
                                {
                                    play.SetBool("way", true);
                                    move = new Vector3(transform.position.x + 3, transform.position.y, -1);
                                }
                                else if ((transform.position.x + 1) % 3 == 0)
                                {
                                    if (active)
                                    {
                                        play.SetBool("way", true);
                                        move = new Vector3(transform.position.x + 3, transform.position.y, -1);
                                        movebox = new Vector3(box[positionbox].transform.position.x + 3, box[positionbox].transform.position.y, -1);
                                    }
                                    else
                                    {
                                        play.SetBool("way", true);
                                        move = new Vector3(transform.position.x + 4, transform.position.y, -1);
                                    }

                                }
                                else if ((transform.position.x - 1) % 3 == 0)
                                {
                                    if (active)
                                    {
                                        play.SetBool("way", true);
                                        move = new Vector3(transform.position.x + 3, transform.position.y, -1);
                                        movebox = new Vector3(box[positionbox].transform.position.x + 3, box[positionbox].transform.position.y, -0.112f);
                                    }
                                    else
                                    {
                                        play.SetBool("way", true);
                                        move = new Vector3(transform.position.x + 2, transform.position.y, -1);
                                    }
                                }
                            }
                            play.SetBool("way", true);
                            stopmove = true;
                            if (active)
                            {
                                waymus.active = true;
                                box[positionbox].GetComponent<AudioSource>().enabled = true;
                                box[positionbox].GetComponent<AudioSource>().Play();
                            }
                            return;
                        }
                    }
                    else if (arrows[2].activeSelf && arrows[2].transform.position.x + 0.5f > checktouch.x && arrows[2].transform.position.x - 0.5f < checktouch.x && arrows[2].transform.position.y - 0.5f < checktouch.y && arrows[2].transform.position.y + 0.5f > checktouch.y)
                    {
                        if (transform.position.x % 3 == 0 && (transform.position.y + 0.5f) % 3 == 0)
                        {
                            play.SetBool("ladder", true);
                            ladder.GetComponent<AudioSource>().enabled = true;
                            ladder.GetComponent<AudioSource>().Play();
                            move = new Vector3(transform.position.x, transform.position.y - 3, -0.002f);
                            transform.position = new Vector3(transform.position.x, transform.position.y, -0.002f);
                            stopmove = true;
                            return;
                        }
                    }
                    else if (arrows[3].activeSelf && arrows[3].transform.position.x + 0.5f > checktouch.x && arrows[3].transform.position.x - 0.5f < checktouch.x && arrows[3].transform.position.y - 0.5f < checktouch.y && arrows[3].transform.position.y + 0.5f > checktouch.y)
                    {
                        if ((transform.position.y + 0.5f) % 3 == 0)
                        {
                            for (int i = 0; i < box.Length; i++)
                            {
                                if (box[i].transform.position.x == transform.position.x - 3 && box[i].transform.position.y == transform.position.y - 0.5f)
                                {
                                    waymus.active = true;
                                    move = new Vector3(transform.position.x - 2, transform.position.y, -1);
                                }
                                else if (box[i].transform.position.x == transform.position.x - 2 && box[i].transform.position.y == transform.position.y - 0.5f)
                                {
                                    waymus.active = true;
                                    move = new Vector3(transform.position.x - 1, transform.position.y, -1);
                                }
                                else if (box[i].transform.position.x == transform.position.x - 4 && box[i].transform.position.y == transform.position.y - 0.5f)
                                {
                                    waymus.active = true;
                                    move = new Vector3(transform.position.x - 3, transform.position.y, -1);
                                }
                            }
                            if (move == new Vector3(-10, -10, -1))
                            {
                                waymus.active = true;
                                if (transform.position.x % 3 == 0)
                                {
                                    move = new Vector3(transform.position.x - 3, transform.position.y, -1);
                                }
                                else if ((transform.position.x + 1) % 3 == 0)
                                {
                                    if (active)
                                    {
                                        move = new Vector3(transform.position.x - 3, transform.position.y, -1);
                                        movebox = new Vector3(box[positionbox].transform.position.x - 3, box[positionbox].transform.position.y, -0.112f);
                                    }
                                    else
                                    {
                                        move = new Vector3(transform.position.x - 2, transform.position.y, -1);
                                    }
                                }
                                else if ((transform.position.x - 1) % 3 == 0)
                                {
                                    if (active)
                                    {
                                        move = new Vector3(transform.position.x - 3, transform.position.y, -1);
                                        movebox = new Vector3(box[positionbox].transform.position.x - 3, box[positionbox].transform.position.y, -0.112f);
                                    }
                                    else
                                    {
                                        move = new Vector3(transform.position.x - 4, transform.position.y, -1);
                                    }
                                }
                            }
                            play.SetBool("way2", true);
                            stopmove = true;
                            if (active)
                            {
                                waymus.active = true;
                                box[positionbox].GetComponent<AudioSource>().enabled = true;
                                box[positionbox].GetComponent<AudioSource>().Play();
                            }
                            return;
                        }
                    }
                    for (int i = 0; i < box.Length; i++)
                    {
                        if (checktouch.x < box[i].transform.position.x + 0.5f && checktouch.x > box[i].transform.position.x - 0.5f && checktouch.y < box[i].transform.position.y + 1f && checktouch.y > box[i].transform.position.y)
                        {
                            if (transform.position.x + 1f == box[i].transform.position.x && transform.position.y - 0.5f == box[i].transform.position.y || transform.position.x - 1 == box[i].transform.position.x && transform.position.y - 0.5f == box[i].transform.position.y)
                            {
                                if (active)
                                {
                                    play.SetBool("box", false);
                                    active = false;
                                }
                                else
                                {
                                    play.SetBool("box", true);
                                    positionbox = i;
                                    active = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    public void Open()
    {
        if (w % 2 == 0)
        {
            player.gameObject.SetActive(false);
            transform.position = new Vector3(rooms[closet[w + 1]].transform.position.x, rooms[closet[w + 1]].transform.position.y - 0.5f, -1);
            wardrobe.SetBool("open", false);
            wardrobe = wardrobeobj[w + 1].GetComponent<Animator>();
        }
        else
        {
            player.gameObject.SetActive(false);
            transform.position = new Vector3(rooms[closet[w - 1]].transform.position.x, rooms[closet[w - 1]].transform.position.y - 0.5f, -1);
            wardrobe.SetBool("open", false);
            wardrobe = wardrobeobj[w - 1].GetComponent<Animator>();
        }
        wardrobe.SetBool("open", true);
    }
    public void Close()
    {
        player.gameObject.SetActive(true);
        wardrobe.SetBool("open", false);
        wardmus.active = false;
        Active();
    }
    private void MoveRoom()
    {
        transform.position = Vector3.MoveTowards(transform.position, move, Time.deltaTime * speed);
        if (active)
        {
            box[positionbox].transform.position = Vector3.MoveTowards(box[positionbox].transform.position, movebox, Time.deltaTime * speed);            
        }
        if (Vector3.Distance(transform.position, move) < 0.00002f)
        {           
            if (active)
            {
                box[positionbox].transform.position = movebox;
                movebox = new Vector3(-10, -10, -1);
            }
            waymus.active = false;
            play.SetBool("ladder", false);
            play.SetBool("way2", false);
            play.SetBool("way", false);
            move = new Vector3(-10, -10, -1);
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
            stopmove = false;
            Active();
            return;
        }
    }
    private void MoveTop()
    {
        if (Vector3.Distance(transform.position, move) < 0.00002f || sd==1)
        {
            play.SetBool("ladder", true);
            sd = 1;
            if (Vector3.Distance(transform.position, movetop) < 0.00002f)
            {
                if (active)
                {
                    movebox = new Vector3(-10, -10, -0.112f);
                }
                waymus.active = false;
                play.SetBool("ladder", false);
                play.SetBool("way2", false);
                play.SetBool("way", false);
                move = new Vector3(-10, -10, -1);
                movetop = new Vector3(-10, -10, -1);
                stopmove = false;
                Active();
                sd = 0;
                downtop = false;
                return;
                
            }
            else 
            {
                transform.position = Vector3.MoveTowards(transform.position, movetop, Time.deltaTime * speed);
            }
        }
        else if(sd==0)
        {
            transform.position = Vector3.MoveTowards(transform.position, move, Time.deltaTime * speed);
        }
    }
    private void Active()
    {
        for(int i = 0; i < 4; i++)
        {
            arrows[i].SetActive(false);
        }
        for (int i = 0, j = 0; i < rooms.Length; i++)
        {
            if (j == 0)
            {
                if (rooms[i].transform.position.x == transform.position.x && rooms[i].transform.position.y == transform.position.y + 0.5f || rooms[i].transform.position.x == transform.position.x && rooms[i].transform.position.y == transform.position.y - 0.5f || rooms[i].transform.position.x == transform.position.x + 1 && rooms[i].transform.position.y == transform.position.y + 0.5f || rooms[i].transform.position.x == transform.position.x - 1 && rooms[i].transform.position.y == transform.position.y + 0.5f)
                {
                    position = i;
                    i = -1;
                    j++; 
                    arrows[0].transform.position = new Vector3(rooms[position].transform.position.x, rooms[position].transform.position.y + 1.5f,-2);
                    arrows[1].transform.position = new Vector3(rooms[position].transform.position.x + 1.5f, rooms[position].transform.position.y, -2);
                    arrows[2].transform.position = new Vector3(rooms[position].transform.position.x, rooms[position].transform.position.y - 1.5f, -2);
                    arrows[3].transform.position = new Vector3(rooms[position].transform.position.x - 1.5f, rooms[position].transform.position.y, -2);
                }
            }          
            else 
            {
                if (rooms[position].transform.localScale.z.ToString().Contains("5"))
                {
                    arrows[0].SetActive(true);
                }
                if (rooms[i].transform.position.x == rooms[position].transform.position.x && rooms[i].transform.position.y - 3 == rooms[position].transform.position.y)
                {              
                    if (rooms[position].transform.localScale.z.ToString().Contains("1") && rooms[i].transform.localScale.z.ToString().Contains("3"))
                    {                      
                        arrows[0].SetActive(true);
                    }
                    for (int l = 0; l < box.Length; l++)
                    {
                        if (rooms[position].transform.localScale.z.ToString().Contains("6") && rooms[i].transform.localScale.z.ToString().Contains("3") && box[l].transform.position.x - 1 == this.transform.position.x && box[l].transform.position.y + 0.5 == this.transform.position.y || rooms[position].transform.localScale.z.ToString().Contains("6") && rooms[i].transform.localScale.z.ToString().Contains("3") && box[l].transform.position.x + 1 == this.transform.position.x && box[l].transform.position.y + 0.5 == this.transform.position.y)
                        {
                            arrows[0].SetActive(true);
                        }
                    }

                }          
                if (rooms[i].transform.position.x - 3 == rooms[position].transform.position.x && rooms[i].transform.position.y == rooms[position].transform.position.y)
                {
                    if (rooms[position].transform.localScale.z.ToString().Contains("2") && rooms[i].transform.localScale.z.ToString().Contains("4"))
                    {                     
                        arrows[1].SetActive(true);                       
                    }                 
                }
                if (rooms[i].transform.position.x == rooms[position].transform.position.x && rooms[i].transform.position.y + 3 == rooms[position].transform.position.y)
                {
                    if (rooms[position].transform.localScale.z.ToString().Contains("3") && rooms[i].transform.localScale.z.ToString().Contains("1"))
                    {                    
                        arrows[2].SetActive(true);
                    }
                }
                if (rooms[i].transform.position.x + 3 == rooms[position].transform.position.x && rooms[i].transform.position.y == rooms[position].transform.position.y)
                {
                    if (rooms[position].transform.localScale.z.ToString().Contains("4") && rooms[i].transform.localScale.z.ToString().Contains("2"))
                    {                      
                        arrows[3].SetActive(true);
                    }
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "star")
        {
            Destroy(collision);
            //take star
        }
    }
}
