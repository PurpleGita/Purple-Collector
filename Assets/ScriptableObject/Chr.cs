using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chr", menuName = "Scriptable Objects/Chr")]
public class Chr : ScriptableObject
{

    public string characterName;
    public float HP;
    public float ATK;
    public float DEF;
    public float MAG;
    public int numberInRow;
    public List<CardData> startingDeck;
    public List<CardData> extraCards;
    public Sprite ultImage;
    public CardData ult;
    public List<Passive> passives;
    //AnimationHandler
}
