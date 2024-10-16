// Domain/Shared/Role.cs
namespace Domain.Shared
{
    public class Role
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public Role(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new System.ArgumentException("O nome da função é obrigatório.");

            Name = name;
        }
    }
}
