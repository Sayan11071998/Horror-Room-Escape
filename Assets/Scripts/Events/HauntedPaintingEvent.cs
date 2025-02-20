using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HauntedPaintingEvent : MonoBehaviour
{
    [SerializeField] private List<GameObject> paintings;
    [SerializeField] private SoundType laughterSound;
    [SerializeField] private List<Transform> furniture;
    [SerializeField] private GameObject shadowFigurePrefab;
    [SerializeField] private Transform shadowSpawnPoint;
    [SerializeField] private float eventDuration = 8f;
    [SerializeField] private float shakeAmount = 0.1f;

    private bool eventTriggered = false;
    private GameObject shadowFigureInstance;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerView>() != null && !eventTriggered)
        {
            eventTriggered = true;
            StartCoroutine(TriggerHauntedPaintingEvent());
        }
    }

    private IEnumerator TriggerHauntedPaintingEvent()
    {
        EventService.Instance.OnCreepyDollEvent.InvokeEvent();
        GameService.Instance.GetSoundView().PlaySoundEffects(laughterSound);

        foreach (GameObject painting in paintings)
        {
            if (painting != null)
            {
                painting.SetActive(false);
            }
        }

        if (shadowFigurePrefab != null && shadowSpawnPoint != null)
        {
            shadowFigureInstance = Instantiate(shadowFigurePrefab, shadowSpawnPoint.position, Quaternion.identity);
            shadowFigureInstance.SetActive(true);
        }

        StartCoroutine(ShakeFurniture());

        yield return new WaitForSeconds(eventDuration);

        foreach (GameObject painting in paintings)
        {
            if (painting != null)
            {
                painting.SetActive(true);
            }
        }

        if (shadowFigureInstance != null)
        {
            Destroy(shadowFigureInstance);
        }

        StopAllCoroutines();
        ResetFurniture();
    }

    private IEnumerator ShakeFurniture()
    {
        float elapsedTime = 0f;

        while (elapsedTime < eventDuration)
        {
            foreach (Transform obj in furniture)
            {
                if (obj != null)
                {
                    obj.position += new Vector3(
                        Random.Range(-shakeAmount, shakeAmount), 
                        Random.Range(-shakeAmount, shakeAmount), 
                        Random.Range(-shakeAmount, shakeAmount)
                    );
                }
            }
            yield return new WaitForSeconds(0.05f);
            elapsedTime += 0.05f;
        }
    }

    private void ResetFurniture()
    {
        foreach (Transform obj in furniture)
        {
            if (obj != null)
            {
                obj.position = new Vector3(obj.position.x, obj.position.y, obj.position.z);
            }
        }
    }
}
