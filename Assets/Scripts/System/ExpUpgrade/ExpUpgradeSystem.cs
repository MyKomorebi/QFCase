using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using ProjectSurvivor;
using System.Linq;

public class ExpUpgradeSystem : AbstractSystem
{

    public List<ExpUpgradeItem> Items { get; } = new List<ExpUpgradeItem>();
    public static bool AllUnlockedFinish=false;

    public static void CheckAllUnlockFinish()
    {
        AllUnlockedFinish = Global.Interface.GetSystem<ExpUpgradeSystem>().Items
            .All(i => i.UpgradeFinish);
    }

    public Dictionary<string, ExpUpgradeItem> Dictionary = new ();
    public Dictionary<string, string> Pairs = new Dictionary<string, string>()
        {
            { "simple_sword", "simple_critical" },
            { "simple_bomb", "simple_fly_count" },
            { "simple_knife", "damage_rate" },
            { "basket_ball", "movement_speed_rate" },
            { "rotate_sword", "simple_exp" },

            { "simple_critical", "simple_sword" },
            { "simple_fly_count", "simple_bomb" },
            { "damage_rate", "simple_knife" },
            { "movement_speed_rate", "basket_ball" },
            { "simple_exp", "rotate_sword" },
        };

    public Dictionary<string, BindableProperty<bool>>PairedProperties =
           new()
           {
                { "simple_sword", Global.SuperSword },
                { "simple_bomb", Global.SuperBomb },
                { "simple_knife", Global.SuperKnife },
                { "basket_ball", Global.SuperBasketBall },
                { "rotate_sword", Global.SuperRotateSword },

               // simple_exp
               // simple_collectable_area
           };
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
            .WithKey("simple_sword")
              .WithName("��")
                .WithIconName("simple_sword_icon")
                .WithPairedName("�ϳɺ�Ľ�")
                .WithPairedIconName("paired_simple_sword_icon")
                .WithPairedDescription("���������� ������Χ����")
            .WithDescription(lv =>
            {
                return lv switch
                {
                    1 => $"��Lv{lv}:������ߵĵ���",
                    2 => $"��Lv{lv}:\n������+3������+2",
                    3 => $"��Lv{lv}:\n������+2�����-0.25s",
                    4 => $"��Lv{lv}:\n������+2�����-0.25s",
                    5 => $"��Lv{lv}:\n������+3������+2",
                    6 => $"��Lv{lv}:\n��Χ+1�����-0.25s",
                    7 => $"��Lv{lv}:\n������+3������+2",
                    8 => $"��Lv{lv}:\n������+2����Χ+1",
                    9 => $"��Lv{lv}:\n������+3�����-0.25s",
                    10 => $"��Lv{lv}\n������+3������+2",
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
             .WithKey("rotate_sword")
              .WithName("������")
                .WithIconName("rotate_sword_icon")
                .WithPairedName("�ϳɺ��������")
                .WithPairedIconName("paired_rotate_sword_icon")
                .WithPairedDescription("���������� ��ת�ٶȷ���")
              .WithMaxLevel(10)
            .WithDescription(lv =>
            {
                return lv switch
                {
                    1 => $"������Lv{lv}: \n����������ߵĽ�",
                    2 => $"������Lv{lv}: \n���� + 1 ������ + 1",
                    3 => $"������Lv{lv}: \n������ + 2 �ٶ�+25%",
                    4 => $"������Lv{lv}: \n�ٶ�+50%",
                    5 => $"������Lv{lv}: \n���� + 1 ������ + 1",
                    6 => $"������Lv{lv}: \n������ + 2 �ٶ�+25%",
                    7 => $"������Lv{lv}: \n���� + 1 ������ + 1",
                    8 => $"������Lv{lv}: \n������ + 2 �ٶ�+25%",
                    9 => $"������Lv{lv}: \n���� + 1 ������ + 1",
                    10 => $"������Lv{lv}: \n������ + 2 �ٶ�+25%",
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
               .WithKey("simple_critical")
                .WithName("����")
                .WithIconName("critical_icon")
               .WithMaxLevel(5)
               .WithDescription(lv =>
               {
                   return lv switch
                   {
                       1 => $"����Lv{lv}:\nÿ���˺� 15% ���ʱ���",
                       2 => $"����Lv{lv}:\nÿ���˺� 28% ���ʱ���",
                       3 => $"����Lv{lv}:\nÿ���˺� 43% ���ʱ���",
                       4 => $"����Lv{lv}:\nÿ���˺� 50% ���ʱ���",
                       5 => $"����Lv{lv}:\nÿ���˺� 80% ���ʱ���",
                       _ => null
                   };
               })
               .OnUpgrade((_, lv) =>
               {
                   switch (lv)
                   {
                       case 1:
                           Global.CriticalRate.Value = 0.15f;
                           break;
                       case 2:
                           Global.CriticalRate.Value = 0.28f;
                           break;
                       case 3:
                           Global.CriticalRate.Value = 0.43f;
                           break;
                       case 4:
                           Global.CriticalRate.Value = 0.5f;
                           break;
                       case 5:
                           Global.CriticalRate.Value = 0.8f;
                           break;
                   }
               }));
        Add(new ExpUpgradeItem(false)
                .WithKey("damage_rate")
                 .WithName("�˺���")
                .WithIconName("damage_icon")
                .WithMaxLevel(5)
                .WithDescription(lv =>
                {
                    return lv switch
                    {
                        1 => $"�˺���Lv{lv}:\n���� 20% �����˺�",
                        2 => $"�˺���Lv{lv}:\n���� 40% �����˺�",
                        3 => $"�˺���Lv{lv}:\n���� 60% �����˺�",
                        4 => $"�˺���Lv{lv}:\n���� 80% �����˺�",
                        5 => $"�˺���Lv{lv}:\n���� 100% �����˺�",

                        _ => null
                    };
                })
                .OnUpgrade((_, lv) =>
                {
                    switch (lv)
                    {
                        case 1:
                            Global.DamageRate.Value = 1.2f;
                            break;
                        case 2:
                            Global.DamageRate.Value = 1.4f;
                            break;
                        case 3:
                            Global.DamageRate.Value = 1.6f;
                            break;
                        case 4:
                            Global.DamageRate.Value = 1.8f;
                            break;
                        case 5:
                            Global.DamageRate.Value = 2f;
                            break;
                    }
                }));
        Add(new ExpUpgradeItem(false)
               .WithKey("simple_fly_count")
                .WithIconName("fly_icon")
                .WithName("������")
               .WithMaxLevel(3)
               .WithDescription(lv =>
               {
                   return lv switch
                   {
                       1 => $"������Lv{lv}:\n�������� 1 ��������",
                       2 => $"������Lv{lv}:\n�������� 2 ��������",
                       3 => $"������Lv{lv}:\n�������� 3 ��������",
                       _ => null
                   };
               })
               .OnUpgrade((_, lv) =>
               {
                   switch (lv)
                   {
                       case 1:
                           Global.AdditionalFlyThingCount.Value++;
                           break;
                       case 2:
                           Global.AdditionalFlyThingCount.Value++;
                           break;
                       case 3:
                           Global.AdditionalFlyThingCount.Value++;
                           break;
                   }
               }));
        Add(new ExpUpgradeItem(false)
               .WithKey("movement_speed_rate")
                .WithName("�ƶ��ٶ�")
                .WithIconName("movement_icon")
               .WithMaxLevel(5)
               .WithDescription(lv =>
               {
                   return lv switch
                   {
                       1 => $"�ƶ��ٶ�Lv{lv}:\n���� 25% �ƶ��ٶ�",
                       2 => $"�ƶ��ٶ�Lv{lv}:\n���� 50% �ƶ��ٶ�",
                       3 => $"�ƶ��ٶ�Lv{lv}:\n���� 75% �ƶ��ٶ�",
                       4 => $"�ƶ��ٶ�Lv{lv}:\n���� 100% �ƶ��ٶ�",
                       5 => $"�ƶ��ٶ�Lv{lv}:\n���� 150% �ƶ��ٶ�",

                       _ => null
                   };
               })
               .OnUpgrade((_, lv) =>
               {
                   switch (lv)
                   {
                       case 1:
                           Global.MovementSpeedRate.Value = 1.25f;
                           break;
                       case 2:
                           Global.MovementSpeedRate.Value = 1.5f;
                           break;
                       case 3:
                           Global.MovementSpeedRate.Value = 1.75f;
                           break;
                       case 4:
                           Global.MovementSpeedRate.Value = 2f;
                           break;
                       case 5:
                           Global.MovementSpeedRate.Value = 2.5f;
                           break;
                   }
               }));
        Add(new ExpUpgradeItem(false)
              .WithKey("simple_collectable_area")
              .WithName("ʰȡ��Χ")
                .WithIconName("collectable_icon")
              .WithMaxLevel(3)
              .WithDescription(lv =>
              {
                  return lv switch
                  {
                      1 => $"ʰȡ��ΧLv{lv}:\n�������� 100% ��Χ",
                      2 => $"ʰȡ��ΧLv{lv}:\n�������� 200% ��Χ",
                      3 => $"ʰȡ��ΧLv{lv}:\n�������� 300% ��Χ",
                      _ => null
                  };
              })
              .OnUpgrade((_, lv) =>
              {
                  switch (lv)
                  {
                      case 1:
                          Global.CollectableArea.Value = 2f;
                          break;
                      case 2:
                          Global.CollectableArea.Value = 3f;
                          break;
                      case 3:
                          Global.CollectableArea.Value = 4f;
                          break;
                  }
              }));
        Add(new ExpUpgradeItem(false)
              .WithKey("simple_exp")
              .WithName("����ֵ")
                .WithIconName("exp_icon")
              .WithMaxLevel(5)
              .WithDescription(lv =>
              {
                  return lv switch
                  {
                      1 => $"����ֵLv{lv}:\n�������� 5% �������",
                      2 => $"����ֵLv{lv}:\n�������� 8% �������",
                      3 => $"����ֵLv{lv}:\n�������� 12% �������",
                      4 => $"����ֵLv{lv}:\n�������� 17% �������",
                      5 => $"����ֵLv{lv}:\n�������� 25% �������",
                      _ => null
                  };
              })
              .OnUpgrade((_, lv) =>
              {
                  switch (lv)
                  {
                      case 1:
                          Global.AdditionalExpPercent.Value = 0.05f;
                          break;
                      case 2:
                          Global.AdditionalExpPercent.Value = 0.08f;
                          break;
                      case 3:
                          Global.AdditionalExpPercent.Value = 0.12f;
                          break;
                      case 4:
                          Global.AdditionalExpPercent.Value = 0.17f;
                          break;
                      case 5:
                          Global.AdditionalExpPercent.Value = 0.25f;
                          break;
                  }
              }));
        Add(new ExpUpgradeItem(false)
            .WithKey("simple_bomb")
            .WithMaxLevel(10)
            .WithDescription(lv =>
            {
                return lv switch
                {
                    1 => $"ը��Lv{lv}:\n����ȫ������(�������)",
                    2 => $"ը��Lv{lv}:\n�������+5% ������+5",
                    3 => $"ը��Lv{lv}:\n�������+5% ������+5",
                    4 => $"ը��Lv{lv}:\n�������+5% ������+5",
                    5 => $"ը��Lv{lv}:\n�������+5% ������+5",
                    6 => $"ը��Lv{lv}:\n�������+5% ������+5",
                    7 => $"ը��Lv{lv}:\n�������+5% ������+5",
                    8 => $"ը��Lv{lv}:\n�������+5% ������+5",
                    9 => $"ը��Lv{lv}:\n�������+5% ������+5",
                    10 => $"ը��Lv{lv}:\n�������+10% ������+5",
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
            .WithKey("simple_knife")
            .WithName("�ɵ�")
                .WithIconName("simple_knife_icon")
                .WithPairedName("�ϳɺ�ķɵ�")
                .WithPairedIconName("paired_simple_knife_icon")
                .WithPairedDescription("����������")
             .WithMaxLevel(10)
           .WithDescription(lv =>
           {
               return lv switch
               {
                   1 => $"�ɵ�Lv{lv}:������ĵ��˷���һ�ѷɵ�",
                   2 => $"�ɵ�Lv{lv}:\n������+3������+2",
                   3 => $"�ɵ�Lv{lv}:\n���-0.1s��������+1������+1",
                   4 => $"�ɵ�Lv{lv}:\n���-0.1s����͸+1������+1",
                   5 => $"�ɵ�Lv{lv}:\n������+3������+1",
                   6 => $"�ɵ�Lv{lv}:\n���-0.1s������+1",
                   7 => $"�ɵ�Lv{lv}:\n���-0.1s����͸+1������+1",
                   8 => $"�ɵ�Lv{lv}:\n������+3������+1",
                   9 => $"�ɵ�Lv{lv}:\n���-0.1s������+1",
                   10 => $"�ɵ�Lv{lv}\n������+3������+1",
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
             .WithName("����")
                .WithIconName("ball_icon")
                .WithPairedName("�ϳɺ������")
                .WithPairedIconName("paired_ball_icon")
                .WithPairedDescription("���������� ������")
             .WithMaxLevel(10)
             .WithDescription(lv =>
             {
                 return lv switch
                 {
                     1 => $"����Lv{lv}:\n����Ļ�ڷ���������",
                     2 => $"����Lv{lv}:\n������+3",
                     3 => $"����Lv{lv}:\n����+1",
                     4 => $"����Lv{lv}:\n������+3",
                     5 => $"����Lv{lv}:\n����+1",
                     6 => $"����Lv{lv}:\n������+3",
                     7 => $"����Lv{lv}:\n�ٶ�+20%",
                     8 => $"����Lv{lv}:\n������+3",
                     9 => $"����Lv{lv}:\n�ٶ�+20%",
                     10 => $"����Lv{lv}:\n����+1",
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
             }));
       
      
        Dictionary = Items.ToDictionary(i => i.Key);
        
    }
    public void Roll()
    {

        foreach(var expUpgradeItem in Items)
        {
            expUpgradeItem.Visible.Value = false;
        }
        
        var list= Items.Where(item => !item.UpgradeFinish).ToList();
        if (list.Count >= 4)
        {
           
            list.GetAndRemoveRandomItem().Visible.Value = true;
            list.GetAndRemoveRandomItem().Visible.Value = true;
            list.GetAndRemoveRandomItem().Visible.Value = true;
            list.GetAndRemoveRandomItem().Visible.Value = true;
        }
        else
        {
          
            foreach(var item in list)
            {
                item.Visible.Value = true;
            }
        }
       

       
    }
}
