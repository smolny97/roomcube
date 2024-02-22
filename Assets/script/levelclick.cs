using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelclick : MonoBehaviour
{
    [SerializeField] private Animator button;
    [SerializeField] private int Scene;
    [SerializeField] private string strlev;
    private Vector2 checktouch;
    private void Start()
    {
        button.SetInteger("lev", PlayerPrefs.GetInt(strlev)); 
    }
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
                    if (transform.position.x + 1.5f > checktouch.x && transform.position.x - 1.5f < checktouch.x && transform.position.y + 1.5f > checktouch.y && transform.position.y - 1.5f < checktouch.y)
                    {
                        SceneManager.LoadScene(Scene);
                    }
                    break;
            }
        }
    }
    
}
