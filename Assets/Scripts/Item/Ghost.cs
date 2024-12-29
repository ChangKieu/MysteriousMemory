using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Transform[] waypoints;   
    public float speed = 2f;        
    public float stoppingDistance = 0.2f; 

    private int m_CurrentWaypointIndex = 0;  
    private bool isMovingForward = true;     
    void Update()
    {
        if (waypoints.Length == 0) return;

        Vector3 targetPosition = waypoints[m_CurrentWaypointIndex].position;
        Vector3 direction = targetPosition - transform.position;

        if (direction.magnitude > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            if (isMovingForward)
            {
                m_CurrentWaypointIndex++;
                if (m_CurrentWaypointIndex >= waypoints.Length)
                {
                    m_CurrentWaypointIndex = 0; 
                }
            }
        }

        if (direction.magnitude > 0.1f) 
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * speed);
        }
    }
}
