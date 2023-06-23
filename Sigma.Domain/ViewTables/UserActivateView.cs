using System;

namespace Sigma.Domain.ViewTables
{
    public class UserActivateView
    {
        public Guid? objID { get; set; }
        public Guid? IDEmpresa { get; set;  }
        public Guid? IDUsuario { get; set;  }
        public bool? Ativo { get; set; }
        public bool? Conectado { get; set; }
        public string ViewerRoler { get; set; }
        public string Email { get; set; }
        public bool Conectar { get; set; }
    }

    public class UsuarioAtivoView
    {
        public Guid objID { get; set; }
        public Guid IDEmpresa { get; set; }
        public String IDUsuario { get; set; }
        public bool Ativo { get; set; }
        public bool Conectado { get; set; }
        public string ViewerRoler { get; set; }

    }

}
