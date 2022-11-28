using UnityEngine;
using System.Collections;

public class CamewaShake : MonoBehaviour {
    public static IEnumerator Shake(float duration, float magnitude) {
        Vector3 orignalPosition = new Vector3(0f, 0f, -10);
        float elapsed = 0f;

        while (elapsed < duration) {
            float x = Random.Range(-0.5f, 0.5f) * magnitude;
            float y = Random.Range(-0.25f, 0.25f) * magnitude;

            Camera.main.transform.position = new Vector3(x, y, -10f);

           // transform.position = new Vector3(x, y, -10f);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        Camera.main.transform.position = orignalPosition;
    }


}