using UnityEngine;

public class ShurikenController : MonoBehaviour
{
    public GameObject projectile;
    public float fireRate;

    private SpriteRenderer _renderer;
    private float shootDelay;
    
    private Animator _animator;

    [SerializeField] private AudioSource shurikenSoundEffect;
    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            Shoot();
        }

        shootDelay -= Time.deltaTime; // Decrease Shoot Delay over Time
    }

    private void Shoot()
    {
        if (shootDelay < 0)
        {
            _animator.SetBool("isAttacking", true);
            shurikenSoundEffect.Play();
            shootDelay = fireRate; // Set the Shoot Delay with Fire Rate again
            // Created                     //Prefab
            GameObject currentProjectile = Instantiate(projectile, this.transform.position, Quaternion.identity);
            if(_renderer.flipX)
                currentProjectile.GetComponent<Shuriken>().speed *= -1;
            _animator.SetBool("isAttacking", false);
        }
    }
}