using UnityEngine;

namespace _GameData.Scripts
{
    public class Jelly : MonoBehaviour
    {
        public Transform target; // Takip edilecek hedef
        public float stiffness = 2.0f; // Yay sertliği
        public float damping = 0.2f; // Sönümleme katsayısı
        private Vector3 velocity; // Hız

        void FixedUpdate()
        {
            Vector3 direction = new Vector3(target.localPosition.x - transform.localPosition.x, 0, target.localPosition.z - transform.localPosition.z); // X ve Z eksenlerindeki fark
            Vector3 force = direction * stiffness; // Kuvvet hesaplama
            velocity += force * Time.fixedDeltaTime; // Hıza kuvveti ekleme
            velocity *= 1.0f - damping; // Sönümleme uygulama
            transform.localPosition += new Vector3(velocity.x * Time.fixedDeltaTime,0, velocity.z * Time.fixedDeltaTime); // Sadece 


            var targetY = target.localPosition.y + 1;
            var pos = transform.localPosition;
            pos.y = targetY;
            transform.localPosition = pos;
        }
    }
    
}
