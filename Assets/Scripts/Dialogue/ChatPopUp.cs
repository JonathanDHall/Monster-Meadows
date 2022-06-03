using System.Collections;
using UnityEngine;
using TMPro;

public class ChatPopUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _dialogue;
    private float TextSpeed = 1;

    public static ChatPopUp Create(string name, string dialogue, Transform pos)
    {
        var popUp = Instantiate(Resources.Load("ChatPopUp"), pos.position, pos.rotation) as GameObject;
        ChatPopUp pop = popUp.GetComponentInChildren<ChatPopUp>();

        pop.SetUp(name, dialogue);
        
        return pop;
    }

    IEnumerator TypeSentence(string sentence)
    {
        _dialogue.text = "";
        string NewSentences = Gamemanager._instance.SetPronouns(sentence);

        foreach (char letter in NewSentences.ToCharArray())
        {
            _dialogue.text += letter;
            yield return new WaitForSeconds(TextSpeed * Gamemanager._instance.textSpeedMod);
        }
    }

    public void SetUp(string name, string dialogue)
    {
        _name.text = name;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(dialogue));
        transform.LookAt(Camera.main.transform);

        Destroy(gameObject, 10);
    }
}