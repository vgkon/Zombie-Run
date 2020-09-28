using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWon : MonoBehaviour
{
    Canvas gameWonCanvas;
    //[SerializeField] Canvas gameWonCanvas;
    // Start is called before the first frame update
    void Start()
    {
        GameObject tempObject = GameObject.Find("Game Won Canvas");
        if (tempObject != null)
        {
            //Debug.Log("Found tempObject " + tempObject.name);
            gameWonCanvas = tempObject.GetComponent<Canvas>();
            if (gameWonCanvas == null)
            {
                Debug.Log("Could not locate Canvas component on " + tempObject.name);
            }
            else
            {
                gameWonCanvas.enabled = false;
            }
        }
        else
        {
            Debug.Log("Could not find tempObject");
        }
    }

    public void HandleVictory()
    {
        gameWonCanvas.enabled = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            HandleVictory();
        }
    }
}
