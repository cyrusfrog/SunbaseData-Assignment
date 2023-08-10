using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // Import DOTween namespace
using System.Collections.Generic;

public class ClientListController : MonoBehaviour
{
    public Transform clientListContent;
    public GameObject clientPrefab;

    private List<string> allClients = new List<string> { "Client 1, 123", "Client 2, 123", "Client 3, 123", "Client 4, 0", "Client 5, 0" };
    private List<string> managerClients = new List<string> { "Client 1, 123" };
    private List<string> nonManagerClients = new List<string> { "Client 2, 123", "Client 3, 0", "Client 4, 0", "Client 5, 0" };

    private void Start()
    {
        UpdateClientList("All Clients"); // Update the client list on start
    }

    public void UpdateClientList(string filterOption)
    {
        ClearClientList();

        List<string> clientsToShow;

        if (filterOption == "All Clients")
        {
            clientsToShow = allClients;
        }
        else if (filterOption == "Managers Only")
        {
            clientsToShow = managerClients;
        }
        else // Non-managers
        {
            clientsToShow = nonManagerClients;
        }

        foreach (string clientName in clientsToShow)
        {
            GameObject clientObject = Instantiate(clientPrefab, clientListContent);
            Text clientText = clientObject.GetComponentInChildren<Text>();
            clientText.text = clientName;

            // Adjust the layout group to arrange clients vertically
            VerticalLayoutGroup layoutGroup = clientListContent.GetComponent<VerticalLayoutGroup>();
            if (layoutGroup != null)
            {
                layoutGroup.childAlignment = TextAnchor.MiddleCenter;
                layoutGroup.childControlWidth = false;
                layoutGroup.childControlHeight = false;
                layoutGroup.childForceExpandWidth = true;
                layoutGroup.childForceExpandHeight = false;
            }

            // Apply DOTween animation to zoom in the client text
            clientText.rectTransform.localScale = Vector3.zero;
            clientText.rectTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).SetDelay(0.2f);
        }
    }

    private void ClearClientList()
    {
        foreach (Transform child in clientListContent)
        {
            Destroy(child.gameObject);
        }
    }
}