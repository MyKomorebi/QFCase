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
