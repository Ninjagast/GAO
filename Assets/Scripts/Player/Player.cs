using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [Header("Prefab References")]
        public Grid tileMap;
        public GameObject playerTexture;
        public int speed;

        //rotate y on 45 based on movement 
    
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
//          implement checks if this should actually be possible
            Movement();
        }

        private void Movement()
        {
            Vector3 movementVec = new Vector3(0,0,0);
            int rotate = -1;
            
//          Todo Implement options
            if (Input.GetKey("w") && Input.GetKey("a")) // up and left
            {
                rotate = 315;
                movementVec.y += 0.01f;
                movementVec.x += -0.01f;
            }
            else if (Input.GetKey("s") && Input.GetKey("a")) // down and left
            {
                rotate = 225;
                movementVec.y -= 0.01f;
                movementVec.x += -0.01f;
            }
            else if (Input.GetKey("w") && Input.GetKey("d")) // up and right
            {
                rotate = 45;
                movementVec.y += 0.01f;
                movementVec.x += 0.01f;
            }
            else if (Input.GetKey("s") && Input.GetKey("d")) // down and right
            {
                rotate = 135;
                movementVec.y -= 0.01f;
                movementVec.x += 0.01f;
            }
            else if (Input.GetKey("w")) //up
            {
                rotate = 0;
                movementVec.y += 0.01f;
            }
            else if (Input.GetKey("s")) //down
            {
                rotate = 180;
                movementVec.y -= 0.01f;
            }
            else if (Input.GetKey("a")) //left
            {
                rotate = 270;
                movementVec.x += -0.01f;
            }
            else if (Input.GetKey("d")) //right
            {
                rotate = 90;
                movementVec.x += 0.01f;
            }

            if (rotate > -1)
            {
                var eulerAngles = playerTexture.transform.eulerAngles;
                eulerAngles = new Vector3(
                    eulerAngles.x,
                    rotate,
                    eulerAngles.z
                );
                playerTexture.transform.eulerAngles = eulerAngles;
                playerTexture.transform.position += movementVec * speed;
            }
        }
    }
}

