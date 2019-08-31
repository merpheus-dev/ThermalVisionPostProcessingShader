using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
[Serializable]
[PostProcess(typeof(ThermalVisionRenderer),PostProcessEvent.BeforeStack, "ThermalVision")]
public class ThermalVision :PostProcessEffectSettings
{
    public ColorParameter minColor = new ColorParameter { value = Color.blue };
    public ColorParameter midColor = new ColorParameter { value = Color.yellow };
    public ColorParameter maxColor = new ColorParameter { value = Color.red  };
}

public sealed class ThermalVisionRenderer : PostProcessEffectRenderer<ThermalVision>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/ThermalVision"));
        sheet.properties.SetColor("_MinColor", settings.minColor);
        sheet.properties.SetColor("_MidColor", settings.midColor);
        sheet.properties.SetColor("_MaxColor", settings.maxColor);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}
