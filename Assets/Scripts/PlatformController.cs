using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public enum PlatformType { Neutral, Blue, Green }
    public PlatformType platformType;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            if (platformType == PlatformType.Neutral)
                return; // Neutral platform, both players can land

            // If the player's color doesn't match the platform, ignore collision
            if ((platformType == PlatformType.Blue && player.playerColor != "Blue") ||
                (platformType == PlatformType.Green && player.playerColor != "Green"))
            {
                Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), true);
            }
        }
    }
}
