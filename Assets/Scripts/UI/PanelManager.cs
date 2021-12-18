using UnityEngine;
using Utils;
using UI.Panel;
using UnityEngine.Serialization;

namespace UI
{
    public class PanelManager : Singleton<PanelManager>
    {
        [SerializeField] private MsgPanel msgPanel;
        
        public void ShowMessage(string message)
        {
            msgPanel.Show(message);
        }

    }
}