using UnityEngine;

namespace World
{
    public class NavigationPanel : MonoBehaviour
    {
        [SerializeField] private GameObject toUpperLayerButton;
        [SerializeField] private GameObject upperLayerPanel;

        public void SetLowerInterfaceState(bool active) => toUpperLayerButton.SetActive(active);

        public void SetUpperLayerPanelState(bool active) => upperLayerPanel.SetActive(active); 
    }
}