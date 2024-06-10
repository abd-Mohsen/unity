using UnityEngine;

public class CustomParticle : MonoBehaviour
{
    public Vector3 velocity;
    public float lifetime = 10f;

    public float radius = 0.1f;

    public bool IsDead(){
        return lifetime <= 0;
    }

    public void Kill(){
        Destroy(gameObject);
        //remove from set
    }

    public void Collide(){
        velocity = new (0,1,0);
    }
    
    void Start()
    {
        // Initialize particle properties
        velocity = new (2,0,0);
    }

    void Update()
    {
        // Update particle position
        transform.position +=  Time.deltaTime * velocity;
        
        // Update age and check if particle should be destroyed
        lifetime -= Time.deltaTime;
        if (IsDead()){
            //Debug.Log("destroyed");
            Destroy(gameObject);
            //Kill(); // doesnt work
        } 
        
    }
}
