using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonExit : MonoBehaviour
{
    public Button button;
    void Start() {
        button.onClick.AddListener(Exit);
    }
    public void Exit() {
        Application.Quit();
    }
}
