using UnityEngine;
using TMPro; // Import the TextMesh Pro namespace

public class VolumeText : MonoBehaviour
{
    [SerializeField] private string volumeName;
    [SerializeField] private string textIntro; //Sound:  or Music:
    private TMP_Text txt; // Use TMP_Text instead of Text

    private void Awake()
    {
        txt = GetComponent<TMP_Text>(); // Get the TMP_Text component
    }

    private void Update()
    {
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        if (txt == null)
        {
            Debug.LogError("TMP_Text component is missing!");
            return;
        }

        if (string.IsNullOrEmpty(volumeName))
        {
            Debug.LogError("volumeName is not set!");
            return;
        }

        float volumeValue = PlayerPrefs.GetFloat(volumeName) * 100;
        txt.text = textIntro + volumeValue.ToString();
    }
}
