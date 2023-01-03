using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensivity : MonoBehaviour
{
    public Slider slider;
    private void Start()
    {
        slider.value = Camera.main.GetComponent<CameraController>().mouseSensivity / 500;
    }
    public void ChangeSensivity()
    {
        Camera.main.GetComponent<CameraController>().mouseSensivity = 500 * slider.value;
        Debug.Log(slider.value);
    }
}
