using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AchievementSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameUIView gameUI;
    [SerializeField] private PlayerSanity playerSanity;
    [SerializeField] private PlayerController playerController;

    [Header("Achievement Conditions")]
    [SerializeField] private int totalKeysRequired = 8;
    [SerializeField] private int totalPotionsRequired = 3;
    [SerializeField] private float insanityThreshold = 10f;
    
    private HashSet<string> unlockedAchievements = new HashSet<string>();
    private int potionsCollected = 0;

    private void OnEnable()
    {
        EventService.Instance.OnKeyPickedUp.AddListener(CheckKeymasterAchievement);
        EventService.Instance.OnPotionDrinkEvent.AddListener(CheckSanitySaverAchievement);
        EventService.Instance.OnPlayerEscapedEvent.AddListener(CheckTormentedSurvivorAchievement);
    }

    private void OnDisable()
    {
        EventService.Instance.OnKeyPickedUp.RemoveListener(CheckKeymasterAchievement);
        EventService.Instance.OnPotionDrinkEvent.RemoveListener(CheckSanitySaverAchievement);
        EventService.Instance.OnPlayerEscapedEvent.RemoveListener(CheckTormentedSurvivorAchievement);
    }

    private void CheckKeymasterAchievement(int keysCollected)
    {
        if (!unlockedAchievements.Contains("KEYMASTER") && keysCollected >= totalKeysRequired)
        {
            UnlockAchievement("KEYMASTER", "Collected all keys in the haunted mansion!");
        }
    }

    private void CheckSanitySaverAchievement(int potionEffect)
    {
        potionsCollected++;

        if (!unlockedAchievements.Contains("SANITY SAVER") && potionsCollected >= totalPotionsRequired)
        {
            UnlockAchievement("SANITY SAVER", "Used all sanity-restoring items!");
        }
    }

    private void CheckTormentedSurvivorAchievement()
    {
        float currentSanity = playerSanity.GetCurrentSanity();
        if (!unlockedAchievements.Contains("TORMENTED SURVIVOR") && currentSanity <= insanityThreshold)
        {
            UnlockAchievement("TORMENTED SURVIVOR", "Escaped the mansion with high insanity!");
        }
    }

    private void UnlockAchievement(string title, string description)
    {
        if (unlockedAchievements.Add(title))
        {
            gameUI.ShowAchievementPopup(title, description);
        }
    }
}