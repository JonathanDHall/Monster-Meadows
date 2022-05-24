using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestLog : MonoBehaviour
{
    [SerializeField] private GameObject _log;
    private Animator _anim;
    private bool _isopen;
    [SerializeField] private GameObject _default;
    [SerializeField] private GameObject _defaultParent;

    public List<GameObject> CurrentLog { get; private set; } = new List<GameObject>();

    void Start()
    {
        _anim = _log.GetComponent<Animator>();
        InvokeRepeating("CheckQuestLog", 0, 1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CancelInvoke("CloseLog");
            OpenLog(!_isopen);
        }
    }

    public void OpenLog(bool temp)
    {
        _isopen = temp;
        switch (_isopen)
        {
            case true:
                _log.SetActive(true);
                _anim.SetTrigger("Enter");
                break;
            case false:
                _anim.ResetTrigger("Enter");
                _anim.SetTrigger("Exit");
                Invoke("CloseLog", 0.8f);
                break;
        }
    }

    void CloseLog()
    {
        _log.SetActive(false);
    }

    void CheckQuestLog()
    {
        if (!Collection._instance._SendUpdate)
            return;

        foreach (var item in CurrentLog)
        {
            Destroy(item);
        }
        CurrentLog.Clear();

        foreach (var item in Collection._instance.QuestLog)
        {
            var temp = Instantiate(_default, _defaultParent.transform);
            CurrentLog.Add(temp);
            temp.SetActive(true);
            temp.GetComponent<TextMeshProUGUI>().text = item;
        }

        Collection._instance._SendUpdate = false;
    }
}