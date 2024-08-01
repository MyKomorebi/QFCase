using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpUpgradeItem 
{
    public EasyEvent Onchange = new EasyEvent();
    public bool UpgradeFinish { get; set; } = false;
    public string Key { get; private set; }


    public string Description { get; private set; }

    public int Price { get; private set; }

    public void Upgrade()
    {
        mOnUpgrade?.Invoke(this);
        UpgradeFinish = true;
        Onchange.Trigger();
        CoinUpgradeSystem.OnCoinUpgradeSystem.Trigger();
    }

    public bool ConditionCheck()
    {
        if (mCondition != null)
        {
            return !UpgradeFinish && mCondition.Invoke(this);
        }
        return !UpgradeFinish;

    }

    private Action<ExpUpgradeItem> mOnUpgrade;

    private Func<ExpUpgradeItem, bool> mCondition;

    public ExpUpgradeItem WithKey(string key)
    {
        Key = Key;
        return this;
    }

    public ExpUpgradeItem WithDescription(string description)
    {
        Description = description;
        return this;
    }

    public ExpUpgradeItem WithPrice(int price)
    {
        Price = price;
        return this;
    }

    public ExpUpgradeItem OnUpgrade(Action<ExpUpgradeItem> onUpgrade)
    {
        mOnUpgrade = onUpgrade;
        return this;
    }
    public ExpUpgradeItem Condition(Func<ExpUpgradeItem, bool> condition)
    {
        mCondition = condition;
        return this;
    }

}
