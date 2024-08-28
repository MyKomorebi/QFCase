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
                         
                           self.onClick.AddListener(() =>
                           {
                               Time.timeScale = 1.0f;
                               itemCache.Upgrade();
                               this.Hide();
                               AudioKit.PlaySound("AbillityLevelUp");
                           });
                           var selfCache = self;
                           itemCache.Visible.RegisterWithInitValue(visible =>
                           {
                               
                               if (visible) 
                               {
                                   self.GetComponentInChildren<Text>().text = expUpgradeItem.Description;
                                   selfCache.Show();
                                   if(expUpgradeSystem.Pairs.TryGetValue(itemCache.Key,out var pairedName))
                                   {
                                       var pairedItem = expUpgradeSystem.Dictionary[pairedName];
                                       if (pairedItem.CurrentLevel.Value > 0 && itemCache.CurrentLevel.Value == 0)
                                       {
                                           var pairedNameText = selfCache.transform.Find("PairedName");
                                           pairedNameText.GetComponent<Text>().text =
                                         "Åä¶Ô¼¼ÄÜ£º" + pairedItem.Key;
                                           pairedNameText.Show();

                                       }
                                       else
                                       {
                                           selfCache.transform.Find("PairedName").Hide();
                                       }
                                   }
                                   else
                                   {
                                       selfCache.transform.Find("PairedName").Hide();
                                   }
                               }
                               else
                               {
                                 
                                   selfCache.Hide() ;
                               }
                           }).UnRegisterWhenGameObjectDestroyed(selfCache);

                           itemCache.CurrentLevel.RegisterWithInitValue(lv =>
                           {
                               self.GetComponentInChildren<Text>().text = expUpgradeItem.Description;
                           }).UnRegisterWhenGameObjectDestroyed(gameObject);
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