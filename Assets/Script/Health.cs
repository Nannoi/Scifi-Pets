using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour 
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _currentHealth;
    //[SerializeField] private GameObject _hitEffect;
    [SerializeField] private SwitchCharacter switchManager;
    public static Health Instance;
    public TMP_Text HealthText;
    public GameObject disableButtonObject;

    [SerializeField] private HealthBar _healthBar;

    public void Start()
    {
        _currentHealth = _maxHealth;
		_healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
    }

    private void Awake()
    {
        HealthManager.Instance.AddCharacter(gameObject.name, this);
    }
    public void IncreaseHealth(int value)
    {
        _currentHealth = Mathf.Min(_currentHealth + value, _maxHealth); // Ensure the health does not exceed the maximum
        HealthText.text = $"{_currentHealth}/{_maxHealth}";
        _healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
    }
    public float HealthPoints
	{
		get{return _currentHealth; }
		set
		{
            _currentHealth = (int)Mathf.Clamp(value, 0, _maxHealth);

            //If health is < 0 then die
            if (_currentHealth <= 0)
            {
                string currentCharacter = gameObject.name;
                Debug.Log(gameObject.name);
                
                

                //switch to another Character that not die

                // Assuming the collection GameObject has the child GameObjects named "cat" and "mouse"
                Transform collectionTransform = GameObject.Find("Collection").transform;

                // Find the cat and mouse GameObjects under the collection GameObject
                Transform duckTransform = collectionTransform.Find("duck");
                Transform catTransform = collectionTransform.Find("cat");
                Transform mouseTransform = collectionTransform.Find("mouse");

                if (currentCharacter == "duck")
                {
                    Debug.Log("duckdestroy");
                    //Assuming you have a reference to the CharacterSwitchManager script
                    if (catTransform != null)
                    {
                        // Assuming you have a reference to the CharacterSwitchManager script
                        switchManager.SwitchToCat(); // Assuming you have a method to switch to the cat character
                    }
                    else if (mouseTransform != null)
                    {
                        // Assuming you have a reference to the CharacterSwitchManager script
                        switchManager.SwitchToMouse(); // Assuming you have a method to switch to the mouse character
                    }

                }

                else if (currentCharacter == "cat")
                {
                    Debug.Log("catdestroy");
                    if (duckTransform != null)
                    {
                        // Assuming you have a reference to the CharacterSwitchManager script
                        switchManager.SwitchToDuck(); // Assuming you have a method to switch to the cat character
                    }
                    else if (mouseTransform != null)
                    {
                        // Assuming you have a reference to the CharacterSwitchManager script
                        switchManager.SwitchToMouse(); // Assuming you have a method to switch to the mouse character
                    }
                }

                else if (currentCharacter == "mouse")
                {
                    Debug.Log("mousedestroy");
                    if (duckTransform != null)
                    {
                        // Assuming you have a reference to the CharacterSwitchManager script
                        switchManager.SwitchToDuck(); // Assuming you have a method to switch to the cat character
                    }
                    if (catTransform != null)
                    {
                        // Assuming you have a reference to the CharacterSwitchManager script
                        switchManager.SwitchToCat(); // Assuming you have a method to switch to the cat character
                    }
                }
                Destroy(gameObject);
                disableButtonObject.GetComponent<DisableButton>().UnButton();

            }

            else
            {
                _healthBar.UpdateHealthBar(_maxHealth, _currentHealth);
            }
		}
	}

}
