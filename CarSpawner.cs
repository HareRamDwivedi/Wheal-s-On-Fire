using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] aiCarPrefabs; // Assign various AI car prefabs
    public Transform playerCar;
    public float spawnDistance = 80f;
    public float laneWidth = 3.5f;
    public float spawnInterval = 2f;

    private float timer = 0f;
    private float[] laneOffsets = { -5.25f, -1.75f, 1.75f, 5.25f }; // 4 lanes

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnCar();
            timer = 0f;
        }
    }

    void SpawnCar()
    {
        // Pick random lane
        int laneIndex = Random.Range(0, laneOffsets.Length);
        float laneX = laneOffsets[laneIndex];

        // Random direction (first 2 lanes same as player, last 2 opposite)
        bool oppositeDir = laneIndex >= 2;

        Vector3 spawnPos = playerCar.position + (oppositeDir ? -playerCar.forward : playerCar.forward) * spawnDistance;
        spawnPos.x = laneX;

        GameObject carPrefab = aiCarPrefabs[Random.Range(0, aiCarPrefabs.Length)];
        GameObject newCar = Instantiate(carPrefab, spawnPos, Quaternion.identity);

        if (oppositeDir)
            newCar.transform.rotation = Quaternion.Euler(0, 180, 0);

        Rigidbody rb = newCar.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = (oppositeDir ? -Vector3.forward : Vector3.forward) * Random.Range(15f, 25f);
        }

        Destroy(newCar, 15f); // Clean up old cars
    }
}
