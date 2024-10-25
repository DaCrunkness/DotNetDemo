using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DataModels;


namespace DataAccess.Configurations.Entities
{
    public class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category
                {
                    Id = 1000,
                    Name = "Art",
                    DisplayOrder = 1
                },
                new Category
                {
                    Id = 2000,
                    Name = "History",
                    DisplayOrder = 2
                },
                new Category
                {
                    Id = 3000,
                    Name = "Technology",
                    DisplayOrder = 3
                },
                new Category
                {
                    Id = 4000,
                    Name = "Sports",
                    DisplayOrder = 4
                }
            );
        }
    }
}