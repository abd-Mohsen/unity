// ParticleComputeShader.compute
#pragma kernel CSMain

struct GPUParticle
{
    float3 position;
    float3 velocity;
    float lifetime;
    float startLifetime;
};

RWStructuredBuffer<GPUParticle> particles;

[numthreads(256, 1, 1)]
void CSMain (uint3 id : SV_DispatchThreadID)
{
    uint index = id.x;
    
    GPUParticle p = particles[index];
    p.position += p.velocity * _DeltaTime;
    p.lifetime -= _DeltaTime;
    
    // Reset particle if its lifetime is up
    if (p.lifetime <= 0)
    {
        p.position = float3(0, 0, 0); // Or some other initial position
        p.lifetime = p.startLifetime;
    }

    particles[index] = p;
}
