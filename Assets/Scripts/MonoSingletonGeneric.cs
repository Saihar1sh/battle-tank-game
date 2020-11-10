using UnityEngine;

public class MonoSingletonGeneric <T> : MonoBehaviour where T: MonoSingletonGeneric<T>
{
    private T instance;
    public T Instance { get { return instance; } }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = (T) this;
        }
        else
        {
            Debug.LogError("There is another Singleton in the class ");
            Destroy(this);
        }
    }
}
