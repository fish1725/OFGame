namespace Assets.Scripts.Core.Event {
    [CustomLuaClass]
    public enum OFEventType {
        None,
        GameUpdate,
        LoadItemComplete,
        LoadItemStart,
        SkillStartCast,
        Timeline,
    }
}