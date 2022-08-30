using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyMassive;
    private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private int maxEnemiesCount = 3;
    [SerializeField] private float speedCoef = 0;

    void Start()
    {
        ResetLevel();
        StartLevel();
    }

    void Update()
    {
        if (speedCoef == 0) return;

        foreach (GameObject enemy in enemies)
        {
            enemy.transform.position -= new Vector3(0, 0, 10 * Time.deltaTime);
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

    private void CreateEnemies()
    {
        Vector3 pos = new Vector3(0, 3, 140);
        if (enemies.Count > 0) 
        {
  
            float deviation = 2.9f - 2.9f * Random.Range(0, 3);
            pos = new Vector3(0, enemies[enemies.Count - 1].transform.position.y, enemies[enemies.Count - 1].transform.position.z) + new Vector3(deviation, 0, 20); 
        }
        GameObject go = Instantiate(enemyMassive[Random.Range(0, 2)], pos, Quaternion.identity);
        go.transform.SetParent(transform);
        enemies.Add(go);
    }

}
