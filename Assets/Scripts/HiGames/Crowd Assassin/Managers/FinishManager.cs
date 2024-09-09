using UnityEngine;
using Framework.Enums;
using Framework.Managers;
using System.Collections;
using Framework.Extension;
using System.Collections.Generic;

namespace Framework.Managers
{
    public class FinishManager : MonoBehaviour
    {
        #region Events



        #endregion Events

        #region Variables

        [SerializeField] private List<GameObject> _confettiGameObjectList;

		#endregion Variables

		#region Properties
		private List<GameObject> ConfettiGameObjectList { get => _confettiGameObjectList; set => _confettiGameObjectList = value; }

		#endregion Properties

        #region Functions

        public void Initialize()
        {
            //GameManagerInstance = GameManager.Instance;
        }

        private void PlayConfettiParticles()
		{
			foreach (GameObject item in ConfettiGameObjectList)
			{
                item.SetActive(true);
			}
		}

        private void OnPlayerHasFinished(Collider collider)
        {
			if (collider.CompareTag("Player"))
			{
                PlayConfettiParticles();
            }
        }

        #endregion Functions

        #region OnTrigger Functions

        void OnTriggerEnter(Collider collider)
        {
            OnPlayerHasFinished(collider);
        }

        #endregion OnTrigger Functions
    }
}