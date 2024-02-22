using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnim : MonoBehaviour
{
    [SerializeField] private GameObject[] roooms = new GameObject[8];
    [SerializeField] private Vector2[] move = new Vector2[16];
    private int nroom = 0, nmove = 0;
    private float speed=3;
    private bool round = true;
    void Update()
    {
        if (round)
        {
            roooms[nroom].transform.position = Vector2.MoveTowards(roooms[nroom].transform.position, move[nmove + 1], Time.deltaTime * speed);
            if(Vector2.Distance(roooms[nroom].transform.position, move[nmove + 1]) < 0.0002f)
            {
                nroom++;
                nmove += 2;
            }
            if (nroom == 8)
            {
                round = false;
                nroom = 0;
                nmove = 0;
            }
        }
        else
        {
            roooms[nroom].transform.position = Vector2.MoveTowards(roooms[nroom].transform.position, move[nmove], Time.deltaTime * speed);
            if (Vector2.Distance(roooms[nroom].transform.position, move[nmove]) < 0.0002f)
            {
                nroom++;
                nmove += 2;
            }
            if (nroom == 8)
            {
                round = true;   
                nroom = 0;
                nmove = 0;
            }
        }
    }
}
