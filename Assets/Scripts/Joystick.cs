using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    public Image joystick;
    public Image sosok;
    Vector2 ImputVector;
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 poss;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystick.rectTransform, eventData.position, eventData.pressEventCamera, out poss))
        {
            poss.x = (poss.x / joystick.rectTransform.sizeDelta.x);
            poss.y = (poss.y / joystick.rectTransform.sizeDelta.y);
            ImputVector = new Vector2(poss.x * 2 - 1, poss.y * 2 - 1);
            ImputVector = (ImputVector.magnitude > 1.0f) ? ImputVector.normalized : ImputVector;

            sosok.rectTransform.anchoredPosition = new Vector2
                (ImputVector.x * (joystick.rectTransform.sizeDelta.x / 2), 
                ImputVector.y * (joystick.rectTransform.sizeDelta.y / 2));


        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ImputVector = Vector2.zero;
        sosok.rectTransform.anchoredPosition = Vector2.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        joystick = GetComponent<Image>();
        sosok = transform.GetChild(0).GetComponent<Image>();
    }

    public float Horizontal()
    {
        if (ImputVector.x != 0)
            return ImputVector.x;
        else
            return Input.GetAxisRaw("Horizontal");
    }
    public float Vertical()
    {
        if (ImputVector.y != 0)
            return ImputVector.y;
        else
            return Input.GetAxisRaw("Vertical");
    }

    public float HorizontalMouse()
    {
        if (ImputVector.x != 0)
            return ImputVector.x;
        else
            return Input.GetAxisRaw("JoystickMouseX");
    }
    public float VerticalMouse()
    {
        if (ImputVector.y != 0)
            return ImputVector.y;
        else
            return Input.GetAxisRaw("JoystickMouseY");
    }


    // Update is called once per frame

    void Update()
        {
        
        }
}
