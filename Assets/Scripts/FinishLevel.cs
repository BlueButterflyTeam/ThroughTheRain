using UnityEngine;
using System.Collections;

public class FinishLevel : MonoBehaviour {
    public string sceneName;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
