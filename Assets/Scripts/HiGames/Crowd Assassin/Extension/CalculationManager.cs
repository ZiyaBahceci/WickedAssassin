using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Framework.Extension
{
    public static class CalculationManager
    {
        #region Events



        #endregion Events

        #region Variables



        #endregion Variables

        #region Properties



        #endregion Properties

        #region Functions

        public static float GetSignedAngleByDirection(Vector3 direction)
		{
            float angle;

            angle = Vector3.Angle(Vector3.forward, direction);
			if (angle != 180)
			{
                if (direction.x < 0 || direction.z < 0)
                    angle *= -1f;

                if (angle == -135f && direction.x > 0)
                    angle *= -1f;
            }

            return angle;
		}

        public static float GetAngleSignByDirection(float angle, Vector3 direction)
		{
            if (angle != 180)
            {
                if (direction.x < 0 || direction.z < 0)
                    angle *= -1f;

                if (angle == -135f && direction.x > 0)
                    angle *= -1f;
            }

            return angle;
        }

        #endregion Functions
    }
}