using UnityEngine;

public class ParticleEmitter : MonoBehaviour
{
    public GameObject particlePrefab;
    public Vector3 emitterPosition = new (0,0,0);
    public Vector3 emitterSize = new (15,15,5);
    private ParticlesManager pm;

    public int spawnRate = 1; // its delaying rather than increasing rate


    private void Awake()
    {
        GameObject gameObject = new GameObject();
        pm = gameObject.AddComponent<ParticlesManager>();
    }

    void Start()
    {
        emitterPosition = transform.position;
        emitterSize = transform.localScale;
        //pm.InitializeBVH(); // put bvh code outside, so the method doesnt get called for every emitter
    }

    void Update(){
        for(int i=0; i<spawnRate; i++) SpawnParticle();
        // pm.UpdateBVH();
        // pm.CheckAirCollisions();
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
