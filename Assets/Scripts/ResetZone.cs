using UnityEngine;

public class ResetZone : MonoBehaviour
{
    [SerializeField] private GameObject _spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<CharacterController>().enabled = false;
            other.transform.position = _spawnPoint.transform.position;
            other.GetComponent<CharacterController>().enabled = true;
        }
    }
}