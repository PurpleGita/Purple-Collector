using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(menuName = "Card Effects/Energy")]
public class Energy : CardEffect
{
    public int energyAmount;


    public override void Execute(BattleManager battleManager, Chr user)
    {
        int userPos = battleManager.GetIntFromChr(user.characterName);
        List<int> userPosList = new List<int>();
        userPosList.Add(userPos);
        battleManager.GiveEnergy(userPosList, energyAmount);
    }
}
