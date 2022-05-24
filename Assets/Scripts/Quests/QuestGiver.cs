using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private string _questName;

    public void StartQuest()
    {
        Collection._instance.UpdateQuestLog(_questName);
    }

    public void CompleteQuest()
    {
        Collection._instance.UpdateQuestLog(_questName, true);
    }
}