using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Status do Jogo")]
    public int vidaMax = 3;
    public int vidaAtual;
    public int frutasColetadas = 0;
    public int frutasParaVencer = 10;
    private bool jogoAtivo = true;

    [Header("UI")]
    public TextMeshProUGUI vidaText;
    public TextMeshProUGUI frutasText;
    public GameObject telaVitoria;
    public GameObject telaDerrota;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        vidaAtual = vidaMax;
        AtualizarUI();
    }

    private void AtualizarUI()
    {
        if (vidaText != null)
            vidaText.text = "Vida: " + vidaAtual.ToString();

        if (frutasText != null)
            frutasText.text = "Frutas: " + frutasColetadas.ToString() + " / " + frutasParaVencer;
    }

    public void TomarDano(int qtd)
    {
        if (!jogoAtivo) return;

        vidaAtual -= qtd;
        if (vidaAtual < 0) vidaAtual = 0;

        AtualizarUI();

        if (vidaAtual <= 0)
            GameOver();
    }

    public void RecuperarVida(int qtd)
    {
        if (!jogoAtivo) return;

        vidaAtual += qtd;
        if (vidaAtual > vidaMax) vidaAtual = vidaMax;

        AtualizarUI();
    }

    public void AddFruit(int qtd)
    {
        if (!jogoAtivo) return;

        frutasColetadas += qtd;
        AtualizarUI();

        if (frutasColetadas >= frutasParaVencer)
            Vencer();
    }

    private void Vencer()
    {
        jogoAtivo = false;
        if (telaVitoria != null)
            telaVitoria.SetActive(true);
        Time.timeScale = 0f;
    }

    private void GameOver()
    {
        jogoAtivo = false;
        if (telaDerrota != null)
            telaDerrota.SetActive(true);
        Time.timeScale = 0f;
    }
}

