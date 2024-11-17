using apiFestivos.Aplicacion.Servicios;
using apiFestivos.Core.Interfaces.Repositorios;
using apiFestivos.Dominio.Entidades;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiFestivos.Test
{
    public class FestivoServicioTests
    {
        //Trabajo hecho por:
        //CARLOS ARTURO ARBOLEDA PALACIO
        //MANUELA RESTREPO TANGARIFE
        //MATEO RESTREPO VILLA
        //JUAN ESTEBAN ZAPATA ROLDAN

        //Pruebas unitarias para el método EsFestivo()
        [Fact]
        public async Task EsFestivo_FechaEsFestiva_DevuelveTrue()
        {
            var mockRepositorio = new Mock<IFestivoRepositorio>();
            mockRepositorio.Setup(r => r.ObtenerTodos()).ReturnsAsync(new List<Festivo>
        {
            new Festivo { Dia = 1, Mes = 1, Nombre = "Año Nuevo", IdTipo = 1 }
        });
            var servicio = new FestivoServicio(mockRepositorio.Object);
            var fechaFestiva = new DateTime(2024, 1, 1);
            var resultado = await servicio.EsFestivo(fechaFestiva);
            Assert.True(resultado);
        }

        [Fact]
        public async Task EsFestivo_FechaNoEsFestiva_DevuelveFalse()
        {
            var mockRepositorio = new Mock<IFestivoRepositorio>();
            mockRepositorio.Setup(r => r.ObtenerTodos()).ReturnsAsync(new List<Festivo>());
            var servicio = new FestivoServicio(mockRepositorio.Object);
            var fechaNoFestiva = new DateTime(2024, 2, 1);
            var resultado = await servicio.EsFestivo(fechaNoFestiva);
            Assert.False(resultado);
        }

        //Pruebas unitarias para el método ObtenerFestivo()
        [Fact]
        public void ObtenerFestivo_Tipo1_FechaFija_DevuelveFechaCorrecta()
        {
            var festivo = new Festivo { Dia = 25, Mes = 12, Nombre = "Navidad", IdTipo = 1 };
            var servicio = new FestivoServicio(null);
            var resultado = servicio.ObtenerFestivo(2024, festivo);
            Assert.Equal(new DateTime(2024, 12, 25), resultado.Fecha);
        }

        [Fact]
        public void ObtenerFestivo_Tipo2_FechaMovible_DevuelveLunesSiguiente()
        {
            var festivo = new Festivo { Dia = 6, Mes = 1, Nombre = "Santos Reyes", IdTipo = 2 };
            var servicio = new FestivoServicio(null);
            var resultado = servicio.ObtenerFestivo(2024, festivo);
            Assert.Equal(DayOfWeek.Monday, resultado.Fecha.DayOfWeek);
        }

        [Fact]
        public void ObtenerFestivo_Tipo4_BasadoEnPascuaYLunesSiguiente_DevuelveFechaCorrecta()
        {
            var festivo = new Festivo { DiasPascua = 40, Nombre = "Ascensión del Señor", IdTipo = 4 };
            var servicio = new FestivoServicio(null);
            var resultado = servicio.ObtenerFestivo(2024, festivo);
            Assert.Equal(DayOfWeek.Monday, resultado.Fecha.DayOfWeek);
        }
    }
}
