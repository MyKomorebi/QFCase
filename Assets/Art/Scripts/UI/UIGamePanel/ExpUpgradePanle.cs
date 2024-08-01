/****************************************************************************
 * 2024.8 USER-20240116VV
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
	public partial class ExpUpgradePanle : UIElement,IController
	{
		private void Awake()
		{
            var expUpgradeSystem=this.GetSystem<ExpUpgradeSystem>();
            foreach(var expUpgradeItem in expUpgradeSystem.Items)
            {
                BtnExpUpgradeItemTemplate.InstantiateWithParent(UpgradeRoot)
                       .Self(self =>
                       {
                           var itemCache = expUpgradeItem;
                           self.GetComponentInChildren<Text>().text = expUpgradeItem.Description + $" {expUpgradeItem.Price} О­бщ";
                           self.onClick.AddListener(() =>
                           {
                               Time.timeScale = 1.0f;
                               itemCache.Upgrade();
                               this.Hide();
                               AudioKit.PlaySound("AbillityLevelUp");
                           });
                           var selfCache = self;

                           expUpgradeItem.Onchange.Register(() =>
                           {
                               if (itemCache.ConditionCheck())
                               {
                                   selfCache.Show();
                               }
                               else
                               {
                                   selfCache.Hide();
                               }
                           }).UnRegisterWhenGameObjectDestroyed(gameObject);
                           if (itemCache.ConditionCheck())
                           {
                               selfCache.Show();
                           }
                           else
                           {
                               selfCache.Hide();
                           }
                           
                       });


               
            }


           
            //BtnSimpleDurationUpgrade.onClick.AddListener(() =>
            //{
            //             Time.timeScale = 1f;

            //             Global.SimpleAbillityDamage.Value *= 0.8f;

            //	UpgradeRoot.Hide();
            //             AudioKit.PlaySound("AbillityLevelUp");
            //         });
        }

        protected override void OnBeforeDestroy()
		{
		}

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }
    }
}