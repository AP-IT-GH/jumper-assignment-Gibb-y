using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject Obstacle;

    [Header("Obstacle props")]
    public float ObstacleSpeedMin = 6f;
    public float ObstacleSpeedMax = 10f;
    public float ObstacleSpeed;
    public float SpawnIntervalMin = 1.3f;
    public float SpawnIntervalMax = 2f;
    [SerializeField]
    private float spawnInterval;

    private float deltaTime = 0f;

    private void Start()
    {
        ObstacleSpeed = Random.Range(ObstacleSpeedMin, ObstacleSpeedMax);
        Obstacle.GetComponent<Obstacle>().speed = ObstacleSpeed;
        spawnInterval = Random.Range(SpawnIntervalMin, SpawnIntervalMax);
    }

    // Update is called once per frame
    private void Update()
    {
        Obstacle.GetComponent<Obstacle>().speed = ObstacleSpeed;
        deltaTime += Time.deltaTime;

        if (deltaTime >= spawnInterval)
        {
            GameObject.Instantiate(Obstacle);
            deltaTime = 0;
            spawnInterval = Random.Range(SpawnIntervalMin, SpawnIntervalMax);
        }
    }
}
