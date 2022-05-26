using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private string _questName;
    [SerializeField] private InventoryItemData _questObject;

    public void StartQuest()
    {
        Collection._instance.UpdateQuestLog(_questName);
    }

    public void CompleteQuest()
    {
        InventorySystem._instance.Remove(_questObject);
        Collection._instance.UpdateQuestLog(_questName, true);
    }
}