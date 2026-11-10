namespace SSunSoft.RPGUdemy
{
    using UnityEngine;

    public class UI : MonoBehaviour
    {
        public UI_SkillToolTip skillToolTip;
        public UI_SkillTree skillTree;
        private bool skillTreeEnabled;

        private void Awake()
        {
            skillToolTip = GetComponentInChildren<UI_SkillToolTip>();
            skillTreeEnabled = GetComponentInChildren<UI_SkillTree>(true);
        }

        public void ToggleSkillTreeUI()
        {
            skillTreeEnabled = !skillTreeEnabled;
            skillTree.gameObject.SetActive(skillTreeEnabled);
            skillToolTip.ShowToolTip(false, null);
        }
    }

}