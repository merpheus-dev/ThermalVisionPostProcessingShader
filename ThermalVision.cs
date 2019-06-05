using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
[Serializable]
[PostProcess(typeof(ScanlineRenderer),PostProcessEvent.BeforeStack, "ThermalVision")]
public class Scanlines :PostProcessEffectSettings
{
    [Range(0f, 1f), Tooltip("Grayscale effect intensity.")]
    public FloatParameter blend = new FloatParameter { value = 0.5f };
    public ColorParameter minColor = new ColorParameter { value = Color.blue };
    public ColorParameter midColor = new ColorParameter { value = Color.yellow };
    public ColorParameter maxColor = new ColorParameter { value = Color.red  };
}

public sealed class ScanlineRenderer : PostProcessEffectRenderer<Scanlines>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/ThermalVision"));
        sheet.properties.SetFloat("_Blend", 1f);
        sheet.properties.SetColor("_MinColor", settings.minColor);
        sheet.properties.SetColor("_MidColor", settings.midColor);
        sheet.properties.SetColor("_MaxColor", settings.maxColor);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}
