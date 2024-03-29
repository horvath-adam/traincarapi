﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace TrainCarAPI.Model.Entity
{
    /// <summary>
    /// Railway company, who can be the owners of the sites and/or the railway rolling stock
    /// The Company class extends the AbstractEntity.
    /// </summary>
    public class Company : AbstractEntity
    {
        /// <summary>
        /// Railway company name
        /// </summary>
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

    }

    public class CompanyEntityTypeConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
        }
    }
}
