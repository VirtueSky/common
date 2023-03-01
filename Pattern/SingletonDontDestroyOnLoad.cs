using UnityEngine;

namespace Virtuesky.common.pattern
{
    public class SingletonDontDestroyOnLoad<T> : MonoBehaviour where T : MonoBehaviour
    {
        static T _instance;

        public static T Instance => _instance ??= FindObjectOfType<T>();

        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);
            if (_instance == null)
            {
                _instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}