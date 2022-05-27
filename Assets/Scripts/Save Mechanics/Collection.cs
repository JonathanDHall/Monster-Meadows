using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collection : MonoBehaviour
{
    public static Collection _instance;

    public HashSet<string> QuestLog { get; private set; } = new HashSet<string>();
    public HashSet<string> CompletedQuests { get; private set; } = new HashSet<string>();
    public HashSet<string> Trash { get; private set; } = new HashSet<string>();

    [System.Serializable]
    public struct CropData
    {
        public string ID;
        public int m_curPhase;
        public int m_dayCount;
        public bool m_isGrowningg;
        public bool m_isWatered;
        public int m_seedTypeInt;
    }
    public List<CropData> CropList { get; private set; } = new List<CropData>();

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
        SaveSystem.Save(QuestLog, "QuestLog");
        SaveSystem.Save(CompletedQuests, "CompletedQuests");
        SaveSystem.Save(Trash, "Trash");
        SaveSystem.Save(CropList, "CropData");
        SaveSystem.Save(SceneManager.GetActiveScene().name, "Scene");
    }

    public void Load()
    {
        if (SaveSystem.SaveExists("Trash"))
            Trash = SaveSystem.Load<HashSet<string>>("Trash");

        if (SaveSystem.SaveExists("QuestLog"))
            QuestLog = SaveSystem.Load<HashSet<string>>("QuestLog");

        if (SaveSystem.SaveExists("CompletedQuests"))
            CompletedQuests = SaveSystem.Load<HashSet<string>>("CompletedQuests");

        if (SaveSystem.SaveExists("CropData"))
            CropList = SaveSystem.Load<List<CropData>>("CropData");

        if (SaveSystem.SaveExists("Scene"))
        {
            _scene = SaveSystem.Load<string>("Scene");
            SceneManager.LoadScene(_scene);
        }
    }

    public void Clear()
    {
        Trash.Clear();
        CompletedQuests.Clear();
        CropList.Clear();
        _scene = null;
    }


    public bool _SendUpdate;
    public void UpdateQuestLog(string _quest, bool _removeQuest = false)
    {
        if (!_removeQuest)
        {
            if (!QuestLog.Contains(_quest))
            {
                QuestLog.Add(_quest);
                _SendUpdate = true;
            }
        }
        else
        {
            if (QuestLog.Contains(_quest))
            {
                QuestLog.Remove(_quest);
                CompletedQuests.Add(_quest);
                _SendUpdate = true;
            }
        }
    }
}