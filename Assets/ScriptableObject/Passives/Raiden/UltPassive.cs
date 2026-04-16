using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Passives/UltPassive")]
public class UltPassive : Passive
{
    public int ultCost = 0;

    public override void OnBattleStart(BattleManager bm, Chr owner)
    {
        Debug.Log("bm mod count:" + bm.modifiedChrs.Count);
        foreach (ChrRuntime chrs in bm.modifiedChrs)
        {
            ultCost += chrs.ult.manaCost;
            Debug.Log("changed raidens ult cost to " + ultCost);
        }

        bm.modifiedChrs[bm.GetIntFromChr(owner.characterName)].ult.manaCost = ultCost;


    }
}