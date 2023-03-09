using System.ComponentModel;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Virtuesky.common
{
    public class Popup : BasePopup
    {
        public override void Show()
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

        public override void Hide()
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
                        canvasGroup.DOFade(0, durationPopup).OnComplete(() =>
                        {
                            canvasGroup.alpha = 1;
                            gameObject.SetActive(false);
                            AfterHidden();
                        });
                        break;
                }
            }
            else
            {
                gameObject.SetActive(false);
                AfterHidden();
            }
        }
    }
}