public class EventService
{
    private static EventService instance;
    public static EventService Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EventService();
            }
            return instance;
        }
    }

    public EventController OnLightSwitchToggled { get; private set; }
    public EventController<int> OnKeyPickedUp { get; private set; }
    public EventController OnLightsOffByGhostEvent { get; private set; }
    public EventController OnRatRush { get; private set; }
    public EventController OnSkullDrop { get; private set; }
    public EventController<int> OnPotionDrinkEvent { get; private set; }
    public EventController OnCreepyDollEvent { get; private set; }
    public EventController OnHallOfWhispersEvent { get; private set; }
    public EventController OnHauntedPaintingEvent { get; private set; }
    public EventController OnHauntedMusicBoxEvent { get; private set; }
    public EventController OnPlayerEscapedEvent { get; private set; }
    public EventController OnPlayerDeathEvent { get; private set; }

    public EventService()
    {
        OnLightSwitchToggled = new EventController();
        OnKeyPickedUp = new EventController<int>();
        OnLightsOffByGhostEvent = new EventController();
        OnRatRush = new EventController();
        OnSkullDrop = new EventController();
        OnPotionDrinkEvent = new EventController<int>();
        OnCreepyDollEvent = new EventController();
        OnHallOfWhispersEvent = new EventController();
        OnHauntedPaintingEvent = new EventController();
        OnHauntedMusicBoxEvent = new EventController();
        OnPlayerEscapedEvent = new EventController();
        OnPlayerDeathEvent = new EventController();
    }
}