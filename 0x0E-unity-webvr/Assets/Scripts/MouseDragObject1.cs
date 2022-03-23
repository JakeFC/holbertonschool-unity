using UnityEngine;

namespace WebXR.Interactions
{
  [RequireComponent(typeof(Rigidbody))]
  public class MouseDragObject1 : MonoBehaviour
  {
    private Camera m_currentCamera;
    private Rigidbody m_rigidbody;
    private Vector3 m_screenPoint;
    private Vector3 m_offset;
    private Vector3 m_currentVelocity;
    private Vector3 m_previousPos;
    private Vector3 _posChange;

    void Awake()
    {
      m_rigidbody = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
      m_currentCamera = FindCamera();
      if (m_currentCamera != null)
      {
        m_screenPoint = m_currentCamera.WorldToScreenPoint(gameObject.transform.position);
        m_offset = gameObject.transform.position - m_currentCamera.ScreenToWorldPoint(GetMousePosWithScreenZ(m_screenPoint.z));
        // Resets check on ball to indicate whether it has touched the area above the net.
        transform.GetComponent<Basketball>().canScore = false;
      }
    }

    void OnMouseUp()
    {
      // The object is given the mouse's direction and velocity, as well as forward velocity
      // equal to two-thirds that of the upward.
      m_rigidbody.velocity = 0.25f * (m_currentVelocity + new Vector3(0f, 0f, 0.67f * m_currentVelocity.y));
      m_currentCamera = null;
    }

    void FixedUpdate()
    {
      if (m_currentCamera != null )
      {
        Vector3 currentScreenPoint = GetMousePosWithScreenZ(m_screenPoint.z);
        m_rigidbody.velocity = Vector3.zero;
        m_rigidbody.MovePosition(m_currentCamera.ScreenToWorldPoint(currentScreenPoint) + m_offset);
        _posChange = (transform.position - m_previousPos);
        // Ensures mouse has reasonable velocity before changing the variable.
        if (_posChange.x < -.05 || _posChange.x > .05 ||
            _posChange.y < -.05 || _posChange.y > .05 ||
            _posChange.z < -.05 || _posChange.z > .05)
          m_currentVelocity = (transform.position - m_previousPos) / Time.deltaTime;
        m_previousPos = transform.position;
      }

      // Adds back the percentage of gravity physics removed in basketball script.
      m_rigidbody.AddForce(Physics.gravity * 0.5f, ForceMode.Acceleration);
    }

    Vector3 GetMousePosWithScreenZ(float screenZ)
    {
      return new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenZ);
    }

    Camera FindCamera()
    {
      Camera[] cameras = FindObjectsOfType<Camera>();
      Camera result = null;
      int camerasSum = 0;
      foreach (var camera in cameras)
      {
        if (camera.enabled)
        {
          result = camera;
          camerasSum++;
        }
      }
      if (camerasSum > 1)
      {
        result = null;
      }
      return result;
    }
  }
}
