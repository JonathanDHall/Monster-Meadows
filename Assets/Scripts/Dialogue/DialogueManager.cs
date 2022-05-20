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
	public float TextSpeed = 0.01f;
	[SerializeField] private GameObject _continueButton;
	[SerializeField] private GameObject _choices;
	[SerializeField] private NPCManager _manager;

	void Start()
	{
		sentences = new Queue<string>();
	}

    private void OnEnable()
    {
		PlayerController._instance.DisableControls();
		animator.SetBool("IsOpen", true);
	}

    private void OnDisable()
    {
		PlayerController._instance.EnableControls();
	}

	public void StartDialogue(Dialogue dialogue)
	{
		_continueButton.SetActive(true);
		_choices.SetActive(false);

		if (_manager.IsNameKnown)
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
			if (_manager.IsNameKnown)
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
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(TextSpeed);
		}
	}

	void EndDialogue()
	{
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