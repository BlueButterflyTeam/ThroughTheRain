using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

    public void LoadByIndex(int sceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }
}
