using UnityEngine;
using UnityEngine.EventSystems;

namespace VisowFrameWork {
    public class EventListener : EventTrigger
    {
        public delegate void VoidDelegate(GameObject go, PointerEventData eventData);

        public VoidDelegate onClick;
        public VoidDelegate onDown;
        public VoidDelegate onEnter;
        public VoidDelegate onExit;
        public VoidDelegate onUp;
        public VoidDelegate onDragBegin;
        public VoidDelegate onDrag;
        public VoidDelegate onDragEnd;

        static public EventListener Get(GameObject go)
        {
            EventListener listener = go.GetComponent<EventListener>();

            if (listener == null)
                listener = go.AddComponent<EventListener>();

            return listener;
        }


        public override void OnPointerClick(PointerEventData eventData)
        {
            if (onClick != null)
                onClick(gameObject, eventData);
        }


        public override void OnPointerDown(PointerEventData eventData)
        {
            if (onDown != null)
                onDown(gameObject, eventData);
        }


        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (onEnter != null)
                onEnter(gameObject, eventData);
        }


        public override void OnPointerExit(PointerEventData eventData)
        {
            if (onExit != null)
                onExit(gameObject, eventData);
        }


        public override void OnPointerUp(PointerEventData eventData)
        {
            if (onUp != null)
                onUp(gameObject, eventData);
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            if (onDragBegin != null)
                onDragBegin(gameObject, eventData);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            if (onDrag != null)
                onDrag(gameObject, eventData);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            if (onDragEnd != null)
                onDragEnd(gameObject, eventData);
        }
    }
}
