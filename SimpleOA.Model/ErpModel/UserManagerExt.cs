using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleOA.Model.ErpModel
{
    [MetadataType(typeof(UserValidate))]
    public partial class UserManager
    {
        
    }
    public class UserValidate
    {
        [Required(ErrorMessage ="Can not Empty")]
        public int UserId { get; set; }

    }
}
