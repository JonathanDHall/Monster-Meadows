using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private GameObject _interactUI;

    [SerializeField] private string _name;
    public string Name { get => _name; set => _name = value; }

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
        Collection._instance.NPCNames.Add(Name);
    }
}