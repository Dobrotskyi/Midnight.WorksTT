namespace Scripts.Game.SaveSystem
{
    public interface IPersistentData
    {
        public void LoadDataFrom(GameData data);
        public void SaveDataTo(GameData data);
    }
}