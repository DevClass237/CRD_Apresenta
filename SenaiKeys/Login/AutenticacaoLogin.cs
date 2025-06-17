using SenaiKeys.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenaiKeys.Login
{
    class AutenticacaoLogin
    {
            public Usuario Autenticar(int matricula, string senha, IEnumerable<Usuario> usuarios) 
            {          
                return usuarios.FirstOrDefault(u => u.Matricula == matricula && u.Senha == senha)!;
            }
        }
    }

