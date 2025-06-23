using UnityEngine;

namespace World
{
    public class NavigationPanel : MonoBehaviour
    {
        [SerializeField] private GameObject toUpperLayerButton;
        [SerializeField] private GameObject upperLayerPanel;

        private bool _upperUnblocked;

        public void UnblockUpperLayer()
        {
            _upperUnblocked = true;
            SetLowerInterfaceState(true);
        }

        public void SetLowerInterfaceState(bool active)
        {
            if (!_upperUnblocked)
            {
                toUpperLayerButton.SetActive(false);
                return;
            }

            toUpperLayerButton.SetActive(active);
        }

        public void SetUpperLayerPanelState(bool active) => upperLayerPanel.SetActive(active);
    }
}