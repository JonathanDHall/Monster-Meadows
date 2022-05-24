using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private GameObject _interactUI;

    [SerializeField] private string _name;
    public string Name { get => _name; set => _name = value; }

    public HashSet<string> CharacterInfo { get; private set; } = new HashSet<string>();
    public int _relationshipLevel = 0;

    private void Awake()
    {
        GameEvents.SaveInitiated += Save;
        GameEvents.LoadInitiated += Load;
        Load();
    }

    void Save()
    {
        SaveSystem.Save(CharacterInfo, Name);
        SaveSystem.Save(_relationshipLevel, Name + "RelationshipLevl");
    }

    void Load()
    {
        if (SaveSystem.SaveExists(Name))
            CharacterInfo = SaveSystem.Load<HashSet<string>>(Name);
        if (SaveSystem.SaveExists(Name + "RelationshipLevl"))
            _relationshipLevel = SaveSystem.Load<int>(Name + "RelationshipLevl");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Player"))
            return;

        _interactUI.SetActive(true);
    }

    private void Update()
    {
        if (_interactUI.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _interactUI.SetActive(false);
                _dialogueBox.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.transform.CompareTag("Player"))
            return;

        _interactUI.SetActive(false);
        _dialogueBox.SetActive(false);
    }

    public void LearnName()
    {
        CharacterInfo.Add(Name);
    }

    public void ImproveRelationshipLevel()
    {
        _relationshipLevel++;
    }
}