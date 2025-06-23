using DG.Tweening;
using UnityEngine;

namespace Utils
{
    public class ObjectFader : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float fadeTime = 0.5f;

        public void StartFade(bool fadeIn) => spriteRenderer.DOFade(fadeIn ? 1 : 0, fadeTime);
    }
}