using UnityEngine;

namespace Utils
{
    public class ObjectFollower : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private bool followX;
        [SerializeField] private bool followY;
        
        private void Update()
        {
            var newPos = new Vector3(followX ? target.position.x : transform.position.x, followY ? target.position.y : transform.position.y, transform.position.z);
            transform.position = newPos;
        }
    }
}