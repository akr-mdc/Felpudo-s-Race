using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Status do Player")]
    public int frutasColetadas = 0;
    public int frutasParaVencer = 10;
    public int vidaAtual = 3;
    public int vidaMaxima = 3;

    [Header("Interface (UI)")]
    public TextMeshProUGUI frutasText;
    public TextMeshProUGUI vidaText;
    public GameObject telaVitoria;
    public GameObject telaDerrota;

    private bool jogoAtivo = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // persiste entre cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        AtualizarUI();
    }

    // ---- VIDA ----
    public void TomarDano(int dano)
    {
        if (!jogoAtivo) return;

        vidaAtual -= dano;
        vidaAtual = Mathf.Max(vidaAtual, 0); // evita valores negativos
        AtualizarUI();

        if (vidaAtual <= 0)
        {
            GameOver();
        }
    }

    public void RecuperarVida(int qtd)
    {
        if (!jogoAtivo) return;

        vidaAtual = Mathf.Min(vidaAtual + qtd, vidaMaxima);
        AtualizarUI();
    }

    // ---- FRUTAS ----
    public void AddFruit(int qtd)
    {
        if (!jogoAtivo) return;

        frutasColetadas += qtd;
        AtualizarUI();

        if (frutasColetadas >= frutasParaVencer)
        {
            Vitoria();
        }
    }

    // ---- UI ----
    void AtualizarUI()
    {
        if (frutasText != null)
            frutasText.text = "Frutas: " + frutasColetadas + "/" + frutasParaVencer;

        if (vidaText != null)
            vidaText.text = "Vida: " + vidaAtual + "/" + vidaMaxima;
    }

    // ---- ESTADOS DO JOGO ----
    public void GameOver()
    {
        jogoAtivo = false;
        Debug.Log("Game Over!");
        if (telaDerrota != null)
            telaDerrota.SetActive(true);
        Time.timeScale = 0f; // pausa o jogo
    }

    public void Vitoria()
    {
        jogoAtivo = false;
        Debug.Log("Vitória!");
        if (telaVitoria != null)
            telaVitoria.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ReiniciarCena()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
