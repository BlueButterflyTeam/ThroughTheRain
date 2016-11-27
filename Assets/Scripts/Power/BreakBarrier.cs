using UnityEngine;
using System.Collections;

public class BreakBarrier : MonoBehaviour {

    public void breakBarrier()
    {
        gameObject.transform.GetChild(0).GetComponent<BreakObjects>().breakObject();
        gameObject.transform.GetChild(1).GetComponent<BreakObjects>().breakObject();

        Destroy(gameObject, 5f);
    }
}
