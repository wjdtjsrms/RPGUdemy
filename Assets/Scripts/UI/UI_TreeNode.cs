namespace SSunSoft.RPGUdemy
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class UI_TreeNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [SerializeField] private Image skillIcon;
        [SerializeField] private string lockedColorHex = "#9F9F9F";
        private Color lastColor;

        public bool isUnlocked;
        public bool isLocked;

        private void Awake()
        {
            UpdateIconColor(GetColorByHex(lockedColorHex));
        }

        private void Unlock()
        {
            isUnlocked = true;
            UpdateIconColor(Color.white);
        }

        private bool CanBeUnlocked()
        {
            if (isUnlocked || isLocked)
                return false;

            return true;
        }

        private void UpdateIconColor(Color color)
        {
            if (skillIcon == null)
                return;

            lastColor = skillIcon.color;
            skillIcon.color = color;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (CanBeUnlocked())
                Unlock();
            else
                Debug.Log("Cannot Be Unlocked");
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!isUnlocked)
                UpdateIconColor(Color.white * .9f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!isUnlocked)
                UpdateIconColor(lastColor);
        }

        private Color GetColorByHex(string hexNumber)
        {
            ColorUtility.TryParseHtmlString(hexNumber, out var color);
            return color;
        }
    }

}