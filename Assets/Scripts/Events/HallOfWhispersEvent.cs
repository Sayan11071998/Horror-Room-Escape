using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HallOfWhispersEvent : MonoBehaviour
{
    [SerializeField] private int keysRequiredToTrigger;
    [SerializeField] private SoundType whisperSound;
    [SerializeField] private List<Light> hallwayLights;
    [SerializeField] private float eventDuration = 10f;
    [SerializeField] private float flickerInterval = 1.5f;

    private bool eventTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerView>() != null && !eventTriggered && GameService.Instance.GetPlayerController().KeysEquipped == keysRequiredToTrigger)
        {
            eventTriggered = true;
            StartCoroutine(TriggerHallOfWhispers());
        }
    }

    private IEnumerator TriggerHallOfWhispers()
    {
        EventService.Instance.OnCreepyDollEvent.InvokeEvent();

        float timer = 0f;

        while (timer < eventDuration)
        {
            GameService.Instance.GetSoundView().PlaySoundEffects(whisperSound);

            SetLights(false);
            yield return new WaitForSeconds(0.5f);

            SetLights(true);
            yield return new WaitForSeconds(flickerInterval);

            timer += flickerInterval + 0.5f;
        }

        SetLights(true);
    }

    private void SetLights(bool state)
    {
        foreach (Light light in hallwayLights)
        {
            if (light != null)
            {
                light.enabled = state;
            }
        }
    }
}