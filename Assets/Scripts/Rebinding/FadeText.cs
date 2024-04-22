using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeText : MonoBehaviour
{
    [SerializeField] public float fadeTime = 2f;
    private TextMeshProUGUI text;
    private float timer;
    
    void Start()
    {
        timer = fadeTime;
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        float ratio = timer / fadeTime;
        text.alpha = ratio;
    }
}
