using UnityEngine;
using Framework.Managers;
using System.Collections;
using System.Collections.Generic;

namespace Framework.Collectables
{
    public class MoneyCollectableController : BaseCollectable
    {
        #region Events



        #endregion Events

        #region Variables



		#endregion Variables

		#region Properties



        #endregion Properties

        #region Start

        protected override void Start()
        {
            base.Start();
            Initialize();
        }

        #endregion Start

        #region Functions

        public void Initialize()
        {
           
        }

        public override void OnCollected()
	    {
            base.OnCollected();
	    }

	    #endregion Functions
    }
}