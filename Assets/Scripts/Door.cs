using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private string _levelToLoad;
    [SerializeField] private Vector3 spawnTrans;
    [SerializeField] private Vector3 spawnRot;

    public void LoadLevl()
    {
        GameEvents.SaveInitiated();

        Gamemanager._instance.SetSpawnPos(Vector3.zero, Vector3.zero);

        if (spawnTrans != Vector3.zero)
            Gamemanager._instance.SetSpawnPos(spawnTrans, spawnRot);
        
        Gamemanager._instance.LoadLevel(_levelToLoad);
    }
}