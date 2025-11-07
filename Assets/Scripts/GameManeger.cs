using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Configurações")]
    public int frutasColetadas = 0;
    public int frutasParaVencer = 10;
    public int vidaMaxima = 3;
    public int vidaAtual;

    [Header("UI (TextMeshProUGUI)")]
    public TextMeshProUGUI frutasText;
    public TextMeshProUGUI vidaText;

    [Header("Telas")]
    public GameObject telaVitoria;
    public GameObject telaDerrota;

    bool jogoAtivo = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Start()
    {
        vidaAtual = Mathf.Clamp(vidaAtual, 0, vidaMaxima);
        AtualizarUI();
    }

    public void AddFruta(int qtd)
    {
        if (!jogoAtivo) return;
        frutasColetadas += qtd;
        AtualizarUI();
        if (frutasColetadas >= frutasParaVencer) Vitoria();
    }

    public void AddFruit(int qtd)
    {
        AddFruta(qtd);
    }


    public void RecuperarVida(int qtd)
    {
        if (!jogoAtivo) return;
        vidaAtual = Mathf.Min(vidaAtual + qtd, vidaMaxima);
        AtualizarUI();
    }

    public void TomarDano(int dano)
    {
        if (!jogoAtivo) return;
        vidaAtual -= dano;
        AtualizarUI();
        if (vidaAtual <= 0) Derrota();
    }

    public void AtualizarUI()
    {
        if (frutasText != null) frutasText.text = $"Frutas: {frutasColetadas}/{frutasParaVencer}";
        if (vidaText != null) vidaText.text = $"Vida: {vidaAtual}/{vidaMaxima}";
    }

    void Vitoria()
    {
        jogoAtivo = false;
        Time.timeScale = 0f;
        if (telaVitoria != null) telaVitoria.SetActive(true);
        Debug.Log("VITÓRIA");
    }

    void Derrota()
    {
        jogoAtivo = false;
        Time.timeScale = 0f;
        if (telaDerrota != null) telaDerrota.SetActive(true);
        Debug.Log("DERROTA");
    }

    // métodos utilitários para UI (botões)
    public void ReiniciarCena()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrMenu()
    {
        Time.timeScale = 1f;
        // carregar cena de menu se houver (troque o nome)
        SceneManager.LoadScene("MainMenu");
    }
}
