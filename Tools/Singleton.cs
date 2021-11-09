using UnityEngine;

// 实现普通的单例模式
public abstract class Singleton<T> where T : new()
{
    private static T _instance;
    private static readonly object Mutex = new object();
    public static T Instance
    {
        get
        {
            if (_instance != null) return _instance;
            lock (Mutex)
            {
                _instance ??= new T();
            }
            return _instance;
        }
    }
}

// Unity Monobeavior Singleton

public class UnitySingleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = FindObjectOfType(typeof(T)) as T;
            var manager = GameObject.Find("Manager");
            if (manager == null)
            {
                manager = new GameObject("Manager");
            }
            _instance = (T)manager.AddComponent(typeof(T));
            return _instance;
        }
    }

    public virtual void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (_instance == null)
        {
            _instance = this as T;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}