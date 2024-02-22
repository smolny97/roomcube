using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    private Vector2 checktouch;
    private void Update()
    { 
        if (Input.touchCount > 0)
        {
            checktouch.x = Camera.main.ScreenPointToRay(Input.GetTouch(0).position).origin.x;
            checktouch.y = Camera.main.ScreenPointToRay(Input.GetTouch(0).position).origin.y;
            if (transform.position.x + 0.5f > checktouch.x && transform.position.x - 0.5f < checktouch.x && transform.position.y - 0.5f < checktouch.y && transform.position.y + 0.5f > checktouch.y)
            {
                if (this.gameObject.tag == "restart")
                {
                    Restart();
                }
            }
        }       
    }
    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }
    private void Menu()
    {

    }
}
