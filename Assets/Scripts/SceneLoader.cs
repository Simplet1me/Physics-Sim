using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour{
    public Button button;
    public Animator animator;
    void Start(){
        button.onClick.AddListener(LoadSceneA);
    }

    // Update is called once per frame
    void Update(){

    }

    private void LoadSceneA(){
        StartCoroutine(LoadScene(1));
    }

    IEnumerator LoadScene(int index) {
        animator.SetBool("FadeIn",true);
        animator.SetBool("FadeOut",false);

        yield return new WaitForSeconds(1);

        AsyncOperation async = SceneManager.LoadSceneAsync(index);
        async.completed += OnLoadedScene;
    }
    private void OnLoadedScene(AsyncOperation obj) {
        animator.SetBool("FadeIn", false);
        animator.SetBool("FadeOut", true);
    }
}
