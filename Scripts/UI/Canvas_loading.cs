using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Canvas_loading : MonoBehaviour
{
    private Slider slider;
    private Button button;
    private string nextSceneName;
    private AsyncOperation async=null;
    public float moviePlayDoneTime=43f;
    private void Awake() 
    {
        slider=transform.GetChild(0).GetComponent<Slider>();
        button=transform.GetChild(1).GetComponent<Button>();
        nextSceneName=GameManager.Instance.sceneName;

        button.onClick.AddListener(Skip);
    }
    void Start() 
    {
       StartCoroutine("LoadScene");
       StartCoroutine("WaitMovieDone");
    }
    // Update is called once per frame
    void Update()
    {
        if(async.progress<0.9f)
        {
            slider.value=async.progress;
        }
        else
        {
            slider.gameObject.SetActive(false);
            button.gameObject.SetActive(true);
        }
    }
    IEnumerator LoadScene()
    {
        async=SceneManager.LoadSceneAsync(nextSceneName);
        async.allowSceneActivation=false;
        yield return null;
    }
    IEnumerator WaitMovieDone()
    {
        yield return new WaitForSeconds(moviePlayDoneTime);
        GameManager.Instance.CanvasStack.Clear();
        async.allowSceneActivation=true;
    }
    void Skip()
    {
        GameManager.Instance.CanvasStack.Clear();
        async.allowSceneActivation=true;
    }
}
