using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class viewDeckHandler : MonoBehaviour
{
    public GameObject Obj_slectedEffekt;
    public GameObject Obj_viewDeck;
    public GameObject Obj_content;
    public ScrollRect scrollRect;
    [SerializeField]
    GameObject Obj_CardUI;
    bool viewingDeck = false;
    [SerializeField]
    InputHandler inputHander;
    [SerializeField]
    BattleManager bm;
    int viewingMenu;
    public float scrollSpeed = 10f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (viewingDeck) 
        {
            if (inputHander.CheckAPressed()) 
            {
                Obj_viewDeck.SetActive(false);
                viewingDeck = false;
                bm.menuOpen = false;
                Debug.Log("Not viewing deck");

            }

            if (inputHander.CheckLeftPressed())
            {
                //minus the view by 1 or well going to the left
                viewingMenu = viewingMenu - 1;
                
                //if beyond limit set back to limit
                if (viewingMenu < 0) { viewingMenu = 0; }

                //if 0 = view cards in deck
                if(viewingMenu == 0) 
                {
                    ChangeView(bm.cardsInDeck);
                }
                //if viewingMenu = 1 view Discard pile
                else if(viewingMenu == 1) 
                {
                    ChangeView(bm.cardsInDiscard);
                }



            }

            if (inputHander.CheckRightPressed())
            {
                viewingMenu = viewingMenu + 1;

                //if beyond limit set back to limit
                if (viewingMenu > 2) { viewingMenu = 2; }

                //if 0 = view cards in deck
                if (viewingMenu == 2)
                {
                    ChangeView(bm.cardsInExile);
                }
                //if viewingMenu = 1 view Discard pile
                else if (viewingMenu == 1)
                {
                    ChangeView(bm.cardsInDiscard);
                }
            }


            //scrollable on keyboard:
            float input = Input.GetAxis("Vertical"); // W/S or Up/Down arrows
            if (Mathf.Abs(input) > 0.01f)
            {
                float newPos = scrollRect.verticalNormalizedPosition + input * scrollSpeed * Time.deltaTime;
                scrollRect.verticalNormalizedPosition = Mathf.Clamp01(newPos);
            }


        }
    }

    public void Selected() 
    { 
        Obj_slectedEffekt.SetActive(true);
        viewingMenu = 0;
    }

    public void Deselect() 
    {
        Obj_slectedEffekt.SetActive(false);
        viewingMenu = 0;
    }


    public void ViewDeck() 
    {
        if (Obj_slectedEffekt.activeSelf)
        {
            Debug.Log("Viewing Deck");
            Obj_slectedEffekt.SetActive(false);
            Obj_viewDeck.SetActive(true);
            bm.menuOpen = true;
            List<CardRuntime> cardsInDeck = bm.cardsInDeck;
            ChangeView(cardsInDeck);
            StartCoroutine(WaitToSetValue());
        }


    }

    public void StopViewingDeck() 
    { 
    
    }

    public void ChangeView(List<CardRuntime> cardsToShow) 
    {
    
        foreach (Transform child in Obj_content.transform) 
        { 
            Destroy(child.gameObject);
        }

        foreach (CardRuntime card in cardsToShow) 
        {
            //creating card object
            GameObject cardObj = Instantiate(Obj_CardUI, Obj_content.transform);
            cardObj.SetActive(true);
            cardObj.GetComponent<CardUI>().Setup(card);

        }
    }

    IEnumerator WaitToSetValue() 
    {
        float time = 0;
        float duration = 0.2f;
        while (time < duration)
        {
            time += Time.deltaTime;

            yield return null;
        }

        viewingDeck = true;
    }


}
