using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private void OnEnable()
    {
        InvokeRepeating("EnableCursor", 0, 0.2f);
    }

    void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    void OnDisable()
    {
        CancelInvoke("EnableCursor");
        Cursor.lockState = CursorLockMode.Locked;
    }
}