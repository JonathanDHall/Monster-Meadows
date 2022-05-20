using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this.gameObject);
    }
}