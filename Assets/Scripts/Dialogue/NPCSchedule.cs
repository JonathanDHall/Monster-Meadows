using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class NPCSchedule : MonoBehaviour
{
    [SerializeField] private UnityEvent[] dailys;
    private NavMeshAgent _agent;

    private void Awake()
    {
        DayNightCycle.eventAtTime += Schedule;
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Schedule(int time)
    {
        dailys[time]?.Invoke();
    }

    public void MoveAgent(GameObject place)
    {
        _agent.SetDestination(place.transform.position);
    }

    public void CreatePopUp(string message)
    {
        ChatPopUp.Create(GetComponent<NPCManager>().Name, message, GetComponent<NPCManager>()._popUpPlacement.transform);
    }

    public void SendDebugMessage(string message)
    {
        Debug.Log(message);
    }
}