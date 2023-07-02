using System.Text.Json;

namespace DataBaseLibrary;

public class DataBase : IDataBase
{
    private Dictionary<Type, List<IEntity>> _tables;

    public DataBase()
    {
        _tables = new();
    }

    /// <summary>
    /// Creating table of type T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <exception cref="DataBaseException">An exception occurs if the table exists</exception>
    public void CreateTable<T>() where T : IEntity
    {
        if (_tables.ContainsKey(typeof(T)))
            throw new DataBaseException($"Table {typeof(T)} already exists.");
        _tables.Add(typeof(T), new List<IEntity>());
    }

    /// <summary>
    /// Inserting item of type T in the table of type T
    /// </summary>
    /// <param name="getEntity">Return an object of type T</param>
    /// <typeparam name="T"></typeparam>
    /// <exception cref="DataBaseException">An exception occurs if the table does not exists</exception>
    public void InsertInto<T>(Func<T> getEntity) where T : IEntity
    {
        var entity = getEntity.Invoke();
        if (!_tables.ContainsKey(typeof(T)))
            throw new DataBaseException($"The {typeof(T)} table is not exists. So you can't add some objects there.");
        _tables[typeof(T)].Add(entity);
    }
    /// <summary>
    /// Getting table of the type T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns>Table of type T</returns>
    /// <exception cref="DataBaseException">An exception occurs if the table does not exists</exception>
    public IEnumerable<T> GetTable<T>() where T : IEntity
    {
        if (!_tables.ContainsKey(typeof(T)))
            throw new DataBaseException($"{typeof(T)} table is not exists.");
        return _tables[typeof(T)].Select(x => (T)x);
    }
    /// <summary>
    /// Serializing table of type T
    /// </summary>
    /// <param name="path">Path to the file with serialization of the table</param>
    /// <typeparam name="T"></typeparam>
    /// <exception cref="DataBaseException">An exception occurs if the table does not exists</exception>
    public void Serialize<T>(string path) where T : IEntity
    {
        using var sw = File.CreateText(path);
        if (!_tables.ContainsKey(typeof(T)))
            throw new DataBaseException($"{typeof(T)} table is not exists.");
        var entity = JsonSerializer.Serialize(_tables[typeof(T)].Select(x => (T)x));
        sw.WriteLine(entity);
        Console.WriteLine("JSON-serialization completed successfully!");
    }

    /// <summary>
    /// Deserializing table of type T
    /// </summary>
    /// <param name="path">Path to the file with json presentation of the table</param>
    /// <typeparam name="T"></typeparam>
    /// <exception cref="DataBaseException">An exception occurs if the path is incorrect</exception>
    public void Deserialize<T>(string path) where T : IEntity
    {
        if (!File.Exists(path))
            throw new DataBaseException("Incorrect path");
        var entity = File.ReadAllText(path);
        var entities = JsonSerializer.Deserialize<List<T>>(entity);
        _tables[typeof(T)] = entities.Select(x => (IEntity)x).ToList();
        Console.WriteLine("JSON-deserialization completed successfully!");
    }
}