using UnityEngine;

namespace UI.DevCards
{
    [CreateAssetMenu(menuName = "Create Development Card")]
    public class Card : ScriptableObject
    {
        public new string name;
        public string description;
        public Sprite artwork;
    }
}
