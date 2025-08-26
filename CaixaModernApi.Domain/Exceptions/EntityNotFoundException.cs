using System;
using System.Runtime.Serialization;

namespace CaixaModernApi.Domain.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        public string EntityName { get; }
        public object? Key { get; }

        public EntityNotFoundException()
            : base("The requested entity was not found.")
        {
            EntityName = string.Empty;
        }

        public EntityNotFoundException(string? message)
            : base(message ?? "The requested entity was not found.")
        {
            EntityName = string.Empty;
        }

        public EntityNotFoundException(string? message, Exception? innerException)
            : base(message ?? "The requested entity was not found.", innerException)
        {
            EntityName = string.Empty;
        }

        public EntityNotFoundException(Type entityType, object? key = null, Exception? innerException = null)
            : base(BuildMessage(entityType, key), innerException)
        {
            EntityName = entityType?.Name ?? string.Empty;
            Key = key;
        }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            EntityName = info.GetString(nameof(EntityName)) ?? string.Empty;
            Key = info.GetValue(nameof(Key), typeof(object));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(EntityName), EntityName);
            info.AddValue(nameof(Key), Key, typeof(object));
        }

        public static EntityNotFoundException For<T>(object? key = null, Exception? innerException = null)
            => new EntityNotFoundException(typeof(T), key, innerException);

        private static string BuildMessage(Type? entityType, object? key)
        {
            var name = entityType?.Name ?? "Entity";
            if (key is null || (key is string s && string.IsNullOrWhiteSpace(s)))
                return $"{name} was not found.";

            return $"{name} with key '{key}' was not found.";
        }
    }   
}
