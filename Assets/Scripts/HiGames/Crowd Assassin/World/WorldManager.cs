using UnityEngine;

namespace CrowdAssassin.World
{
    public class WorldManager : MonoBehaviour
    {
        #region Variables

		[SerializeField] private GameObject _road0;
		[SerializeField] private GameObject _road1;

        [SerializeField] private GameObject _carEnvironment;
        [SerializeField] private GameObject _trainEnvironment;

		#endregion Variables

		#region Properties

		private GameObject Road0 { get => _road0; set => _road0 = value; }
		private GameObject Road1 { get => _road1; set => _road1 = value; }
		
		private GameObject CarEnvironment { get => _carEnvironment; set => _carEnvironment = value; }
		private GameObject TrainEnvironment { get => _trainEnvironment; set => _trainEnvironment = value; }

		#endregion Properties

		#region Awake

		void Awake()
		{
			Initialize();
		}

		#endregion Awake

		#region Functions

		private void Initialize()
        {
			ActivateRoadRandomly();
			ActivateEnvironmentRandomly();
		}

        private void SubscribeEvents()
        {

        }

        private void UnSubscribeEvents()
        {

        }

		private void ActivateRoadRandomly()
		{
			int randomValue = Random.Range(0, 2);

			if (randomValue == 0)
			{
				Road0.SetActive(false);
			}
			else
			{
				Road1.SetActive(false);
			}
		}

		private void ActivateEnvironmentRandomly()
		{
            int randomValue = Random.Range(0, 2);

            if (randomValue == 0)
			{
                TrainEnvironment.SetActive(false);
			}
			else
			{
				CarEnvironment.SetActive(false);
			}
		}

		#endregion Functions
	}
}