using Catan.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Resource
{
    public class ResourceCard : MonoBehaviour
    {
        [SerializeField] private Image cardImage;
        [SerializeField] private Sprite wood;
        [SerializeField] private Sprite brick;
        [SerializeField] private Sprite sheep;
        [SerializeField] private Sprite wheat;
        [SerializeField] private Sprite ore;

        public ResourceEnum resource;

        private void Start()
        {
            switch (resource)
            {
                case ResourceEnum.WOOD:
                    cardImage.sprite = wood;
                    break;

                case ResourceEnum.BRICK:
                    cardImage.sprite = brick;
                    break;

                case ResourceEnum.SHEEP:
                    cardImage.sprite = sheep;
                    break;

                case ResourceEnum.WHEAT:
                    cardImage.sprite = wheat;
                    break;

                case ResourceEnum.ORE:
                    cardImage.sprite = ore;
                    break;

                case ResourceEnum.NONE:
                    break;
                
                default:
                    break;
            } 
            
        }
    }
}