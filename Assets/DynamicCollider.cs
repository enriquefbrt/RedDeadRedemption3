using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCollider : MonoBehaviour
{
    private PolygonCollider2D polygonCollider;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        polygonCollider = GetComponent<PolygonCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        // Update the collider to match the current sprite
        UpdateColliderShape();
    }

    void UpdateColliderShape()
    {
        // Ensure the sprite has physics shape data
        Sprite sprite = spriteRenderer.sprite;
        if (sprite != null)
        {
            polygonCollider.pathCount = sprite.GetPhysicsShapeCount(); // Number of paths in the shape
            var path = new List<Vector2>();

            for (int i = 0; i < polygonCollider.pathCount; i++)
            {
                path.Clear();
                sprite.GetPhysicsShape(i, path);
                polygonCollider.SetPath(i, path.ToArray());
            }
        }
    }

}
