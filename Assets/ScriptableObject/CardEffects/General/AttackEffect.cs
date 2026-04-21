using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(menuName = "Card Effects/Attack")]
public class AttackEffect : CardEffect
{
    public int damagePercentage;

    public Stat scalingStat;

    public Elements elementalDamage;

    public int target;

    public override void Execute(BattleManager battleManager, Chr user)
    {
        float damageAmount;
        int damagedRounded = 0;
        switch (scalingStat)
        {
            case Stat.ATK:
                damageAmount = (user.ATK / 100) * damagePercentage;
                damagedRounded = (int)Math.Round(damageAmount);
                battleManager.DealDamageToEnemies(damagedRounded, elementalDamage,target);
                break;

            case Stat.MAG:
                damageAmount = (user.MAG / 100) * damagePercentage;
                damagedRounded = (int)Math.Round(damageAmount);
                battleManager.DealDamageToEnemies(damagedRounded, elementalDamage, target);
                break;

            case Stat.DEF:
                damageAmount = (user.DEF / 100) * damagePercentage;
                damagedRounded = (int)Math.Round(damageAmount);
                battleManager.DealDamageToEnemies(damagedRounded, elementalDamage, target);
                break;

            case Stat.HP:
                damageAmount = (user.HP / 100) * damagePercentage;
                damagedRounded = (int)Math.Round(damageAmount);
                battleManager.DealDamageToEnemies(damagedRounded, elementalDamage, target);
                break;

            default:
                damageAmount = (user.ATK / 100) * damagePercentage;
                damagedRounded = (int)Math.Round(damageAmount);
                battleManager.DealDamageToEnemies(damagedRounded, elementalDamage, target);
                break;
        }
    }
}
