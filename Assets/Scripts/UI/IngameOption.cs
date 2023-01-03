using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameOption : MonoBehaviour
{
    public GameObject optionPanel;

    public void Help()
    {
        optionPanel.gameObject.SetActive(true);
    }
    public void Back()
    {
        optionPanel.gameObject.SetActive(false);
    }
}
