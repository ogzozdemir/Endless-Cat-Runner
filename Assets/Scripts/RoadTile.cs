using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoadTile : MonoBehaviour
{
    private void OnTriggerExit(Collider other) => Invoke("ChangePosition", 1f); 
    private void ChangePosition()
    {
        RoadSpawner.instance.ChangePosition(gameObject);
        ActivateObstacle();
    }

    [SerializeField] private GameObject obstaclePrefab;
    private GameObject obstacle_left;
    private GameObject obstacle_middle;
    private GameObject obstacle_right;
    
    [SerializeField] private GameObject donutPrefab;
    [SerializeField] private GameObject burgerPrefab;
    private GameObject burger;
    private List<GameObject> foodList = new List<GameObject>();

    private void Start()
    {
        burger = Instantiate(burgerPrefab);
        burger.SetActive(false);
        
        for (int i = 0; i < 3; i++)
        {
            GameObject donut = Instantiate(donutPrefab);
            foodList.Add(donut);
            
            donut.SetActive(false);
        }
        
        obstacle_left = Instantiate(obstaclePrefab);
        obstacle_middle = Instantiate(obstaclePrefab);
        obstacle_right = Instantiate(obstaclePrefab);
        
        ActivateObstacle();
    }
    
    private void ActivateObstacle()
    {
        for (int i = 0; i < foodList.Count; i++)
        {
            foodList[i].transform.position = new Vector3(transform.position.x + Random.Range(-3.5f, 3.5f),
                donutPrefab.transform.position.y + Random.Range(-.1f, 0f), transform.position.z + Random.Range(-5f, 5f));
            foodList[i].transform.rotation = Quaternion.Euler(donutPrefab.transform.rotation.eulerAngles.x,
                Random.Range(-30f, 30f), donutPrefab.transform.rotation.eulerAngles.z);
            foodList[i].SetActive(true);
        }

        if (Random.Range(0,100) < 15)
        {
            burger.transform.position = new Vector3(transform.position.x + Random.Range(-3.5f, 3.5f),
                burgerPrefab.transform.transform.position.y + Random.Range(-.1f, 0f), transform.position.z + Random.Range(-5f, 5f));
            burger.transform.rotation = Quaternion.Euler(burgerPrefab.transform.rotation.eulerAngles.x,
                Random.Range(-30f, 30f), burgerPrefab.transform.rotation.eulerAngles.z);
            burger.SetActive(true);
        }
        
        obstacle_left.SetActive(false);
        obstacle_middle.SetActive(false);
        obstacle_right.SetActive(false);
        
        switch (Random.Range(2, 7 + 1))
        {
            case 2:
                obstacle_left.transform.position = transform.GetChild(2).transform.position;
                obstacle_left.SetActive(true);
                break;
            
            case 3:
                obstacle_middle.transform.position = transform.GetChild(3).transform.position;
                obstacle_middle.SetActive(true);
                break;
            
            case 4:
                obstacle_right.transform.position = transform.GetChild(4).transform.position;
                obstacle_right.SetActive(true);
                break;
            
            case 5:
                obstacle_left.transform.position = transform.GetChild(2).transform.position;
                obstacle_right.transform.position = transform.GetChild(4).transform.position;
                obstacle_left.SetActive(true);
                obstacle_right.SetActive(true);
                break;
            
            case 6:
                obstacle_left.transform.position = transform.GetChild(2).transform.position;
                obstacle_middle.transform.position = transform.GetChild(3).transform.position;
                obstacle_left.SetActive(true);
                obstacle_middle.SetActive(true);
                break;
            
            case 7:
                obstacle_right.transform.position = transform.GetChild(4).transform.position;
                obstacle_middle.transform.position = transform.GetChild(3).transform.position;
                obstacle_right.SetActive(true);
                obstacle_middle.SetActive(true);
                break;
            
            case 8:
                // Do nothing
                break;
        }
    }
}
