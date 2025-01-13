using UnityEngine;

namespace CodeBase.Configs.Enemy
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/Enemy/EnemyConfig", order = 1)]
    public class EnemyConfig : ScriptableObject
    {
        public Entity.Character.Enemy.Enemy enemyPrefab;
    }
}
