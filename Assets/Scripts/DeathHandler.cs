using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    Canvas gameOverCanvas;
    //[SerializeField] Canvas gameOverCanvas;
    // Start is called before the first frame update
    void Start()
    {
        GameObject tempObject = GameObject.Find("Game Over Canvas");
        if(tempObject != null)
        {
            //Debug.Log("Found tempObject " + tempObject.name);
            gameOverCanvas = tempObject.GetComponent<Canvas>();
            if (gameOverCanvas == null)
            {
                Debug.Log("Could not locate Canvas component on " + tempObject.name);
            }
            else
            {
                gameOverCanvas.enabled = false;
            }
        }
        else
        {
            Debug.Log("Could not find tempObject");
        }
    }

    public void HandleDeath()
    {
        gameOverCanvas.enabled = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
