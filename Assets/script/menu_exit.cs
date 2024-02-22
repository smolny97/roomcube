using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu_exit : MonoBehaviour
{
    [SerializeField] private Animator button;
    private Vector2 checktouch;
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    checktouch.x = Camera.main.ScreenPointToRay(Input.GetTouch(0).position).origin.x;
                    checktouch.y = Camera.main.ScreenPointToRay(Input.GetTouch(0).position).origin.y;
                    if (transform.position.x + 3f > checktouch.x && transform.position.x - 3f < checktouch.x && transform.position.y + 0.2f > checktouch.y && transform.position.y - 0.2f < checktouch.y)
                    {
                        button.SetBool("click", true);
                        Invoke("NewScene", 0.5f);
                    }
                    break;
            }
        }
    }
    private void NewScene()
    {
        button.SetBool("click", false);
        Application.Quit();
    }
}
