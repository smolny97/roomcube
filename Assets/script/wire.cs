using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class wire : MonoBehaviour
{
    [SerializeField] private GameObject[] rooms, Wires;
    private int[,] mas;
    [SerializeField] private Animator[] door;
    [SerializeField] private Animator wiree;
    [SerializeField] private int x, y, n=0;
    [SerializeField] private int[] number;
    private void Start()
    {
        mas = new int[x * 3, y * 3];
        int f = 0,h=0;
        while (f<31)
        {
            if (!GameObject.Find("room" + f))
            {
                rooms = new GameObject[f];
                for (int i = 0; i < f; i++)
                {
                    rooms[i] = GameObject.Find("room" + i);
                    if (rooms[i].tag.Contains('n') || rooms[i].tag.Contains('e') || rooms[i].tag.Contains('s') || rooms[i].tag.Contains('w'))
                    {
                        n++;
                    }
                }              
                Wires = new GameObject[n];                         
                for (int i = 0; i < rooms.Length; i++)
                {
                    if (rooms[i].tag.Contains('n') || rooms[i].tag.Contains('e') || rooms[i].tag.Contains('s') || rooms[i].tag.Contains('w'))
                    {
                        Wires[h] = rooms[i];
                        h++;
                    }
                }
                f =31;
            }
            f++;
        }
    }
    private void CheckWires()
    {
        for (int i = 0; i < x * 3; i++)
        {
            for (int j = 0; j < y * 3; j++)
            {
                mas[i, j] = 99;
            }
        }
        for (int i = 0; i < Wires.Length; i++)
        {
            mas[Convert.ToInt32(Wires[i].transform.position.x) + 1, Convert.ToInt32(Wires[i].transform.position.y) + 1] = 0;
            if (Wires[i].tag.Contains('n'))
            {
                mas[Convert.ToInt32(Wires[i].transform.position.x) + 1, Convert.ToInt32(Wires[i].transform.position.y) + 2] = 0;
            }
            if (Wires[i].tag.Contains('e'))
            {
                mas[Convert.ToInt32(Wires[i].transform.position.x) + 2, Convert.ToInt32(Wires[i].transform.position.y) + 1] = 0;
            }
            if (Wires[i].tag.Contains('s'))
            {
                mas[Convert.ToInt32(Wires[i].transform.position.x) + 1, Convert.ToInt32(Wires[i].transform.position.y)] = 0;
            }
            if (Wires[i].tag.Contains('w'))
            {
                mas[Convert.ToInt32(Wires[i].transform.position.x), Convert.ToInt32(Wires[i].transform.position.y) + 1] = 0;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Box")
        {
            wiree.SetBool("active", true);
            CheckWires();
            AstarWire();
            for (int i = 0; i < number.Length; i++)
            {
                if (mas[Convert.ToInt32(rooms[number[i]].transform.position.x + 1), Convert.ToInt32(rooms[number[i]].transform.position.y + 1)] != 99 && mas[Convert.ToInt32(rooms[number[i]].transform.position.x + 2), Convert.ToInt32(rooms[number[i]].transform.position.y + 1)] != 0)
                {
                    if (rooms[number[i]].transform.localScale.z.ToString().Contains("6") && !rooms[number[i]].transform.localScale.z.ToString().Contains("2") || rooms[number[i]].transform.localScale.z.ToString().Contains("7") && !rooms[number[i]].transform.localScale.z.ToString().Contains("4"))
                    {
                        door[i].SetBool("active", true);
                        rooms[number[i]].transform.localScale = new Vector3(rooms[number[i]].transform.localScale.x, rooms[number[i]].transform.localScale.y, Convert.ToInt32(rooms[number[i]].transform.localScale.z.ToString() + '2'));
                        door[i].GetComponent<AudioSource>().enabled = true;
                        door[i].GetComponent<AudioSource>().Play();
                    }
                    this.GetComponent<AudioSource>().enabled = true;
                    this.GetComponent<AudioSource>().Play();
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Box")
        {
            wiree.SetBool("active", false);
            for (int i = 0; i < door.Length; i++)
            {
                if (door[i].GetBool("active"))
                {
                    door[i].SetBool("active", false);
                    door[i].GetComponent<AudioSource>().Play();
                    string s = rooms[number[i]].transform.localScale.z.ToString();
                    s = Regex.Replace(s, "2", "", RegexOptions.IgnoreCase);
                    rooms[number[i]].transform.localScale = new Vector3(rooms[number[i]].transform.localScale.x, rooms[number[i]].transform.localScale.y, Convert.ToInt32(s));
                    this.GetComponent<AudioSource>().enabled = true;
                    this.GetComponent<AudioSource>().Play();
                }
            }
        }
    }
    private void AstarWire()
    {
        var first = new Queue<string>();
        var second = new Queue<string>();
        string pos = (transform.position.x+1).ToString() + "," + (transform.position.y + 2.155f).ToString(), pos2;   
        first.Enqueue(pos);
        int i = 1;       
        while (first.Count > 0 || second.Count > 0)
        {
            pos = first.Dequeue();
            mas[Convert.ToInt32(pos.Substring(0, pos.IndexOf(','))), Convert.ToInt32(pos.Substring(pos.IndexOf(',')+1))] = i;
            if (Convert.ToInt32(pos.Substring(0, pos.IndexOf(','))) + 1 < x*3 && mas[Convert.ToInt32(pos.Substring(0, pos.IndexOf(','))) + 1, Convert.ToInt32(pos.Substring(pos.IndexOf(',') + 1))] == 0)
            {
                pos2 = (Convert.ToInt32(pos.Substring(0, pos.IndexOf(','))) + 1).ToString() + "," + (Convert.ToInt32(pos.Substring(pos.IndexOf(',') + 1))).ToString();
                second.Enqueue(pos2);
            }
            if (Convert.ToInt32(pos.Substring(0, pos.IndexOf(','))) - 1 >= 0 && mas[Convert.ToInt32(pos.Substring(0, pos.IndexOf(','))) - 1, Convert.ToInt32(pos.Substring(pos.IndexOf(',') + 1))] == 0)
            {
                pos2 = (Convert.ToInt32(pos.Substring(0, pos.IndexOf(','))) - 1).ToString() + "," + (Convert.ToInt32(pos.Substring(pos.IndexOf(',') + 1))).ToString();
                second.Enqueue(pos2);
            }
            if (Convert.ToInt32(pos.Substring(pos.IndexOf(',') + 1)) + 1 < y*3 && mas[Convert.ToInt32(pos.Substring(0, pos.IndexOf(','))), Convert.ToInt32(pos.Substring(pos.IndexOf(',') + 1)) + 1] == 0)
            {
                pos2 = Convert.ToInt32(pos.Substring(0, pos.IndexOf(','))).ToString() + "," + (Convert.ToInt32(pos.Substring(pos.IndexOf(',') + 1)) + 1).ToString();
                second.Enqueue(pos2);
            }
            if (Convert.ToInt32(pos.Substring(pos.IndexOf(',') + 1)) - 1 >= 0 && mas[Convert.ToInt32(pos.Substring(0, pos.IndexOf(','))), Convert.ToInt32(pos.Substring(pos.IndexOf(',') + 1)) - 1] == 0)
            {
                pos2 = Convert.ToInt32(pos.Substring(0, pos.IndexOf(','))).ToString() + "," + (Convert.ToInt32(pos.Substring(pos.IndexOf(',') + 1)) - 1).ToString();
                second.Enqueue(pos2);
            }
            if (first.Count == 0)
            {
                while (second.Count > 0)
                {
                    first.Enqueue(second.Dequeue());
                }
                i++;                
            }
        }
    }  
}
