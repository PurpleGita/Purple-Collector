using UnityEngine;

public class EnemyRunTime
{
    Enemy baseData;
    public string enemyName;
    public float hp;
    public float currentHP;
    public bool alive;
    public float currentBlock;
    public int numberInRow;
    public Sprite enemyImage;
    //List<EnemyAttakcsRuntime>


    public EnemyRunTime(Enemy data) 
    {
        baseData = data;
        enemyName = data.enemyName;
        hp = data.hp;
        currentHP = data.currentHP;
        alive = data.alive;
        currentBlock = data.currentBlock;
        numberInRow = data.numberInRow;
        enemyImage = data.enemyImage;

    }
    
}
