using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PhoneCursor : MonoBehaviour
{
    [SerializeField] private bool logRayInput = false;

    [SerializeField] private GraphicRaycaster graphicRaycaster;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private RectTransform mouse;

    [SerializeField] private float extentX = 360f, extentY = 640f;

    [SerializeField] private float cursorSpeed = 2f;

    private GameObject lastHovered;

    private void Update()
    {
        Movement();

        PointerEventData eventData = new PointerEventData(eventSystem);
        eventData.position = mouse.anchoredPosition;

        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(eventData, results);

        if (results.Count <= 0)
        {
            if (lastHovered != null)
            {
                ExecuteEvents.Execute(lastHovered, eventData, ExecuteEvents.pointerExitHandler);
                lastHovered = null;
            }

            if (logRayInput)
                Debug.Log("Not hitting anything");

            return;
        }

        GameObject hovered = results[0].gameObject;

        if (logRayInput)
            Debug.Log("HOvering: " + hovered.name);

        if (hovered != lastHovered)
        {
            if (lastHovered != null)
                ExecuteEvents.Execute(lastHovered, eventData, ExecuteEvents.pointerExitHandler);

            ExecuteEvents.Execute(hovered, eventData, ExecuteEvents.pointerEnterHandler);
            lastHovered = hovered;
        }

        if (Input.GetMouseButtonDown(0))
            ExecuteEvents.Execute(hovered, eventData, ExecuteEvents.pointerDownHandler);
        else if (Input.GetMouseButtonUp(0))
        {
            ExecuteEvents.Execute(hovered, eventData, ExecuteEvents.pointerUpHandler);
            ExecuteEvents.Execute(hovered, eventData, ExecuteEvents.pointerClickHandler);
        }
    }

    private void Movement()
    {
        Vector3 movement = (Vector3.up * Input.GetAxis("Mouse Y")) + (Vector3.right * Input.GetAxis("Mouse X"));
        movement = Vector3.ClampMagnitude(movement, 1f);
        movement *= cursorSpeed * Time.deltaTime;

        Vector3 newPos = mouse.localPosition + movement;
        newPos.x = Mathf.Clamp(newPos.x, 0, extentX);
        newPos.y = Mathf.Clamp(newPos.y, 0, extentY);

        mouse.localPosition = newPos;
    }
}
