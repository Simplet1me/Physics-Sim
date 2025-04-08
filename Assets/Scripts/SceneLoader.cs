using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour{
    public Button button;
    public Animator animator;
    public int sceneIndex;
    void Start(){
        button.onClick.AddListener(LoadToScene);
    }

    // Update is called once per frame
    void Update(){

    }

    public void OnButtonExit() {
        Application.Quit();
    }
    private void LoadToScene(){
        StartCoroutine(LoadScene(sceneIndex));
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
