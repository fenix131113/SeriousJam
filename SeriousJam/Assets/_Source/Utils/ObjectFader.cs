using DG.Tweening;
using UnityEngine;

namespace Utils
{
    public class ObjectFader : MonoBehaviour
    {
        [SerializeField] private float fadeTime = 0.5f;
        
        private SpriteRenderer _spriteRenderer;

        private void Awake() => _spriteRenderer = GetComponent<SpriteRenderer>();

        public void StartFade(bool fadeIn) => _spriteRenderer.DOFade(fadeIn ? 1 : 0, fadeTime);
    }
}