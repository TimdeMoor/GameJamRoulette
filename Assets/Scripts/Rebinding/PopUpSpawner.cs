using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject PopupPrefab;
    [SerializeField] private float DespawnDelay = 2f;
    [SerializeField] private float FadeTime = 2f;
    [SerializeField] private Vector3 MoveDirection = Vector3.zero;
    
    public static PopUpSpawner instance { get; private set; }
    private void Awake()
    {
        instance = this;
    }

    public void SpawnPopUp(Vector2 pos, string text)
    {
        GameObject popup = Instantiate(PopupPrefab, transform);
        popup.GetComponent<TMP_Text>().text = text;
        popup.GetComponent<Transform>().position = pos;
        popup.GetComponent<DespawnAfterDelay>().DelayInSec = DespawnDelay;
        popup.GetComponent<FadeText>().fadeTime = FadeTime;
        popup.GetComponent<MoveVector>().MoveDelta = MoveDirection;
    }
}
