using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private InputInfo _inputInfo;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _rb.AddTorque(_inputInfo.Lateral() * - Vector3.forward);
        _rb.AddTorque(_inputInfo.Longitudinal() * Vector3.right);
    }
}
