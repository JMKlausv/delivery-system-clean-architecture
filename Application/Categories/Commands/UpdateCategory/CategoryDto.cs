using Application.Common.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories.Commands.UpdateCategory
{
    public class CategoryDto:IMapFrom<Category>
    {
        public string Name { get; set; }
        public int OrderCount { get; set; } = 0;
    }
}
