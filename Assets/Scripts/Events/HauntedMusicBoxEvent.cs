using UnityEngine;
using System.Collections;

public class HauntedMusicBoxEvent : MonoBehaviour
{
    [SerializeField] private SoundType soundToPlay;
    [SerializeField] private float eventDuration = 10f;

    private bool eventTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerView>() != null && !eventTriggered)
        {
            eventTriggered = true;
            StartCoroutine(TriggerMusicBoxEvent());
        }
    }

    private IEnumerator TriggerMusicBoxEvent()
    {
        EventService.Instance.OnHauntedMusicBoxEvent.InvokeEvent();
        GameService.Instance.GetSoundView().PlaySoundEffects(soundToPlay, true);

        yield return new WaitForSeconds(eventDuration);

        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.BackgroundMusic, true);
    }
}