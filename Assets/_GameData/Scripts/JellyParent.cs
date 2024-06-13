using UnityEngine;

namespace _GameData.Scripts
{
    public class JellyParent : MonoBehaviour
    {
        public Block block;
        void Awake()
        {
            block = transform.parent.GetComponent<Block>();
            block.OnBlockDestroy += OnBlockDestroyHandler;
            transform.SetParent(null);
        }

        private void OnBlockDestroyHandler()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            block.OnBlockDestroy -= OnBlockDestroyHandler;
        }
    }
}
