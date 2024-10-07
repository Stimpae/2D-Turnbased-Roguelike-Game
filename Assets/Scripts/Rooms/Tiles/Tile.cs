using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Tile : MonoBehaviour, ISelectable {
    [SerializeField] private SpriteRenderer baseSpriteRenderer;
    
    [SerializeField] private GameObject highlightObject;
    [SerializeField] private GameObject movementHighlightObject;
    [SerializeField] private GameObject attackHighlightObject;
    
    [SerializeField] private Color highlightColor;
    [SerializeField] private Color movementHighlightColor;
    [SerializeField] private Color attackHighlightColor;
    
    public event Action<Tile> OnTileSelectedEvent;
    public event Action<Tile> OnTileDeselectedEvent;
    
    public void Initialise(TileData data) {
        baseSpriteRenderer.color = data.tileColor; // change to sprite 
        
        highlightObject.SetActive(false);
        movementHighlightObject.SetActive(false);
        attackHighlightObject.SetActive(false);
        
        highlightObject.GetComponent<SpriteRenderer>().color = highlightColor;
        movementHighlightObject.GetComponent<SpriteRenderer>().color = movementHighlightColor;
        attackHighlightObject.GetComponent<SpriteRenderer>().color = attackHighlightColor;
    }
    
    public void OnTileSelected() {
        OnTileSelectedEvent?.Invoke(this);
        highlightObject.SetActive(true);
    }
    
    public void OnTileDeselected() {
        OnTileDeselectedEvent?.Invoke(this);
        highlightObject.SetActive(false);
    }

    public void OnSelect() {
    }

    public void OnDeselect() {
    }

    public void OnHover() {
        OnTileSelectedEvent?.Invoke(this);
        highlightObject.SetActive(true);
    }

    public void OnUnhover() {
        OnTileDeselectedEvent?.Invoke(this);
        highlightObject.SetActive(false);
    }
}
