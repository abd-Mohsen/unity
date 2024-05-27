using UnityEngine;

public class GPUParticleSystem : MonoBehaviour
{
    public int particleCount = 100000;
    public ComputeShader computeShader;
    public Material particleMaterial;
    public Mesh particleMesh;
    
    private ComputeBuffer particleBuffer;
    private int kernelHandle;
    private GPUParticle[] particles;

    struct GPUParticle
    {
        public Vector3 position;
        public Vector3 velocity;
        public float lifetime;
        public float startLifetime;
    }

    void Start()
    {
        InitializeParticles();
    }

    void InitializeParticles()
    {
        particles = new GPUParticle[particleCount];

        for (int i = 0; i < particleCount; i++)
        {
            particles[i].position = Random.insideUnitSphere * 5.0f;
            particles[i].velocity = Random.insideUnitSphere;
            particles[i].lifetime = Random.Range(1.0f, 5.0f);
            particles[i].startLifetime = particles[i].lifetime;
        }

        particleBuffer = new ComputeBuffer(particleCount, sizeof(float) * 8);
        particleBuffer.SetData(particles);

        kernelHandle = computeShader.FindKernel("CSMain");
        computeShader.SetBuffer(kernelHandle, "particles", particleBuffer);
    }

    void Update()
    {
        computeShader.SetFloat("_DeltaTime", Time.deltaTime);
        computeShader.Dispatch(kernelHandle, particleCount / 256, 1, 1);

        Graphics.DrawMeshInstancedProcedural(particleMesh, 0, particleMaterial, new Bounds(Vector3.zero, Vector3.one * 1000), particleCount, null, UnityEngine.Rendering.ShadowCastingMode.Off, false, 0, null, UnityEngine.Rendering.LightProbeUsage.Off, null);
    }

    void OnDestroy()
    {
        particleBuffer?.Release();
    }
}
