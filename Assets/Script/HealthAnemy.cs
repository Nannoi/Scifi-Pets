using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] public int _currentHealth;
    public GameObject puzzlePrefab;
    [SerializeField] private HealthBar _healthBar;

    public void Start()
    {
        _currentHealth = _maxHealth;
        _healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
    }

    public void TakeDamage(int damageAmount)
    {
        _currentHealth -= damageAmount;

        // If health is less than or equal to 0, then destroy the enemy
        if (_currentHealth <= 0)
        {
            DropPuzzle();
            Destroy(gameObject);
        }
        else
        {
            _healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
        }
    }

    private void DropPuzzle()
    {
        if (puzzlePrefab != null)
        {
            GameObject puzzle = Instantiate(puzzlePrefab, transform.position, Quaternion.identity);
            // Adjust the position offset if needed
            puzzle.transform.position = transform.position + new Vector3(0, 1, 0);
            puzzle.SetActive(true);
        }
    }

    public void SetMaxHealth(int maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
        _healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
    }
}
