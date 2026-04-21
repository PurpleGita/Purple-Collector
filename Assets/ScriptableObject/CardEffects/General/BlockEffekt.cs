using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(menuName = "Card Effects/Defend")]
public class Defend : CardEffect
{
    public int blockPercentage;

    public Stat scalingStat;

    public override void Execute(BattleManager battleManager, Chr user)
    {
        float blockAmount = 0;
        int blockRounded = 0;

        switch (scalingStat)
        {

            case Stat.ATK:
                blockAmount = (user.ATK / 100) * blockPercentage;
                break;

            case Stat.DEF:
                blockAmount = (user.DEF / 100) * blockPercentage;
                break;

            case Stat.MAG:
                blockAmount = (user.DEF / 100) * blockPercentage;
                break;

            case Stat.HP:
                blockAmount = (user.DEF / 100) * blockPercentage;
                break;

            default:
                blockAmount = (user.DEF / 100) * blockPercentage;
                break;
        }

        blockRounded = (int)Math.Round(blockAmount);
        battleManager.Gainblock(blockRounded);

    }
}
