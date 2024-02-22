using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prob : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        var people = new Queue<string>();
        var people2 = new Queue<string>();
        people.Enqueue("Tom");
        people.Enqueue("Bob");
        var n = people.Dequeue();
        people2 = people;
        string s = "dfgh,dfg";
        print(s.Substring(0, s.IndexOf(',')));
        print(s.Substring(s.IndexOf(',')+1));

    }

}
