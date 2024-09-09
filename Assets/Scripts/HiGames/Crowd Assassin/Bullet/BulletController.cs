using UnityEngine;
using Framework.Enums;
using CrowdAssassin.Data;
using CrowdAssassin.Pool;
using CrowdAssassin.Event;
using CrowdAssassin.Civilian;

namespace CrowdAssassin.Bullet
{
    public class BulletController : MonoBehaviour
    {
        #region Variables

        [SerializeField] private float _speed;

        private BulletOwner _bulletOwner;

        private Vector3 _direction;

        private Rigidbody _rigidbody;

        [SerializeField] private HitEventSO _hitEventSO;
        [SerializeField] private BulletPoolEventSO _bulletPoolEventSO;

		#endregion Variables

		#region Properties
		
        private float Speed { get => _speed; set => _speed = value; }

		private BulletOwner BulletOwner { get => _bulletOwner; set => _bulletOwner = value; }
		
        private Vector3 Direction { get => _direction; set => _direction = value; }

		private Rigidbody Rigidbody { get => _rigidbody; set => _rigidbody = value; }
        
		private HitEventSO HitEventSO { get => _hitEventSO; set => _hitEventSO = value; }
        private BulletPoolEventSO BulletPoolEventSO { get => _bulletPoolEventSO; set => _bulletPoolEventSO = value; }

		#endregion Properties

		#region Awake

		void Awake()
        {
            Initialize();
            SubscribeEvents();
        }

        #endregion Awake

        #region Functions

        private void Initialize()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        private void SubscribeEvents()
        {

        }

        private void UnSubscribeEvents()
        {

        }

        public void ActivateBullet(Vector3 initialPosition, Vector3 direction, BulletOwner bulletOwner)
        {
            Direction = direction;
            transform.position = initialPosition;
            BulletOwner = bulletOwner;

            gameObject.SetActive(true);

            Rigidbody.velocity = direction * Speed;
        }

        private void DeactivateBullet()
		{
            Direction = Vector3.zero;
            transform.position = Vector3.zero;
            Rigidbody.velocity = Vector3.zero;
            BulletOwner = BulletOwner.None;

            gameObject.SetActive(false);

            AddBulletBackToPool();
        }
        private void AddBulletBackToPool()
		{
            BulletData bulletData = new BulletData(gameObject);
            BulletPoolEventSO.RaiseOnBulletDataSentBack(bulletData);
		}

        private void OnCollideWithCharacter(Collider collider)
		{
			if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
			{
                HitEventSO.RaiseOnPlayerHit();
                Debug.Log("Player Hit");
            }
            else if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                HitEventSO.RaiseOnEnemyHit();
                Debug.Log("Enemy Hit");
            }
            else if (collider.gameObject.layer == LayerMask.NameToLayer("Civilian"))
            {
                CivilianController civilianController = collider.GetComponent<CivilianController>();
                HitEventSO.RaiseOnCivilianHit(civilianController, BulletOwner, Direction);
                Debug.Log("Civilian Hit");
            }

            DeactivateBullet();
        }

		#endregion Functions

		#region OnCollision Functions

		void OnTriggerEnter(Collider collider)
		{
            OnCollideWithCharacter(collider);
        }

		#endregion OnCollision Functions
	}
}