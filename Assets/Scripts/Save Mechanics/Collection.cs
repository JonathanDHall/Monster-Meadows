using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collection : MonoBehaviour
{
    public static Collection _instance;

    public HashSet<string> QuestLog { get; private set; } = new HashSet<string>();
    public HashSet<string> NPCNames { get; private set; } = new HashSet<string>();
    public HashSet<string> Trash { get; private set; } = new HashSet<string>();

    public string _scene;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            GameEvents.SaveInitiated += Save;
            GameEvents.LoadInitiated += Load;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Save()
    {
        SaveSystem.Save(NPCNames, "NPCNames");
        SaveSystem.Save(QuestLog, "QuestLog");
        SaveSystem.Save(Trash, "Trash");

        SaveSystem.Save(SceneManager.GetActiveScene().name, "Scene");
    }

    public void Load()
    {
        if (SaveSystem.SaveExists("NPCNames"))
            NPCNames = SaveSystem.Load<HashSet<string>>("NPCNames");

        if (SaveSystem.SaveExists("Trash"))
            Trash = SaveSystem.Load<HashSet<string>>("Trash");

        if (SaveSystem.SaveExists("NPCNames"))
            NPCNames = SaveSystem.Load<HashSet<string>>("NPCNames");

        if (SaveSystem.SaveExists("Scene"))
        {
            _scene = SaveSystem.Load<string>("Scene");
            SceneManager.LoadScene(_scene);
        }
    }
}