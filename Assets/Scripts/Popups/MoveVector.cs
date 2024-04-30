using UnityEngine;

namespace Popups
{
    public class MoveVector : MonoBehaviour
    {
        [SerializeField] public Vector3 MoveDelta = Vector3.zero;

        // Update is called once per frame
        void Update()
        {
            transform.Translate(MoveDelta * Time.deltaTime);    
        }
    }
}
