using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo = Soindus.Interfaces.Modelos.SOcomData;

namespace Soindus.Interfaces.Repositorios.SOcomData
{
    public class uowSOcomData
    {
        private Modelo.Modelo_SOcomData context;

        public uowSOcomData()
        {
            context = new Modelo.Modelo_SOcomData();
        }

        private Generico.RepositorioGenerico<Modelo.RegCom> regCom;
        public Generico.RepositorioGenerico<Modelo.RegCom> RegCom
        {
            get
            {
                if (regCom == null)
                {
                    regCom = new Generico.RepositorioGenerico<Modelo.RegCom>(context);
                }
                return regCom;
            }
        }
    }
}
