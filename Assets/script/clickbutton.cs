using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class clickbutton : MonoBehaviour
{
    [SerializeField] private Animator button;
    [SerializeField] private int Scene;
    private Vector2 checktouch;
    [SerializeField] private GameObject Timer;
    [SerializeField] private float x, y;
    void Start()
    {
        Timer.GetComponent<Timer>().enabled = false;
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
                    if (transform.position.x + x > checktouch.x && transform.position.x - x < checktouch.x && transform.position.y + y > checktouch.y && transform.position.y - y < checktouch.y)
                    {
                        button.SetBool("click", true);
                        int n = 0,t= Scene - 4;
                        for(int i = 0; i < 3; i++)
                        {
                            if(GameObject.Find("star" + (i + 1)).active)
                            {
                                n++;
                            }
                        }
                        if(PlayerPrefs.GetInt("levelstar" + t) > n)
                        {
                            n = PlayerPrefs.GetInt("levelstar" + t);
                        }
                        PlayerPrefs.SetInt("levelstar", n);
                        PlayerPrefs.Save();
                        Invoke("NewScene", 0.5f);
                    }
                    break;
            }
        }
    }
    private void NewScene()
    {
        button.SetBool("click", false);
        SceneManager.LoadScene(Scene);
    }
}
