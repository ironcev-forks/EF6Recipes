using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ModelingFundamentals.Recipe5
{
    [Table("PictureCategorys",Schema = "Chapter2")]
    public class PictureCategory
    {
        public PictureCategory()
        {
            Subcategories = new List<PictureCategory>();
        }
        public int PictureCategoryId { get; set; }
        public string Name { get; set; }
        [ForeignKey("ParentCategory")]
        public int? ParentCategoryId { get; set; }
        
        public virtual PictureCategory ParentCategory { get; set; }//书中没有virtual关键字，这会导致导航属性不能加载，后面的输出就只有根目录！
        public virtual List<PictureCategory> Subcategories { get; set; }
    }
}
