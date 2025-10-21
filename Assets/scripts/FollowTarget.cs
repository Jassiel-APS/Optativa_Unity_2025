using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform Target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position, 20f * Time.deltaTime);
    }
}
