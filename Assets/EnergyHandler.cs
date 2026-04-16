using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnergyHandler : MonoBehaviour
{
    public List<int> chrsEnergy = new();
    public List<int> chrsMaxEnergy = new();
    public List<int> timesUltet = new();
    public int ultViewing = 0;
    public GameObject obj_ult;
    public Color readyUltColor;
    public Color notReadyUltColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        timesUltet.Add(0);
        timesUltet.Add(0);
        timesUltet.Add(0);
        timesUltet.Add(0);
        List<int> zeroEnergy = new List<int>() { 0 };
        GainEnergy(zeroEnergy, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GainEnergy(List<int> chrsToGain,int amount) 
    {
        for (int i = 0; i < chrsToGain.Count; i++)
        {
            int targetChrToGiveEnergy = chrsToGain[i];
            chrsEnergy[targetChrToGiveEnergy] += amount;
            if(chrsEnergy[targetChrToGiveEnergy] >= chrsMaxEnergy[targetChrToGiveEnergy]) 
            { 
                chrsEnergy[targetChrToGiveEnergy] = chrsMaxEnergy[targetChrToGiveEnergy];
                this.transform.GetChild(i).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = readyUltColor;
            }
            else { this.transform.GetChild(i).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = notReadyUltColor; }

            GameObject effeckt = this.transform.GetChild(i).GetChild(0).GetChild(1).GetChild(0).GetChild(0).gameObject;
            effeckt.SetActive(true);
            StartCoroutine(WaitToDelete(effeckt));



            Debug.Log("Energy: " + chrsEnergy[targetChrToGiveEnergy]);
        }
    }

    public void SetMaxEnergyFirstTime(List<ChrRuntime> modifiedChrs) 
    {
        // Ensure list has correct size
        for (int i = 0; i < modifiedChrs.Count; i++)
        {
            chrsMaxEnergy.Add(0);
        }

        foreach (ChrRuntime chr in modifiedChrs) 
        {
            Debug.Log("setting max energi: " + chr.characterName + " amount: " + chr.ult.manaCost);
            chrsMaxEnergy[chr.numberInRow] = chr.ult.manaCost;
            chrsEnergy.Add(0);
        }
        


        UpdateEnergyVisual();
    }

    public void UltUsed() 
    { 
    
    }

    public void UpdateEnergyVisual()
    {
        for (int i = 0; i < chrsEnergy.Count; i++)
        {
            Slider slider = this.transform.GetChild(i).GetChild(0).GetComponent<Slider>();
            slider.value = chrsEnergy[i];
            slider.maxValue = chrsMaxEnergy[i];
            
        }
    }

    public void SetUltImage(Sprite spriteToSet, int chrIndex) 
    {
        GameObject imageObj = this.transform.GetChild(chrIndex).GetChild(1).gameObject;
        imageObj.GetComponent<Image>().sprite = spriteToSet; 
    }

    IEnumerator WaitToDelete(GameObject deleteObj)
    {
        float time = 0;
        float duration = 0.2f;
        while (time < duration)
        {
            time += Time.deltaTime;

            yield return null;
        }

        deleteObj.SetActive(false);


    }

    public void ViewUlts(BattleManager bm) 
    {
        ultViewing = 0;
        obj_ult.SetActive(true);
        TextMeshProUGUI ultDescText = obj_ult.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        ultDescText.text = bm.modifiedChrs[ultViewing].ult.description;

        TextMeshProUGUI ultNameText = obj_ult.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        ultNameText.text = bm.modifiedChrs[ultViewing].ult.cardName;

    }

    public void DeSelectUlt() 
    {
        obj_ult.SetActive(false);
    }

    public void AttemptToUseUlt(List<ChrRuntime> modifiedChrs,BattleManager bm) 
    {
        if (chrsEnergy[ultViewing] >= chrsMaxEnergy[ultViewing]) {
            chrsEnergy[ultViewing] = 0;
            chrsMaxEnergy[ultViewing] += 10;
            bm.modifiedChrs[ultViewing].ult.manaCost += 10;
            timesUltet[ultViewing] += 1;
            bm.hasUsedUltThisTurn[ultViewing] = true;
            CardRuntime cardToPlay = modifiedChrs[ultViewing].ult;
            bm.ActuallyPlayCardEffects(cardToPlay);
            List<int> zeroEnergy = new List<int>() {0};
            GainEnergy(zeroEnergy,0);

            UpdateEnergyVisual();


        }
    }

}
