using UnityEngine;

namespace CodeBase.Common
{
    public class GlobalObject : MonoBehaviour
    {
        public static GlobalObject instance;
    
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else
                Destroy(gameObject);
        }
    }
}