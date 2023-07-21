using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Buttons
{
    public class TabGroup : MonoBehaviour
    {
        public List<TabButton> tabButtons;

        public Sprite tabIdle;
        public Sprite tabHover;
        public Sprite tabSelected;

        public List<GameObject> objectsToSwap;

        private TabButton _selectedTab;

        public void Subscribe(TabButton button)
        {
            // tabButtons.Add(button);

            if (tabButtons == null)
            {
                tabButtons = new List<TabButton>();
            }

            tabButtons.Add(button);
        }

        private void _hardReset()
        {
            _selectedTab = null;
            ResetTabs();
            _updateTabWindows(-1);
        }

        public void OnTabEnter(TabButton button)
        {
            // ResetTabs();
            _hardReset();

            if (_selectedTab == null || button != _selectedTab)
            {
                button.background.sprite = tabHover;
            }
        }

        public void OnTabExit(TabButton button)
        {
            //ResetTabs();
            _hardReset();
        }

        public void OnTabSelected(TabButton button)
        {
            _selectedTab = button;
            _updateTabWindows(-1);

            button.background.sprite = tabSelected;

            _updateTabWindows(button.transform.GetSiblingIndex());
        }

        public void ResetTabs()
        {
            foreach (TabButton button in tabButtons)
            {
                button.background.sprite = tabIdle;
            }
        }

        private void _updateTabWindows(int index = 0)
        {
            for (int i = 0; i < objectsToSwap.Count; i++)
            {
                objectsToSwap[i].SetActive(i == index);
            }
        }
    }
}