using UnityEngine;

public class viewDeckHandler : MonoBehaviour
{
    public GameObject Obj_slectedEffekt;
    public GameObject Obj_viewDeck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Selected() 
    { 
        Obj_slectedEffekt.SetActive(true);
    }

    public void Deselect() 
    {
        Obj_slectedEffekt.SetActive(false);
    }


    public void ViewDeck() 
    {
        if (Obj_slectedEffekt.activeSelf)
        {
            Debug.Log("Viewing Deck");
            Obj_slectedEffekt.SetActive(false);
            Obj_viewDeck.SetActive(true);
        }


    }
}
