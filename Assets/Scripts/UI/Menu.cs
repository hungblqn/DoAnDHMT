using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject UIPanel;
    private bool isActive = false;
    // Update is called once per frame
    public bool getIsActive()
    {
        return isActive;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isActive == true)
            {
                isActive = false;
            }
            else
            {
                isActive = true;
            }
        }
        if (isActive == true)
        {
            UIPanel.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            UIPanel.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
    
}
