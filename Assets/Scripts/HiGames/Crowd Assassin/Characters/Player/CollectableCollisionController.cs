using UnityEngine;
using System.Collections;
using Framework.Collectables;
using System.Collections.Generic;

namespace Framework.Player
{
    public class CollectableCollisionController : MonoBehaviour
    {
		#region Events



		#endregion Events

		#region Variables

		private int _collectableLayer;

		#endregion Variables

		#region Properties

		private int CollectableLayer { get => _collectableLayer; set => _collectableLayer = value; }

		#endregion Properties

		#region Start

		void Start()
		{
			Initialize();
		}

		#endregion Start

		#region Functions

		public void Initialize()
        {
			CollectableLayer = LayerMask.NameToLayer("Collectable");
		}

		private void OnCollisionFunction(Collider collider)
		{
			if (collider.gameObject.layer == CollectableLayer)
			{

			}
		}

		private void OnTriggerFunction(Collider collider)
		{
			if (collider.gameObject.layer == CollectableLayer)
			{
				BaseCollectable baseCollectable= collider.GetComponent<BaseCollectable>();

				if (baseCollectable)
					baseCollectable.OnCollected();
			}
		}

		#endregion Functions

		#region OnCollision Functions

		void OnCollisionEnter(Collision collision)
		{
			
		}

		void OnCollisionExit(Collision collision)
		{
			
		}

		#endregion OnCollision Functions

		#region OnTrigger Functions

		void OnTriggerEnter(Collider collider)
		{
			OnTriggerFunction(collider);
		}

		void OnTriggerExit(Collider collider)
		{
			
		}

		#endregion OnTrigger Functions
	}
}