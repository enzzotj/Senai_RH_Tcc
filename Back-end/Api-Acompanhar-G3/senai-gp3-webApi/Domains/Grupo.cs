﻿using System;
using System.Collections.Generic;

#nullable disable

namespace senai_gp3_webApi.Domains
{
    public partial class Grupo
    {
        public Grupo()
        {
            Lotacaos = new HashSet<Lotacao>();
        }

        public byte IdGrupo { get; set; }
        public int IdGestor { get; set; }
        public string NomeGrupo { get; set; }

        public virtual Usuario IdGestorNavigation { get; set; }
        public virtual ICollection<Lotacao> Lotacaos { get; set; }
    }
}
