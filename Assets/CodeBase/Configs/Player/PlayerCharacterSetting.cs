using UnityEngine;

namespace CodeBase.Configs.Player
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/Player/PlayerConfig", order = 1)]
    public class PlayerCharacterSetting : ScriptableObject
    {
        public string PathCameraPrefab;
        public string PathPlayerPrefabe;
        public string PathPlayerHUDPrefab;

        public int MaxHP;
        public float WalkSpeed;
        
        public Vector3 defaultSpawnPosition;
    }
}
