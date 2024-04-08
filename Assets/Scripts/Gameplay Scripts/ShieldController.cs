using System.Collections; // Include the System.Collections namespace
using UnityEngine;
using UnityEngine.UI;

public class ShieldController : MonoBehaviour
{
    public GameObject shieldObject; // Reference to the shield GameObject
    public Button shieldButton;
    public float activationDuration = 1.5f; // Duration for which the shield stays active
    public float cooldownDuration = 10f; // Cooldown duration after shield deactivation
    private bool isShieldActive = false; // Flag to track shield activation state
    private float lastActivationTime; // Time when the shield was last activated

    private void Start()
    {
        // Initialize the last activation time to a distant past
        lastActivationTime = -cooldownDuration;
        shieldObject.SetActive(false);
        shieldButton.gameObject.SetActive(true);

        // Register the OnClick event for the shieldButton
        shieldButton.onClick.AddListener(ActivateShield);
    }

    public void ActivateShield()
    {
        if (!isShieldActive && Time.time - lastActivationTime >= cooldownDuration)
        {
            // Activate the shield GameObject
            shieldObject.SetActive(true);
            isShieldActive = true;
            lastActivationTime = Time.time;
            shieldButton.gameObject.SetActive(false);

            // Start coroutine to deactivate the shield after activationDuration
            StartCoroutine(DeactivateAfterDuration());
        }
    }

    private IEnumerator DeactivateAfterDuration()
    {
        yield return new WaitForSeconds(activationDuration);

        // Deactivate the shield after activationDuration
        DeactivateShield();

        // Start cooldown coroutine
        StartCoroutine(ShieldCooldown());
    }

    private IEnumerator ShieldCooldown()
    {
        yield return new WaitForSeconds(cooldownDuration);

        // Re-enable the shield button after cooldownDuration
        shieldButton.gameObject.SetActive(true);
    }

    private void DeactivateShield()
    {
        shieldObject.SetActive(false);
        isShieldActive = false;
    }
}
