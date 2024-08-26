using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using ProjectSurvivor;
using System.Linq;

public class ExpUpgradeSystem : AbstractSystem
{

    public List<ExpUpgradeItem> Items { get; } = new List<ExpUpgradeItem>();
    public ExpUpgradeItem Add(ExpUpgradeItem item)
    {
        Items.Add(item);
        return item;
    }
    protected override void OnInit()
    {
        ResetData();

        Global.Level.Register(_ =>
        {
            Roll();
        });
    }

    public void ResetData()
    {
        Items.Clear();
        Add(new ExpUpgradeItem(true))
            .WithKey("Simple_Sword")
            .WithDescription(lv =>
            {
                return lv switch
                {
                    1 => $"½£Lv{lv}:¹¥»÷Éí±ßµÄµÐÈË",
                    2 => $"½£Lv{lv}:\n¹¥»÷Á¦+3£¬ÊýÁ¿+2",
                    3 => $"½£Lv{lv}:\n¹¥»÷Á¦+2£¬¼ä¸ô-0.25s",
                    4 => $"½£Lv{lv}:\n¹¥»÷Á¦+2£¬¼ä¸ô-0.25s",
                    5 => $"½£Lv{lv}:\n¹¥»÷Á¦+3£¬ÊýÁ¿+2",
                    6 => $"½£Lv{lv}:\n·¶Î§+1£¬¼ä¸ô-0.25s",
                    7 => $"½£Lv{lv}:\n¹¥»÷Á¦+3£¬ÊýÁ¿+2",
                    8 => $"½£Lv{lv}:\n¹¥»÷Á¦+2£¬·¶Î§+1",
                    9 => $"½£Lv{lv}:\n¹¥»÷Á¦+3£¬¼ä¸ô-0.25s",
                    10 => $"½£Lv{lv}\n¹¥»÷Á¦+3£¬ÊýÁ¿+2",
                    _ => null
                };
            })
            .WithMaxLevel(10)
            .OnUpgrade((_, level) =>
            {
                switch (level)
                {
                    case 1:
                        Global.SimpleSwordUnlocked.Value = true;
                        break;
                    case 2:
                        Global.SimpleAbillityDamage.Value += 3;
                        Global.SimpleSwordCount.Value += 2;
                        break;
                    case 3:
                        Global.SimpleAbillityDamage.Value += 2;
                        Global.SimpleAbillityDuration.Value -= 0.25f;
                        break;
                    case 4:
                        Global.SimpleAbillityDamage.Value += 2;
                        Global.SimpleAbillityDuration.Value -= 0.25f;
                        break;
                    case 5:
                        Global.SimpleAbillityDamage.Value += 3;
                        Global.SimpleSwordCount.Value += 2;
                        break;
                    case 6:
                        Global.SimpleSwordRange.Value++;
                        Global.SimpleAbillityDuration.Value -= 0.25f;
                        break;
                    case 7:
                        Global.SimpleAbillityDamage.Value += 3;
                        Global.SimpleSwordCount.Value += 2;
                        break;
                    case 8:
                        Global.SimpleAbillityDamage.Value += 2;
                        Global.SimpleSwordRange.Value++;
                        break;
                    case 9:
                        Global.SimpleAbillityDamage.Value += 3;
                        Global.SimpleAbillityDuration.Value -= 0.25f;
                        break;
                    case 10:
                        Global.SimpleAbillityDamage.Value += 3;
                        Global.SimpleSwordCount.Value += 2;
                        break;

                }
            });

       Add(new ExpUpgradeItem(true))
             .WithKey("Rotate_Sword")
              .WithMaxLevel(10)
            .WithDescription(lv =>
            {
                return lv switch
                {
                    1 => $"ÊØÎÀ½£Lv{lv}: \n»·ÈÆÖ÷½ÇÉí±ßµÄ½£",
                    2 => $"ÊØÎÀ½£Lv{lv}: \nÊýÁ¿ + 1 ¹¥»÷Á¦ + 1",
                    3 => $"ÊØÎÀ½£Lv{lv}: \n¹¥»÷Á¦ + 2 ËÙ¶È+25%",
                    4 => $"ÊØÎÀ½£Lv{lv}: \nËÙ¶È+50%",
                    5 => $"ÊØÎÀ½£Lv{lv}: \nÊýÁ¿ + 1 ¹¥»÷Á¦ + 1",
                    6 => $"ÊØÎÀ½£Lv{lv}: \n¹¥»÷Á¦ + 2 ËÙ¶È+25%",
                    7 => $"ÊØÎÀ½£Lv{lv}: \nÊýÁ¿ + 1 ¹¥»÷Á¦ + 1",
                    8 => $"ÊØÎÀ½£Lv{lv}: \n¹¥»÷Á¦ + 2 ËÙ¶È+25%",
                    9 => $"ÊØÎÀ½£Lv{lv}: \nÊýÁ¿ + 1 ¹¥»÷Á¦ + 1",
                    10 => $"ÊØÎÀ½£Lv{lv}: \n¹¥»÷Á¦ + 2 ËÙ¶È+25%",
                    _ => null
                };
            })
           
            .OnUpgrade((_, level) =>
            {
                switch (level)
                {
                    case 1:
                        Global.RotateSwordUnlocked.Value = true;
                        break;
                    case 2:
                        Global.RotateSwordCount.Value++;
                        Global.RotateSwordDamage.Value++;
                        break;
                    case 3:
                        Global.RotateSwordDamage.Value++;
                        Global.RotateSwordDamage.Value++;
                        Global.RotateSwordSpeed.Value *= 1.25f;
                        break;
                    case 4:
                        Global.RotateSwordSpeed.Value *= 1.5f;
                        break;
                    case 5:
                        Global.RotateSwordCount.Value++;
                        Global.RotateSwordDamage.Value++;
                        break;
                    case 6:
                        Global.RotateSwordDamage.Value++;
                        Global.RotateSwordDamage.Value++;
                        Global.RotateSwordSpeed.Value *= 1.25f;
                        break;
                    case 7:
                        Global.RotateSwordCount.Value++;
                        Global.RotateSwordDamage.Value++;
                        break;
                    case 8:
                        Global.RotateSwordDamage.Value++;
                        Global.RotateSwordDamage.Value++;
                        Global.RotateSwordSpeed.Value *= 1.25f;
                        break;
                    case 9:
                        Global.RotateSwordCount.Value++;
                        Global.RotateSwordDamage.Value++;
                        break;
                    case 10:
                        Global.RotateSwordDamage.Value++;
                        Global.RotateSwordDamage.Value++;
                        Global.RotateSwordSpeed.Value *= 1.25f;
                        break;

                }
            });
        Add(new ExpUpgradeItem(false)
            .WithKey("simple_bomb")
            .WithMaxLevel(10)
            .WithDescription(lv =>
            {
                return lv switch
                {
                    1 => $"Õ¨µ¯Lv{lv}:\n¹¥»÷È«²¿µÐÈË(¹ÖÎïµôÂä)",
                    2 => $"Õ¨µ¯Lv{lv}:\nµôÂä¸ÅÂÊ+5% ¹¥»÷Á¦+5",
                    3 => $"Õ¨µ¯Lv{lv}:\nµôÂä¸ÅÂÊ+5% ¹¥»÷Á¦+5",
                    4 => $"Õ¨µ¯Lv{lv}:\nµôÂä¸ÅÂÊ+5% ¹¥»÷Á¦+5",
                    5 => $"Õ¨µ¯Lv{lv}:\nµôÂä¸ÅÂÊ+5% ¹¥»÷Á¦+5",
                    6 => $"Õ¨µ¯Lv{lv}:\nµôÂä¸ÅÂÊ+5% ¹¥»÷Á¦+5",
                    7 => $"Õ¨µ¯Lv{lv}:\nµôÂä¸ÅÂÊ+5% ¹¥»÷Á¦+5",
                    8 => $"Õ¨µ¯Lv{lv}:\nµôÂä¸ÅÂÊ+5% ¹¥»÷Á¦+5",
                    9 => $"Õ¨µ¯Lv{lv}:\nµôÂä¸ÅÂÊ+5% ¹¥»÷Á¦+5",
                    10 => $"Õ¨µ¯Lv{lv}:\nµôÂä¸ÅÂÊ+10% ¹¥»÷Á¦+5",
                    _ => null
                };
            })
            .OnUpgrade((_, level) =>
            {
                switch (level)
                {
                    case 1:
                        Global.BombUnlocked.Value = true;
                        break;
                    case 2:
                        Global.BombDamage.Value += 5;
                        Global.BombPercent.Value += 0.05f;
                        break;
                    case 3:
                        Global.BombDamage.Value += 5;
                        Global.BombPercent.Value += 0.05f;
                        break;
                    case 4:
                        Global.BombDamage.Value += 5;
                        Global.BombPercent.Value += 0.05f;
                        break;
                    case 5:
                        Global.BombDamage.Value += 5;
                        Global.BombPercent.Value += 0.05f;
                        break;
                    case 6:
                        Global.BombDamage.Value += 5;
                        Global.BombPercent.Value += 0.05f;
                        break;
                    case 7:
                        Global.BombDamage.Value += 5;
                        Global.BombPercent.Value += 0.05f;
                        break;
                    case 8:
                        Global.BombDamage.Value += 5;
                        Global.BombPercent.Value += 0.05f;
                        break;
                    case 9:
                        Global.BombDamage.Value += 5;
                        Global.BombPercent.Value += 0.05f;
                        break;
                    case 10:
                        Global.BombDamage.Value += 5;
                        Global.BombPercent.Value += 0.1f;
                        break;
                }
            })
        );

        Add(new ExpUpgradeItem(true))
            .WithKey("Simple_knife")
             .WithMaxLevel(10)
           .WithDescription(lv =>
           {
               return lv switch
               {
                   1 => $"·Éµ¶Lv{lv}:Ïò×î½üµÄµÐÈË·¢ÉäÒ»°Ñ·Éµ¶",
                   2 => $"·Éµ¶Lv{lv}:\n¹¥»÷Á¦+3£¬ÊýÁ¿+2",
                   3 => $"·Éµ¶Lv{lv}:\n¼ä¸ô-0.1s£¬¹¥»÷Á¦+1£¬ÊýÁ¿+1",
                   4 => $"·Éµ¶Lv{lv}:\n¼ä¸ô-0.1s£¬´©Í¸+1£¬ÊýÁ¿+1",
                   5 => $"·Éµ¶Lv{lv}:\n¹¥»÷Á¦+3£¬ÊýÁ¿+1",
                   6 => $"·Éµ¶Lv{lv}:\n¼ä¸ô-0.1s£¬ÊýÁ¿+1",
                   7 => $"·Éµ¶Lv{lv}:\n¼ä¸ô-0.1s£¬´©Í¸+1£¬ÊýÁ¿+1",
                   8 => $"·Éµ¶Lv{lv}:\n¹¥»÷Á¦+3£¬ÊýÁ¿+1",
                   9 => $"·Éµ¶Lv{lv}:\n¼ä¸ô-0.1s£¬ÊýÁ¿+1",
                   10 => $"·Éµ¶Lv{lv}\n¹¥»÷Á¦+3£¬ÊýÁ¿+1",
                   _ => null
               };
           })

           .OnUpgrade((_, level) =>
           {
               switch (level)
               {
                   case 1:
                       Global.SimpleKnifeUnlocked.Value = true;
                       break;
                   case 2:
                       Global.SimpleKinfeDamage.Value += 3;
                       Global.SimpleKinfeCount.Value += 2;
                       break;
                   case 3:
                       Global.SimpleKinfeDamage.Value += 2;
                       Global.SimpleKinfeDuration.Value -= 0.1f;
                       Global.SimpleKinfeCount.Value++;
                       break;
                   case 4:
                       Global.SimpleKinfeDuration.Value -= 0.1f;
                       Global.SimpleKinfeAttackCount.Value++;
                       Global.SimpleKinfeCount.Value++;
                       break;
                   case 5:
                       Global.SimpleKinfeDamage.Value += 3;
                       Global.SimpleKinfeCount.Value++;
                       break;
                   case 6:
                       Global.SimpleKinfeDuration.Value -= 0.1f;
                       Global.SimpleKinfeCount.Value++;
                       break;
                   case 7:
                       Global.SimpleKinfeDuration.Value -= 0.1f;
                       Global.SimpleKinfeAttackCount.Value++;
                       Global.SimpleKinfeCount.Value++;
                       break;
                   case 8:
                       Global.SimpleKinfeDamage.Value += 3;
                       Global.SimpleKinfeCount.Value++;
                       break;
                   case 9:
                       Global.SimpleKinfeDuration.Value -= 0.1f;
                       Global.SimpleKinfeCount.Value++;
                       break;
                   case 10:
                       Global.SimpleKinfeDamage.Value += 3;
                       Global.SimpleKinfeCount.Value++;
                       break;

               }
           });
        Add(new ExpUpgradeItem(true)
             .WithKey("basket_ball")
             .WithMaxLevel(10)
             .WithDescription(lv =>
             {
                 return lv switch
                 {
                     1 => $"ÀºÇòLv{lv}:\nÔÚÆÁÄ»ÄÚ·´µ¯µÄÀºÇò",
                     2 => $"ÀºÇòLv{lv}:\n¹¥»÷Á¦+3",
                     3 => $"ÀºÇòLv{lv}:\nÊýÁ¿+1",
                     4 => $"ÀºÇòLv{lv}:\n¹¥»÷Á¦+3",
                     5 => $"ÀºÇòLv{lv}:\nÊýÁ¿+1",
                     6 => $"ÀºÇòLv{lv}:\n¹¥»÷Á¦+3",
                     7 => $"ÀºÇòLv{lv}:\nËÙ¶È+20%",
                     8 => $"ÀºÇòLv{lv}:\n¹¥»÷Á¦+3",
                     9 => $"ÀºÇòLv{lv}:\nËÙ¶È+20%",
                     10 => $"ÀºÇòLv{lv}:\nÊýÁ¿+1",
                     _ => null
                 };
             })
             .OnUpgrade((_, level) =>
             {
                 switch (level)
                 {
                     case 1:
                         Global.BasketBallUnlocked.Value = true;
                         break;
                     case 2:
                         Global.BasketBallDamage.Value += 3;
                         break;
                     case 3:
                         Global.BasketBallCount.Value++;
                         break;
                     case 4:
                         Global.BasketBallDamage.Value += 3;
                         break;
                     case 5:
                         Global.BasketBallCount.Value++;
                         break;
                     case 6:
                         Global.BasketBallDamage.Value += 3;
                         break;
                     case 7:
                         Global.BasketBallSpeed.Value *= 1.2f;
                         break;
                     case 8:
                         Global.BasketBallDamage.Value += 3;
                         break;
                     case 9:
                         Global.BasketBallSpeed.Value *= 1.2f;
                         break;
                     case 10:
                         Global.BasketBallCount.Value++;
                         break;
                 }
             })
         );

    }
    public void Roll()
    {

        foreach(var expUpgradeItem in Items)
        {
            expUpgradeItem.Visible.Value = false;
        }
       foreach(var item in Items.Where(item => !item.UpgradeFinish).Take(5))
        {
            if (item == null)
            {

            }
            else
            {
                item.Visible.Value = true;
            }
            item.Visible.Value = true;
        }

       
    }
}
