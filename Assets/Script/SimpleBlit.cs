using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering.HDPipeline;

namespace Evp3
{
    sealed class SimpleBlit : MonoBehaviour
    {
        [SerializeField] RenderTexture _source;

        void Start()
        {
            GetComponent<HDAdditionalCameraData>().customRender += CustomRender;
        }

        void CustomRender(ScriptableRenderContext context, HDCamera camera)
        {
            var cmd = CommandBufferPool.Get("");

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
