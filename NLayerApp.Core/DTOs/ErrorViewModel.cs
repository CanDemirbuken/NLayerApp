using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerApp.Core.DTOs
{
    public class ErrorViewModel
    {
        public List<string> Errors { get; set; }

        public ErrorViewModel()
        {
            Errors = new List<string>();
        }
    }
}
