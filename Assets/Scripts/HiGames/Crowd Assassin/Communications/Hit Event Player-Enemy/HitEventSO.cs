using UnityEngine;
using CrowdAssassin.Civilian;
using Framework.Enums;

namespace CrowdAssassin.Event
{
    [CreateAssetMenu]
    public class HitEventSO : ScriptableObject
    {
        #region Events

        public delegate void Hit();
        public event Hit OnEnemyHit;
        public event Hit OnPlayerHit;

        public delegate void CivilianHit(CivilianController civilianController, BulletOwner bulletOwner, Vector3 bulletDirection);
        public event CivilianHit OnCivilianHit;

        #endregion Events

        #region Functions

        public void RaiseOnEnemyHit()
        {
            OnEnemyHit?.Invoke();
        }

        public void RaiseOnPlayerHit()
        {
            OnPlayerHit?.Invoke();
        }

        public void RaiseOnCivilianHit(CivilianController civilianController, BulletOwner bulletOwner, Vector3 bulletDirection)
        {
            OnCivilianHit?.Invoke(civilianController, bulletOwner, bulletDirection);
        }

        #endregion Functions
    }
}