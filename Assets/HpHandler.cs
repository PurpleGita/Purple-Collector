using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpHandler : MonoBehaviour
{
    public int currentBlock;
    public int currentHP;
    public int maxHP;
    
    [SerializeField]
    Slider hpSlider;
    
    [SerializeField]
    TextMeshProUGUI textHP;

    [SerializeField]
    GameObject obj_Block;

    [SerializeField]
    TextMeshProUGUI textBlock;


    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void SetupMaxHP(List<ChrRuntime> chrs) 
    {
        maxHP = 0;
        foreach (ChrRuntime chr in chrs) 
        { 
            maxHP += Mathf.RoundToInt(chr.HP);
            Debug.Log(chr.HP);
        }

        hpSlider.maxValue = maxHP;
        hpSlider.value = maxHP;
        textHP.text = maxHP + "/" + maxHP;
        currentHP = maxHP;
        currentBlock = 0;
        obj_Block.SetActive(false);
    }

    public void GainBlock(int blockToGain) 
    {
        currentBlock += blockToGain;
        if (currentBlock > 999) { currentBlock = 999; }
        obj_Block.SetActive(true);
        textBlock.text = currentBlock.ToString();
       
    }


    public void GainHP(int hpToGain) 
    { 
        currentHP += hpToGain;
        if (currentHP > maxHP) 
        { 
            currentHP = maxHP;
        }
        textHP.text = currentHP + "/" + maxHP;

    }

    public void TakeDamage(int amountToLose) 
    {
        if(amountToLose > currentBlock) 
        { 
            amountToLose -= currentBlock;
            currentHP -= amountToLose;
            currentBlock = 0;
            textBlock.text = currentBlock.ToString();
            obj_Block.SetActive(false);
            hpSlider.value = currentHP;
            textHP.text = currentHP.ToString();
        }
        else { 
            currentBlock -= amountToLose;
            textBlock.text = currentBlock.ToString();
        }
    }

}
