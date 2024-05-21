using UnityEngine;

public class WallEmitterManager : MonoBehaviour
{
    public GameObject emitterPrefab; // Reference to the emitter prefab
    public int numberOfEmitters = 5; // Number of emitters to create
    public float spacing = 1.0f; // Spacing between emitters

    void Start()
    {
        AddEmitters();
    }

    void AddEmitters()
    {
        Vector3 wallSize = GetComponent<Renderer>().bounds.size; // Get the size of the wall
        Vector3 startPosition = transform.position - wallSize / 2 + new Vector3(spacing, 0, 0);

        for (int i = 0; i < numberOfEmitters; i++)
        {
            Vector3 emitterPosition = startPosition + new Vector3(0, i * spacing, 0);
            GameObject emitter = Instantiate(emitterPrefab, emitterPosition, Quaternion.identity);
            emitter.transform.parent = transform; // Parent the emitter to the wall
        }
    }
}
