using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CrowdAssassin.Civilian
{
    public class CivilianAnimationController : MonoBehaviour
    {
        #region Events



        #endregion Events

        #region Variables

        private int _walkHash;
        private int _deathHash;

        private Animator _animator;

		#endregion Variables

		#region Properties
		
        private int WalkHash { get => _walkHash; set => _walkHash = value; }
		private int DeathHash { get => _deathHash; set => _deathHash = value; }

		private Animator Animator { get => _animator; set => _animator = value; }

		#endregion Properties

        #region Functions

        public void Initialize()
        {
            WalkHash = Animator.StringToHash("Walk");
            DeathHash = Animator.StringToHash("Death");

            Animator = GetComponent<Animator>();
        }

        public void SubscribeEvents()
        {

        }

        public void UnSubscribeEvents()
        {

        }

        public void OnCivilianHit()
		{
            Animator.SetBool(WalkHash, false);
            Animator.SetTrigger(DeathHash);
        }

        public void ResetAnimator()
		{
            Animator.SetBool(WalkHash, true);
		}

        #endregion Functions
    }
}