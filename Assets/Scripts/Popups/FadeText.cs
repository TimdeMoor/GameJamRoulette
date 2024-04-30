using TMPro;
using UnityEngine;

namespace Popups
{
    public class FadeText : MonoBehaviour
    {
        [SerializeField] public float fadeTime = 2f;
        private TextMeshPro text;
        private float timer;
    
        void Start()
        {
            timer = fadeTime;
            text = GetComponent<TextMeshPro>();
        }

        // Update is called once per frame
        void Update()
        {
            timer -= Time.deltaTime;

            float ratio = timer / fadeTime;
            text.alpha = ratio;
        }
    }
}
