using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Damage
    {
        get => damage;
    }

    [SerializeField]
    private float damage = 10f;
}
