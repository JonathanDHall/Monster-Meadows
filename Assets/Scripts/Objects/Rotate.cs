using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private Vector3 _rotateAmount;
    void Update()
    {
        transform.Rotate(_rotateAmount * Time.deltaTime);
    }
}