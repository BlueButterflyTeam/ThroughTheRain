using UnityEngine;
using System.Collections;

public class BreakObjects : MonoBehaviour {

	public void breakObject()
    {
        StartCoroutine(ExecuteAfterTime(.25f));
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;

        Destroy(gameObject, 2f);
    }
}
