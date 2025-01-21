using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AnimateSpriteRenderer start;
    public AnimateSpriteRenderer middle;
    public AnimateSpriteRenderer end;

    public void SetActiveRenderer(AnimateSpriteRenderer renderer)
    {
        start.enabled = renderer == start;
        middle.enabled = renderer == middle;
        end.enabled = renderer == end;
    }

    public void SetDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
