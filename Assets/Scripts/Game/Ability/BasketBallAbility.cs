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
            Global.BasketBallCount.RegisterWithInitValue(count =>
            {
                if(mBalls.Count<count)
                {
                    mBalls.Add(Ball.Instantiate()
                        .SyncPosition2DFrom(this)
                        .Show());
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}
