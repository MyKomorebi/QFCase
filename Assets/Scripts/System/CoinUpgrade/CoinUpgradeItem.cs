using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinUpgradeItem : MonoBehaviour
{

    
    public EasyEvent Onchange=new EasyEvent();

    private CoinUpgradeItem mNext=null;

    public CoinUpgradeItem Next(CoinUpgradeItem next)
    {
        mNext = next;

        mNext.Condition(_ => UpgradeFinish);
        return mNext;

    }
    public bool UpgradeFinish { get; set; } = false;
    public string Key { get;private set; }


    public string Description { get;private set; }

    public int Price {  get;private set; } 

    public void Upgrade()
    {
        mOnUpgrade?.Invoke(this);
            UpgradeFinish = true;
        TriggerOnChanged();
        CoinUpgradeSystem.OnCoinUpgradeSystem.Trigger();
    }
    public void TriggerOnChanged()
    {
        Onchange.Trigger();
        mNext?.TriggerOnChanged();
    }
    public bool ConditionCheck()
    {
        if (mCondition != null)
        {
            return !UpgradeFinish&& mCondition.Invoke(this);
        }
        return !UpgradeFinish;

    }

    private Action<CoinUpgradeItem> mOnUpgrade;

    private Func<CoinUpgradeItem, bool> mCondition;

    public CoinUpgradeItem WithKey(string key)
    {
        Key = Key;
        return this;
    }

    public CoinUpgradeItem WithDescription(string description)
    {
        Description = description;
        return this;
    }

    public CoinUpgradeItem WithPrice(int price)
    {
        Price = price;
        return this;
    }

    public CoinUpgradeItem OnUpgrade(Action<CoinUpgradeItem> onUpgrade)
    {
        mOnUpgrade= onUpgrade;
        return this;
    }
    public CoinUpgradeItem Condition(Func<CoinUpgradeItem, bool> condition)
    {
        mCondition= condition;
        return this;
    }

    
}
