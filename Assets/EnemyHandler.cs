using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHandler : MonoBehaviour
{
    public List<Enemy> baseEnemiesInCombat;

    public List<EnemyRunTime> modifiedEnemies = new();

    public List<GameObject> OBJ_enmemies = new();

    public List<GameObject> OBJ_enemiesCanvas = new();

    int maxEnemies = 4;

    int currentEnemies = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Enemy enemy in baseEnemiesInCombat)
        {
            modifiedEnemies.Add(new EnemyRunTime(enemy));
        }

        UpdateEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateEnemies() 
    {
        foreach (EnemyRunTime enemy in modifiedEnemies)
        {
            SummonEnemy(enemy);
        }
    }

    public void SummonEnemy(EnemyRunTime enemytoSummon) 
    { 
        if (currentEnemies <= maxEnemies) 
        {
            
            //summon enemy
            currentEnemies++;
            OBJ_enmemies[currentEnemies - 1].SetActive(true);
            OBJ_enemiesCanvas[currentEnemies - 1].SetActive(true);
            OBJ_enmemies[currentEnemies-1].GetComponent<SpriteRenderer>().sprite = enemytoSummon.enemyImage;
            Slider slider = OBJ_enemiesCanvas[currentEnemies - 1].GetComponent<Slider>(); 
            slider.maxValue = enemytoSummon.hp;
            enemytoSummon.currentHP = enemytoSummon.hp;
            slider.value = enemytoSummon.currentHP;
            enemytoSummon.currentBlock = 0;
            GainEnemyBlock(currentEnemies - 1, 0);
            OBJ_enemiesCanvas[currentEnemies - 1].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = enemytoSummon.currentHP + "/" + enemytoSummon.hp;
        }


    }

    public void GainEnemyBlock(int postionOfEnemy,float blockGain) 
    { 
        if(currentEnemies-1 <= postionOfEnemy) 
        { 
            postionOfEnemy = currentEnemies-1;
        }

        modifiedEnemies[postionOfEnemy].currentBlock += blockGain;
        OBJ_enemiesCanvas[postionOfEnemy].transform.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text =  "" + modifiedEnemies[postionOfEnemy].currentBlock;
        
        if(modifiedEnemies[postionOfEnemy].currentBlock <= 0) 
        {
            modifiedEnemies[postionOfEnemy].currentBlock = 0;
            OBJ_enemiesCanvas[postionOfEnemy].transform.GetChild(4).gameObject.SetActive(false);
        }
    }


}
