using FullStackCI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStackCITest.Dtos
{
    public class CalculadoraTest
    {


        // [Unidad]_[Escenario]_[ResultadoEsperado]
        [Fact]
        public void Sumar_DosNumerosPositivos_RetornaLaSuma()
        {
            // Arrange
            var calculadora = new Calculadora();
            string operacion = "sumar";
            double resultadoEsperado = 15;

            calculadora.Operacion = operacion;
            calculadora.Numero1 = 5;
            calculadora.Numero2 = 10;
            // Act
            double resultado = calculadora.Calcular();
            // Assert
            Assert.Equal(resultadoEsperado, resultado);
            Assert.Equal(operacion, "sumar");
        }
        [Fact]
        public void dividir_DosNumerosPositivos_RetornaLaDivision()
        {
            // Arrange
            var calculadora = new Calculadora();
            string operacion = "dividir";
            double resultadoEsperado = 0.5;

            calculadora.Operacion = operacion;
            calculadora.Numero1 = 5;
            calculadora.Numero2 = 10;
            // Act
            double resultado = calculadora.Calcular();
            // Assert

            Assert.Equal(resultadoEsperado, resultado);
            Assert.NotEmpty(operacion);
        }

        [Fact]
        public void dividir_DosNumerosPositivos_RetornaLaExepcion()
        {
            // Arrange
            var calculadora = new Calculadora();
            string operacion = "dividir";

            calculadora.Operacion = operacion;
            calculadora.Numero1 = 5;
            calculadora.Numero2 = 0;

            // Act & Assert
            var error = Assert.Throws<DivideByZeroException>(() => calculadora.Calcular());
            Assert.IsType<DivideByZeroException>(error);
            Assert.NotEmpty(operacion);
        }

        [Fact]
        public void multiplicar_DosNumerosPositivos_RetornaLaMultiplicacion()
        {
            // Arrange
            var calculadora = new Calculadora();
            string operacion = "multiplicar";
            double resultadoEsperado = 50;
            calculadora.Operacion = operacion;
            calculadora.Numero1 = 5;
            calculadora.Numero2 = 10;
            // Act
            double resultado = calculadora.Calcular();
            // Assert
            Assert.Equal(resultadoEsperado, resultado);
            Assert.NotEmpty(operacion);
        }

        [Fact]
        public void restar_DosNumerosPositivos_RetornaLaResta()
        {
            // Arrange
            var calculadora = new Calculadora();
            string operacion = "restar";
            double resultadoEsperado = -5;
            calculadora.Operacion = operacion;
            calculadora.Numero1 = 5;
            calculadora.Numero2 = 10;
            // Act
            double resultado = calculadora.Calcular();
            // Assert
            Assert.Equal(resultadoEsperado, resultado);
            Assert.NotEmpty(operacion);
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(5, 5, 10)]
        public void sumar_DosNumerosPositivos_RetornaLaSuma(int n1, int n2, double resultadoEsperado)
        {
            // Arrange
            var calculadora = new Calculadora();            
          
            // Act
            double resultado = calculadora.Sumar(n1, n2);

            // Assert
            Assert.Equal(resultadoEsperado, resultado);
           
        }

        [Theory]
        [InlineData(5, 2, 3)]
        [InlineData(20, 5, 15)]
        public void restar_DosNumerosPositivos_RetornaLaResta2(int n1, int n2, double resultadoEsperado)
        {
            // Arrange
            var calculadora = new Calculadora();

            // Act
            double resultado = calculadora.Restar(n1, n2);

            // Assert
            Assert.Equal(resultadoEsperado, resultado);

        }

        [Theory]
        [InlineData(5, 2, 10)]
        [InlineData(20, 5, 100)]
        public void multiplicar_DosNumerosPositivos_RetornaLaMultiplicacion2(int n1, int n2, double resultadoEsperado)
        {
            // Arrange
            var calculadora = new Calculadora();

            // Act
            double resultado = calculadora.Multiplicar(n1, n2);

            // Assert
            Assert.Equal(resultadoEsperado, resultado);

        }

        [Theory]
        [InlineData(5, 2, 2.5)]
        [InlineData(20, 5, 4)]
        public void dividir_DosNumerosPositivos_RetornaLaDivision2(int n1, int n2, double resultadoEsperado)
        {
            // Arrange
            var calculadora = new Calculadora();

            // Act
            double resultado = calculadora.Dividir(n1, n2);

            // Assert
            Assert.Equal(resultadoEsperado, resultado);

        }

        [Theory]
        [InlineData(5, 0, 2.5)]
        [InlineData(20, 0, 4)]
        public void dividir_DosNumerosPositivos_RetornaLaDivisionExc(int n1, int n2, double resultadoEsperado)
        {
            // Arrange
            var calculadora = new Calculadora();
           
            // Assert
            var resultado = Assert.Throws<DivideByZeroException>(() => calculadora.Dividir(n1, n2));
            Assert.IsType<DivideByZeroException>(resultado);           

        }


    }
}
