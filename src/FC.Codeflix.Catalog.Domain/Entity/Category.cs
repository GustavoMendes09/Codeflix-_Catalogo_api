using FC.Codeflix.Catalog.Domain.Exceptions;

namespace FC.Codeflix.Catalog.Domain.Entity
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public Category(string name, string description, bool isActive = true)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            IsActive = isActive;
            CreatedAt = DateTime.Now;

            Validate();
        }

        public void Validate()
        {
            if(string.IsNullOrWhiteSpace(Name))
                throw new EntityValidationException($"{nameof(Name)} should not bem empty or null");
            if (Description is null)
                throw new EntityValidationException($"{nameof(Description)} should not bem empty or null");
        }
    }
}
