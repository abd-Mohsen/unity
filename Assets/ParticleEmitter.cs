using UnityEngine;

public class ParticleEmitter : MonoBehaviour
{
    public GameObject particlePrefab;
    public Vector3 emitterPosition = new (0,0,0);
    public Vector3 emitterSize = new (15,15,5);
    private ParticlesManager pm = new();

    void Start()
    {
        pm.InitializeBVH(); 
    }

    void Update(){
        SpawnParticle();
        pm.UpdateBVH(); //edit hash
        pm.CheckCollisions(); //check hash for collision
    }


    void SpawnParticle()
    {
        Vector3 position = emitterPosition + new Vector3(
            Random.Range(-emitterSize.x / 15, emitterSize.x / 15),
            Random.Range(-emitterSize.y / 15, emitterSize.y / 15),
            Random.Range(-emitterSize.z / 15, emitterSize.z / 15)
        );

        GameObject particle = Instantiate(particlePrefab, position, Quaternion.identity);
       
        pm.AddParticle(particle.GetComponent<CustomParticle>());

        pm.DeleteParticles();
    }
}
