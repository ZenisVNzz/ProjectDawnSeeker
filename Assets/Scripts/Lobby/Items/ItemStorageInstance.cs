using UnityEngine;

public class ItemStorageInstance : MonoBehaviour
{
    public static ItemStorageInstance Instance { get; private set; }
    public ItemStorage itemStorage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
