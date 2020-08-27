﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveGhost : MonoBehaviour
{
    private UnityEngine.Experimental.Rendering.Universal.Light2D GhostGlow; //the glow of our ghost
    private GameObject player; //reference to the player in the scene
    private SpriteRenderer ghostRenderer; //the sprite rendered of our ghost
    private float ghostSpriteOpacity; //the current opacity of our ghost
    private float ghostSpriteMaxOpacity; //the max opacity our ghost should achieve
    private float fadeAmount; //the amount to change our opacity every frame

    private void Awake()
    {
        player = GameObject.Find("PlayerCharacter"); //find reference to the player by searching in the scene
        GhostGlow = GetComponentInChildren<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        ghostRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        ghostSpriteOpacity = 0; //set our ghost to be hidden by default
        ghostSpriteMaxOpacity = 0.6f; //our ghost should always be slightly opaque, so keep this value below 0
        fadeAmount = 0.05f;
        ghostRenderer.color = new Color(1, 1, 1, ghostSpriteOpacity); //apply the ghostopacity value
    }
    private void FixedUpdate()
    {
        if(Vector2.Distance(player.transform.position, transform.position) <= 3f ) //as long as we close to the player, show the ghost
        {
            if(ghostSpriteOpacity < ghostSpriteMaxOpacity) //as long as we havent reached our max opacity, make less opaque
            {
                FadeIn();
            }
        }
        else if(ghostSpriteOpacity > 0) //otherwise slowly fadeout
        {
            FadeOut();
        }
    }
    private void FadeIn()
    {
        if(ghostSpriteOpacity > ghostSpriteMaxOpacity) //make sure we dont exceed the max alpha value we want
        {
            ghostSpriteOpacity = ghostSpriteMaxOpacity;
            return;
        }
        ghostSpriteOpacity += fadeAmount;
        ghostRenderer.color = new Color(1, 1, 1, ghostSpriteOpacity);
        GhostGlow.intensity = ghostSpriteOpacity / ghostSpriteMaxOpacity; //swing between 0 and 1, depending on our fade
    }
    public void FadeOut()
    {
        if (ghostSpriteOpacity < 0)
        {
            ghostSpriteOpacity = 0;
            GhostGlow.intensity = 0; //this value becomes negative if we dont set it to 0
            return;
        }
        ghostSpriteOpacity -= fadeAmount;
        ghostRenderer.color = new Color(1, 1, 1, ghostSpriteOpacity);
        GhostGlow.intensity = Mathf.Max(0, ghostSpriteOpacity / ghostSpriteMaxOpacity);
    }
}
