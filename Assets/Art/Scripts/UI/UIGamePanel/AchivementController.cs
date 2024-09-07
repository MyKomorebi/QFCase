/****************************************************************************
 * 2024.9 枕头
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.U2D;
using Unity.VisualScripting;

namespace ProjectSurvivor
{
	public partial class AchivementController : UIElement
	{
		ResLoader mResLoader = ResLoader.Allocate();
		private void Awake()
		{
            var originLocalPosY = AchivemntItem.LocalPositionY();
			var iconAtlas = mResLoader.LoadSync<SpriteAtlas>("Icon");

			AchievementSystem.OnAchievementUnlocked.Register(item =>
			{
                Title.text = $"<b>成就 {item.Name} 达成!</b>";
                Desription.text = item.Description;
                var sprite = iconAtlas.GetSprite(item.IconName);
                Icon.sprite = sprite;
                AchivemntItem.Show();

                AchivemntItem.LocalPositionY(-200);

                AudioKit.PlaySound("Achievement");

                ActionKit.Sequence()
                    .Lerp(-200, originLocalPosY, 0.3f, (y) => AchivemntItem.LocalPositionY(y))
                    .Delay(2)
                    .Lerp(originLocalPosY, -200, 0.3f, (y) => AchivemntItem.LocalPositionY(y), () =>
                    {
                        AchivemntItem.Hide();
                    })
                    .Start(this);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

		}

		protected override void OnBeforeDestroy()
		{

			mResLoader.Recycle2Cache();
			mResLoader=null;
		}
	}
}