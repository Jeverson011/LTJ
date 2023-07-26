namespace Ltj.Shared.Helpers
{
    public static class Validation
    {
        public static bool IsName(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return false;

            return true;
        }

        public static bool ValidaCPF(string CPF)
        {

            string valor = CPF.Replace(".", "");
            valor = valor.Replace("-", "");

            if (valor.Length != 11)
                return false;

            bool igual = true;

            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0])
                    igual = false;

            if (igual || valor == "12345678909")
                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(
                  valor[i].ToString());

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }

            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;

            for (int i = 0; i < 10; i++)

                soma += (11 - i) * numeros[i];


            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }

            else
                if (numeros[10] != 11 - resultado)
                return false;
            return true;
        }

        public static bool ValidaPIS(string pis)
        {
            string valor = pis.Replace(".", "");
            valor = valor.Replace("-", "");

            int[] multiplicador = new int[10] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;

            if (pis.Trim().Length != 11)
                return false;

            pis = pis.Trim();
            pis = pis.PadLeft(11, '0');

            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(pis[i].ToString()) * multiplicador[i];

                resto = soma % 11;

                if (resto < 2)
                {
                    resto = 0;
                }
                else
                {
                    resto = 11 - resto;
                    return pis.EndsWith(resto.ToString());
                }
            }

            return true;

        }

        public static int ValidaIdade(DateTime DtNascimento)
        {
            // Calcular a idade com base na data de nascimento
            int idade = DateTime.Today.Year - DtNascimento.Year;

            // Verificar se o aniversário ainda não ocorreu este ano
            if (DtNascimento > DateTime.Today.AddYears(-idade))
                idade--;

            return idade;
        }
                   
    }
}
