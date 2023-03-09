using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Virtuesky.common
{
    public class BasePopup : MonoBehaviour
    {
        public bool UseAnimation;
        [ShowIf(nameof(UseAnimation), true)] public float durationPopup = 1.0f;
        [ShowIf("UseAnimation")] public GameObject Background;
        [ShowIf("UseAnimation")] public GameObject Container;
        [ShowIf("UseAnimation")] public bool UseShowAnimation;
        [ShowIf("UseShowAnimation")] public ShowAnimationType ShowAnimationType;
        [ShowIf("UseAnimation")] public bool UseHideAnimation;
        [ShowIf("UseHideAnimation")] public HideAnimationType HideAnimationType;
        public CanvasGroup canvasGroup => GetComponent<CanvasGroup>();
        public Canvas Canvas => GetComponent<Canvas>();

        public virtual void Show()
        {
        }

        public virtual void Hide()
        {
        }

        public virtual void AfterInstantiate()
        {
        }

        public virtual void BeforeShow()
        {
        }

        public virtual void AfterShown()
        {
        }

        public virtual void BeforeHide()
        {
        }

        public virtual void AfterHidden()
        {
        }
    }

    public enum ShowAnimationType
    {
        OutBack,
        Flip,
    }

    public enum HideAnimationType
    {
        InBack,
        Fade,
    }
}