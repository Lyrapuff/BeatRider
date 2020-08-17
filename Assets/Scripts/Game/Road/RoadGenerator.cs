using UnityEngine;

namespace Game.Road
{
    public class RoadGenerator : MonoBehaviour
    {
        [SerializeField] private ComputeShader _computeShader;
        [SerializeField] private int _bufferSize = 512;

        private ComputeBuffer _heightsBuffer;
        private float[] _heights;

        public float[] Generate(float offset, float seed)
        {
            _bufferSize = 5000;
            
            int index = _computeShader.FindKernel("generate");

            _heights = new float[_bufferSize];
            _heightsBuffer = new ComputeBuffer(_bufferSize, sizeof(float), ComputeBufferType.Default);
            _heightsBuffer.SetData(_heights);
            
            _computeShader.SetBuffer(index, "heights", _heightsBuffer);
            _computeShader.SetInt("size", _bufferSize);
            _computeShader.SetFloat("offset", offset + seed);
            
            _computeShader.Dispatch(index, _bufferSize, 1 ,1);
            
            _heightsBuffer.GetData(_heights);

            return _heights;
        }
    }
}