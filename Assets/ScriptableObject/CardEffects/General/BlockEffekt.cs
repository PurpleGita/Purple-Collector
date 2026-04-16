using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(menuName = "Card Effects/Defend")]
public class Defend : CardEffect
{
    public int blockPercentage;

    public Stat scalingStat;

    public override void Execute(BattleManager battleManager, Chr user)
    {


    }
}
