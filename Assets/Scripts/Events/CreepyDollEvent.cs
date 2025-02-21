using UnityEngine;
using System.Collections;

public class CreepyDollEvent : MonoBehaviour
{
    [SerializeField] private GameObject dollPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private SoundType soundToPlay;
    [SerializeField] private float effectDuration = 10f;

    private GameObject spawnedDoll;
    private Light dollEyesLight;
    private Renderer dollRenderer;
    private bool isFlickering = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerView>() != null)
        {
            TriggerCreepyDollEvent();
        }
    }

    private void TriggerCreepyDollEvent()
    {
        spawnedDoll = Instantiate(dollPrefab, spawnPoint.position, spawnPoint.rotation);
        dollEyesLight = spawnedDoll.AddComponent<Light>();
        dollEyesLight.color = Color.red;
        dollEyesLight.range = 5f;
        dollEyesLight.intensity = 5f;
        dollRenderer = spawnedDoll.GetComponentInChildren<Renderer>();

        StartCoroutine(FlickerEffect());

        EventService.Instance.OnCreepyDollEvent.InvokeEvent();
        GameService.Instance.GetSoundView().PlaySoundEffects(soundToPlay);
        
        StartCoroutine(StopAndDestroyEffect());
        GetComponent<Collider>().enabled = false;
    }

    private IEnumerator FlickerEffect()
    {
        while (isFlickering)
        {
            if (dollEyesLight != null)
            {
                dollEyesLight.intensity = Random.Range(1f, 10f);
            }

            if (dollRenderer != null)
            {
                dollRenderer.enabled = !dollRenderer.enabled;
            }

            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
        }
    }

    private IEnumerator StopAndDestroyEffect()
    {
        yield return new WaitForSeconds(effectDuration);
        isFlickering = false;

        if (spawnedDoll != null)
        {
            Destroy(spawnedDoll);
        }
    }
}