using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilFamStudioNet5.ViewModels.ConnectionAbonementToDanceGroup
{
    public class ConnectionAbonementToDanceGroupLiteViewModel
    {
        public int id { get; set; }
        public int id_of_abonement { get; set; }
        public int id_of_dance_group { get; set; }

        public ConnectionAbonementToDanceGroupLiteViewModel(int id, int id_of_abonement, int id_of_dance_group)
        {
            this.id = id;
            this.id_of_abonement = id_of_abonement;
            this.id_of_dance_group = id_of_dance_group;
        }
    }
}
