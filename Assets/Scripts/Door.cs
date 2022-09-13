using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private string _levelToLoad;

    public void LoadLevl()
    {
        Gamemanager._instance.LoadLevel(_levelToLoad);
    }
}
