using UnityEngine;
using Framework.Enums;
using Framework.Player;
using CrowdAssassin.Pool;
using CrowdAssassin.Data;
using CrowdAssassin.Event;
using CrowdAssassin.Bullet;

namespace CrowdAssassin.Character
{
    public class BaseCharacterController : MonoBehaviour
    {
        #region Variables

        private float _health;

        [SerializeField] private ParticleSystem _bloodParticle;
        [SerializeField] private ParticleSystem _bloodPoolParticle;
        [SerializeField] private ParticleSystem _bulletMuzzle;
        [SerializeField] private float _minZPosition;
        [SerializeField] private float _maxZPosition;

        private Rigidbody _rigidbody;

        [SerializeField] private GameObject _gunGameObject;

        [SerializeField] private HitEventSO _hitEventSO;
        [SerializeField] private LevelDataEventSO _levelDataEventSO;
        [SerializeField] private BulletPoolEventSO _bulletPoolEventSO;
        [SerializeField] private PlayerEnemyEventSO _playerEnemyEventSO;

        #endregion Variables

        #region Properties

        protected float Health
        {
            get => _health;
            set => _health = value;
        }

        public float MinZPosition
        {
            get => _minZPosition;
            set => _minZPosition = value;
        }

        public float MaxZPosition
        {
            get => _maxZPosition;
            set => _maxZPosition = value;
        }

        public Rigidbody Rigidbody
        {
            get => _rigidbody;
            set => _rigidbody = value;
        }

        protected GameObject GunGameObject
        {
            get => _gunGameObject;
            set => _gunGameObject = value;
        }

        protected HitEventSO HitEventSO
        {
            get => _hitEventSO;
            set => _hitEventSO = value;
        }

        protected LevelDataEventSO LevelDataEventSO
        {
            get => _levelDataEventSO;
            set => _levelDataEventSO = value;
        }

        private BulletPoolEventSO BulletPoolEventSO
        {
            get => _bulletPoolEventSO;
            set => _bulletPoolEventSO = value;
        }

        protected PlayerEnemyEventSO PlayerEnemyEventSO
        {
            get => _playerEnemyEventSO;
            set => _playerEnemyEventSO = value;
        }

        #endregion Properties

        #region Functions

        public virtual void Initialize()
        {
            Rigidbody = GetComponent<Rigidbody>();

            SetCharacterYPosition();
        }

        public virtual void SubscribeEvents()
        {
        }

        public virtual void UnSubscribeEvents()
        {
        }

        protected virtual void OnHit()
        {
            _bloodParticle.Play();
        }

        protected virtual void SetCharacterYPosition()
        {
            float zPosition = Random.Range(MinZPosition, MaxZPosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, zPosition);
        }

        protected void CheckDeath()
        {
            Health -= 1;

            if (Health <= 0)
            {
                _bloodPoolParticle.Play();
                PlayerEnemyEventSO.RaiseOnCharacterDied(this);
            }
        }

        private BulletOwner GetBulletOwner()
        {
            if (this is PlayerController)
                return BulletOwner.Player;
            else
                return BulletOwner.Enemy;
        }

        private GameObject GetBulletGameObjectFromThePool()
        {
            BulletData bulletData = new BulletData(null);
            BulletPoolEventSO.RaiseOnBulletDataRequested(bulletData);
            return bulletData.BulletGameObject;
        }

        protected virtual void Fire(Vector3 direction)
        {
            _bulletMuzzle.Play();
            BulletOwner bulletOwner = GetBulletOwner();

            GameObject bulletGameObject = GetBulletGameObjectFromThePool();
            bulletGameObject.transform.rotation = Quaternion.LookRotation(direction.normalized);

            BulletController bulletController = bulletGameObject.GetComponent<BulletController>();
            bulletController.ActivateBullet(GunGameObject.transform.position, direction, bulletOwner);
        }

        #endregion Functions
    }
}