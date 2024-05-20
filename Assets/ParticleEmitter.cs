using UnityEngine;
//using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class ParticleEmitter : MonoBehaviour
{
    public GameObject particlePrefab;
    public int particleCount = 5000;
    public Vector3 emitterPosition = new (0,0,0);
    public Vector3 emitterSize = new (10,10,2);
    //public Vector3 initialVelocity = new (2,0,0);

    public HashSet<GameObject> particles = new(); // put set outside

    //todo: make a wall with multiple emitters

    void Start()
    {
        // for (int i = 0; i < particleCount; i++)
        // {
        //     SpawnParticle();
        // }
    }

    void Update(){
        SpawnParticle();
    }

    void SpawnParticle()
    {
        Vector3 position = emitterPosition + new Vector3(
            Random.Range(-emitterSize.x / 2, emitterSize.x / 2),
            Random.Range(-emitterSize.y / 2, emitterSize.y / 2),
            Random.Range(-emitterSize.z / 2, emitterSize.z / 2)
        );

        GameObject particle = Instantiate(particlePrefab, position, Quaternion.identity);
        CustomParticle customParticle = particle.GetComponent<CustomParticle>();
        //customParticle.velocity = initialVelocity;

        particles.Add(particle);

        //delete dead particles
        foreach(GameObject p in particles){
            if(p.GetComponent<CustomParticle>().IsDead()){
                particles.Remove(p);
            }
        }

        //check 
    }
}
