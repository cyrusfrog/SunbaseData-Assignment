using UnityEngine;
using UnityEngine.UI;

public class DropdownHandler : MonoBehaviour
{
    public Dropdown filterDropdown;
    public ClientListController clientListController;

    private void Start()
    {
        filterDropdown.onValueChanged.AddListener(OnFilterChanged);
    }

    private void OnFilterChanged(int index)
    {
        string selectedOption = filterDropdown.options[index].text;
        clientListController.UpdateClientList(selectedOption);
    }
}
