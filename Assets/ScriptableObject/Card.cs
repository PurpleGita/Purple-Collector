using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "Card", menuName = "Scriptable Objects/Card")]
public class CardData : ScriptableObject
{
    public string cardName;
    public int manaCost;
    public string description;
    public Chr chr;
    public Sprite image;
    public bool signiture;
    public bool exile;

    public List<CardEffect> effects;
}

public abstract class CardEffect : ScriptableObject
{
    public abstract void Execute(BattleManager battleManager, Chr user);
}