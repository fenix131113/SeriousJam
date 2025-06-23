using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Utils
{
    public class TimeDestroyableObject : MonoBehaviour
    {
        [SerializeField] private TMP_Text rend;
        [SerializeField] private bool destroyOnStart = true;
        [SerializeField] private bool fadeOut;
        [SerializeField] private float destroyTime = 1f;

        private void Start()
        {
            if (destroyOnStart)
                KillObject();
        }

        public void StartDestroyTimer()
        {
            if (!destroyOnStart)
                KillObject();
        }

        private void KillObject()
        {
            if (fadeOut)
                rend.DOFade(0f, destroyTime);
            
            Destroy(gameObject, destroyTime + 0.02f);
        }
    }
}