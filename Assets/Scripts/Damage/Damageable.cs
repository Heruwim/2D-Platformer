using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _health = 100;
    [SerializeField] private bool _isAlive = true;
    [SerializeField] private bool _isInvincible = false;
    [SerializeField] private float _invincibilityTime = 0.25f;

    private Animator _animator;

    private float _timeSinceHit = 0;

    public UnityEvent<int, Vector2> DamageableHit;
    public UnityEvent DemageableDeath;

    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        private set
        {
            _isAlive = value;
            _animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("Is Alive set " + value);

            if (value == false)
            {
                DemageableDeath?.Invoke();
            }
        }
    }

    public bool LockVelocity
    {
        get
        {
            return _animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            _animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isInvincible)
        {
            if (_timeSinceHit > _invincibilityTime)
            {
                _isInvincible = false;
                _timeSinceHit = 0;
            }
            _timeSinceHit += Time.deltaTime;
        }
    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if (_isAlive && !_isInvincible)
        {
            Health -= damage;
            _isInvincible = true;

            _animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;
            DamageableHit?.Invoke(damage, knockback);
            CharacterEvents.CharacterDamaged.Invoke(gameObject, damage);

            return true;
        }
        return false;
    }

    public bool Heal(int healthRestore)
    {
        if (IsAlive && Health < MaxHealth)
        {
            int maxHealth = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHealth, healthRestore);
            Health += actualHeal;

            CharacterEvents.CharacterHealed.Invoke(gameObject, actualHeal);
            return true;
        }
        return false;
    }
}
