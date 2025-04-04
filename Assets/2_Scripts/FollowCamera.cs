using UnityEngine;

public class FollowComara : MonoBehaviour
{
    [SerializeField] GameObject followTarget;
   
    void LateUpdate()
    {
        transform.position = followTarget.transform.position + new Vector3(0, 0, -10);
    }
}
