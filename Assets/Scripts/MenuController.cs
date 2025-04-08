using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    bool isOpen;
    public GameObject canvas;
    public CameraController controller;
    public CameraFreeController freeController;
    public Toggle toggle;
    void Start(){

        isOpen = false;
        canvas.SetActive(false);
    }

    void Update(){
        if (toggle.isOn){
            controller.enabled = false;
            freeController.enabled = true;
        }else{
            controller.enabled = true;
            freeController.enabled = false;
        }


        if (Input.GetKeyDown(KeyCode.Escape)){
        if(isOpen){
                isOpen = false;
            }else{
                isOpen = true;
            }
            Debug.Log($"isOpen = {isOpen}");
        }
        if (isOpen) {
            canvas.SetActive(true);
        }else{
            canvas.SetActive(false);
        }
    }
}
