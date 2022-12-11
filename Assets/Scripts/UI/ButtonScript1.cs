using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript1 : MonoBehaviour
{
    public GameObject helpPanel;

    public void Help()
    {
        helpPanel.gameObject.SetActive(true);
    }
    public void Back()
    {
        helpPanel.gameObject.SetActive(false);
    }
}
