using UnityEngine;

namespace Moblik.Core.Singleton
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        [Header("<------ Singleton Configs ------>")]
        [Tooltip("If true, prevents the destruction of this GameObject when a Scene is loaded."), SerializeField] private bool persistBetweenScenes = false;

        public static T Instance;

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;

                if (persistBetweenScenes == true) DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}