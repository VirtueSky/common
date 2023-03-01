using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Virtuesky.common.UI
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
        public CanvasGroup CvGroup => GetComponent<CanvasGroup>();
        public Canvas Canvas => GetComponent<Canvas>();

        public virtual void Show()
        {
            BeforeShow();
            gameObject.SetActive(true);
            if (UseShowAnimation)
            {
                switch (ShowAnimationType)
                {
                    case ShowAnimationType.OutBack:
                        DOTween.Sequence().OnStart(() => Container.transform.localScale = Vector3.one * .9f)
                            .Append(Container.transform.DOScale(Vector3.one, durationPopup)
                                .SetEase(Ease.OutBack).OnComplete(() => { AfterShown(); }));
                        break;
                    case ShowAnimationType.Flip:
                        DOTween.Sequence().OnStart(() => Container.transform.localEulerAngles = new Vector3(0, 180, 0))
                            .Append(Container.transform.DORotate(Vector3.zero, durationPopup))
                            .SetEase(Ease.Linear).OnComplete(() => { AfterShown(); });
                        break;
                }
            }
            else
            {
                AfterShown();
            }
        }

        public virtual void Hide()
        {
            BeforeHide();
            if (UseHideAnimation)
            {
                switch (HideAnimationType)
                {
                    case HideAnimationType.InBack:
                        DOTween.Sequence().Append(Container.transform
                            .DOScale(Vector3.one * .7f, durationPopup).SetEase(Ease.InBack)
                            .OnComplete(() =>
                            {
                                gameObject.SetActive(false);
                                AfterShown();
                            }));
                        break;
                    case HideAnimationType.Fade:
                        // CvGroup.DOFade(0, durationPopup).OnComplete(() =>
                        // {
                        //     CvGroup.alpha = 1;
                        //     gameObject.SetActive(false);
                        //     AfterHidden();
                        // });
                        break;
                }
            }
            else
            {
                gameObject.SetActive(false);
                AfterHidden();
            }
        }

        protected virtual void AfterInstantiate()
        {
        }

        protected virtual void BeforeShow()
        {
        }

        protected virtual void AfterShown()
        {
        }

        protected virtual void BeforeHide()
        {
        }

        protected virtual void AfterHidden()
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