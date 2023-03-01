using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Virtuesky.common.pattern;
using Virtuesky.common.UI;
using Debug = System.Diagnostics.Debug;

namespace Virtuesky.common.manager
{
    public class PopupManager : Singleton<PopupManager>
    {
        public Transform canvasTransform;
        public CanvasScaler canvasScaler;
        public List<BasePopup> popups;

        private readonly Dictionary<Type, BasePopup> _dictionary = new Dictionary<Type, BasePopup>();

        protected override void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(gameObject);
            Initialize();
            Debug.Assert(Camera.main != null, "Camera.main != null");
            canvasScaler.matchWidthOrHeight = Camera.main.aspect > .7f ? 1 : 0;
        }

        public void Initialize()
        {
            int index = 0;
            popups.ForEach(popup =>
            {
                BasePopup popupInstance = Instantiate(popup, canvasTransform);
                popupInstance.gameObject.SetActive(false);
                popupInstance.Canvas.sortingOrder = index++;
                _dictionary.Add(popupInstance.GetType(), popupInstance);
            });
        }

        public void Show<T>()
        {
            if (_dictionary.TryGetValue(typeof(T), out BasePopup popup))
            {
                if (!popup.isActiveAndEnabled)
                {
                    popup.Show();
                }
            }
        }

        public void Hide<T>()
        {
            if (_dictionary.TryGetValue(typeof(T), out BasePopup popup))
            {
                if (popup.isActiveAndEnabled)
                {
                    popup.Hide();
                }
            }
        }

        public void HideAll()
        {
            foreach (BasePopup item in _dictionary.Values)
            {
                if (item.isActiveAndEnabled)
                {
                    item.Hide();
                }
            }
        }

        public BasePopup Get<T>()
        {
            if (_dictionary.TryGetValue(typeof(T), out BasePopup popup))
            {
                return popup;
            }

            return null;
        }
    }
}