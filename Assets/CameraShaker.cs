using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private List<Transform> affectedObjects;
    [SerializeField][Range(.1f, 10f)] private float shakeDuration = 1f;
    [SerializeField][Range(.1f, 10f)] private float shakeMagnitude = 1f;
    
    
    public static CameraShaker Instance { get; private set; }
    private void Awake() 
    {
        if (Instance != null && Instance != this){Destroy(this);} 
        else{Instance = this;} 
    }

    public void Shake()
    {
        foreach (Transform t in affectedObjects)
        {
            StartCoroutine(ShakeAsync(shakeDuration, shakeMagnitude, t));
        }
    }

    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        Shake();
    }
    
    IEnumerator ShakeAsync(float duration, float magnitude, Transform target)
    {
        Vector3 OriginalPos = target.localPosition;
        float elapsed = 0.0f; 
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            target.localPosition = new Vector3(x, y, OriginalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        target.localPosition = OriginalPos;
    }
}
