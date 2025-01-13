using UnityEngine;

namespace CodeBase.Entity.Character.Player
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private float smoothing = 5f;
        
        private Transform _playerTransform;
        private Vector3 _offset;

        public void Initialization(Transform playerTransform)
        {
            _playerTransform = playerTransform;
            _offset = transform.position - _playerTransform.position;
        }

        private void LateUpdate()
        {
            Vector3 targetCamPos = _playerTransform.position + _offset;
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }
}
