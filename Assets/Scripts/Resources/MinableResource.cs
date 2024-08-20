using System.Collections;
using UnityEngine;

public class MinableResource : MonoBehaviour
{
    [SerializeField]
    private GameObject[] resourcesToSpawn;

    [SerializeField]
    private float minSpawnLocalX = -5f;

    [SerializeField]
    private float maxSpawnLocalX = 5f;

    [SerializeField]
    private float minSpawnLocalZ = -1.5f;

    [SerializeField]
    private float maxSpawnLocalZ = -5f;

    [SerializeField]
    private Material hitFlashMaterial;

    [SerializeField]
    private float flashEffectDuration = .05f;

    private Coroutine flashEffectCoroutine;
    private Material defaultMaterial;
    private ParticleSystem hitParticleSystem;

    private SpriteRenderer spriteRenderer;
    private AudioSource hitAudioSource;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;
        hitParticleSystem = GetComponentInChildren<ParticleSystem>();
        hitAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Projectile") && !other.CompareTag("MiningProjectile"))
        {
            return;
        }

        PlayOnHitEffects();

        GameObject resourceToSpawn = resourcesToSpawn[Random.Range(0, resourcesToSpawn.Length)];

        float spawnLocationX = Random.Range(minSpawnLocalX, maxSpawnLocalX);
        float spawnLocationZ = Random.Range(minSpawnLocalZ, maxSpawnLocalZ);

        Vector3 spawnLocation =
            transform.position + new Vector3(spawnLocationX, .1f, spawnLocationZ);

        GameObject spawnedResource = Instantiate(
            resourceToSpawn,
            spawnLocation,
            Quaternion.Euler(Constants.CameraXRotation, 0f, 0f)
        );
    }

    private void PlayOnHitEffects()
    {
        hitParticleSystem.Play();
        hitAudioSource.Play();
        PlayFlashEffect();
    }

    private void PlayFlashEffect()
    {
        if (flashEffectCoroutine != null)
        {
            StopCoroutine(flashEffectCoroutine);
        }

        flashEffectCoroutine = StartCoroutine(FlashEffectCoroutine());
    }

    private IEnumerator FlashEffectCoroutine()
    {
        spriteRenderer.material = hitFlashMaterial;

        yield return new WaitForSeconds(flashEffectDuration);

        spriteRenderer.material = defaultMaterial;

        flashEffectCoroutine = null;
    }
}
