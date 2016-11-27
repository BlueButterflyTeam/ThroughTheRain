using UnityEngine;
using System.Collections;

public class FinishLevel : MonoBehaviour {
    public int level;
    public AudioClip sound;
    public UnityEngine.UI.Text gameWonText;

    private AsyncOperation async;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameWonText.gameObject.SetActive(true);
            other.GetComponent<BasePlayerController>().immobilize();
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            other.GetComponent<Rigidbody2D>().isKinematic = true;

            StartCoroutine(Load());
        }
    }

    private IEnumerator Load()
    {
        async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(level);
        async.allowSceneActivation = false;


        AudioManager manager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        manager.StopMusic();
        manager.RandomizeSfx(sound);

        while (manager.isSoundPlaying())
        {
            yield return null;
        }

        async.allowSceneActivation = true;
    }
}
