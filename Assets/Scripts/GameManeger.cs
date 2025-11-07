using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Referências de UI")]
    public Text vidaText;
    public Text frutasText;
    public GameObject telaVitoria;
    public GameObject telaDerrota;

    [Header("Configurações")]
    public int frutasParaVencer = 10;
    public int vidaMaxima = 3;

    private int frutasColetadas = 0;
    private int vidaAtual = 0;
    private bool jogoAtivo = true;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // opcional
        }
        else Destroy(gameObject);
    }

    void Start()
    {
        vidaAtual = Mathf.Clamp(vidaAtual, 0, vidaMaxima);
        AtualizarFrutas();
        AtualizarVida(vidaAtual);
    }

    // --- VIDA -------------------------------------------------
    // Atualiza o texto de vida (passar nova vida)
    public void AtualizarVida(int novaVida)
    {
        vidaAtual = Mathf.Clamp(novaVida, 0, vidaMaxima);
        if (vidaText != null)
            vidaText.text = "Vida: " + vidaAtual + "/" + vidaMaxima;
    }

    // Sobre carga que atualiza com a vida atual (sem parâmetro)
    public void AtualizarVida()
    {
        AtualizarVida(vidaAtual);
    }

    // Recupera vida (usado por frutas)
    public void RecuperarVida(int qtd)
    {
        if (!jogoAtivo) return;
        vidaAtual = Mathf.Min(vidaAtual + qtd, vidaMaxima);
        AtualizarVida();
    }

    // Para compatibilidade: Curar -> RecuperarVida
    public void Curar(int qtd)
    {
        RecuperarVida(qtd);
    }

    // --- FRUTAS -----------------------------------------------
    // Versões compatíveis de AddFruit/AddFruta
    public void AddFruit()
    {
        AddFruit(1);
    }

    public void AddFruit(int qtd)
    {
        if (!jogoAtivo) return;
        frutasColetadas += qtd;
        AtualizarFrutas();
        if (frutasColetadas >= frutasParaVencer) Vitoria();
    }

    public void AddFruta(int qtd) { AddFruit(qtd); } // compatibilidade pt-br

    void AtualizarFrutas()
    {
        if (frutasText != null)
            frutasText.text = "Frutas: " + frutasColetadas + "/" + frutasParaVencer;
    }

    // --- ESTADOS DO JOGO -------------------------------------
    public void GameOver()
    {
        if (!jogoAtivo) return;
        jogoAtivo = false;
        if (telaDerrota != null) telaDerrota.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Vitoria()
    {
        if (!jogoAtivo) return;
        jogoAtivo = false;
        if (telaVitoria != null) telaVitoria.SetActive(true);
        Time.timeScale = 0f;
    }

    // --- UTILITÁRIOS -----------------------------------------
    public void ReiniciarJogo()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public int GetVidaAtual() => vidaAtual;
    public int GetVidaMaxima() => vidaMaxima;
}
