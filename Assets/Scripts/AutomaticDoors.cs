using UnityEngine;

public class AutomaticDoors : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;
    public Transform leftClosedLocation;
    public Transform rightClosedLocation;
    public Transform leftOpenLocation;
    public Transform rightOpenLocation;
    
    public float doorSpeed = 1.0f;
    
    private bool m_IsOpening;
    private bool m_IsClosing;
    private Vector3 m_Distance;

    private void Update()
    {
        if (m_IsOpening)
        {
            m_Distance = leftDoor.position - leftOpenLocation.position;

            if (m_Distance.magnitude < 0.001f)
            {
                m_IsOpening = false;
                leftDoor.localPosition = leftOpenLocation.localPosition;
                rightDoor.localPosition = rightOpenLocation.localPosition;
            }
            else
            {
                leftDoor.localPosition = Vector3.Lerp(leftDoor.localPosition, 
                    leftOpenLocation.localPosition,
                    Time.deltaTime * doorSpeed);
                rightDoor.localPosition = Vector3.Lerp(rightDoor.localPosition,
                    rightOpenLocation.localPosition,
                    Time.deltaTime * doorSpeed);
            }
        }
        else if (m_IsClosing)
        {
            m_Distance = leftDoor.position - leftClosedLocation.position;
            
            if (m_Distance.magnitude < 0.001f)
            {
                m_IsClosing = false;
                leftDoor.localPosition = leftClosedLocation.localPosition;
                rightDoor.localPosition = rightClosedLocation.localPosition;
            }
            else
            {
                leftDoor.localPosition = Vector3.Lerp(leftDoor.localPosition, 
                    leftClosedLocation.localPosition,
                    Time.deltaTime * doorSpeed);
                rightDoor.localPosition = Vector3.Lerp(rightDoor.localPosition,
                    rightClosedLocation.localPosition,
                    Time.deltaTime * doorSpeed);
            }
        }
    }
    
    private void OnTriggerEnter(Collider col)
    {
        m_IsOpening = true;
        m_IsClosing = false;
    }
    
    private void OnTriggerStay(Collider col)
    {
        m_IsOpening = true;
        m_IsClosing = false;
    }
    
    private void OnTriggerExit(Collider col)
    {
        m_IsOpening = false;
        m_IsClosing = true;
    }

}
