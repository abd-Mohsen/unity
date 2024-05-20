using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject emitterPrefab; // Reference to the emitter prefab

    void Start()
    {
        AddEmitters();
    }

    void AddEmitters()
    {
        // Define wall position and scale
        Vector3 wallPosition = new Vector3(0, 5, 0);
        Vector3 wallScale = new Vector3(10, 10, 1);

        // Calculate emitter positions
        Vector3 bottomLeftPosition = wallPosition + new Vector3(-wallScale.x / 2, -wallScale.y / 2, 0);
        Vector3 centerPosition = wallPosition;
        Vector3 topRightPosition = wallPosition + new Vector3(wallScale.x / 2, wallScale.y / 2, 0);

        // Instantiate emitters at specified positions
        InstantiateEmitter(bottomLeftPosition);
        InstantiateEmitter(centerPosition);
        InstantiateEmitter(topRightPosition);
    }

    void InstantiateEmitter(Vector3 position)
    {
        GameObject emitter = Instantiate(emitterPrefab, position, Quaternion.identity);
        emitter.transform.parent = transform; // Parent the emitter to the wall
    }
}
