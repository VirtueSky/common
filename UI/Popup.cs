using DG.Tweening;
using UnityEngine;

namespace Virtuesky
{
    public class Popup : MonoBehaviour
    {
        public CanvasGroup canvasGroup => GetComponent<CanvasGroup>();
        public Canvas Canvas => GetComponent<Canvas>();

        public virtual void Show()
        {
            BeforeShow();
            gameObject.SetActive(true);
            AfterShown();
        }

        public virtual void Hide()
        {
            BeforeHide();
            gameObject.SetActive(false);
            AfterHidden();
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
}