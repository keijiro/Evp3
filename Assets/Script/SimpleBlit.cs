using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering.HDPipeline;

namespace Evp3
{
    [ExecuteInEditMode]
    sealed class SimpleBlit : MonoBehaviour
    {
        [SerializeField] RenderTexture _source = null;

        void Start()
        {
            GetComponent<HDAdditionalCameraData>().customRender += CustomRender;
        }

        void CustomRender(ScriptableRenderContext context, HDCamera camera)
        {
            var cmd = CommandBufferPool.Get("");

            cmd.SetViewport(new Rect(0, 0, _source.width, _source.height));
            CoreUtils.ClearRenderTarget(cmd, ClearFlag.All, Color.clear);

            HDUtils.BlitQuad(
                cmd, _source,
                new Vector4(1, 1, 0, 0),
                new Vector4(1, -1, 0, 1),
                0, true
            );

            context.ExecuteCommandBuffer(cmd);

            CommandBufferPool.Release(cmd);
        }
    }
}
