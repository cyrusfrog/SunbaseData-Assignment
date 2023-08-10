using UnityEngine;
using UnityEngine.UI;

public class ClientPopup : MonoBehaviour
{
    public Text nameLabel;
    public Text pointsLabel;
    public Text addressLabel;
    public GameObject popupPanel;

    public void ShowClientInfo(string name, string points, string address)
    {
        nameLabel.text = "Name: " + name;
        pointsLabel.text = "Points: " + points;
        addressLabel.text = "Address: " + address;

        popupPanel.SetActive(true); // Show the popup panel
    }

    public void ClosePopup()
    {
        popupPanel.SetActive(false); // Hide the popup panel
    }
}
