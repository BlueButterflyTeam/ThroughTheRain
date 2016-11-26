using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClickToLoadASync : MonoBehaviour {

    public Slider loadingBar;
    public GameObject loadingPanel;

    private AsyncOperation async;

    public void clickToLoadASync(int lvl)
    {
        loadingPanel.SetActive(true);

        StartCoroutine(LoadLevelWithBar(lvl));
    }

    IEnumerator LoadLevelWithBar(int level)
    {
        async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(level);

        while(!async.isDone)
        {
            loadingBar.value = async.progress;
            yield return null;
        }
    }
}
