using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        transform.position = _target.position;
    }
}
