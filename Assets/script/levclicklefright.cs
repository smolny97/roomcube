using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levclicklefright : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private bool left, way = false;
    private Vector2 checktouch,move;
    private float speed = 30;
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    checktouch.x = Camera.main.ScreenPointToRay(Input.GetTouch(0).position).origin.x;
                    checktouch.y = Camera.main.ScreenPointToRay(Input.GetTouch(0).position).origin.y;
                    if (transform.position.x + 0.5f > checktouch.x && transform.position.x - 0.5f < checktouch.x && transform.position.y + 0.5f > checktouch.y && transform.position.y - 0.5f < checktouch.y)
                    {
                        if (left && wall.transform.position.x > -105f)
                        {
                            move = new Vector3(wall.transform.position.x - 17.8f, wall.transform.position.y,-1);
                            way = true;
                        }
                        if (!left && wall.transform.position.x < -1)
                        {
                            move = new Vector3(wall.transform.position.x + 17.8f, wall.transform.position.y, -1);
                            way = true;
                        }
                    }
                    break;
            }
        }
        if (way)
        {
            moves();
        }
    }
    private void moves() 
    {
        wall.transform.position = Vector3.MoveTowards(wall.transform.position, move, Time.deltaTime * speed);
        if (Vector2.Distance(wall.transform.position, move) < 0.0002f)
        {
            way = false;
            return;
        }
    }
}
