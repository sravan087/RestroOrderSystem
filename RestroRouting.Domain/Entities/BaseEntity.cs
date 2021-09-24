using System;

namespace RestroRouting.Domain.Entities
{
    public class BaseEntity
    {
        public DateTime EffectiveStartDate { get; set; }

        public DateTime? EffectiveEndDate { get; set; }
    }
}
