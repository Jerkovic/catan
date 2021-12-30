using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using UI.Panel;
using UnityEngine.Serialization;

namespace UI
{
    public class PanelManager : Singleton<PanelManager>
    {
        [SerializeField] private MsgPanel msgPanel;
        
        private readonly Queue<string> _messageQueue = new Queue<string>();
        private bool _isDisplayingMessage = false;

        private void Awake()
        {
            StartCoroutine(_handleMessageQueue());
        }

        public void ShowMessage(string message)
        {
            _messageQueue.Enqueue(message);
        }
        
        private IEnumerator _handleMessageQueue() 
        {
            while (true) 
            {
                yield return new WaitForSeconds(.5f);
                if (_messageQueue.Count <= 0 || _isDisplayingMessage) continue;

                _isDisplayingMessage = true;
                var queuedMsg = _messageQueue.Dequeue();
                msgPanel.Show(queuedMsg, () => _isDisplayingMessage = false);
            }
            // ReSharper disable once IteratorNeverReturns
        }

    }
}