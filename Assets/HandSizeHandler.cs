using TMPro;
using UnityEngine;

public class HandSizeHandler : MonoBehaviour
{

    public int maxHandSize;
    public int absoluteMaxHandSize = 15;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCardsInHandCount(int count) 
    {
        TextMeshProUGUI textHand = this.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        textHand.text = count + "/" + maxHandSize;
    }
}
