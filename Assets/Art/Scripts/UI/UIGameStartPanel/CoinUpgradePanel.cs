/****************************************************************************
 * 2024.8 USER-20240116VV
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Linq;

namespace ProjectSurvivor
{
    public partial class CoinUpgradePanel : UIElement, IController
    {







        private void Awake()
        {
            CoinUpgradeItemTemplate.Hide();




            Global.Coin.RegisterWithInitValue(coin =>
            {
                CoinText.text = "金币：" + coin;
            });



            foreach (var coinUpgradeItem in this.GetSystem<CoinUpgradeSystem>().Items.Where(item => !item.UpgradeFinish))
            {
                CoinUpgradeItemTemplate.InstantiateWithParent(CoinUpgradeItemRoot)
                        .Self(self =>
                        {
                            var itemCache = coinUpgradeItem;
                            self.GetComponentInChildren<Text>().text = coinUpgradeItem.Description + $" {coinUpgradeItem.Price} 金币";
                            self.onClick.AddListener(() =>
                            {
                                itemCache.Upgrade();
                                AudioKit.PlaySound("AbillityLevelUp");
                            });
                            var selfCache = self;

                            coinUpgradeItem.Onchange.Register(() =>
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
                            Global.Coin.RegisterWithInitValue(coin =>
                            {

                                if (coin >= itemCache.Price)
                                {
                                    selfCache.interactable = true;
                                }
                                else
                                {
                                    selfCache.interactable = false;
                                }

                            }).UnRegisterWhenGameObjectDestroyed(self);
                        });


                BtnClose.onClick.AddListener(() =>
                {
                   this.Hide();
                });
            }
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