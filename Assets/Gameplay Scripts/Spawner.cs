using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] asteroidPrefabs; // Array of asteroid prefabs
    public float spawnRate = .3f; // Rate at which asteroids spawn
    public float spawnBoundsWidth = 2.5f; // Width of the area where asteroids can spawn
    public float spawnHeight = 10f; // Height above the camera where asteroids can spawn
    private float nextSpawnTime = 0f; // Time when the next asteroid should spawn

    private void Start()
    {
        // Assign asteroid prefabs programmatically if not assigned in the Inspector
        if (asteroidPrefabs == null || asteroidPrefabs.Length == 0)
        {
            asteroidPrefabs = new GameObject[4]; // Adjust the size as needed
            asteroidPrefabs[0] = Resources.Load<GameObject>("Prefabs/Asteroid_Prefabs/asteroid_01");
            asteroidPrefabs[1] = Resources.Load<GameObject>("Prefabs/Asteroid_Prefabs/asteroid_02");
            asteroidPrefabs[2] = Resources.Load<GameObject>("Prefabs/Asteroid_Prefabs/asteroid_03");
            asteroidPrefabs[3] = Resources.Load<GameObject>("Prefabs/Asteroid_Prefabs/asteroid_04");
        }
    }

    private void FixedUpdate()
    {
        // Check if it's time to spawn a new asteroid
        if (Time.time >= nextSpawnTime)
        {
            // Spawn the asteroid
            GameObject asteroid = SpawnAsteroid();

            // Calculate the time when the next asteroid should spawn
            nextSpawnTime = Time.time + spawnRate;

            // Destroy the asteroid after a certain duration
            Destroy(asteroid, 8);
        }
    }

    private GameObject SpawnAsteroid()
    {
        // Check if asteroid prefabs are assigned
        if (asteroidPrefabs == null || asteroidPrefabs.Length == 0)
        {
            Debug.LogError("Asteroid prefabs are not assigned or the array is empty!");
            return null;
        }

        // Randomly select an asteroid prefab from the array
        GameObject asteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];

        // Randomly select a spawn position within the specified bounds
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnBoundsWidth, spawnBoundsWidth), spawnHeight, 0f);

        // Instantiate the selected asteroid prefab at the random spawn position
        return Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
    }
}
