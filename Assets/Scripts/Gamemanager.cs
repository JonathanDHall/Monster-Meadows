using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager _instance;

    public string Name = "Goblin";
    public string SubPronouns = "They";
    public string ObjPronouns = "Them";
    public string PosPronouns = "TheirS";
    public string RefPronouns = "Themselves";
    public int BirthMonth = 1;
    public int BirthDay = 1;

    public bool isBirthday;

    public float textSpeedMod = 1;

    //Insures only one instance of the gamemanager is ever loaded
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
           // DontDestroyOnLoad(this);
        }
        else
            Destroy(this.gameObject);

        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    //Loads certain player prefs if they exist
    private void Start()
    {
        Name = PlayerPrefs.GetString("Name");

        SubPronouns = PlayerPrefs.GetString("SubPronouns");
        ObjPronouns = PlayerPrefs.GetString("ObjPronouns");
        PosPronouns = PlayerPrefs.GetString("PosPronouns");
        RefPronouns = PlayerPrefs.GetString("RefPronouns");

        BirthMonth = PlayerPrefs.GetInt("BirthMonth");
        BirthDay = PlayerPrefs.GetInt("BirthDay");

        if (BirthMonth == DateTime.Today.Month && BirthDay == DateTime.Today.Day)
            isBirthday = true;
    }

    //Will search through strings of text and replace certain phrases with saved strings such as names or pronouns.
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

    [Header("Loading Screen")]
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Scrollbar _bar;
    float totalSceneProgress;

    //Used to initiate the loading of a new scene
    public void LoadLevel(string sceneName)
    {
        _loadingScreen.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1)));
        scenesLoading.Add(SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
    }

    //Displays a loding screen while loading a level in the background
    public IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                totalSceneProgress = 0;

                foreach (var item in scenesLoading)
                {
                    totalSceneProgress += item.progress;
                }

                totalSceneProgress = (totalSceneProgress / scenesLoading.Count);

                _bar.size = totalSceneProgress;

                yield return null;
            }
        }

        yield return new WaitForSeconds(0.2f);

        if (FindObjectOfType<CharacterController>())
            GetComponent<InventorySystem>()._isActive = true;
        else 
            GetComponent<InventorySystem>()._isActive = false;

        _loadingScreen.SetActive(false);
    }
}