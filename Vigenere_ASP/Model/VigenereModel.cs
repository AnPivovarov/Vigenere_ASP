using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vigenere_ASP.Model
{
    public class VigenereModel
    {
        public string InputText { get; set; }

        public string Key { get; set; }

        public string OutputText { get; set; }

        public string Mode { get; set; }

        public string OutFileName { get; set; }

        public string OutFileExt { get; set; }
    }
}
