using UnityEngine;
using UnityEngine.UI;

public class TeamSelectionManager : MonoBehaviour
{
    public GameObject teamSelectionPanel; // Tak�m se�im paneli
    public Button teamSelectionButton;    // Tak�m se�imi a�an buton
    public Button redTeamButton;          // K�rm�z� tak�m butonu
    public Button blueTeamButton;         // Mavi tak�m butonu

    public MenuMenager menuMenager;      // MenuMenager nesnesi referans�

    void Start()
    {
        // Paneli ba�lang��ta gizle
        teamSelectionPanel.SetActive(false);

        // E�er menuMenager atanmam��sa, FindObjectOfType ile bulmaya �al��
        if (menuMenager == null)
        {
            menuMenager = FindObjectOfType<MenuMenager>();
        }

        // Butonlara t�klan�ld���nda �al��acak fonksiyonlar� atama
        teamSelectionButton.onClick.AddListener(ShowTeamSelectionPanel);
        redTeamButton.onClick.AddListener(SelectRedTeam);
        blueTeamButton.onClick.AddListener(SelectBlueTeam);
    }

    void Update()
    {
        // ESC tu�una bas�ld���nda paneli kapat
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (teamSelectionPanel.activeSelf) // Panel a��k m� kontrol et
            {
                teamSelectionPanel.SetActive(false); // Paneli kapat
            }
        }
    }
    void ShowTeamSelectionPanel()
    {
        teamSelectionPanel.SetActive(true);
    }

    void SelectRedTeam()
    {
        if (menuMenager != null)
        {
            // K�rm�z� tak�m� se� ve MenuMenager �zerinden tak�m de�i�tirme i�lemini yap
            menuMenager.RedTeam();
            teamSelectionPanel.SetActive(false); // Paneli kapat
        }
        else
        {
            Debug.LogError("MenuMenager bulunamad�!");
        }
    }

    void SelectBlueTeam()
    {
        if (menuMenager != null)
        {
            // Mavi tak�m� se� ve MenuMenager �zerinden tak�m de�i�tirme i�lemini yap
            menuMenager.BlueTeam();
            teamSelectionPanel.SetActive(false); // Paneli kapat
        }
        else
        {
            Debug.LogError("MenuMenager bulunamad�!");
        }
    }
}
