using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleManager : MonoBehaviour
{


    //a template object to copy when adding cards
    [SerializeField]
    GameObject Obj_CardTemplate;

    //and object that contains the cards in hand as child objects
    [SerializeField]
    GameObject Obj_CardsinHand;

    //used to show the players hand size and current cards in hand
    [SerializeField]
    GameObject Obj_HandSize;

    //A list of the enemy objects 
    [SerializeField]
    List<GameObject> Obj_Enemies;

    //a list of objects keeping track of enemy hp and block
    [SerializeField]
    List<GameObject> Obj_EnemiesHP;

    //a list of chacters in the combat
    [SerializeField]
    List<GameObject> Obj_Chrs;

    //a object containing the players HP (all charcters share hp)
    [SerializeField]
    GameObject Obj_ChrsHP;

    //an object used to show the amount of mana the player has left
    [SerializeField]
    GameObject Obj_Mana;

    //a list of objects showing how much energy the diffrent characters have.
    [SerializeField]
    GameObject Obj_Energy;

    [SerializeField]
    List<Chr> baseChrsInCombat;

    [SerializeField]
    public List<ChrRuntime> modifiedChrs = new();

    [SerializeField]
    List<CardRuntime> cardsInHand;

    [SerializeField]
    List<CardRuntime> cardsInDeck;

    [SerializeField]
    List<CardRuntime> cardsInDiscard;

    [SerializeField]
    List<CardRuntime> cardsInExile;

    [SerializeField]
    InputHandler inputHander;

    [SerializeField]
    viewDeckHandler viewDeckHandler;

    //[SerializeField]
    //List<Enemy> enemiesInBattle;

    int maxMana;

    int HP;

    int maxHP;

    int currentBlock;

    int absoluteMaxHandSize = 15;

    public List<bool> hasUsedUltThisTurn;

    int maxCardsInHand = 10;

    bool selectingCard = false;

    public int highlightedCard;

    ManaHandler manaHandler;

    HandSizeHandler handSizeHandler;

    EnergyHandler energyHandler;

    List<Passive> activePassives = new();

    bool viewBuffs = false;

    bool viewUlts = false;

    void Start()
    {
        //temp things that will be loaded from elsewhere later:
        maxHP = 100;
        HP = maxHP;
        List<int> maxEnergyTemp = new List<int>();


        manaHandler = Obj_Mana.GetComponent<ManaHandler>();
        handSizeHandler = Obj_HandSize.GetComponent<HandSizeHandler>();
        energyHandler = Obj_Energy.GetComponent<EnergyHandler>();
        

        StartBattle();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("b")) { DrawCard(1); }
        

            if (inputHander.CheckDownPressed()) 
        {
            Debug.Log("DownPressed");

            //select card if currently not doing anything
            if(viewBuffs == false && viewUlts == false && selectingCard == false) 
            {
                Debug.Log("selecting Card");
                selectingCard = true;
                highlightedCard = 0;
                Obj_CardsinHand.transform.GetChild(0).GetComponent<CardUI>().SelectCard();
            }

            //Select the first card in hand if currently viewing buffs
            if (selectingCard == false && cardsInHand.Count > 0 && viewBuffs == true)
            {
                Debug.Log("selecting Card");
                selectingCard = true;
                highlightedCard = 0;
                Obj_CardsinHand.transform.GetChild(0).GetComponent<CardUI>().SelectCard();
                viewBuffs = false;

            }

            //view buffs if currently viewing ults
            if (viewUlts)
            {
                Debug.Log("viewing buffs");
                viewBuffs = true;
                viewUlts = false;
                energyHandler.DeSelectUlt();
            }

        }
        if (inputHander.CheckRightPressed())
        {
            Debug.Log("RightPressed");
            if (selectingCard == true && cardsInHand.Count > 0)
            {
                    highlightedCard = Mathf.Min(highlightedCard + 1, cardsInHand.Count - 1);
                    UpdateHandLayout();
                    DeselectAllCards();
                    Obj_CardsinHand.transform.GetChild(highlightedCard).GetComponent<CardUI>().SelectCard();
                    viewDeckHandler.Deselect();


            }
        }

        if (inputHander.CheckLeftPressed())
        {
            Debug.Log("LeftPressed");
            if (selectingCard == true && cardsInHand.Count > 0)
            {
                if (highlightedCard <= 0) { 
                    Debug.Log("viewSomething");
                    DeselectAllCards();
                    viewDeckHandler.Selected();
                    UpdateHandLayout();
                    highlightedCard = -1;
                }
                else
                {
                    highlightedCard = Mathf.Max(highlightedCard - 1, 0);
                    UpdateHandLayout();
                    DeselectAllCards();
                    Obj_CardsinHand.transform.GetChild(highlightedCard).GetComponent<CardUI>().SelectCard();
                }



            }
        }

        if (inputHander.CheckUpPressed())
        {


            Debug.Log("UpPressed");
            if (viewBuffs) 
            {
                Debug.Log("viewing Ult");
                viewBuffs = false;
                viewUlts = true;
                energyHandler.ViewUlts(this);
            }
            
            if (selectingCard)
            {
                Debug.Log("viewing buffs");
                DeselectAllCards();
                selectingCard = false;
                UpdateHandLayout();
                viewBuffs = true;
                viewDeckHandler.Deselect();

            }
        }

        if (inputHander.CheckAPressed()) 
        {
            Debug.Log("AButtonPressed");
            if (selectingCard && highlightedCard >= 0)
            {
                CardUI cardUI = Obj_CardsinHand.transform.GetChild(highlightedCard).GetComponent<CardUI>();
                PlayCardFromHand(cardUI);
            }
            else if (selectingCard && highlightedCard == -1) { viewDeckHandler.ViewDeck(); } 
            
            if(viewUlts) { energyHandler.AttemptToUseUlt(modifiedChrs,this); }


        }

    }

    public void StartBattle()
    {
        cardsInHand = new List<CardRuntime>();
        cardsInDeck = new List<CardRuntime>();
        cardsInDiscard = new List<CardRuntime>();
        cardsInExile = new List<CardRuntime>();
        modifiedChrs = new List<ChrRuntime>();
        activePassives = new List<Passive>();
        hasUsedUltThisTurn = new List<bool>();
        foreach (Chr chr in baseChrsInCombat)
        {
            modifiedChrs.Add(new ChrRuntime(chr));
        }



        
        foreach (ChrRuntime chr in modifiedChrs)
        {
            foreach (CardRuntime card in chr.startingDeck)
            {
                Debug.Log(cardsInDeck.Count);
                cardsInDeck.Add(card);
                foreach(Passive chrPassive in chr.passives) 
                {
                    activePassives.Add(chrPassive);
                }
                
                Debug.Log(card.cardName);
            }
        }

        foreach (Passive startPassive in activePassives)
        {
            startPassive.OnBattleStart(this, startPassive.ownerChr);
        }


        hasUsedUltThisTurn.Add(false);
        hasUsedUltThisTurn.Add(false);
        hasUsedUltThisTurn.Add(false);
        hasUsedUltThisTurn.Add(false);

        DrawCard(5);
        manaHandler.startMana = 3;
        manaHandler.mana = manaHandler.startMana;
        manaHandler.UpdateManaText();
        handSizeHandler.maxHandSize = 10;
        maxCardsInHand = 10;
        if (maxCardsInHand > absoluteMaxHandSize) { maxCardsInHand = absoluteMaxHandSize; }
        handSizeHandler.UpdateCardsInHandCount(cardsInHand.Count);
        energyHandler.SetUltImage(modifiedChrs[0].ultImage,0);
        energyHandler.SetMaxEnergyFirstTime(modifiedChrs);

    }

    //Called from cards
    public void DealDamageToEnemies(int amount,Elements element,int target)
    {
        Debug.Log("Dealt " + amount + " " + element.ToString() + " to target " + target);
    }

    //Called from cards
    public void GiveEnergy(List<int> targetChrs, int amount) 
    {
        energyHandler.GainEnergy(targetChrs, amount);
        energyHandler.UpdateEnergyVisual();
    }

    //Called from cards
    public void Gainblock(int amount)
    {
        Debug.Log("Gained " + amount + " blcok");
        currentBlock += amount;
    }

    public void DrawCard(int amount)
    {
         StartCoroutine(DrawCardRoutine(amount));
    }

    IEnumerator DrawCardRoutine(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (cardsInHand.Count+1 <= maxCardsInHand)
            {
                if (cardsInDeck.Count > 0)
                {
                    var drawnCard = cardsInDeck[0];
                    cardsInDeck.RemoveAt(0);
                    cardsInHand.Add(drawnCard);

                    SpawnCardVisual(drawnCard);

                    yield return new WaitForSeconds(0.2f); // small delay

                    UpdateHandLayout();
                }
            }
        }

        yield return new WaitForSeconds(0.01f);

    }

    IEnumerator MoveCardToHand(GameObject cardObj, int index)
    {
        RectTransform rect = cardObj.GetComponent<RectTransform>();

        Vector2 startPos = new Vector2(-1000, -100);

        float spacing = 305f;
        Vector2 endPos = new Vector2(index * spacing, -415);

        float duration = 0.2f;
        float time = 0;

        rect.anchoredPosition = startPos;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            rect.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        rect.anchoredPosition = endPos;
    }

    void SpawnCardVisual(CardRuntime cardData)
    {
        GameObject cardObj = Instantiate(Obj_CardTemplate, Obj_CardsinHand.transform);
        cardObj.SetActive(true);
        

        cardObj.GetComponent<CardUI>().Setup(cardData);

        int index = Obj_CardsinHand.transform.childCount - 1;

        StartCoroutine(MoveCardToHand(cardObj, index));
    }

    void UpdateHandLayout()
    {
        float handY = -415f;

        int cardCount = Obj_CardsinHand.transform.childCount;

        float baseSpacing = 300f;
        float spacing = baseSpacing;

        if (cardCount > 5)
        {
            spacing = baseSpacing - (cardCount * 13);
        }
        float totalWidth = (cardCount - 1) * spacing;

        float maxRotation = 15f;
        float curveStrength = 0.0002f;

        for (int i = 0; i < cardCount; i++)
        {
            RectTransform rect = Obj_CardsinHand.transform.GetChild(i).GetComponent<RectTransform>();

            float xPos = i * spacing - totalWidth / 2f;

            float t = cardCount == 1 ? 0.5f : (float)i / (cardCount - 1);
            float rotation = Mathf.Lerp(maxRotation, -maxRotation, t);

            float yOffset = -Mathf.Abs(xPos) * Mathf.Abs(xPos) * curveStrength;

            Vector2 targetPos = new Vector2(xPos, handY + yOffset);

            var cardUI = rect.GetComponent<CardUI>();
            cardUI.SetBasePosition(targetPos);
            cardUI.MoveTo(targetPos, rotation);

        }

        handSizeHandler.UpdateCardsInHandCount(cardsInHand.Count);

    }

    //Called from cards
    public int GetIntFromChr(ChrRuntime chrToGet) 
    {
        for (int i = 0; i < 4; i++)
        {
            if(chrToGet.characterName == modifiedChrs[i].characterName) { return modifiedChrs[i].numberInRow; }
        }

        return 0;
    }

    public int GetIntFromChr(string chrToGet)
    {
        for (int i = 0; i < 4; i++)
        {
            if (chrToGet == modifiedChrs[i].characterName) { return modifiedChrs[i].numberInRow; }
        }

        return 0;
    }

    public void ShuffleDeck()
    {

    }

    public void PlayCardFromHand(CardUI cardUI) 
    {
        CardRuntime card = cardUI.cardData;
        if (card.manaCost > manaHandler.mana) return;
        manaHandler.mana = manaHandler.mana - card.manaCost;
        ActuallyPlayCardEffects(card);
        cardsInHand.Remove(card);
        cardUI.PlayedCard();
        cardsInDiscard.Add(card);
        selectingCard = false;
        AfterCardPlayed();
        manaHandler.UpdateManaText();
    }

    public void ActuallyPlayCardEffects(CardRuntime cardToPlayer) 
    {
        List<CardEffect> effects = cardToPlayer.effects;
        foreach (CardEffect effect in effects)
        {
            effect.Execute(this, cardToPlayer.chr);
        }
    }

    void AfterCardPlayed()
    {
        // Stay in selection mode!
        selectingCard = true;

        // Clamp index so it doesn't go out of bounds
        int cardCount = Obj_CardsinHand.transform.childCount;

        if (cardCount == 0)
        {
            selectingCard = false;
            highlightedCard = -1;
            return;
        }

        // If last card was removed, step back
        if (highlightedCard >= cardCount)
        {
            highlightedCard = highlightedCard -1;
        }

        highlightedCard = -1;

        DeselectAllCards();

        StartCoroutine(DelayedLayout());
    }

    IEnumerator DelayedLayout()
    {
        yield return null; // wait 1 frame

        UpdateHandLayout();
    }

    void DeselectAllCards()
    {
        for (int i = 0; i < Obj_CardsinHand.transform.childCount; i++)
        {
            Obj_CardsinHand.transform.GetChild(i).GetComponent<CardUI>().DeselectCard();
        }
    }

}
