using UnityEngine;

namespace CrowdAssassin.Data
{
    [CreateAssetMenu]
    public class CharacterDataSO : ScriptableObject
    {
        #region Variables

        [SerializeField] private float _enemyHP;
        [SerializeField] private float _playerHP;

        #endregion Variables

        #region Properties

        public float EnemyHP { get => _enemyHP; set => _enemyHP = value; }
        public float PlayerHP { get => _playerHP; set => _playerHP = value; }

        #endregion Properties
    }
}