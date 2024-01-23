using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public static RoadSpawner instance { get; private set; }
    
    [Space(5), Header("Ground"), Space(15)]
    [SerializeField] private int groundTileCount;
    [SerializeField] private GameObject groundPrefab;
    private Vector3 nextSpawnPoint;

    private void Awake() => instance = this;

    private void Start()
    {
        nextSpawnPoint = new Vector3(0, -0.8f, -5f);
        
        for (int i = 0; i < groundTileCount; i++)
            SpawnRoad();
    }
    
    private void SpawnRoad()
    {
        GameObject road = Instantiate(groundPrefab, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = road.transform.GetChild(1).transform.position;
    }

    public void ChangePosition(GameObject road)
    {
        if (!GameController.instance.gameEnded)
        {
            road.transform.position = nextSpawnPoint;
            nextSpawnPoint = road.transform.GetChild(1).transform.position;
        }
    }
}
