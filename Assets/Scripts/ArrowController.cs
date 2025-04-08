using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class ArrowController : MonoBehaviour {
    [Header("Ðý×ªÉèÖÃ")]
    public Slider slider;
    void Start() {
        
    }

    void Update() {
        transform.rotation = Quaternion.Euler(90-slider.value,0,0);
    }
}
