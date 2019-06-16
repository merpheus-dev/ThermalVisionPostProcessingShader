# Thermal/Heat Vision Post Processing Shader

![](https://media.giphy.com/media/Piiqorz16CPsIOACuK/giphy.gif)

A post processing shader that made with Unity Shadergraph. Uses Custom Forward Renderer pipeline for Replacement Shaders.

## Notes
- This effect also support heat level changes, such as heating up and down by the time, Sample Gif shows a dead man loses his body temperature by the time.
- This effect made on LWRP pipeline with a custom forward renderer.

## How it works?
- There are two layers as "Hot" and "Cold", replacement shaders replaces everything in the "Hot" layer with a luminance material and a black material for the
"Cold" layer.
- According to the brightness, post processing HLSL shader replaces brighter colors with "Red" and darker colors with "Blue".
-Colors can be changed from PostProcessing Effect's custom editor inside at the PostProcessingVolume.
