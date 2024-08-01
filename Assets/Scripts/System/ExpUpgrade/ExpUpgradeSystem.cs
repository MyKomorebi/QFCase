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
     var simpleDamageLv1=Add(new ExpUpgradeItem()
            .WithKey("simple_damage_lv1")
            .WithDescription("简单能力的攻击力提升 Lv1")
            .OnUpgrade(_ =>
            {
                Global.SimpleAbillityDamage.Value *= 1.5f;
            })
           );
       var simpleDamageLv2= Add(new ExpUpgradeItem()
           .WithKey("simple_damage_lv2")
           .WithDescription("简单能力的攻击力提升 Lv2")
           .Condition(item=>simpleDamageLv1.UpgradeFinish)
           .OnUpgrade(_ =>
           {
               Global.SimpleAbillityDamage.Value *= 1.5f;
           })
          );
        var simpleDamageLv3=Add(new ExpUpgradeItem()
           .WithKey("simple_damage_lv3")
           .WithDescription("简单能力的攻击力提升 Lv3")
           .Condition(item => simpleDamageLv2.UpgradeFinish)
           .OnUpgrade(_ =>
           {
               Global.SimpleAbillityDamage.Value *= 1.5f;
           })
          );


      var simpleDurationLv1=Add(new ExpUpgradeItem()
            .WithKey("simple_duration_lv1")
            .WithDescription("简单能力的攻击间隔减少 Lv1")
            .OnUpgrade(_ =>
            {
                Global.SimpleAbillityDamage.Value *= 0.8f;
            })
           );
     var simpleDurationLv2=Add(new ExpUpgradeItem()
            .WithKey("simple_duration_lv2")
            .WithDescription("简单能力的攻击间隔减少 Lv2")
            .Condition(_=> simpleDurationLv1.UpgradeFinish)
            .OnUpgrade(_ =>
            {
                Global.SimpleAbillityDamage.Value *= 0.8f;
            })
           );
        var simpleDurationLv3=   Add(new ExpUpgradeItem()
            .WithKey("simple_duration_lv3")
            .WithDescription("简单能力的攻击间隔减少 Lv3")
            .Condition(_ => simpleDurationLv2.UpgradeFinish)
            .OnUpgrade(_ =>
            {
                Global.SimpleAbillityDamage.Value *= 0.8f;
            })
           );
        simpleDamageLv1.Onchange.Register(() =>
        {
            simpleDamageLv2.Onchange.Trigger();
        });
        simpleDamageLv2.Onchange.Register(() =>
        {
            simpleDamageLv3.Onchange.Trigger();
        });
        simpleDurationLv1.Onchange.Register(() =>
        {
            simpleDurationLv2.Onchange.Trigger();
        });
        simpleDurationLv2.Onchange.Register(() =>
        {
            simpleDurationLv3.Onchange.Trigger();
        });
    }

}
