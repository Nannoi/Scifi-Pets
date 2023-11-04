using UnityEngine;

namespace StarterAssets
{
    public class CameraController : MonoBehaviour
    {
        public GameObject duck;
        public GameObject cat;
        public GameObject mouse;
        public StarterAssetsInputs duckLook;
        public StarterAssetsInputs catLook;
        public StarterAssetsInputs mouseLook;
        public CatControl duckControl;
        public CatControl catControl;
        public CatControl mouseControl;
        public SwitchCharacter switchCharacter;

        private GameObject currentCharacter;

        // Start is called before the first frame update
        void Start()
        {
            // Set the initial character as the current character
            currentCharacter = duck;
        }

        // Update is called once per frame
        void Update()
        {
            if (switchCharacter.currentCharacter == duck)
            {
                catLook.look = duckLook.look;
                mouseLook.look = duckLook.look;

                catControl.CurrentCamRotation = duckControl.CurrentCamRotation;
                mouseControl.CurrentCamRotation = duckControl.CurrentCamRotation;
            }
            else if (switchCharacter.currentCharacter == cat)
            {
                duckLook.look = catLook.look;
                mouseLook.look = catLook.look;

                duckControl.CurrentCamRotation = catControl.CurrentCamRotation;
                mouseControl.CurrentCamRotation = catControl.CurrentCamRotation;
            }
            else if (switchCharacter.currentCharacter == mouse)
            {
                catLook.look = mouseLook.look;
                duckLook.look = mouseLook.look;

                catControl.CurrentCamRotation = mouseControl.CurrentCamRotation;
                duckControl.CurrentCamRotation = mouseControl.CurrentCamRotation;
            }
        }

        // Call this method to switch the current character
    }
}
