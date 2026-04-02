using GestaoPatrimonios.Exceptions;

namespace GestaoPatrimonios.Applications.Regras
{
    public class Validar
    {
        public static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("Nome é obrigatório");
            }
        }

        public static void ValidarNumero(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero))
            {
                throw new DomainException("Número é obrigatório");
            }
        }
    }
}
