using System;
using UnityEngine;

public class AnimateSpriteRenderer : MonoBehaviour
{
   private SpriteRenderer sr;
   
   public float animation_time = 0.25f;
   private int animation_frame;
   
   public Sprite[] animation_sprites;
   public Sprite idle_sprite;

   public bool loop = true;
   public bool idle = true;
   private void Awake()
   {
       sr = GetComponent<SpriteRenderer>();
   }

   private void OnEnable()
   {
       sr.enabled = true;
   }

   private void OnDisable()
   {
       sr.enabled = false;
   }

   private void Start()
   {
       InvokeRepeating(nameof(NextFrame), animation_time, animation_time);
   }

   private void NextFrame()
   {
       animation_frame += 1;
       if(loop && animation_frame >= animation_sprites.Length)
       {
           animation_frame = 0;
       }

       if (idle)
       {
           sr.sprite = idle_sprite;
       }
       else if (animation_frame >= 0 && animation_frame < animation_sprites.Length)
       {
           sr.sprite = animation_sprites[animation_frame];
       }
   }
}
