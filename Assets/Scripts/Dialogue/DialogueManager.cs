using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
	public Text nameText;
	public Text dialogueText;
	public Animator animator;
	private Queue<string> sentences;
	public float TextSpeed = 1f;
	[SerializeField] private GameObject _continueButton;
	[SerializeField] private GameObject _choices;
	[SerializeField] private NPCManager _manager;
	private DialogueChoice[] dialogueChoices;

	[SerializeField] private string _birthdayMessage;

	void Start()
	{
		sentences = new Queue<string>();
		if (_manager.CharacterInfo.Contains(_manager.Name))
			nameText.text = _manager.Name;

		if (Gamemanager._instance.isBirthday)
        {
			StopAllCoroutines();
			StartCoroutine(TypeSentence(_birthdayMessage));
		}
	}

	private void OnEnable()
    {
		if (_manager.CharacterInfo.Contains(_manager.Name))
			nameText.text = _manager.Name;

		if (dialogueChoices == null)
        {
			dialogueChoices = _choices.GetComponentsInChildren<DialogueChoice>();
		}

		PlayerController._instance.DisableControls();
		animator.SetBool("IsOpen", true);

		foreach (var item in dialogueChoices)
		{
			item.gameObject.SetActive(true);
		}
	}

    private void OnDisable()
    {
		PlayerController._instance.EnableControls();
	}

	public void StartDialogue(Dialogue dialogue)
	{
		_continueButton.SetActive(true);
		_choices.SetActive(false);

		if (_manager.CharacterInfo.Contains(_manager.Name))
			nameText.text = _manager.Name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if (sentences.Count == 0)
		{
			if (_manager.CharacterInfo.Contains(_manager.Name))
				nameText.text = _manager.Name;
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		string NewSentences = Gamemanager._instance.SetPronouns(sentence);

		foreach (char letter in NewSentences.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(TextSpeed * Gamemanager._instance.textSpeedMod);
		}
	}

	void EndDialogue()
	{
		foreach (var item in dialogueChoices)
		{
			item.gameObject.SetActive(true);
		}
		_continueButton.SetActive(false);
		_choices.SetActive(true);
	}

	public void DisableDialogue()
    {
		animator.SetBool("IsOpen", false);

		Invoke("DisableBox", 0.5f);
	}

	void DisableBox()
    {
		transform.parent.gameObject.SetActive(false);
    }
}