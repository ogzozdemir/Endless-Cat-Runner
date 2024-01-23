using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Space(5), Header("References"), Space(15)]
    [SerializeField] private Transform followTarget;
    
    [Space(5), Header("Camera"), Space(15)]
    [SerializeField] private float followSmoothing;
    private Vector3 offset;

    private void Start() => offset = transform.position - followTarget.position;

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, followTarget.position.x, followSmoothing),
            transform.position.y, followTarget.position.z + offset.z);
    }
}
