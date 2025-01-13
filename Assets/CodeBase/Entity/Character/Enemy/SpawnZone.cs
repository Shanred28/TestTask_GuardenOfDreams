using UnityEngine;

namespace CodeBase.Entity.Character.Enemy
{
    public class SpawnZone : MonoBehaviour
    {
        [SerializeField] private float radius;

        public Vector3 GetRandomPosition()
        {
            Vector2 randomPoint = Random.insideUnitCircle * radius; 
            return new Vector3(randomPoint.x , randomPoint.y, 0f);
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Gizmos.color = Color.green; 
            Gizmos.DrawWireSphere(transform.position, radius);
        }
#endif
    }
}
