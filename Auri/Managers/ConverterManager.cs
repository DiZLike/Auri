using Auri.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auri.Managers
{
    public class ConverterManager
    {
        private BassAudioService bass;

        public ConverterManager()
        {
            bass = new BassAudioService();
        }

    }
}