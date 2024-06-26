using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class ParticlesManager : MonoBehaviour
{
    public HashSet<CustomParticle> particles = new(); // put set outside
    public Dictionary<Vector3Int, List<CustomParticle>> bvh = new();
    public float voxelSize = 0.5f;
    public Vector3 sphereCenter = new(7,15,-86);
    public float sphereRadius = 1.0f;

     void Start(){
        transform.position = sphereCenter;
        transform.localScale = new(sphereRadius,sphereRadius,sphereRadius);
        InitializeBVH();
    }

    void Update(){
        UpdateBVH();
        CheckCollisions();
    }

    public void AddParticle(CustomParticle particle){
        particles.Add(particle);
    }

    public void DeleteParticles(){
        //delete dead particles(accumulate then delete, to not disturb the iteration)
        List<CustomParticle> particlesToRemove = new();

        foreach(CustomParticle p in particles){
            if(p.IsDead()){
                particlesToRemove.Add(p);
            }
        }

        foreach(CustomParticle p in particlesToRemove){
            particles.Remove(p);
        }
    }
    
    public void InitializeBVH(){
        foreach (CustomParticle particle in particles){
            AddParticleToHash(particle);
        }
    }

    public void UpdateBVH(){
        bvh.Clear();
        foreach (CustomParticle particle in particles){
            if (!particle.IsDead()) AddParticleToHash(particle);
        }
    }

    void AddParticleToHash(CustomParticle particle){
        Vector3Int voxel = GetVoxelCoordinate(particle.transform.position);
        if (!bvh.ContainsKey(voxel)){
            bvh[voxel] = new List<CustomParticle>();
        }
        bvh[voxel].Add(particle);
    }

    Vector3Int GetVoxelCoordinate(Vector3 position){
        int x = Mathf.FloorToInt(position.x / voxelSize);
        int y = Mathf.FloorToInt(position.y / voxelSize);
        int z = Mathf.FloorToInt(position.z / voxelSize);
        return new Vector3Int(x, y, z);
    }

    public void CheckCollisions(){
        foreach (CustomParticle particle in particles){
            CheckCollisionWithSphere(particle);
            List<CustomParticle> inVoxel = bvh[GetVoxelCoordinate(particle.transform.position)]; // in the same voxel
            List<CustomParticle> nearby = GetNearbyParticles(GetVoxelCoordinate(particle.transform.position));
            
            foreach (CustomParticle other in inVoxel.Union(nearby)){
                if (other != particle && IsColliding(particle, other)){
                    //Debug.Log($"{particle.name} is colliding with {other.name}");
                }
            }
        }
    }

    void CheckCollisionWithSphere(CustomParticle particle){
        //todo optimize later by only checking particles in voxels that include the sphere
        Vector3 toParticle = particle.transform.position - sphereCenter;
        float distance = toParticle.magnitude;

        if (
            //distance <= sphereRadius + particle.radius
            particle.transform.position.x - sphereCenter.x < 0.1
            )
        {
            //HandleCollision(particle, toParticle.normalized);
            // TODO do a simple collision with a cube (calculate normal and so on..)
            Debug.Log($"{particle.name} is colliding with sphere");
            particle.Collide();
        }
    }

    List<CustomParticle> GetNearbyParticles(Vector3Int voxel){
        List<CustomParticle> nearbyParticles = new();

        for (int x = -1; x <= 1; x++){
            for (int y = -1; y <= 1; y++){
                for (int z = -1; z <= 1; z++){
                    Vector3Int neighborVoxel = voxel + new Vector3Int(x, y, z);
                    if (bvh.ContainsKey(neighborVoxel)){
                        nearbyParticles.AddRange(bvh[neighborVoxel]);
                    }
                }
            }
        }

        return nearbyParticles;
    }

    bool IsColliding(CustomParticle p1, CustomParticle p2){
        float distance = Vector3.Distance(p1.transform.position, p2.transform.position);
        float collisionDistance = 0.1f;
        return distance < collisionDistance;
    }

}
