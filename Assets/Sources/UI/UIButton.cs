using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sources.UI
{
    public class UIButton : MonoBehaviour,IPointerClickHandler
    {
        public event Action Clicked;
        
        public void OnPointerClick(PointerEventData eventData) => 
            Clicked?.Invoke();
    }
}