using System.Collections.Generic;
using Bilim.DbFolder;

namespace Bilim.ViewModels.AdminViewModels
{
    public class CategoryView
    {
        public List<Category> Categories { get; set; }
        public List<Category2> Categories2 { get; set; }
        public List<Category3> Categories3 { get; set; }

        public CategoryView()
        {
            Categories = new List<Category>();
            Categories2 = new List<Category2>();
            Categories3 = new List<Category3>();
        }
    }
}
