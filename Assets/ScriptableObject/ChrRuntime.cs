using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChrRuntime
{
    public Chr baseData;

    public string characterName;
    public float HP;
    public float currentHP;
    public float ATK;
    public float DEF;
    public float MAG;
    public int numberInRow;
    public List<CardRuntime> extraCards;
    public List<CardRuntime> startingDeck = new();
    public Sprite ultImage;
    public CardRuntime ult;
    public List<Passive> passives;

    public ChrRuntime(Chr data)
    {
        baseData = data;

        characterName = baseData.characterName;
        currentHP = baseData.HP;
        HP = baseData.HP;
        ATK = baseData.ATK;
        DEF = baseData.DEF;
        MAG = baseData.MAG;
        numberInRow = baseData.numberInRow;
        ultImage = baseData.ultImage;
        ult = new CardRuntime(baseData.ult);

        extraCards = new List<CardRuntime>();
        foreach (var card in baseData.extraCards)
        {
            startingDeck.Add(new CardRuntime(card));
        }

        startingDeck = new List<CardRuntime>();
        foreach (var card in baseData.startingDeck)
        {
            startingDeck.Add(new CardRuntime(card));
        }

        passives = new List<Passive>(baseData.passives);

    }
}