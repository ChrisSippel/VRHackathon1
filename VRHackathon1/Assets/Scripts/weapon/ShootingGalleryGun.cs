using UnityEngine;
using VRStandardAssets.Utils;

namespace VRStandardAssets.ShootingGallery
{
    // This script controls the gun for the shooter
    // scenes, including it's movement and shooting.
    public class ShootingGalleryGun : MonoBehaviour
    {
        [SerializeField] private float m_GunContainerSmoothing = 10f;                   // How fast the gun arm follows the reticle.
        [SerializeField] private Transform m_CameraTransform;                           // Used as a reference to move this gameobject towards.
        [SerializeField] private Transform m_GunContainer;                              // This contains the gun arm needs to be moved smoothly.
        [SerializeField] private Reticle m_Reticle;                                     // This is what the gun arm should be aiming at.


        private void Update()
        {
            // Move this gameobject to the camera.
            transform.position = m_CameraTransform.position;

            // Find a rotation for the gun to be pointed at the reticle.
            Quaternion lookAtRotation = Quaternion.LookRotation(m_Reticle.ReticleTransform.position - m_GunContainer.position);

            // Smoothly interpolate the gun's rotation towards that rotation.
            //m_GunContainer.rotation = Quaternion.Slerp (m_GunContainer.rotation, lookAtRotation,
            transform.rotation = Quaternion.Slerp(m_GunContainer.rotation, lookAtRotation,
                m_GunContainerSmoothing * Time.deltaTime);

            Debug.LogWarning(string.Format("right position: {0} rotation: {1}", transform.position, transform.rotation));
        }
    }
}