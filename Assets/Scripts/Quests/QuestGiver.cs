using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private string _questName;

    private void Start()
    {
        foreach (var item in Collection._instance.QuestLog)
        {
            Debug.Log(item);
        }
    }

    public void StartQuest()
    {
        if (!Collection._instance.QuestLog.Contains(_questName))
            Collection._instance.QuestLog.Add(_questName);

        foreach (var item in Collection._instance.QuestLog)
        {
            Debug.Log(item);
        }
    }
}