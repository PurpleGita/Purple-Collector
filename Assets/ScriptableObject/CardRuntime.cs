using System.Collections.Generic;
using UnityEngine;

public class CardRuntime
{
    public CardData basedata;
    public string cardName;
    public int manaCost;
    public string description;
    public Chr chr;
    public Sprite image;
    public bool signiture;
    public bool exile;

    public List<CardEffect> effects;

    public CardRuntime(CardData data)
    {
        basedata = data;

        manaCost = basedata.manaCost;
        cardName = basedata.cardName;
        manaCost = basedata.manaCost;
        description = basedata.description;
        chr = basedata.chr;
        image = basedata.image;
        signiture = basedata.signiture;
        exile = basedata.exile;

        effects = new List<CardEffect>(basedata.effects); 
    }
}
