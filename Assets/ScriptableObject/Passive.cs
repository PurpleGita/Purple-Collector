using UnityEngine;


public abstract class Passive : ScriptableObject
{
    public Chr ownerChr;

    public virtual void OnBattleStart(BattleManager bm, Chr owner) { }

    public virtual void OnCardDrawn(BattleManager bm, Chr owner, CardData card) { }

    public virtual void OnCardPlayed(BattleManager bm, Chr owner, CardData card) { }

    public virtual void OnTurnStart(BattleManager bm, Chr owner) { }

    public virtual void OnGainEnergy(BattleManager bm, Chr owner, int amount) { }
}