namespace DataBaseLibrary
{
    public interface IDataBase
    {
        public void CreateTable<T>() where T : IEntity;
        public void InsertInto<T>(Func<T> getEntity) where T : IEntity;
        public IEnumerable<T> GetTable<T>() where T : IEntity;
        public void Serialize<T>(string path) where T : IEntity;
        public void Deserialize<T>(string path) where T : IEntity;
    }
}