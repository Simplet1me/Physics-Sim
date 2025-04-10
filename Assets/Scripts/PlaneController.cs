using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaneController : MonoBehaviour
{
    public Slider slider;
    void Start()
    {
        
    }
    void Update()
    {
        transform.rotation = Quaternion.Euler(slider.value, 0, 0);
    }
}
