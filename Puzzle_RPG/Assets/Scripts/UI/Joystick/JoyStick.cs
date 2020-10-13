using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler
{
    GameObject player;
    public float moveSpeed = 5.0f;
    RectTransform rect;
    Vector2 touch = Vector2.zero;
    public RectTransform handle;
    float widthHalf;
    bool moveFlag;
    Animator ani;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rect = GetComponent<RectTransform>();
        widthHalf = rect.sizeDelta.x * 0.5f;
        moveFlag = false;
        ani = player.GetComponent<Animator>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        touch = (eventData.position - rect.anchoredPosition) / widthHalf;
        if (touch.magnitude > 1) touch = touch.normalized;
        handle.anchoredPosition = touch * widthHalf;
        player.transform.eulerAngles = new Vector3(0, Mathf.Atan2(touch.x, touch.y) * Mathf.Rad2Deg, 0);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        moveFlag = true;
        ani.SetBool("IsMove", true);
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        moveFlag = false;
        ani.SetBool("IsMove", false);
        handle.anchoredPosition = Vector2.zero;
    }

    void Update()
    {
        if (moveFlag)
            player.transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }
}
