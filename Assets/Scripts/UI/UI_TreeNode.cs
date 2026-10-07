namespace SSunSoft.RPGUdemy
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class UI_TreeNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        private UI ui;
        private RectTransform rect;
        [SerializeField] private Skill_DataSO skillData;
        [SerializeField] private string skillName;

        [SerializeField] private Image skillIcon;
        [SerializeField] private string lockedColorHex = "#9F9F9F";
        private Color lastColor;

        public bool isUnlocked;
        public bool isLocked;

        private void Awake()
        {
            ui = GetComponentInParent<UI>();
            rect = GetComponent<RectTransform>();

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
            ui.skillToolTip.ShowToolTip(true, rect, skillData);

            if (!isUnlocked)
                UpdateIconColor(Color.white * .9f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ui.skillToolTip.ShowToolTip(false, rect);

            if (!isUnlocked)
                UpdateIconColor(lastColor);
        }

        private Color GetColorByHex(string hexNumber)
        {
            ColorUtility.TryParseHtmlString(hexNumber, out var color);
            return color;
        }

        private void OnValidate()
        {
            if (skillData == null)
                return;

            skillName = skillData.displayName;
            skillIcon.sprite = skillData.icon;
            gameObject.name = $"UI_TreeNode - {skillData.displayName}";
        }
    }
}