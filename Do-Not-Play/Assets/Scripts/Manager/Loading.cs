using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Loading : MonoBehaviour
{
    public Slider slider;
    public Text text;//百分制显示进度加载情况
    private void Start()
    {
        if (GlobalScene.nextScene == "first")
        {
            StartLoading();
        }
        GlobalScene.nextScene = null;
    }
    
    public void StartLoading(LoadSceneMode LoadSceneMode = LoadSceneMode.Single)
    {
        StartCoroutine(loadScene(LoadSceneMode));
    }
    IEnumerator loadScene(LoadSceneMode loadSceneMode)
    {
        int displayProgress = 0;
        int toProgress = 0;
        AsyncOperation op = SceneManager.LoadSceneAsync(GlobalScene.nextScene, loadSceneMode);
        op.allowSceneActivation = false;
        while (op.progress < 0.9f)
        {
            toProgress = (int)op.progress * 100;
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                SetLoadingPercentage(displayProgress);
                yield return new WaitForEndOfFrame();
            }
        }
        toProgress = 100;
        while (displayProgress < toProgress)
        {
            ++displayProgress;
            SetLoadingPercentage(displayProgress);
            yield return new WaitForEndOfFrame();
        }
        op.allowSceneActivation = true;
    }
    private void SetLoadingPercentage(int displayProgress)
    {
        if (slider != null && text != null)
        {
            slider.value = displayProgress;
            text.text = displayProgress.ToString() + "%";
        }
    }
}