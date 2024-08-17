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

        var simpleDamageLv1 = Add(new ExpUpgradeItem()
              .WithKey("simple_damage")
              .WithDescription(lv=>$"简单能力的攻击力提升 Lv{lv}")
              .WithMaxLevel(10)
              .OnUpgrade((_, level) =>
              {
                  if (level == 1)
                  {

                  }
                  Global.SimpleAbillityDamage.Value *= 1.5f;
              })
             );



        var simpleDurationLv1 = Add(new ExpUpgradeItem()
              .WithKey("simple_duration")
              .WithDescription(lv=>$"简单能力的攻击间隔减少 Lv{lv}")
                .WithMaxLevel(10)
              .OnUpgrade((_, level) =>
              {
                  if (level == 1)
                  {

                  }
                  Global.SimpleAbillityDamage.Value *= 1.5f;
              })
             );
    }
    public void Roll()
    {

        foreach(var expUpgradeItem in Items)
        {
            expUpgradeItem.Visible.Value = false;
        }
        var item = Items.Where(item => !item.UpgradeFinish).ToList().GetRandomItem();

        if(item == null)
        {

        }
        else
        {
            item.Visible.Value = true;
        }
        item.Visible.Value = true;
    }
}
