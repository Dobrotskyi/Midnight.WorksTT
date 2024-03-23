using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Scripts.Input
{
    public static class PlayerInputUtils
    {
        public static Vector3 GetMouseInWorldPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if (hit.collider == null)
                return Vector3.zero;
            else
                return hit.point;
        }

        public static bool PointIsOnUI(Vector2 screenPosition)
        {
            return GetRaycastResults(screenPosition).Count(r => r.gameObject.layer == 5) > 0;
        }

        private static List<RaycastResult> GetRaycastResults(Vector2 screenPosition)
        {
            PointerEventData pointerEventData = new(EventSystem.current);
            pointerEventData.position = screenPosition;
            List<RaycastResult> results = new();
            EventSystem.current.RaycastAll(pointerEventData, results);
            return results;
        }
    }
}
