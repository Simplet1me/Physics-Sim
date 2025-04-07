using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    bool isOpen;
    public GameObject canvas;
    void Start(){
        isOpen = false;
        canvas.SetActive(false);
    }

    void Update(){
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
