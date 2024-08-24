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
        Add(new ExpUpgradeItem())
            .WithKey("Simple_Sword")
            .WithDescription(lv =>
            {
                return lv switch
                {
                    1 => $"剑Lv{lv}:攻击身边的敌人",
                    2 => $"剑Lv{lv}:\n攻击力+3，数量+2",
                    3 => $"剑Lv{lv}:\n攻击力+2，间隔-0.25s",
                    4 => $"剑Lv{lv}:\n攻击力+2，间隔-0.25s",
                    5 => $"剑Lv{lv}:\n攻击力+3，数量+2",
                    6 => $"剑Lv{lv}:\n范围+1，间隔-0.25s",
                    7 => $"剑Lv{lv}:\n攻击力+3，数量+2",
                    8 => $"剑Lv{lv}:\n攻击力+2，范围+1",
                    9 => $"剑Lv{lv}:\n攻击力+3，间隔-0.25s",
                    10 => $"剑Lv{lv}\n攻击力+3，数量+2",
                    _ => null
                };
            })
            .WithMaxLevel(10)
            .OnUpgrade((_, level) =>
            {
                switch (level)
                {
                    case 1:
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

       Add(new ExpUpgradeItem())
             .WithKey("Simple_knife")
              .WithMaxLevel(10)
            .WithDescription(lv =>
            {
                return lv switch
                {
                    1 => $"飞刀Lv{lv}:向最近的敌人发射一把飞刀",
                    2 => $"飞刀Lv{lv}:\n攻击力+3，数量+2",
                    3 => $"飞刀Lv{lv}:\n间隔-0.1s，攻击力+1，数量+1",
                    4 => $"飞刀Lv{lv}:\n间隔-0.1s，穿透+1，数量+1",
                    5 => $"飞刀Lv{lv}:\n攻击力+3，数量+1",
                    6 => $"飞刀Lv{lv}:\n间隔-0.1s，数量+1",
                    7 => $"飞刀Lv{lv}:\n间隔-0.1s，穿透+1，数量+1",
                    8 => $"飞刀Lv{lv}:\n攻击力+3，数量+1",
                    9 => $"飞刀Lv{lv}:\n间隔-0.1s，数量+1",
                    10 => $"飞刀Lv{lv}\n攻击力+3，数量+1",
                    _ => null
                };
            })
           
            .OnUpgrade((_, level) =>
            {
                switch (level)
                {
                    case 1:
                        break;
                    case 2:
                        Global.SimpleKinfeDamage.Value += 3;
                        Global.SimpleKinfeCount.Value += 2;
                        break;
                    case 3:
                        Global.SimpleKinfeDamage.Value += 2;
                        Global.SimpleKinfeDuration.Value -= 0.1f;
                        Global.SimpleKinfeCount.Value ++;
                        break;
                    case 4:
                        Global.SimpleKinfeDuration.Value -= 0.1f;
                        Global.SimpleKinfeAttackCount.Value ++;
                        Global.SimpleKinfeCount.Value++;
                        break;
                    case 5:
                        Global.SimpleKinfeDamage.Value += 3;
                        Global.SimpleKinfeCount.Value ++;
                        break;
                    case 6:
                        Global.SimpleKinfeDuration.Value-=0.1f;
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
                        Global.SimpleKinfeCount.Value ++;
                        break;
                    case 10:
                        Global.SimpleKinfeDamage.Value += 3;
                        Global.SimpleKinfeCount.Value++;
                        break;

                }
            });

    }
    public void Roll()
    {

        foreach(var expUpgradeItem in Items)
        {
            expUpgradeItem.Visible.Value = false;
        }
       foreach(var item in Items.Where(item => !item.UpgradeFinish).Take(2))
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
