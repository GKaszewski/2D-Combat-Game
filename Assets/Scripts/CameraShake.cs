using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float strength)
    {
        Vector3 startPosition = transform.localEulerAngles;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            Vector2 shakeOffset = new Vector2(Random.Range(-1f, 1f) * strength, Random.Range(-1f, 1f) * strength);
            transform.localEulerAngles = new Vector3(shakeOffset.x, shakeOffset.y, startPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localEulerAngles= startPosition;
    }
}
