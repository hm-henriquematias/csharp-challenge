using System.Text.Json;

namespace Domain.Entities
{
    public abstract class BaseEntity
    {
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
