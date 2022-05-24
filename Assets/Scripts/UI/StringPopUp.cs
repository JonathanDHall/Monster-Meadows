using TMPro;
using UnityEngine;

public class StringPopUp : MonoBehaviour
{
    private const float DISAPPEAR_TIMER_MAX = 1;
    private static int SortOrder;

    private TextMeshProUGUI mesh;
    private float disappearTimer;
    private Color textColor;
    private Vector3 MoveVector;

    private void Awake()
    {
        mesh = GetComponent<TextMeshProUGUI>();
    }

    public static StringPopUp Create(string text, bool IsCrit = false, float size = 1f)
    {
        var popUp = Instantiate(Resources.Load("NumberPopup")) as GameObject;
        StringPopUp pop = popUp.GetComponentInChildren<StringPopUp>();
        pop.SetUp(text, IsCrit, size);

        return pop;
    }

    public void SetUp(string text, bool IsCrit, float size = 0.1f)
    {
        //transform.LookAt(transform.position - (GameManager._instance._player.transform.position - transform.position));
        transform.localScale = new Vector3(size, size, size);

        mesh.SetText(text);

        mesh.fontSize = 36;
        textColor = Color.black;


        mesh.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;

        SortOrder++;
        //mesh.sortingOrder = SortOrder;

        MoveVector = new Vector3(0.7f, 0.5f) * 2;
    }

    private void Update()
    {
        //transform.LookAt(transform.position - (GameManager._instance._player.transform.position - transform.position));

        transform.position += MoveVector * Time.deltaTime;
        MoveVector -= MoveVector * 8 * Time.deltaTime;

        if (disappearTimer > DISAPPEAR_TIMER_MAX * 0.5f)
        {
            float increaseScaleAmount = 0.1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 0.1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            float DisSpeed = 3;
            textColor.a -= DisSpeed * Time.deltaTime;
            mesh.color = textColor;
            if (textColor.a < 0)
                Destroy(gameObject);
        }
    }
}