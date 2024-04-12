using UnityEngine;
using UnityEngine.Events;

public class RaycastOnSurface : MonoBehaviour
{
    [SerializeField] private UnityEvent<Vector3> touchedPosition;
    
    private void Update()
    {
        #if UNITY_EDITOR
        NoticeClickedPosition();
        #else
        if (GetTouch(out var primaryTouch))
            NoticeTouchedPosition(primaryTouch);
        #endif
    }

    #if UNITY_EDITOR
    private void NoticeClickedPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePosition = Input.mousePosition;

            var ray = Camera.main.ScreenPointToRay(mousePosition);
            var raycastHit = Physics.Raycast(ray, out var hit, 1000.0f);

            if (raycastHit)
                touchedPosition?.Invoke(hit.point);
        }
    }
    #endif
    
    private void NoticeTouchedPosition(Touch touch)
    {
        if (touch.phase is TouchPhase.Began)
        {
            var point = touch.position;

            var ray = Camera.main.ScreenPointToRay(point);
            var raycastHit = Physics.Raycast(ray, out var hit, 1000.0f);

            if (raycastHit)
                touchedPosition?.Invoke(hit.point);
        }
    }

    private bool GetTouch(out Touch touch)
    {
        var touchCount = Input.touchCount;

        if (touchCount > 0)
        {
            Touch primaryTouch = Input.GetTouch(0);

            touch = primaryTouch;
            
            return true;
        }

        touch = default;
        return false;
    }
}
