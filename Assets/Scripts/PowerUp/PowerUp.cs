using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public abstract class PowerUp : GameplayObject
    {
        protected override Collider2D Collider2D {  get; }
        public bool FlyingToPlayer { get; set; }

        private int FlyingToPlayerFrameCount = 0;

        protected abstract void Execute();
        private void Update()
        {
            if (FlyingToPlayer)
            {
                if (FlyingToPlayerFrameCount == 0)
                {
                    GetComponent<SpriteRenderer>().sortingOrder = 5;
                }
                FlyingToPlayerFrameCount++;
                if (Player.Default)
                {
                    var direction = Player.Default.DirectionFrom(this);

                    var distance = direction.magnitude;

                    if (FlyingToPlayerFrameCount <= 15)
                    {
                        transform.Translate(direction.normalized * -2 * Time.deltaTime);


                    }
                    else
                    {
                        transform.Translate(direction.normalized * 7.5f * Time.deltaTime);
                    }
                    if (distance < 0.1f)
                    {
                        Execute();
                    }

                }
            }
        }
    }
}
