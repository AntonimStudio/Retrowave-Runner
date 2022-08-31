using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] public GameObject RoadPrefab;
    private List<GameObject> roads = new List<GameObject>();
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI recordText;
    [SerializeField] private float minSpeed = 10;
    [SerializeField] private float maxSpeed = 100;
    private float distance = 0;
    private float time = 0;
    public float speed = 0;
    private int record = 0;
    [SerializeField] private int maxRoadCount = 5;

    void Start()
    {
        ResetLevel();
        StartLevel();
        StartCoroutine(StartTimer());
        //record = PlayerPrefs.GetInt("Record");
    }

    void FixedUpdate()
    {
        if (speed == 0) return;

        foreach (GameObject road in roads)
        {
            road.transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
        }

        if (roads[0].transform.position.z < -15)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
            CreateNextRoad();
        }

        if (speed < maxSpeed / 3) { speed = speed + (time * 0.00005f); }
        else if (speed > maxSpeed / 3) { speed = speed + (time * 0.000001f); }
        else if (speed > maxSpeed / 2) { speed = speed + (time * 0.0000001f); }

        if (distance > record)
        {
            record = (int)Mathf.Round(distance);
            recordText.text = record.ToString();
            PlayerPrefs.SetInt("Record", record);
            PlayerPrefs.Save();
        }
    }

    private IEnumerator StartTimer()
    {
        while (speed > 0 && speed < maxSpeed)
        {
            time += Time.deltaTime;
            distance = time * speed / 4;
            distanceText.text = Mathf.Round(distance).ToString();
            yield return null;
        }
    }

    private void CreateNextRoad()
    {
        Vector3 pos = Vector3.zero;
        if (roads.Count > 0) { pos = roads[roads.Count - 1].transform.position + new Vector3(0, 0, 15); }
        GameObject go = Instantiate(RoadPrefab, pos, Quaternion.identity);
        go.transform.SetParent(transform);
        roads.Add(go);
    }

    public void StartLevel()
    {
        speed = minSpeed;
    }

    public void ResetLevel()
    {
        speed = 0;
        while (roads.Count > 0)
        {
            Destroy(roads[0]);
            roads.RemoveAt(0);
        }
        for (int i = 0; i < maxRoadCount; i++)
        {
            CreateNextRoad();
        }
    }
}


