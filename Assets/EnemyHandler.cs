using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public List<Enemy> baseEnemiesInCombat;

    public List<EnemyRunTime> modifiedEnemies;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Enemy enemy in baseEnemiesInCombat)
        {
            modifiedEnemies.Add(new EnemyRunTime(enemy));

        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
