# FrameByFrameAnimation
A lightweight animation system that greatly simplifies 2D frame by frame animation in Unity.

## Installation
See [Install a UPM package from a Git URL](https://docs.unity3d.com/Manual/upm-ui-giturl.html)

## Requirements
Unity version 6000.0.83f1 or later

## Using FrameByFrameAnimation
### Setup
1. Add an 'AnimController' component to a GameObject that also has a 'SpriteRenderer' component.
2. In the Inspector, add an entry to the **Clips** array for each different animation on the object. For each clip, set:
    - **Name:** The name of the animation (no two animations on the same object can have the same name). This is what you will use to play animations.
    - **Frames:** The sprites that will be played in the animation, in order.
    - **Frame Length:** The length of each frame (unless **Use Frame Length Array** is enabled).
    - **Loop:** Whether or not the animation should loop.
    - **Use Frame Length Array:** Enables per frame timing with **Frame Length Array**.
    - **Frame Length Array:** The length of each individual frame, in order. Must be the same length as **Frames** if **Use Frame Length Array** is enabled.
3. Set **Start Anim** to the name of an animation to be played on `Start()`. Leave as `"None"` for no start animation.
<img width="443" height="379" alt="image" src="https://github.com/user-attachments/assets/e2326552-fc69-4ebc-8afd-c19aca0a4fd2" />


### Usage

```csharp
// Get AnimController component
AnimController anim = GetComponent<AnimController>();

// Play an animation
anim.PlayAnim("Idle");

// Play an animation with a callback
anim.PlayAnim("Attack", () => Debug.Log("Attack Finished"));

/*
    Note that the callback only triggers when the animation completes.
    Looping animations will never complete, and therefore won't trigger the callback.
*/
```

## Changelog
 - **0.1.0:** Initial release
