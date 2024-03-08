using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossComboController : MonoBehaviour
{
    [SerializeField] private bool startComboAutomatically = true;
    [SerializeField] private BossCombo[] combos;
    [SerializeField] private bool allowSameComboTwiceInARow = true;

    [SerializeField] private int numberOfEasyCombosUntilHardOne;
    private int comboDifficultyCounter;
    
    private BossCombo previousCombo;
    private ComboAttackHandler comboAttackHandler;
    private BossCombo currentCombo; 
    private List<BossCombo> comboQueue = new List<BossCombo>();

    private void Start()
    {
        comboDifficultyCounter = 0;
        comboAttackHandler = GetComponent<ComboAttackHandler>();

        if (startComboAutomatically)
            StartNewCombo();
    }
    public void StartNewCombo()
    {
        SelectRandomCombo();
        DoCurrentCombo();
    }
    void SelectRandomCombo()
    {
        if (comboQueue.Count > 0)
        {
            currentCombo = comboQueue[0];
            comboQueue.Remove(currentCombo);
            return;
        }

        currentCombo = combos[Random.Range(0, combos.Length)];

        if (!allowSameComboTwiceInARow && currentCombo == previousCombo)
        {
            SelectRandomCombo();
            return;
        }
        previousCombo = currentCombo;
        comboDifficultyCounter++;
    }

    public void AddComboToQueue(BossCombo comboToAdd)
    {
        comboQueue.Add(comboToAdd);
    }
    public void RemoveComboFromQueue(BossCombo comboToRemove)
    {
        comboQueue.Remove(comboToRemove);
    }
    void DoCurrentCombo()
    {
        if (comboAttackHandler == null)
            comboAttackHandler = GetComponent<ComboAttackHandler>();
        comboAttackHandler.StartCombo(currentCombo);
    }

    public void FinishedWithCombo()
    {
        StartNewCombo();
    }

    public List<BossCombo> GetComboQueue() => comboQueue;
}
