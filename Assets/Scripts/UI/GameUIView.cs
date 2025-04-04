using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameUIView : MonoBehaviour
{
    [Header("Player Sanity")]
    [SerializeField] GameObject rootViewPanel;
    [SerializeField] Image insanityImage;
    [SerializeField] Image redVignette;

    [Header("Keys UI")]
    [SerializeField] TextMeshProUGUI keysFoundText;

    [Header("Game End Panel")]
    [SerializeField] GameObject gameEndPanel;
    [SerializeField] TextMeshProUGUI gameEndText;
    [SerializeField] Button tryAgainButton;
    [SerializeField] Button quitButton;

    [Header("Achievement UI")]
    [SerializeField] private GameObject achievementPopup;
    [SerializeField] private TextMeshProUGUI achievementTitleText;
    [SerializeField] private TextMeshProUGUI achievementDescriptionText;

    private void OnEnable()
    {
        EventService.Instance.OnKeyPickedUp.AddListener(OnKeyEquipped);
        EventService.Instance.OnLightsOffByGhostEvent.AddListener(SetRedVignette);
        EventService.Instance.OnPlayerEscapedEvent.AddListener(OnPlayerEscaped);
        EventService.Instance.OnPlayerDeathEvent.AddListener(SetRedVignette);
        EventService.Instance.OnPlayerDeathEvent.AddListener(OnPlayerDeath);
        EventService.Instance.OnRatRush.AddListener(SetRedVignette);
        EventService.Instance.OnSkullDrop.AddListener(SetRedVignette);
        EventService.Instance.OnCreepyDollEvent.AddListener(SetRedVignette);
        EventService.Instance.OnHallOfWhispersEvent.AddListener(SetRedVignette);
        EventService.Instance.OnHauntedMusicBoxEvent.AddListener(SetRedVignette);
        EventService.Instance.OnHauntedPaintingEvent.AddListener(SetRedVignette);

        tryAgainButton.onClick.AddListener(OnTryAgainButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    private void OnDisable()
    {
        EventService.Instance.OnKeyPickedUp.RemoveListener(OnKeyEquipped);
        EventService.Instance.OnLightsOffByGhostEvent.RemoveListener(SetRedVignette);
        EventService.Instance.OnPlayerEscapedEvent.RemoveListener(OnPlayerEscaped);
        EventService.Instance.OnPlayerDeathEvent.RemoveListener(SetRedVignette);
        EventService.Instance.OnPlayerDeathEvent.RemoveListener(OnPlayerDeath);
        EventService.Instance.OnRatRush.RemoveListener(SetRedVignette);
        EventService.Instance.OnSkullDrop.RemoveListener(SetRedVignette);
        EventService.Instance.OnCreepyDollEvent.RemoveListener(SetRedVignette);
        EventService.Instance.OnHallOfWhispersEvent.RemoveListener(SetRedVignette);
        EventService.Instance.OnHauntedMusicBoxEvent.RemoveListener(SetRedVignette);
        EventService.Instance.OnHauntedPaintingEvent.RemoveListener(SetRedVignette);
    }

    public void UpdateInsanity(float playerSanity) => insanityImage.rectTransform.localScale = new Vector3(1, playerSanity, 1);
    private void OnKeyEquipped(int keys) => keysFoundText.SetText($"Keys Found: {keys}/8");
    private void OnQuitButtonClicked() => Application.Quit();
    private void OnTryAgainButtonClicked() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    private void SetRedVignette()
    {
        redVignette.enabled = true;
        redVignette.canvasRenderer.SetAlpha(0.5f);
        redVignette.CrossFadeAlpha(0, 5, false);
    }

    private void OnPlayerDeath()
    {
        gameEndText.SetText("Game Over");
        gameEndPanel.SetActive(true);
    }

    private void OnPlayerEscaped()
    {
        gameEndText.SetText("You Escaped");
        gameEndPanel.SetActive(true);
    }

    public void ShowAchievementPopup(string title, string description)
    {
        achievementTitleText.SetText(title);
        achievementDescriptionText.SetText(description);
        achievementPopup.SetActive(true);

        StartCoroutine(HideAchievementPopup());
    }

    private IEnumerator HideAchievementPopup()
    {
        yield return new WaitForSeconds(3f);
        achievementPopup.SetActive(false);
    }
}