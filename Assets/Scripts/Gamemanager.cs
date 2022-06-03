using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager _instance;

    public string Name = "Goblin";
    public string SubPronouns = "They";
    public string ObjPronouns = "Them";
    public string PosPronouns = "TheirS";
    public string RefPronouns = "Themselves";

    public float textSpeedMod = 1;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        Name = PlayerPrefs.GetString("Name");

        SubPronouns = PlayerPrefs.GetString("SubPronouns");
        ObjPronouns = PlayerPrefs.GetString("ObjPronouns");
        PosPronouns = PlayerPrefs.GetString("PosPronouns");
        RefPronouns = PlayerPrefs.GetString("RefPronouns");
    }

    public string SetPronouns (string sentence)
    {
        string Adjusted = sentence;
        Adjusted = Adjusted.Replace("(Name)", Name);
        Adjusted = Adjusted.Replace("(SubPronouns)", SubPronouns);
        Adjusted = Adjusted.Replace("(ObjPronouns)", ObjPronouns);
        Adjusted = Adjusted.Replace("(PosPronouns)", PosPronouns);
        Adjusted = Adjusted.Replace("(RefPronouns)", RefPronouns);

        return Adjusted;
    }
}