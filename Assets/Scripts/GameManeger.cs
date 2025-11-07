using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI")]
    public TextMeshProUGUI vidaText;
    public TextMeshProUGUI frutasText;

    [Header("Status do Jogo")]
    public int vidaMaxima = 3;
    private int vidaAtual;
    private int frutasColetadas = 0;

    private void Awake()
    {
        // Implementação do padrão Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        vidaAtual = vidaMaxima;
        AtualizarVida();
        AtualizarFrutas();
    }

    // =========================
    // SISTEMA DE VIDA
    // =========================
    public void TomarDano(int dano)
    {
        vidaAtual -= dano;
        if (vidaAtual < 0)
            vidaAtual = 0;

        AtualizarVida();

        if (vidaAtual == 0)
            GameOver();
    }

    public void RecuperarVida(int qtd)
    {
        vidaAtual += qtd;
        if (vidaAtual > vidaMaxima)
            vidaAtual = vidaMaxima;

        AtualizarVida();
    }

    private void AtualizarVida()
    {
        if (vidaText != null)
            vidaText.text = "Vida: " + vidaAtual.ToString();
    }

    // =========================
    // SISTEMA DE FRUTAS
    // =========================
    public void AddFruit(int qtd)
    {
        frutasColetadas += qtd;
        AtualizarFrutas();
    }

    private void AtualizarFrutas()
    {
        if (frutasText != null)
            frutasText.text = "Frutas: " + frutasColetadas.ToString();
    }

    // =========================
    // GAME OVER
    // =========================
    private void GameOver()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
