using RPGDots.Scripts.ComponentData;
using Unity.Entities;

namespace RPGDots.Scripts.Jobs
{
    public partial struct BuffTickJob : IJobEntity
    {
        public float DeltaTime;

        public void Execute(DynamicBuffer<BuffElement> buffer)
        {
            for (var i = buffer.Length - 1; i >= 0; i--)
            {
                var buff = buffer[i];
                buff.Duration -= DeltaTime;
                if (buff.Duration <= 0) buffer.RemoveAt(i);
                else buffer[i] = buff;
            }
        }
    }
}