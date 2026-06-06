using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform alvo;

    void LateUpdate()
    {
        transform.position = new Vector3(
            alvo.position.x,
            alvo.position.y,
            -10
        );
    }
}