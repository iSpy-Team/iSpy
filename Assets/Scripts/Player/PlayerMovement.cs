using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerMovement : NetworkBehaviour
    {
        [Header("Components")] [SerializeField]
        private Joystick joystick;

        [SerializeField] private new Camera camera;
        [SerializeField] private new Rigidbody2D rigidbody2D;
        [SerializeField] private TouchField touch;

        [Header("Properties")] [SerializeField]
        public Button btnShoot;
        private float speed;

        private Vector2 _mousePosition;
        private Vector2 _inputMovement;

        private void Update()
        {
           
            if (Input.GetKey(KeyCode.A))
            {
                btnShoot.onClick.Invoke();
                Debug.Log("Test");
                _inputMovement.x = 1;
                
                rigidbody2D.MovePosition(rigidbody2D.position + _inputMovement * speed * Time.fixedDeltaTime);

            }
            InputMovement();
        }

        private void InputMovement()
        {
            _inputMovement.x = joystick.Horizontal;
            _inputMovement.y = joystick.Vertical;

            _mousePosition.x -= touch.TouchDistance.x * 0.2f;
        }

        private void Movement()
        {
            rigidbody2D.MovePosition(rigidbody2D.position + _inputMovement * speed * Time.fixedDeltaTime);

            rigidbody2D.rotation = _mousePosition.x;
        }

        private void FixedUpdate()
        {
            Movement();
        }

        private void LateUpdate()
        {
            var cameraTransform = camera.transform;
            var position = transform.position;
            cameraTransform.position = new Vector3(position.x, position.y, -10f);
            cameraTransform.rotation = Quaternion.identity;
        }
    }
}