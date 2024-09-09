using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerClickHandler
{
    public delegate void InputUpdateDelegate(Vector2 inputVal);

    public event InputUpdateDelegate OnInputUpdated;
    public event InputUpdateDelegate OnInputClicked;
    [SerializeField] private RectTransform rangeTransform;
    [SerializeField] private RectTransform thumbStickTransform;
    [SerializeField] private float deadZone = 0.2f;
    

    private float _range;

    private void Awake()
    {
        _range = rangeTransform.sizeDelta.x/2f;
        deadZone *= deadZone;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        rangeTransform.position = eventData.pressPosition;
        thumbStickTransform.position = eventData.pressPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rangeTransform.localPosition = Vector2.zero;
        thumbStickTransform.localPosition = Vector2.zero;
        OnInputUpdated?.Invoke(Vector2.zero);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 offset = eventData.position - eventData.pressPosition;
        thumbStickTransform.position = eventData.pressPosition + offset;
        Vector2 input = offset / _range;
        if(input.sqrMagnitude < deadZone)
            return;
        
        OnInputUpdated?.Invoke(offset/_range);

    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        OnInputClicked.Invoke(Vector2.zero);
    }
}
