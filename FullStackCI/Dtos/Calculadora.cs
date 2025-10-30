namespace FullStackCI.Dtos
{
    public class Calculadora
    {
        //Crea los metodos de una calculadora sumar, restar, dividir, multiplicar
        public int Numero1 { get; set; }
        public int Numero2 { get; set; }
        public string Operacion { get; set; } = string.Empty;
        public double Resultado { get; set; }
        public double Calcular()
        {
            switch (Operacion)
            {
                case "sumar":
                    return Resultado = Numero1 + Numero2;
                    break;
                case "restar":
                    return Resultado = Numero1 - Numero2;
                    break;
                case "multiplicar":
                    return Resultado = Numero1 * Numero2;
                    break;
                case "dividir":
                    if (Numero2 != 0)
                    {
                        return Resultado = (double)Numero1 / Numero2;
                    }
                    else
                    {
                        throw new DivideByZeroException("No se puede dividir por cero");
                    }
                    break;
                default:
                    throw new InvalidOperationException("Operación no válida");
            }
        }

        public int Sumar(int num1, int num2)
        {
            return num1 + num2;
        }

        public int Restar(int num1, int num2)
        {
            return num1 - num2;
        }

        public int Multiplicar(int num1, int num2)
        {
            return num1 * num2;
        }

        public double Dividir(int num1, int num2)
        {
            if (num2 != 0)
            {
                return (double)num1 / num2;
            }
            else
            {
                throw new DivideByZeroException("No se puede dividir por cero");
            }            
        }

    }
}
