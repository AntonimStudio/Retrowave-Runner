using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyMassive;
    private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private int maxEnemiesCount = 3;
    [SerializeField] private float speedCoef = 0;
    [SerializeField] private RoadGenerator rg;
    private int[] repeatList = new int[10];
    private int[] probabilityList = new int[100];
    private int prev = 0;
    private int prevprev = 0;
    public int startDifficulty = 10;

    private void Awake()
    {
        for (int i = 0; i < 100; i++)
        {
            probabilityList[i] = 1;
        }
        FullProbabilityList(10);
    }
    void Start()
    {
        ResetLevel();
        StartLevel();
    }
    /*
     * Возможные позиции:
     * 1)     001, 010, 100
     Перевод    1,   2,   3
     * 2)     011, 101, 110
     Перевод    4,   5,   6
    */
    public void FullProbabilityList(int difficulty) /// Сложность - вероятность выпадения двойных блоков, от 10% на каждый вариант (011, 101, 110) до 20%
    {
        for (int i = 0; i < difficulty; i++)
        {
            probabilityList[i] = 4; // 011
            probabilityList[i + difficulty] = 5; // 101
            probabilityList[i + difficulty * 2] = 6; // 110
        }

        int x = (100 - difficulty * 3) / 3;
        for (int i = 3 * difficulty; i < 100 - x * 2; i++)
        {
            probabilityList[i] = 1; // 001
            probabilityList[i + x] = 2; // 010
            probabilityList[i + x * 2] = 3; // 100
        }
    }

    void FixedUpdate()
    {
        if (speedCoef == 0) return;

        foreach (GameObject enemy in enemies)
        {
            enemy.transform.position -= new Vector3(0, 0, rg.speed * Time.deltaTime * speedCoef);
        }

        if (enemies[0].transform.position.z < -15)
        {
            Destroy(enemies[0]);
            enemies.RemoveAt(0);
            CreateEnemies();
        }
    }

    public void ResetLevel()
    {
        speedCoef = 0;
        while (enemies.Count > 0)
        {
            Destroy(enemies[0]);
            enemies.RemoveAt(0);
        }
        for (int i = 0; i < maxEnemiesCount; i++)
        {
            CreateEnemies();
        }
    }

    public void StartLevel()
    {
        speedCoef = 1;
    }
    /*
    private void CreateEnemies()
    {


        Vector3 pos = new Vector3(0, 3, 140);
        Vector3 pos1 = new Vector3(0, 3, 140);

        float deviationExtra = -100f;
        if (enemies.Count > 0) 
        {       
            float deviation = 0f;
            int n = probabilityList[Random.Range(0, 100)];
            switch (n)
            {     
                case 1:
                    deviation = -2.9f;
                    deviationExtra = -100f;
                    break;
                case 2:
                    deviation = 0f;
                    deviationExtra = -100f;
                    break;
                case 3:
                    deviation = 2.9f;
                    deviationExtra = -100f;
                    break;
                case 4:
                    deviation = -2.9f;
                    deviationExtra = 0f;
                    break;
                case 5:
                    deviation = -2.9f;
                    deviationExtra = 2.9f;
                    break;
                case 6:
                    deviation = 0f;
                    deviationExtra = 2.9f;
                    break;
                default:
                    break;
            }
            int distance = Random.Range(15, 35);
            pos = new Vector3(0, enemies[enemies.Count - 1].transform.position.y, enemies[enemies.Count - 1].transform.position.z) + new Vector3(deviation, 0, distance);
            if (deviationExtra != -100f)
            {
                pos1 = new Vector3(0, pos.y, pos.z + deviationExtra); //(deviation, 0, Random.Range(15, 35)); 
            }
        }
        
        GameObject go = Instantiate(enemyMassive[Random.Range(0, 2)], pos, Quaternion.identity);
        if (deviationExtra != -100f)
        {
            GameObject go1 = Instantiate(enemyMassive[Random.Range(0, 2)], pos1, Quaternion.identity, go.transform);
        }
        go.transform.SetParent(transform);
        enemies.Add(go);
    }*/
    private void CreateEnemies()
    {
        Vector3 pos = new Vector3(0, 3, 100);
        Vector3 pos1 = new Vector3(0, 3, 100);
        float deviationExtra = -100f;
        
        if (enemies.Count > 0)
        {
            float deviation = 0f;
            int n = probabilityList[Random.Range(0, 100)];

            while (prevprev == prev && n == prev)
            {
                n = probabilityList[Random.Range(0, 100)];
            }
            prevprev = prev;
            prev = n;
            if (n == 1) { deviation = -2.9f; deviationExtra = -100f; }
            else if (n == 2) { deviation = 0f; deviationExtra = -100f; }
            else if (n == 3) { deviation = 2.9f; deviationExtra = -100f; }
            else if (n == 4) { deviation = -2.9f; deviationExtra = 0f; }
            else if (n == 5) { deviation = -2.9f; deviationExtra = 2.9f; }
            else if (n == 6) { deviation = 0f; deviationExtra = 2.9f; }
            float distance = Random.Range(25, 40);
            pos = new Vector3(0, enemies[enemies.Count - 1].transform.position.y, enemies[enemies.Count - 1].transform.position.z) + new Vector3(deviation, 0, distance);
            if (deviationExtra != -100f && deviationExtra != deviation)
            {
                pos1 = new Vector3(0, enemies[enemies.Count - 1].transform.position.y, enemies[enemies.Count - 1].transform.position.z + distance) + new Vector3(deviationExtra, 0, 0);
            }
        }
        GameObject go = Instantiate(enemyMassive[Random.Range(0, 2)], pos, Quaternion.identity);
        if (deviationExtra != -100f)
        {
            GameObject go1 = Instantiate(enemyMassive[Random.Range(0, 2)], pos1, Quaternion.identity, go.transform);
        }
        go.transform.SetParent(transform);
        enemies.Add(go);
    }
}
