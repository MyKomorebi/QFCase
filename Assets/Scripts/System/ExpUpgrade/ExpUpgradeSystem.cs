using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using ProjectSurvivor;

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
        Add(new ExpUpgradeItem()
            .WithKey("simple_damage_lv1")
            .WithDescription("简单能力的攻击力提升 Lv1")
            .OnUpgrade(_ =>
            {
                Global.SimpleAbillityDamage.Value *= 1.5f;
            })
            );
    }

   
}
