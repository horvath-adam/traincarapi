using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TrainCarAPI.Model.Entity
{
    /// <summary>
    /// Vasúttársaság, akik a tulajdonosai lehetnek a telephelyeknek és/vagy a vasúti gördülőállománynak
    /// </summary>
    public class Company : AbstractEntity
    {
        /// <summary>
        /// A vasúttársaság egyedi neve
        /// </summary>
        public string Name { get; set; }
    }

    public class CompanyEntityTypeConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasQueryFilter(e => !e.Deleted);
        }
    }
}
