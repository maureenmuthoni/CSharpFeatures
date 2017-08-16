using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arrays
{
    public class Weapon : MonoBehaviour
    {
        public int damage = 10;
        public int maxBullets = 30;
        public float bulletSpeed = 20f;
        public float fireInterval = 0.2f;
        public GameObject bulletPrefab;
        public Transform spawnPoint;

        private Bullet[] spawnedBullets;
        private int currentBullets = 0;
        private bool isFired = false;
        private Transform target;

        // Use this for initialization
        void Start()
        {
            spawnedBullets = new Bullet[maxBullets];
        }

        // Update is called once per frame
        void Update()
        {
            // IF !isFired AND currentBullets < maxBullets
                if (!isFired && currentBullets < maxBullets)
            {
                //startCoroutine Fire()
                StartCoroutine(Fire());
            }
        }
        IEnumerator Fire()
        {
            //run whatever is here first
            isFired = true;

            yield return new WaitForSeconds(fireInterval); //wait a few seconds
            
            //run whatever was here last
            isFired = false;
            
            // Spawn the bullet
            spawnBullet();

        }
        // Fire a cremeBullet
        void spawnBullet()
        {
            // 1. i=Instantiate a bullet clone

            GameObject clone = Instantiate(bulletPrefab, spawnPoint.position,Quaternion.identity);
            // 2. Make a direction that goes to target
            Vector2 direction = target.position - transform.position;direction.Normalize();
            // 3. Grab bullet script from clone
            transform.rotation = Quaternion.LookRotation(direction);

            Vector3 eulerAngles = transform.eulerAngles;
            float angle = Vector3.Angle(Vector3.right, direction);
            eulerAngles.z = angle;
            transform.eulerAngles = eulerAngles;
            // 4. Send bullet to target
            Bullet bullet = clone.GetComponent<Bullet>();
            // 5. Store bullet in Array
            bullet.direction = direction;
            //6. Increment currentBullets
            currentBullets++;
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}