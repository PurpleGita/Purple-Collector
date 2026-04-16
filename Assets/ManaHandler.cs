using System.Collections;
using TMPro;
using UnityEngine;

public class ManaHandler : MonoBehaviour
{
    public int mana;
    public int startMana;
    public GameObject manaEffekt;
    [SerializeField]
    GameObject viewUltOBJ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateManaText() 
    { 
        TextMeshProUGUI textMana = this.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        textMana.text = mana + "/" + startMana;
        manaEffekt.SetActive(true);
        StartCoroutine(WaitToDelete());
    }

    IEnumerator WaitToDelete() 
    {
        float time = 0;
        float duration = 0.2f;
        while (time < duration)
        {
            time += Time.deltaTime;

            yield return null;
        }

        manaEffekt.SetActive(false);


    }
}
