using UnityEngine;

public class DialogueChoice : MonoBehaviour
{
    [SerializeField] private NPCManager _manager;
    [SerializeField] private bool _requiresInfo;
    [SerializeField] private string _requiredInfo;

    [SerializeField] private bool _alreadyKnownInfo;
    [SerializeField] private string _knownInfo;

    [SerializeField] private bool _requiresQuest;
    [SerializeField] private string _quest;

    [SerializeField] private bool _alreadyKnownQuest;
    [SerializeField] private string _knownQuest;

    [SerializeField] private bool _requiresQuestItem;
    [SerializeField] private InventoryItemData _questItem;

    [SerializeField] private bool _questNotCompleted;
    [SerializeField] private string _completedQuest;

    [SerializeField] private bool _requiresMinimumRelationship;
    [SerializeField] private int _minimumRelationship;
    [SerializeField] private int _maximumRelationship;


    private void OnEnable()
    {
        if (_requiresInfo)
        {
            if (!_manager.CharacterInfo.Contains(_requiredInfo))
                gameObject.SetActive(false);
        }

        if (_alreadyKnownInfo)
        {
            if (_manager.CharacterInfo.Contains(_knownInfo))
                gameObject.SetActive(false);
        }

        if (_requiresQuest)
        {
            if (!Collection._instance.QuestLog.Contains(_quest))
                gameObject.SetActive(false);
        }

        if (_alreadyKnownQuest)
        {
            if (Collection._instance.QuestLog.Contains(_knownQuest))
                gameObject.SetActive(false);
        }

        if (_requiresQuestItem)
        {
            if (!InventorySystem._instance.CheckIfInventoryContains(_questItem))
                gameObject.SetActive(false);
        }

        if (_questNotCompleted)
        {
            if (Collection._instance.CompletedQuests.Contains(_completedQuest))
                gameObject.SetActive(false);
        }

        if (_requiresMinimumRelationship)
        {
            if (_manager._relationshipLevel < _minimumRelationship || _manager._relationshipLevel > _maximumRelationship)
                gameObject.SetActive(false);
        }
    }
}