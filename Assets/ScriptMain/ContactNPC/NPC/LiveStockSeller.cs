using UnityEngine;
using UnityEngine.UI;

public class LiveStockSeller : MonoBehaviour
{
    public GameObject confirmPanel;
    public Button yesButton;
    public Button noButton;
    public GameObject buyCanvas;
    
    public Button WhiteSheepButton;
    public Button WhiteGoatButton;
    
    public Transform spawnPoint;

    private bool playerInRange = false;
    private AnimalType selectedType = AnimalType.None;

    private void Start()
    {
        buyCanvas.gameObject.SetActive(false);
        confirmPanel.SetActive(false);

        yesButton.onClick.AddListener(OnConfirmPurchase);
        noButton.onClick.AddListener(OnCancelPurchase);

        WhiteGoatButton.onClick.AddListener(() => SelectAnimal(AnimalType.WhiteGoat));
        WhiteSheepButton.onClick.AddListener(() => SelectAnimal(AnimalType.WhiteSheep));
    }

    void SelectAnimal(AnimalType type)
    {
        selectedType = type;
        confirmPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void OnConfirmPurchase()
    {
        if (selectedType != AnimalType.None)
        {
            AnimalFactory.CreateAnimal(selectedType, spawnPoint.position);
        }
        confirmPanel.SetActive(false);
        buyCanvas.gameObject.SetActive(false);
        selectedType = AnimalType.None;
    }

    void OnCancelPurchase()
    {
        confirmPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.B))
        {
            buyCanvas.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            buyCanvas.gameObject.SetActive(false);
            confirmPanel.SetActive(false);
            selectedType = AnimalType.None;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
