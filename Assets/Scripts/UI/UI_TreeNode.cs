namespace SSunSoft.RPGUdemy
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class UI_TreeNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        private UI ui;
        private RectTransform rect;
        private UI_SkillTree skillTree;

        [Header("Unlock details")]
        public UI_TreeNode[] neededNodes;
        public UI_TreeNode[] conflictNodes;
        public bool isUnlocked;
        public bool isLocked;

        [Header("Skill deatils")]
        public Skill_DataSO skillData;
        [SerializeField] private string skillName;
        [SerializeField] private Image skillIcon;
        [SerializeField] private string lockedColorHex = "#9F9F9F";
        private Color lastColor;

        private void Awake()
        {
            ui = GetComponentInParent<UI>();
            rect = GetComponent<RectTransform>();
            skillTree = GetComponentInParent<UI_SkillTree>();

            UpdateIconColor(GetColorByHex(lockedColorHex));
        }

        private void Unlock()
        {
            isUnlocked = true;
            UpdateIconColor(Color.white);
            skillTree.RemoveSkillPoints(skillData.cost);
            LockConflictNodes();
        }

        private bool CanBeUnlocked()
        {
            if (isUnlocked || isLocked)
                return false;

            if (skillTree.EnoughSkillPoints(skillData.cost) == false)
                return false;

            foreach (var node in neededNodes)
            {
                if (node.isUnlocked == false)
                    return false;
            }

            foreach (var node in conflictNodes)
            {
                if (node.isUnlocked)
                    return false;
            }

            return true;
        }

        private void LockConflictNodes()
        {
            foreach (var node in conflictNodes)
                node.isLocked = true;
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
            ui.skillToolTip.ShowToolTip(true, rect, this);

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