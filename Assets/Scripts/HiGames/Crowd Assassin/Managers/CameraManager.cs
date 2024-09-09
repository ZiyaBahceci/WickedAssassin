using System;
using UnityEngine;
using Cinemachine;
using System.Collections;
using Framework.Extension;
using System.Collections.Generic;

namespace Framework.Managers
{
    public class CameraManager : SingletonDestroyable<CameraManager>
    {
        #region Events

        #endregion Events

        #region Variables

        private Vector3 _cameraOffset;

        private Camera _mainCamera;
        [SerializeField] private CinemachineVirtualCamera _virtualCameraStart;
        [SerializeField] private CinemachineVirtualCamera _virtualCameraFinish;
		[SerializeField] private CinemachineVirtualCamera _virtualCameraGameplay;

        private PlayerManager _playerManagerInstance; 

		#endregion Variables

		#region Properties

		private Vector3 CameraOffset { get => _cameraOffset; set => _cameraOffset = value; }
		public Camera MainCamera { get => _mainCamera; set => _mainCamera = value; }
		public CinemachineVirtualCamera VirtualCameraGameplay { get => _virtualCameraGameplay; set => _virtualCameraGameplay = value; }
		private CinemachineVirtualCamera VirtualCameraStart { get => _virtualCameraStart; set => _virtualCameraStart = value; }
		private CinemachineVirtualCamera VirtualCameraFinish { get => _virtualCameraFinish; set => _virtualCameraFinish = value; }
		private PlayerManager PlayerManagerInstance { get => _playerManagerInstance; set => _playerManagerInstance = value; }

		#endregion Properties

		#region Functions

		public void Initialize()
        {
            MainCamera = Camera.main;

            VirtualCameraStart.gameObject.SetActive(true);
            VirtualCameraFinish.gameObject.SetActive(false);
            VirtualCameraGameplay.gameObject.SetActive(false);
        }

        public void ActivateGameplayCamera()
		{
            VirtualCameraStart.gameObject.SetActive(false);
            VirtualCameraFinish.gameObject.SetActive(false);

            VirtualCameraGameplay.gameObject.SetActive(true);
        }

        public void ActiveFinishCamera()
		{
            VirtualCameraGameplay.gameObject.SetActive(false);
            VirtualCameraFinish.gameObject.SetActive(true);
		}

        public void SetFinishCamera(Transform transform)
		{
            VirtualCameraFinish.LookAt = transform;
            VirtualCameraFinish.Follow = transform;
            ActiveFinishCamera();
        }

        private void IncreaseBlendTime() => MainCamera.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = 1.25f;
        private void DecreaseBlendTime() => MainCamera.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Time = 0.5f;

        #endregion Functions
    }
}