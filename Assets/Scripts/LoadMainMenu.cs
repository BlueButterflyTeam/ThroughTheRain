using UnityEngine;
using System.Collections;

public class LoadMainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        clickToLoadASync(1);
	}

    private AsyncOperation async;

    public void clickToLoadASync(int lvl)
    {
        StartCoroutine(LoadLevelWithBar(lvl));
    }

    IEnumerator LoadLevelWithBar(int level)
    {
        async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(level);

        while (!async.isDone)
        {
            yield return null;
        }
    }
}
