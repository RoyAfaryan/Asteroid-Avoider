using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CopyTime : MonoBehaviour
{
    public TextMeshProUGUI sourceTextMeshPro; // Reference to the source TextMeshPro object
    public TextMeshProUGUI destinationTextMeshPro; // Reference to the destination TextMeshPro object

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check if sourceTextMeshPro and destinationTextMeshPro are assigned
        if (sourceTextMeshPro != null && destinationTextMeshPro != null)
        {
            // Copy text from source to destination
            destinationTextMeshPro.text = sourceTextMeshPro.GetComponent<TextMeshProUGUI>().text;
        }
        else
        {
            Debug.LogError("Source or destination TextMeshPro objects are not assigned!");
        }
    }
}
