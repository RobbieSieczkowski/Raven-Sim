using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Quaternion rotateTo;

    // Detects when player collides w/ another gameobject w an "Is Trigger" = true collider

    void OnTriggerEnter(Collider collisionInfo)
    {
        // Flips bird around when it hits the flock boundary

        if (collisionInfo.tag.Equals("Boundary")) {
            // N & S are problem walls... :(
            if (collisionInfo.name.Equals("N")) {
                rotateTo = new Quaternion(gameObject.transform.localRotation.x, -gameObject.transform.localRotation.y, gameObject.transform.localRotation.z, gameObject.transform.localRotation.w);
            } else if (collisionInfo.name.Equals("S")) {
                rotateTo = new Quaternion(gameObject.transform.localRotation.x, -gameObject.transform.localRotation.y, gameObject.transform.localRotation.z, gameObject.transform.localRotation.w);
            } else if (collisionInfo.name.Equals("E")) {
                rotateTo = Quaternion.Inverse(gameObject.transform.rotation);
            } else if (collisionInfo.name.Equals("W")) {
                rotateTo = Quaternion.Inverse(gameObject.transform.rotation);
            } else if (collisionInfo.name.Equals("Floor")) {
                rotateTo = new Quaternion(-gameObject.transform.localRotation.x, gameObject.transform.localRotation.y, -gameObject.transform.localRotation.z, gameObject.transform.localRotation.w);
            } else if (collisionInfo.name.Equals("Ceiling")) {
                rotateTo = new Quaternion(-gameObject.transform.localRotation.x, gameObject.transform.localRotation.y, -gameObject.transform.localRotation.z, gameObject.transform.localRotation.w);
            }

            gameObject.transform.rotation = rotateTo;
        }

        // To implement:
        // Detect when a player has touched another raven, and then should queue grooming interaction/animation
        // Detect when flock touches food
    }
}
