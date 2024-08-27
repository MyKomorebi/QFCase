using UnityEngine;
using QFramework;
using System.Collections.Generic;

namespace ProjectSurvivor
{
	public partial class BasketBallAbility : ViewController
	{
		private List<Ball>mBalls=new List<Ball>();

        private void Start()
        {

            void CreateBall()
            {
                mBalls.Add(Ball.Instantiate().SyncPosition2DFrom(this)
                    .Show());
            }
            void CreateBalls()
            {
                var ballCountCreate = Global.BasketBallCount.Value + Global.AdditionalFlyThingCount.Value - mBalls.Count;
                for (var i = 0; i < ballCountCreate; i++)
                {
                    CreateBall();
                }
            }
            Global.BasketBallCount.Or(Global.AdditionalFlyThingCount).Register((CreateBalls)
            ).UnRegisterWhenGameObjectDestroyed(gameObject);
         CreateBalls();
        }
    }
}
