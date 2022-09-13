using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSaver : MonoBehaviour
{
    private RectTransform _trans;
    Vector2 screenBounds;
    float objectWidth;
    float objectHeight;

    // Start is called before the first frame update
    void Start()
    {
        _trans = GetComponent<RectTransform>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        objectWidth = _trans.sizeDelta.x / 2;
        objectHeight = _trans.sizeDelta.y / 2;
    }

    public float _desiredXMovement = 10;
    public float _desiredYMovement = 10;

    void LateUpdate()
    {
        //transform.position += new Vector3(_desiredXMovement * Time.deltaTime, _desiredYMovement * Time.deltaTime, 0);

        Vector3 viewPos = _trans.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x + objectWidth, screenBounds.x * -1 - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y + objectHeight, screenBounds.y * -1 - objectHeight);
        _trans.position = viewPos;



        //if (viewPos.x < screenBounds.x + objectWidth) _desiredXMovement = -_desiredXMovement;
        //if (viewPos.x > screenBounds.x * -1 - objectWidth) _desiredXMovement = -_desiredXMovement;
        //if (viewPos.y < screenBounds.y + objectHeight) _desiredYMovement = -_desiredYMovement;
        //if (viewPos.y > screenBounds.y * -1 - objectHeight) _desiredYMovement = -_desiredYMovement;
    }
}
